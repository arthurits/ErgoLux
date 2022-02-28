using System.Drawing;
using System.Linq;
using System.IO.Ports;


namespace ErgoLux;

public partial class FrmMain : Form
{
    private readonly System.Timers.Timer m_timer;
    private ClassSettings _sett;
    private FTDISample myFtdiDevice;
    private double[][] _plotData;
    private double _max = 0;
    private double _min = 0;
    private double _average = 0;
    private double[,] _plotRadar;
    private double[] _plotRadialGauge;
    private int _nPoints = 0;
    private string[] _seriesLabels;
    private DateTime _timeStart;
    private DateTime _timeEnd;
    private bool _reading = false;   // this controls whether clicking the plots is allowed or not

    private readonly System.Resources.ResourceManager StringsRM = new("ErgoLux.localization.strings", typeof(FrmMain).Assembly);

    public FrmMain()
    {
        // Load settings. This has to go before custom initialization, since some routines depends on this
        _sett = new ClassSettings(Path.GetDirectoryName(System.Environment.ProcessPath));
        LoadProgramSettingsJSON();

        // Set form icon
        if (File.Exists(_sett.AppPath + @"\images\logo.ico")) this.Icon = new Icon(_sett.AppPath + @"\images\logo.ico");

        // Initialize components and GUI
        InitializeComponent();
        InitializeToolStripPanel();
        InitializeToolStrip();
        InitializeStatusStrip();
        InitializeMenuStrip();
        InitializePlots();

        // Language GUI
        UpdateUI_Language();
        SetFormTitle(this);

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
                        StringsRM.GetString("strMsgBoxExit", _sett.AppCulture) ?? "Are you sure you want to exit\nthe application?",
                        StringsRM.GetString("strMsgBoxExitTitle", _sett.AppCulture) ?? "Exit?",
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

    private void OnTimedEvent(object sender, EventArgs e)
    {
        bool result = myFtdiDevice.Write(ClassT10.ReceptorsSingle[0]);
    }

    private void OnDataReceived(object sender, DataReceivedEventArgs e)
    {
        //_data = true;
        (int Sensor, double Iluminance, double Increment, double Percent) result = (0, 0, 0, 0);
        //string str = System.Text.Encoding.UTF8.GetString(e.DataReceived, 0, e.DataReceived.Length);

        if (e.StrDataReceived.Length == ClassT10.LongBytesLength)
        {
            result = ClassT10.DecodeCommand(e.StrDataReceived);
            //System.Diagnostics.Debug.Print("Daata: {0} — TimeStamp: {1}",
            //                            result.ToString(),
            //                            DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture));
            if (result.Sensor < _sett.T10_NumberOfSensors - 1)
            {
                myFtdiDevice.Write(ClassT10.ReceptorsSingle[result.Sensor + 1]);
            }
        }
        else if (e.StrDataReceived.Length == ClassT10.ShortBytesLength)
        {
            return;
        }

        Plots_Update(result.Sensor, result.Iluminance);

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
        string strSep = StringsRM.GetString("strFormTitle", _settings.AppCulture) ?? " - ";
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
        frm.Text = StringsRM.GetString("strFormTitle", _settings.AppCulture) ?? "Signal analysis" + strText;
    }

    /// <summary>
    /// Update user-interface language and localization
    /// </summary>
    private void UpdateUI_Language()
    {
        // Update the form's tittle
        SetFormTitle(this, String.Empty);

        // Update ToolStrip
        toolStripMain_Exit.Text = StringsRM.GetString("strToolStripExit", _sett.AppCulture) ?? "Exit";
        toolStripMain_Exit.ToolTipText = StringsRM.GetString("strToolTipExit", _sett.AppCulture) ?? "Exit the application";
        toolStripMain_Open.Text = StringsRM.GetString("strToolStripOpen", _sett.AppCulture) ?? "Open";
        toolStripMain_Open.ToolTipText = StringsRM.GetString("strToolTipOpen", _sett.AppCulture) ?? "Open data file from disk";
        toolStripMain_Save.Text = StringsRM.GetString("strToolStripSave", _sett.AppCulture) ?? "Save";
        toolStripMain_Save.ToolTipText = StringsRM.GetString("strToolTipSave", _sett.AppCulture) ?? "Save data";
        toolStripMain_Connect.Text = StringsRM.GetString("strToolStripConnect", _sett.AppCulture) ?? "Connect";
        toolStripMain_Connect.ToolTipText = StringsRM.GetString("strToolTipConnect", _sett.AppCulture) ?? "Start receiving data from T-10A device";
        toolStripMain_Disconnect.Text = StringsRM.GetString("strToolStripDisconnect", _sett.AppCulture) ?? "Disconnect";
        toolStripMain_Disconnect.ToolTipText = StringsRM.GetString("strToolTipDisconnect", _sett.AppCulture) ?? "Stop and disconnect T-10A device";
        toolStripMain_Settings.Text = StringsRM.GetString("strToolStripSettings", _sett.AppCulture) ?? "Settings";
        toolStripMain_Settings.ToolTipText = StringsRM.GetString("strToolTipSettings", _sett.AppCulture) ?? "Settings for plots, data, and UI";
        toolStripMain_About.Text = StringsRM.GetString("strToolStripAbout", _sett.AppCulture) ?? "About";
        toolStripMain_About.ToolTipText = StringsRM.GetString("strToolTipAbout", _sett.AppCulture) ?? "About this software";

        // Update StatusStrip
        statusStripLabelRaw.ToolTipText = StringsRM.GetString("strStatusTipRaw", _sett.AppCulture) ?? "Plot raw data";
        statusStripLabelRadar.ToolTipText = StringsRM.GetString("strStatusTipDistribution", _sett.AppCulture) ?? "Plot distribution";
        statusStripLabelMax.ToolTipText = StringsRM.GetString("strStatusTipMax", _sett.AppCulture) ?? "Plot max, average and min";
        statusStripLabelRatio.ToolTipText = StringsRM.GetString("strStatusTipRatio", _sett.AppCulture) ?? "Plot ratios";

        statusStripLabelID.Text = StringsRM.GetString("strStatusID", _sett.AppCulture) ?? "Device ID";
        statusStripLabelID.ToolTipText = StringsRM.GetString("strStatusTipID", _sett.AppCulture) ?? "Device ID";
        statusStripLabelType.Text = StringsRM.GetString("strStatusType", _sett.AppCulture) ?? "Device type";
        statusStripLabelType.ToolTipText = StringsRM.GetString("strStatusTipType", _sett.AppCulture) ?? "Device type";
        statusStripLabelLocation.Text = StringsRM.GetString("strStatusLocation", _sett.AppCulture) ?? "Location ID";
        statusStripLabelLocation.ToolTipText = StringsRM.GetString("strStatusTipLocation", _sett.AppCulture) ?? "T-10A location ID";

        statusStripIconOpen.Text = StringsRM.GetString("strStatusOpen", _sett.AppCulture) ?? "Disconnected";
        statusStripIconOpen.ToolTipText = StringsRM.GetString("strStatusTipOpen", _sett.AppCulture) ?? "Connexion status";
        statusStripIconExchange.Text = StringsRM.GetString("strStatusExchange", _sett.AppCulture) ?? "Receiving data";
        statusStripIconExchange.ToolTipText = StringsRM.GetString("strStatusTipExchange", _sett.AppCulture) ?? "Exchange status";


        // Update menu
        mnuMainFrm_File.Text = StringsRM.GetString("strMenuMainFile", _sett.AppCulture) ?? "&File";
        mnuMainFrm_File_Open.Text = StringsRM.GetString("strMenuMainFileOpen", _sett.AppCulture) ?? "&Open";
        mnuMainFrm_File_Save.Text = StringsRM.GetString("strMenuMainFileSave", _sett.AppCulture) ?? "&Save";
        mnuMainFrm_File_Exit.Text = StringsRM.GetString("strMenuMainFileExit", _sett.AppCulture) ?? "&Exit";
        mnuMainFrm_View.Text = StringsRM.GetString("strMenuMainView", _sett.AppCulture) ?? "&View";
        mnuMainFrm_View_Menu.Text = StringsRM.GetString("strMenuMainViewMenu", _sett.AppCulture) ?? "Show menu";
        mnuMainFrm_View_Toolbar.Text = StringsRM.GetString("strMenuMainViewToolbar", _sett.AppCulture) ?? "Show toolbar";
        mnuMainFrm_View_Raw.Text = StringsRM.GetString("strMenuMainViewRaw", _sett.AppCulture) ?? "Raw data";
        mnuMainFrm_View_Distribution.Text = StringsRM.GetString("strMenuMainViewDistribution", _sett.AppCulture) ?? "Radial distribution";
        mnuMainFrm_View_Average.Text = StringsRM.GetString("strMenuMainViewAverage", _sett.AppCulture) ?? "Averages";
        mnuMainFrm_View_Ratio.Text = StringsRM.GetString("strMenuMainViewRatio", _sett.AppCulture) ?? "Ratios";
        mnuMainFrm_Tools.Text = StringsRM.GetString("strMenuMainTools", _sett.AppCulture) ?? "&Tools";
        mnuMainFrm_Tools_Connect.Text = StringsRM.GetString("strMenuMainToolsConnect", _sett.AppCulture) ?? "&Connect";
        mnuMainFrm_Tools_Disconnect.Text = StringsRM.GetString("strMenuMainToolsDisconnect", _sett.AppCulture) ?? "&Disconnect";
        mnuMainFrm_Tools_Settings.Text = StringsRM.GetString("strMenuMainToolsSettings", _sett.AppCulture) ?? "&Settings";
        mnuMainFrm_Help.Text = StringsRM.GetString("strMenuMainHelpText", _sett.AppCulture) ?? "&Help";
        mnuMainFrm_Help_About.Text = StringsRM.GetString("strMenuMainHelpAbout", _sett.AppCulture) ?? "&About";

        // Update plots
        formsPlot1.Plot.Title(StringsRM.GetString("strPlotRawTitle", _sett.AppCulture) ?? "Illuminance");
        formsPlot1.Plot.YLabel(StringsRM.GetString("strPlotRawYLabel", _sett.AppCulture) ?? "Lux");
        formsPlot1.Plot.XLabel(StringsRM.GetString("strPlotRawXLabel", _sett.AppCulture) ?? "Time (seconds)");
        formsPlot2.Plot.Title(StringsRM.GetString("strPlotDistributionTitle", _sett.AppCulture) ?? "Illuminance distribution");
        formsPlot3.Plot.Title(StringsRM.GetString("strPlotAverageTitle", _sett.AppCulture) ?? "Max, average, min");
        formsPlot3.Plot.YLabel(StringsRM.GetString("strPlotAverageYLabel", _sett.AppCulture) ?? "Lux");
        formsPlot3.Plot.XLabel(StringsRM.GetString("strPlotAverageXLabel", _sett.AppCulture) ?? "Time (seconds)");
        formsPlot4.Plot.Title(StringsRM.GetString("strPlotRatiosTitle", _sett.AppCulture) ?? "Illuminance ratios");
        formsPlot4.Plot.YLabel(StringsRM.GetString("strPlotRatiosYLabel", _sett.AppCulture) ?? "Ratio");
        formsPlot4.Plot.XLabel(StringsRM.GetString("strPlotRatiosXLabel", _sett.AppCulture) ?? "Time (seconds)");

        // Update plots' legends
        if (_plotData is not null)
        {
            _seriesLabels = new string[_plotData.Length];
            for (int i = 0; i < _seriesLabels.Length - _sett.ArrayFixedColumns; i++)
            {
                _seriesLabels[i] = $"{(StringsRM.GetString("strFileHeader08", _sett.AppCulture) ?? "Sensor #")}{i:#0}";
            }
            _seriesLabels[_seriesLabels.Length - _sett.ArrayFixedColumns + 0] = $"{(StringsRM.GetString("strFileHeader09", _sett.AppCulture) ?? "Maximum")}";
            _seriesLabels[_seriesLabels.Length - _sett.ArrayFixedColumns + 1] = $"{(StringsRM.GetString("strFileHeader10", _sett.AppCulture) ?? "Average")}";
            _seriesLabels[_seriesLabels.Length - _sett.ArrayFixedColumns + 2] = $"{(StringsRM.GetString("strFileHeader11", _sett.AppCulture) ?? "Minimum")}";
            _seriesLabels[_seriesLabels.Length - _sett.ArrayFixedColumns + 3] = $"{(StringsRM.GetString("strFileHeader12", _sett.AppCulture) ?? "Min/Average")}";
            _seriesLabels[_seriesLabels.Length - _sett.ArrayFixedColumns + 4] = $"{(StringsRM.GetString("strFileHeader13", _sett.AppCulture) ?? "Min/Max")}";
            _seriesLabels[_seriesLabels.Length - _sett.ArrayFixedColumns + 5] = $"{(StringsRM.GetString("strFileHeader14", _sett.AppCulture) ?? "Average/Max")}";

            for (int i = 0; i < formsPlot1.Plot.GetPlottables().Length; i++)
            {
                formsPlot1.Plot.GetPlottables()[i].GetLegendItems()[0].label = _seriesLabels[i];
            }

            if (formsPlot3.Plot.GetPlottables().Length == 3)
            {
                formsPlot3.Plot.GetPlottables()[0].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _sett.ArrayFixedColumns + 0];
                formsPlot3.Plot.GetPlottables()[1].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _sett.ArrayFixedColumns + 1];
                formsPlot3.Plot.GetPlottables()[2].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _sett.ArrayFixedColumns + 2];
            }

            if (formsPlot4.Plot.GetPlottables().Length == 3)
            {
                formsPlot4.Plot.GetPlottables()[0].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _sett.ArrayFixedColumns + 3];
                formsPlot4.Plot.GetPlottables()[1].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _sett.ArrayFixedColumns + 4];
                formsPlot4.Plot.GetPlottables()[2].GetLegendItems()[0].label = _seriesLabels[_plotData.Length - _sett.ArrayFixedColumns + 5];
            }
        }

    }
}

