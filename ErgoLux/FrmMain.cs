using System.Drawing;
using System.Linq;
using System.IO.Ports;

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

    private readonly System.Resources.ResourceManager StringsRM = new("ErgoLux.localization.strings", typeof(FrmMain).Assembly);

    public FrmMain()
    {
        // Load settings. This has to go before custom initialization, since some routines depend on these values
        _settings = new();

        // Set form icon
        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);

        // Initialize components and GUI
        InitializeComponent();
        InitializeToolStripPanel();
        InitializeToolStrip();
        InitializeStatusStrip();
        InitializeMenuStrip();
        InitializePlots();

        // Language GUI
        bool result = LoadProgramSettingsJSON();
        if (result)
            ApplySettingsJSON(_settings.WindowPosition);
        else
            ApplySettingsJSON();

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

    private void OnTimedEvent(object? sender, EventArgs e)
    {
        bool result = myFtdiDevice.Write(ClassT10.ReceptorsSingle[0]);
        
        if (result == false)
            System.Diagnostics.Debug.Print("OnTimedEvent receptor 0 with code {0} and result: {1}", ClassT10.ReceptorsSingle[0], result);
    }

    private void OnDataReceived(object? sender, DataReceivedEventArgs e)
    {
        //System.Diagnostics.Debug.Print("OnDataReceived");
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
    private void SetFormTitle(System.Windows.Forms.Form frm, string? strFileName = null)
    {
        string strText = String.Empty;
        string strSep = StringsRM.GetString("strFrmTitleUnion", _settings.AppCulture) ?? " - ";
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
        frm.Text = StringsRM.GetString("strFormTitle", _settings.AppCulture) ?? "ErgoLux" + strText;
    }

    /// <summary>
    /// Update user-interface language and localization
    /// </summary>
    private void UpdateUI_Language(int DataLength = default)
    {
        StringResources.Culture = _settings.AppCulture;

        // Update the form's tittle
        SetFormTitle(this, String.Empty);

        // Update ToolStrip
        toolStripMain_Exit.Text = StringsRM.GetString("strToolStripExit", _settings.AppCulture) ?? "Exit";
        toolStripMain_Exit.ToolTipText = StringsRM.GetString("strToolTipExit", _settings.AppCulture) ?? "Exit the application";
        toolStripMain_Open.Text = StringsRM.GetString("strToolStripOpen", _settings.AppCulture) ?? "Open";
        toolStripMain_Open.ToolTipText = StringsRM.GetString("strToolTipOpen", _settings.AppCulture) ?? "Open data file from disk";
        toolStripMain_Save.Text = StringsRM.GetString("strToolStripSave", _settings.AppCulture) ?? "Save";
        toolStripMain_Save.ToolTipText = StringsRM.GetString("strToolTipSave", _settings.AppCulture) ?? "Save data";
        toolStripMain_Connect.Text = StringsRM.GetString("strToolStripConnect", _settings.AppCulture) ?? "Connect";
        toolStripMain_Connect.ToolTipText = StringsRM.GetString("strToolTipConnect", _settings.AppCulture) ?? "Start receiving data from T-10A device";
        toolStripMain_Disconnect.Text = StringsRM.GetString("strToolStripDisconnect", _settings.AppCulture) ?? "Disconnect";
        toolStripMain_Disconnect.ToolTipText = StringsRM.GetString("strToolTipDisconnect", _settings.AppCulture) ?? "Stop and disconnect T-10A device";
        toolStripMain_Settings.Text = StringsRM.GetString("strToolStripSettings", _settings.AppCulture) ?? "Settings";
        toolStripMain_Settings.ToolTipText = StringsRM.GetString("strToolTipSettings", _settings.AppCulture) ?? "Settings for plots, data, and UI";
        toolStripMain_About.Text = StringsRM.GetString("strToolStripAbout", _settings.AppCulture) ?? "About";
        toolStripMain_About.ToolTipText = StringsRM.GetString("strToolTipAbout", _settings.AppCulture) ?? "About this software";

        // Update StatusStrip
        statusStripLabelRaw.ToolTipText = StringsRM.GetString("strStatusTipRaw", _settings.AppCulture) ?? "Plot raw data";
        statusStripLabelRadar.ToolTipText = StringsRM.GetString("strStatusTipDistribution", _settings.AppCulture) ?? "Plot distribution";
        statusStripLabelMax.ToolTipText = StringsRM.GetString("strStatusTipMax", _settings.AppCulture) ?? "Plot max, average and min";
        statusStripLabelRatio.ToolTipText = StringsRM.GetString("strStatusTipRatio", _settings.AppCulture) ?? "Plot ratios";
        statusStripLabelCross.ToolTipText = StringsRM.GetString("strStatusTipCrossHair", _settings.AppCulture) ?? "Show plot's crosshair mode";

        statusStripLabelID.Text = StringsRM.GetString("strStatusID", _settings.AppCulture) ?? "Device ID";
        statusStripLabelID.ToolTipText = StringsRM.GetString("strStatusTipID", _settings.AppCulture) ?? "Device ID";
        statusStripLabelType.Text = StringsRM.GetString("strStatusType", _settings.AppCulture) ?? "Device type";
        statusStripLabelType.ToolTipText = StringsRM.GetString("strStatusTipType", _settings.AppCulture) ?? "Device type";
        statusStripLabelLocation.Text = StringsRM.GetString("strStatusLocation", _settings.AppCulture) ?? "Location ID";
        statusStripLabelLocation.ToolTipText = StringsRM.GetString("strStatusTipLocation", _settings.AppCulture) ?? "T-10A location ID";

        statusStripIconOpen.Text = StringsRM.GetString("strStatusOpen", _settings.AppCulture) ?? "Disconnected";
        statusStripIconOpen.ToolTipText = StringsRM.GetString("strStatusTipOpen", _settings.AppCulture) ?? "Connexion status";
        statusStripIconExchange.Text = StringsRM.GetString("strStatusExchange", _settings.AppCulture) ?? "Receiving data";
        statusStripIconExchange.ToolTipText = StringsRM.GetString("strStatusTipExchange", _settings.AppCulture) ?? "Exchange status";

        statusStripLabelUILanguage.Text = _settings.AppCulture.Name == String.Empty ? "Invariant" : _settings.AppCulture.Name;
        statusStripLabelUILanguage.ToolTipText = (StringsRM.GetString("strToolTipUILanguage", _settings.AppCulture) ?? "User interface language") + ":"+ Environment.NewLine + _settings.AppCulture.NativeName; ;

        // Update menu
        mnuMainFrm_File.Text = StringsRM.GetString("strMenuMainFile", _settings.AppCulture) ?? "&File";
        mnuMainFrm_File_Open.Text = StringsRM.GetString("strMenuMainFileOpen", _settings.AppCulture) ?? "&Open";
        mnuMainFrm_File_Save.Text = StringsRM.GetString("strMenuMainFileSave", _settings.AppCulture) ?? "&Save";
        mnuMainFrm_File_Exit.Text = StringsRM.GetString("strMenuMainFileExit", _settings.AppCulture) ?? "&Exit";
        mnuMainFrm_View.Text = StringsRM.GetString("strMenuMainView", _settings.AppCulture) ?? "&View";
        mnuMainFrm_View_Menu.Text = StringsRM.GetString("strMenuMainViewMenu", _settings.AppCulture) ?? "Show menu";
        mnuMainFrm_View_Toolbar.Text = StringsRM.GetString("strMenuMainViewToolbar", _settings.AppCulture) ?? "Show toolbar";
        mnuMainFrm_View_Raw.Text = StringsRM.GetString("strMenuMainViewRaw", _settings.AppCulture) ?? "Raw data";
        mnuMainFrm_View_Distribution.Text = StringsRM.GetString("strMenuMainViewDistribution", _settings.AppCulture) ?? "Radial distribution";
        mnuMainFrm_View_Average.Text = StringsRM.GetString("strMenuMainViewAverage", _settings.AppCulture) ?? "Averages";
        mnuMainFrm_View_Ratio.Text = StringsRM.GetString("strMenuMainViewRatio", _settings.AppCulture) ?? "Ratios";
        mnuMainFrm_Tools.Text = StringsRM.GetString("strMenuMainTools", _settings.AppCulture) ?? "&Tools";
        mnuMainFrm_Tools_Connect.Text = StringsRM.GetString("strMenuMainToolsConnect", _settings.AppCulture) ?? "&Connect";
        mnuMainFrm_Tools_Disconnect.Text = StringsRM.GetString("strMenuMainToolsDisconnect", _settings.AppCulture) ?? "&Disconnect";
        mnuMainFrm_Tools_Settings.Text = StringsRM.GetString("strMenuMainToolsSettings", _settings.AppCulture) ?? "&Settings";
        mnuMainFrm_Help.Text = StringsRM.GetString("strMenuMainHelpText", _settings.AppCulture) ?? "&Help";
        mnuMainFrm_Help_About.Text = StringsRM.GetString("strMenuMainHelpAbout", _settings.AppCulture) ?? "&About";

        // Update plots
        plotData.CultureUI = _settings.AppCulture;
        plotData.Plot.Title(StringsRM.GetString("strPlotRawTitle", _settings.AppCulture) ?? "Illuminance");
        plotData.Plot.YLabel(StringsRM.GetString("strPlotRawYLabel", _settings.AppCulture) ?? "Lux");
        plotData.Plot.XLabel(StringsRM.GetString("strPlotRawXLabel", _settings.AppCulture) ?? "Time (seconds)");
        plotDistribution.Plot.Title(StringsRM.GetString("strPlotDistributionTitle", _settings.AppCulture) ?? "Illuminance distribution");
        plotStats.CultureUI = _settings.AppCulture;
        plotStats.Plot.Title(StringsRM.GetString("strPlotAverageTitle", _settings.AppCulture) ?? "Max, average, min");
        plotStats.Plot.YLabel(StringsRM.GetString("strPlotAverageYLabel", _settings.AppCulture) ?? "Lux");
        plotStats.Plot.XLabel(StringsRM.GetString("strPlotAverageXLabel", _settings.AppCulture) ?? "Time (seconds)");
        plotRatio.CultureUI = _settings.AppCulture;
        plotRatio.Plot.Title(StringsRM.GetString("strPlotRatiosTitle", _settings.AppCulture) ?? "Illuminance ratios");
        plotRatio.Plot.YLabel(StringsRM.GetString("strPlotRatiosYLabel", _settings.AppCulture) ?? "Ratio");
        plotRatio.Plot.XLabel(StringsRM.GetString("strPlotRatiosXLabel", _settings.AppCulture) ?? "Time (seconds)");

        // Update plots' legends
        if (DataLength > 0)
        {
            _seriesLabels = new string[DataLength];
            for (int i = 0; i < _seriesLabels.Length - _settings.ArrayFixedColumns; i++)
            {
                _seriesLabels[i] = $"{(StringsRM.GetString("strFileHeader08", _settings.AppCulture) ?? "Sensor #")}{i:#0}";
            }
            _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 0] = $"{(StringsRM.GetString("strFileHeader09", _settings.AppCulture) ?? "Maximum")}";
            _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 1] = $"{(StringsRM.GetString("strFileHeader10", _settings.AppCulture) ?? "Average")}";
            _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 2] = $"{(StringsRM.GetString("strFileHeader11", _settings.AppCulture) ?? "Minimum")}";
            _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 3] = $"{(StringsRM.GetString("strFileHeader12", _settings.AppCulture) ?? "Min/Average")}";
            _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 4] = $"{(StringsRM.GetString("strFileHeader13", _settings.AppCulture) ?? "Min/Max")}";
            _seriesLabels[_seriesLabels.Length - _settings.ArrayFixedColumns + 5] = $"{(StringsRM.GetString("strFileHeader14", _settings.AppCulture) ?? "Average/Max")}";

            for (int i = 0; i < plotData.Plot.GetPlottables().Length; i++)
            {
                plotData.Plot.GetPlottables()[i].GetLegendItems()[0].label = _seriesLabels[i];
            }

            if (plotStats.Plot.GetPlottables().Length == 3)
            {
                plotStats.Plot.GetPlottables()[0].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 0];
                plotStats.Plot.GetPlottables()[1].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 1];
                plotStats.Plot.GetPlottables()[2].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 2];
            }

            if (plotRatio.Plot.GetPlottables().Length == 3)
            {
                plotRatio.Plot.GetPlottables()[0].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 3];
                plotRatio.Plot.GetPlottables()[1].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 4];
                plotRatio.Plot.GetPlottables()[2].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _settings.ArrayFixedColumns + 5];
            }
        }

    }

}