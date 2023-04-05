using System.Drawing;
using System.Linq;
using System.IO.Ports;
using ScottPlot.Plottable;
using System.Xml.Linq;

namespace ErgoLux;

public partial class FrmMain : Form
{
    private readonly System.Timers.Timer m_timer;
    private ClassSettings _settings;
    private FTDISample myFtdiDevice;
    private double[][] _plotData = Array.Empty<double[]>();
    private double _max = 0;
    private double _min = 0;
    private double _average = 0;
    private double[,] _plotRadar = new double[0, 0];
    private double[] _plotRadialGauge = Array.Empty<double>();
    private int _nPoints = 0;
    private string[] _seriesLabels = Array.Empty<string>();
    private DateTime _timeStart;
    private DateTime _timeEnd;
    private bool _reading = false;   // this controls whether clicking the plots is allowed or not

    public FrmMain()
    {
        // Load settings. This has to go before custom initialization, since some routines depend on these values
        _settings = new();
        bool result = LoadProgramSettingsJSON();
        if (result)
            ApplySettingsJSON(_settings.WindowPosition);
        else
            ApplySettingsJSON();

        // Set form icon
        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);

        // Initialize components and GUI
        InitializeComponent();
        InitializeToolStrip();
        InitializeStatusStrip();
        InitializeToolStripPanel();
        InitializeMenuStrip();
        InitializePlots();

        // Language GUI
        UpdateUI_Language();

