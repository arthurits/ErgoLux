using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Xml.Linq;
using ScottPlot.Plottable;

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
        this._settings = new();
        bool result = this.LoadProgramSettingsJSON();
        if (result)
            this.ApplySettingsJSON(this._settings.WindowPosition);
        else
            this.ApplySettingsJSON();

        // Set form icon
        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);

        // Initialize components and GUI
        this.InitializeComponent();
        this.InitializeToolStrip();
        this.InitializeStatusStrip();
        this.InitializeToolStripPanel();
        this.InitializeMenuStrip();
        this.InitializePlots();

        // Language GUI
        this.UpdateUI_Language();

        // Initialize the internal timer
        this.m_timer = new System.Timers.Timer() { Interval = 500, AutoReset = true };
        this.m_timer.Elapsed += this.OnTimedEvent;
        this.m_timer.Enabled = false;
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
        if (this.m_timer.Enabled) this.m_timer.Stop();

        this.m_timer.Dispose();

        // Close the device if it's still open
        if (this.myFtdiDevice != null && this.myFtdiDevice.IsOpen)
            this.myFtdiDevice.Close();

        // Save settings data
        this.SaveProgramSettingsJSON();
    }

    /// <summary>
    /// Under development. Checks the sensors exist.
    /// </summary>
    private void CheckSensors()
    {
        //if (!myFtdiDevice.Write(ClassT10.Command_54)) return;

        this.myFtdiDevice.DataReceived += this.OnDataReceived;
        this.myFtdiDevice.Write(ClassT10.Command_55_Set);
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

        bool result = this.myFtdiDevice.Write(ClassT10.ReceptorsSingle[0]);

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
            if (result.Sensor < this._settings.T10_NumberOfSensors - 1)
            {
                this.myFtdiDevice.Write(ClassT10.ReceptorsSingle[result.Sensor + 1]);
            }
            else
            {
                this.myFtdiDevice.Write(ClassT10.Command_55_Release);
            }
        }
        else if (e.StrDataReceived.Length == ClassT10.ShortBytesLength)
        {
            return;
        }

        // Needed, otherwise we get an error from cross-threading access
        this.Invoke(() => this.Plots_Update(result.Sensor, result.Iluminance));
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
        TimeSpan nTime = this._timeEnd - this._timeStart;

        if (nTime == TimeSpan.Zero)
        {
            this.statusStripLabelXtras.Text = string.Empty;
            this.statusStripLabelXtras.ToolTipText = string.Empty;
        }
        else
        {
            this.statusStripLabelXtras.Text = $"{nTime.Days} {StringResources.FileHeader19}, " +
                $"{nTime.Hours} {StringResources.FileHeader20}, " +
                $"{nTime.Minutes} {StringResources.FileHeader21}, " +
                $"{nTime.Seconds} {StringResources.FileHeader22} " +
                $"{StringResources.FileHeader23} " +
                $"{nTime.Milliseconds} {StringResources.FileHeader24}";
            this.statusStripLabelXtras.ToolTipText = this.statusStripLabelXtras.Text;
        }
    }

    /// <summary>
    /// Updates the plots' legends according to the culture in <see cref="ClassSettings.AppCulture"/> stored as '_settings.AppCulture'.
    /// </summary>
    private void UpdateUI_Series()
    {
        if (this._seriesLabels.Length < this._settings.ArrayFixedColumns) return;

        for (int i = 0; i < this._seriesLabels.Length - this._settings.ArrayFixedColumns; i++)
        {
            this._seriesLabels[i] = $"{StringResources.FileHeader08}{i:#0}";
        }
        this._seriesLabels[this._seriesLabels.Length - this._settings.ArrayFixedColumns + 0] = StringResources.FileHeader09;
        this._seriesLabels[this._seriesLabels.Length - this._settings.ArrayFixedColumns + 1] = StringResources.FileHeader10;
        this._seriesLabels[this._seriesLabels.Length - this._settings.ArrayFixedColumns + 2] = StringResources.FileHeader11;
        this._seriesLabels[this._seriesLabels.Length - this._settings.ArrayFixedColumns + 3] = StringResources.FileHeader12;
        this._seriesLabels[this._seriesLabels.Length - this._settings.ArrayFixedColumns + 4] = StringResources.FileHeader13;
        this._seriesLabels[this._seriesLabels.Length - this._settings.ArrayFixedColumns + 5] = StringResources.FileHeader14;
    }

    /// <summary>
    /// Update user-interface language and localization
    /// </summary>
    private void UpdateUI_Language(int DataLength = default)
    {
        this.SuspendLayout();

        StringResources.Culture = this._settings.AppCulture;

        // Update the form's tittle
        this.SetFormTitle(this, String.Empty);

        this.UpdateUI_MeasuringTime();

        // Update ToolStrip
        this.toolStripMain_Exit.Text = StringResources.ToolStripExit;
        this.toolStripMain_Exit.ToolTipText = StringResources.ToolTipExit;
        this.toolStripMain_Open.Text = StringResources.ToolStripOpen;
        this.toolStripMain_Open.ToolTipText = StringResources.ToolTipOpen;
        this.toolStripMain_Save.Text = StringResources.ToolStripSave;
        this.toolStripMain_Save.ToolTipText = StringResources.ToolTipSave;
        this.toolStripMain_Connect.Text = StringResources.ToolStripConnect;
        this.toolStripMain_Connect.ToolTipText = StringResources.ToolTipConnect;
        this.toolStripMain_Disconnect.Text = StringResources.ToolStripDisconnect;
        this.toolStripMain_Disconnect.ToolTipText = StringResources.ToolTipDisconnect;
        this.toolStripMain_Settings.Text = StringResources.ToolStripSettings;
        this.toolStripMain_Settings.ToolTipText = StringResources.ToolTipSettings;
        this.toolStripMain_About.Text = StringResources.ToolStripAbout;
        this.toolStripMain_About.ToolTipText = StringResources.ToolTipAbout;

        // Update StatusStrip
        this.statusStripLabelRaw.ToolTipText = StringResources.StatusTipRaw;
        this.statusStripLabelRadar.ToolTipText = StringResources.StatusTipDistribution;
        this.statusStripLabelMax.ToolTipText = StringResources.StatusTipMax;
        this.statusStripLabelRatio.ToolTipText = StringResources.StatusTipRatio;
        this.statusStripLabelCross.ToolTipText = StringResources.StatusTipCrossHair;

        this.statusStripLabelID.Text = StringResources.StatusID;
        this.statusStripLabelID.ToolTipText = StringResources.StatusTipID;
        this.statusStripLabelType.Text = StringResources.StatusType;
        this.statusStripLabelType.ToolTipText = StringResources.StatusTipType;
        this.statusStripLabelLocation.Text = StringResources.StatusLocation;
        this.statusStripLabelLocation.ToolTipText = StringResources.StatusTipLocation;

        this.statusStripIconOpen.Text = StringResources.StatusOpen;
        this.statusStripIconOpen.ToolTipText = StringResources.StatusTipOpen;
        this.statusStripIconExchange.Text = StringResources.StatusExchange;
        this.statusStripIconExchange.ToolTipText = StringResources.StatusTipExchange;

        this.statusStripLabelUILanguage.Text = this._settings.AppCulture.Name == String.Empty ? "Invariant" : this._settings.AppCulture.Name;
        this.statusStripLabelUILanguage.ToolTipText = StringResources.ToolTipUILanguage + ":" + Environment.NewLine + this._settings.AppCulture.NativeName;

        // Update menu
        this.mnuMainFrm_File.Text = StringResources.MenuMainFile;
        this.mnuMainFrm_File_Open.Text = StringResources.MenuMainFileOpen;
        this.mnuMainFrm_File_Save.Text = StringResources.MenuMainFileSave;
        this.mnuMainFrm_File_Exit.Text = StringResources.MenuMainFileExit;
        this.mnuMainFrm_View.Text = StringResources.MenuMainView;
        this.mnuMainFrm_View_Menu.Text = StringResources.MenuMainViewMenu;
        this.mnuMainFrm_View_Toolbar.Text = StringResources.MenuMainViewToolbar;
        this.mnuMainFrm_View_Raw.Text = StringResources.MenuMainViewRaw;
        this.mnuMainFrm_View_Distribution.Text = StringResources.MenuMainViewDistribution;
        this.mnuMainFrm_View_Average.Text = StringResources.MenuMainViewAverage;
        this.mnuMainFrm_View_Ratio.Text = StringResources.MenuMainViewRatio;
        this.mnuMainFrm_Tools.Text = StringResources.MenuMainTools;
        this.mnuMainFrm_Tools_Connect.Text = StringResources.MenuMainToolsConnect;
        this.mnuMainFrm_Tools_Disconnect.Text = StringResources.MenuMainToolsDisconnect;
        this.mnuMainFrm_Tools_Settings.Text = StringResources.MenuMainToolsSettings;
        this.mnuMainFrm_Help.Text = StringResources.MenuMainHelpText;
        this.mnuMainFrm_Help_About.Text = StringResources.MenuMainHelpAbout;

        // Update plots
        this.plotData.CultureUI = this._settings.AppCulture;
        this.plotData.Plot.Title(StringResources.PlotRawTitle);
        this.plotData.Plot.YLabel(StringResources.PlotRawYLabel);
        this.plotData.Plot.XLabel(StringResources.PlotRawXLabel);

        this.plotDistribution.CultureUI = this._settings.AppCulture;
        this.plotDistribution.Plot.Title(StringResources.PlotDistributionTitle);

        this.plotStats.CultureUI = this._settings.AppCulture;
        this.plotStats.Plot.Title(StringResources.PlotAverageTitle);
        this.plotStats.Plot.YLabel(StringResources.PlotAverageYLabel);
        this.plotStats.Plot.XLabel(StringResources.PlotAverageXLabel);

        this.plotRatio.CultureUI = this._settings.AppCulture;
        this.plotRatio.Plot.Title(StringResources.PlotRatiosTitle);
        this.plotRatio.Plot.YLabel(StringResources.PlotRatiosYLabel);
        this.plotRatio.Plot.XLabel(StringResources.PlotRatiosXLabel);

        this.Plots_Refresh();

        // Update plots' legends
        this.UpdateUI_Series();
        if (DataLength > this._settings.ArrayFixedColumns)
        {
            // Since the crosshair might be activated, it's necessary to filter by typeof
            var signalPlot = this.plotData.Plot.GetPlottables().Where(x => x.GetType() == typeof(SignalPlot)).Cast<SignalPlot>().ToArray();
            int count = signalPlot.Length;
            for (int i = 0; i < count; i++)
            {
                //((ScottPlot.Plottable.SignalPlot)plotData.Plot.GetPlottables()[i]).Label = _seriesLabels[i];
                signalPlot[i].Label = this._seriesLabels[i];
            }

            if (this.plotDistribution.Plot.GetPlottables()[0].GetType() == typeof(RadarPlot))
                ((RadarPlot)this.plotDistribution.Plot.GetPlottables()[0]).CategoryLabels = this._seriesLabels[0..this._settings.T10_NumberOfSensors];
            else if (this.plotDistribution.Plot.GetPlottables()[0].GetType() == typeof(RadialGaugePlot))
                ((RadialGaugePlot)this.plotDistribution.Plot.GetPlottables()[0]).Labels = this._seriesLabels[0..this._settings.T10_NumberOfSensors];

            signalPlot = this.plotStats.Plot.GetPlottables().Where(x => x.GetType() == typeof(SignalPlot)).Cast<SignalPlot>().ToArray();
            if (signalPlot.Length == 3)
            {
                signalPlot[0].Label = this._seriesLabels[this._plotData.Length - this._settings.ArrayFixedColumns + 0];
                signalPlot[1].Label = this._seriesLabels[this._plotData.Length - this._settings.ArrayFixedColumns + 1];
                signalPlot[2].Label = this._seriesLabels[this._plotData.Length - this._settings.ArrayFixedColumns + 2];
            }

            signalPlot = this.plotRatio.Plot.GetPlottables().Where(x => x.GetType() == typeof(SignalPlot)).Cast<SignalPlot>().ToArray();
            if (signalPlot.Length == 3)
            {
                signalPlot[0].Label = this._seriesLabels[this._plotData.Length - this._settings.ArrayFixedColumns + 3];
                signalPlot[1].Label = this._seriesLabels[this._plotData.Length - this._settings.ArrayFixedColumns + 4];
                signalPlot[2].Label = this._seriesLabels[this._plotData.Length - this._settings.ArrayFixedColumns + 5];
            }

            this.Plots_ShowLegends();
        }

        this.ResumeLayout();
    }

}