        // Initialize the internal timer
        m_timer = new System.Timers.Timer() { Interval = 500, AutoReset = true };
        m_timer.Elapsed += OnTimedEvent;
        m_timer.Enabled = false;
    }

    private void FrmMain_Shown(object sender, EventArgs e)
    {
        // Signal the native process (that launched us) to close the splash screen
        using var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent");
        closeSplashEvent.Set();
    }

    private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        using (new CenterWinDialog(this))
        {
            if (DialogResult.No == MessageBox.Show(this,
                        StringResources.MsgBoxExit,
                        StringResources.MsgBoxExitTitle,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2))
            {
                // Cancel
                e.Cancel = true;
            }
        }

        // Stop the time if it's still running
        if (m_timer.Enabled) m_timer.Stop();

        m_timer.Dispose();

        // Close the device if it's still open
        if (myFtdiDevice != null && myFtdiDevice.IsOpen)
            myFtdiDevice.Close();

        // Save settings data
        SaveProgramSettingsJSON();
    }

    /// <summary>
    /// Under development. Checks the sensors exist.
    /// </summary>
    private void CheckSensors()
    {
        //if (!myFtdiDevice.Write(ClassT10.Command_54)) return;

        myFtdiDevice.DataReceived += OnDataReceived;
        myFtdiDevice.Write(ClassT10.Command_55_Set);
        //myFtdiDevice.Write(ClassT10.ReceptorsSingle[5]);
        //int i = _settings.T10_NumberOfSensors - 1;
        //while (i >= 0)
        //{
        //    if (myFtdiDevice.Write(ClassT10.ReceptorsSingle[i])) break;
        //    i--;
        //}
        //_settings.T10_NumberOfSensors = i;
    }

    private void OnTimedEvent(object? sender, EventArgs e)
    {
        
        bool result = myFtdiDevice.Write(ClassT10.ReceptorsSingle[0]);

        //result = myFtdiDevice.Write(ClassT10.Command_55_Set);
        //result = myFtdiDevice.Write(ClassT10.ReceptorsSingle[0]);

        if (result == false)
            System.Diagnostics.Debug.Print("OnTimedEvent receptor 0 with code {0} and result: {1}", ClassT10.ReceptorsSingle[0], result);
    }

    private void OnDataReceived(object? sender, DataReceivedEventArgs e)
    {
        (int Sensor, double Iluminance, double Increment, double Percent) result = (0, 0, 0, 0);
        //string str = System.Text.Encoding.UTF8.GetString(e.DataReceived, 0, e.DataReceived.Length);
        
        if (e.StrDataReceived.Length == ClassT10.LongBytesLength)
        {
            result = ClassT10.DecodeCommand(e.StrDataReceived);
            //System.Diagnostics.Debug.Print("Data: {0} — TimeStamp: {1}",
            //                            result.ToString(),
            //                            DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture));
            if (result.Sensor < _settings.T10_NumberOfSensors - 1)
            {
                myFtdiDevice.Write(ClassT10.ReceptorsSingle[result.Sensor + 1]);
            }
            else
            {
                myFtdiDevice.Write(ClassT10.Command_55_Release);
            }
        }
        else if (e.StrDataReceived.Length == ClassT10.ShortBytesLength)
        {
            return;
        }

        // Needed, otherwise we get an error from cross-threading access
        Invoke(()=>Plots_Update(result.Sensor, result.Iluminance));
    }

    /// <summary>
    /// Sets the form's title
    /// </summary>
    /// <param name="frm">Form which title is to be set</param>
    /// <param name="strFileName">String to be added at the default title in 'strFormTitle' string.
    /// If <see langword="null"/>, no string is added.
    /// If <see cref="String.Empty"/>, the current added text is mantained.
    /// Other values are added to the default title.</param>
    private void SetFormTitle(Form frm, string? strFileName = null)
    {
        string strText = String.Empty;
        string strSep = StringResources.FrmTitleUnion;
        if (strFileName is not null)
        {
            if (strFileName != String.Empty)
                strText = $"{strSep}{strFileName}";
            else
            {
                int index = frm.Text.IndexOf(strSep) > -1 ? frm.Text.IndexOf(strSep) : frm.Text.Length;
                strText = frm.Text[index..];
            }
        }
        frm.Text = StringResources.FormTitle + strText;
    }

    /// <summary>
    /// Shows the measuring time in the StatusStrip control
    /// </summary>
    private void UpdateUI_MeasuringTime()
    {
        TimeSpan nTime = _timeEnd - _timeStart;

        if (nTime == TimeSpan.Zero)
        {
            statusStripLabelXtras.Text = string.Empty;
            statusStripLabelXtras.ToolTipText = string.Empty;
        }
        else
        {
            statusStripLabelXtras.Text = $"{nTime.Days} {GetTimeString(StringResources.FileHeader19, nTime.Days)}, " +
                $"{nTime.Hours} {GetTimeString(StringResources.FileHeader20, nTime.Hours)}, " +
                $"{nTime.Minutes} {GetTimeString(StringResources.FileHeader21, nTime.Minutes)}, " +
                $"{nTime.Seconds} {GetTimeString(StringResources.FileHeader22, nTime.Seconds)} " +
                $"{StringResources.FileHeader23} " +
                $"{nTime.Milliseconds} {GetTimeString(StringResources.FileHeader24, nTime.Milliseconds)}";
            statusStripLabelXtras.ToolTipText = statusStripLabelXtras.Text;
        }
    }

    /// <summary>
    /// Extracts the given substring from a string with multiple values delimited by <paramref name="strSplit"/>
    /// </summary>
    /// <param name="strValues">String with multiples values</param>
    /// <param name="time">Index from wich the substring will be returned.
    /// If this is bigger that the number of values in <paramref name="strValues"/>, then the last one is returned</param>
    /// <param name="strSplit">String used as delimiter</param>
    /// <returns>Substring at (array)position determined by <paramref name="time"/></returns>
    private string GetTimeString(string strValues, int time = 0, string strSplit = ", ")
    {
        string[] arrValues = strValues.Split(strSplit);
        int uBound = arrValues.GetUpperBound(0);
        if (time >= uBound)
            return arrValues[uBound];
        else
            return arrValues[time % uBound];
    }

    /// <summary>
    /// Updates the plots' legends according to the culture in <see cref="ClassSettings.AppCulture"/> stored as '_settings.AppCulture'.
    /// </summary>
    private void UpdateUI_Series()
    {
        if (_seriesLabels.Length < _settings.ArrayFixedColumns) return;

        for (int i = 0; i < _seriesLabels.Length - _settings.ArrayFixedColumns; i++)
        {
            _seriesLabels[i] = $"{StringResources.FileHeader08}{i:#0}";
        }
        _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 0] = StringResources.FileHeader09;
        _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 1] = StringResources.FileHeader10;
        _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 2] = StringResources.FileHeader11;
        _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 3] = StringResources.FileHeader12;
        _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 4] = StringResources.FileHeader13;
        _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 5] = StringResources.FileHeader14;
    }

    /// <summary>
    /// Update user-interface language and localization
    /// </summary>
    private void UpdateUI_Language(int DataLength = default)
    {
        this.SuspendLayout();

        StringResources.Culture = _settings.AppCulture;

        // Update the form's tittle
        SetFormTitle(this, String.Empty);

        UpdateUI_MeasuringTime();

        // Update ToolStrip
        toolStripMain_Exit.Text = StringResources.ToolStripExit;
        toolStripMain_Exit.ToolTipText = StringResources.ToolTipExit;
        toolStripMain_Open.Text = StringResources.ToolStripOpen;
        toolStripMain_Open.ToolTipText = StringResources.ToolTipOpen;
        toolStripMain_Save.Text = StringResources.ToolStripSave;
        toolStripMain_Save.ToolTipText = StringResources.ToolTipSave;
        toolStripMain_Connect.Text = StringResources.ToolStripConnect;
        toolStripMain_Connect.ToolTipText = StringResources.ToolTipConnect;
        toolStripMain_Disconnect.Text = StringResources.ToolStripDisconnect;
        toolStripMain_Disconnect.ToolTipText = StringResources.ToolTipDisconnect;
        toolStripMain_Settings.Text = StringResources.ToolStripSettings;
        toolStripMain_Settings.ToolTipText = StringResources.ToolTipSettings;
        toolStripMain_About.Text = StringResources.ToolStripAbout;
        toolStripMain_About.ToolTipText = StringResources.ToolTipAbout;

        // Update StatusStrip
        statusStripLabelRaw.ToolTipText = StringResources.StatusTipRaw;
        statusStripLabelRadar.ToolTipText = StringResources.StatusTipDistribution;
        statusStripLabelMax.ToolTipText = StringResources.StatusTipMax;
        statusStripLabelRatio.ToolTipText = StringResources.StatusTipRatio;
        statusStripLabelCross.ToolTipText = StringResources.StatusTipCrossHair;

        statusStripLabelID.Text = StringResources.StatusID;
        statusStripLabelID.ToolTipText = StringResources.StatusTipID;
        statusStripLabelType.Text = StringResources.StatusType;
        statusStripLabelType.ToolTipText = StringResources.StatusTipType;
        statusStripLabelLocation.Text = StringResources.StatusLocation;
        statusStripLabelLocation.ToolTipText = StringResources.StatusTipLocation;

        statusStripIconOpen.Text = StringResources.StatusOpen;
        statusStripIconOpen.ToolTipText = StringResources.StatusTipOpen;
        statusStripIconExchange.Text = StringResources.StatusExchange;
        statusStripIconExchange.ToolTipText = StringResources.StatusTipExchange;

        statusStripLabelUILanguage.Text = _settings.AppCulture.Name == String.Empty ? "Invariant" : _settings.AppCulture.Name;
        statusStripLabelUILanguage.ToolTipText = StringResources.ToolTipUILanguage + ":"+ Environment.NewLine + _settings.AppCulture.NativeName;

        // Update menu
        mnuMainFrm_File.Text = StringResources.MenuMainFile;
        mnuMainFrm_File_Open.Text = StringResources.MenuMainFileOpen;
        mnuMainFrm_File_Save.Text = StringResources.MenuMainFileSave;
        mnuMainFrm_File_Exit.Text = StringResources.MenuMainFileExit;
        mnuMainFrm_View.Text = StringResources.MenuMainView;
        mnuMainFrm_View_Menu.Text = StringResources.MenuMainViewMenu;
        mnuMainFrm_View_Toolbar.Text = StringResources.MenuMainViewToolbar;
        mnuMainFrm_View_Raw.Text = StringResources.MenuMainViewRaw;
        mnuMainFrm_View_Distribution.Text = StringResources.MenuMainViewDistribution;
        mnuMainFrm_View_Average.Text = StringResources.MenuMainViewAverage;
        mnuMainFrm_View_Ratio.Text = StringResources.MenuMainViewRatio;
        mnuMainFrm_Tools.Text = StringResources.MenuMainTools;
        mnuMainFrm_Tools_Connect.Text = StringResources.MenuMainToolsConnect;
        mnuMainFrm_Tools_Disconnect.Text = StringResources.MenuMainToolsDisconnect;
        mnuMainFrm_Tools_Settings.Text = StringResources.MenuMainToolsSettings;
        mnuMainFrm_Help.Text = StringResources.MenuMainHelpText;
        mnuMainFrm_Help_About.Text = StringResources.MenuMainHelpAbout;

        // Update plots
        plotData.CultureUI = _settings.AppCulture;
        plotData.Plot.Title(StringResources.PlotRawTitle);
        plotData.Plot.YLabel(StringResources.PlotRawYLabel);
        plotData.Plot.XLabel(StringResources.PlotRawXLabel);

        plotDistribution.CultureUI = _settings.AppCulture;
        plotDistribution.Plot.Title(StringResources.PlotDistributionTitle);

        plotStats.CultureUI = _settings.AppCulture;
        plotStats.Plot.Title(StringResources.PlotAverageTitle);
        plotStats.Plot.YLabel(StringResources.PlotAverageYLabel);
        plotStats.Plot.XLabel(StringResources.PlotAverageXLabel);

        plotRatio.CultureUI = _settings.AppCulture;
        plotRatio.Plot.Title(StringResources.PlotRatiosTitle);
        plotRatio.Plot.YLabel(StringResources.PlotRatiosYLabel);
        plotRatio.Plot.XLabel(StringResources.PlotRatiosXLabel);

        Plots_Refresh();

        // Update plots' legends
        UpdateUI_Series();
        if (DataLength > _settings.ArrayFixedColumns)
        {
            // Since the crosshair might be activated, it's necessary to filter by typeof
            var signalPlot = plotData.Plot.GetPlottables().Where(x => x.GetType() == typeof(SignalPlot)).Cast<SignalPlot>().ToArray();
            int count = signalPlot.Length;
            for (int i = 0; i < count; i++)
            {
                //((ScottPlot.Plottable.SignalPlot)plotData.Plot.GetPlottables()[i]).Label = _seriesLabels[i];
                signalPlot[i].Label = _seriesLabels[i];
            }

            if (_seriesLabels.Length > 0)
            {
                if (plotDistribution.Plot.GetPlottables()[0].GetType() == typeof(RadarPlot))
                    ((RadarPlot)plotDistribution.Plot.GetPlottables()[0]).CategoryLabels = _seriesLabels[0.._settings.T10_NumberOfSensors];
                else if (plotDistribution.Plot.GetPlottables()[0].GetType() == typeof(RadialGaugePlot))
                    ((RadialGaugePlot)plotDistribution.Plot.GetPlottables()[0]).Labels = _seriesLabels[0.._settings.T10_NumberOfSensors];
            }

            signalPlot = plotStats.Plot.GetPlottables().Where(x => x.GetType() == typeof(SignalPlot)).Cast<SignalPlot>().ToArray();
            if (signalPlot.Length == 3)
            {
                signalPlot[0].Label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 0];
                signalPlot[1].Label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 1];
                signalPlot[2].Label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 2];
            }

            signalPlot = plotRatio.Plot.GetPlottables().Where(x => x.GetType() == typeof(SignalPlot)).Cast<SignalPlot>().ToArray();
            if (signalPlot.Length == 3)
            {
                signalPlot[0].Label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 3];
                signalPlot[1].Label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 4];
                signalPlot[2].Label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 5];
            }

            // Update legends only if there is data plotted, otherwise, plots are empty and no legends should be visible
            if (_plotData.Length > 0)
                Plots_ShowLegends();
        }

        this.ResumeLayout();
    }

}