namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Initialize the ToolStripPanel component: add the child components to it
    /// </summary>
    private void InitializeToolStripPanel()
    {
        //tspTop = new ToolStripPanel();
        //tspBottom = new ToolStripPanel();
        tspTop.Join(toolStripMain);
        tspTop.Join(mnuMainFrm);
        tspBottom.Join(this.statusStrip);

        // Exit the method
        return;
    }

    /// <summary>
    /// Initialize the ToolStrip component
    /// </summary>
    private void InitializeToolStrip()
    {
        toolStripMain.Renderer = new customRenderer<ToolStripButton>(System.Drawing.Brushes.SteelBlue, System.Drawing.Brushes.LightSkyBlue);

        //var path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        if (File.Exists(_sett.AppPath + @"\images\exit.ico")) this.toolStripMain_Exit.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\exit.ico", 48, 48).ToBitmap();
        if (File.Exists(_sett.AppPath + @"\images\connect.ico")) this.toolStripMain_Connect.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\connect.ico", 48, 48).ToBitmap();
        if (File.Exists(_sett.AppPath + @"\images\disconnect.ico")) this.toolStripMain_Disconnect.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\disconnect.ico", 48, 48).ToBitmap();
        if (File.Exists(_sett.AppPath + @"\images\save.ico")) this.toolStripMain_Save.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\save.ico", 48, 48).ToBitmap();
        if (File.Exists(_sett.AppPath + @"\images\openfolder.ico")) this.toolStripMain_Open.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\openfolder.ico", 48, 48).ToBitmap();
        if (File.Exists(_sett.AppPath + @"\images\settings.ico")) this.toolStripMain_Settings.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\settings.ico", 48, 48).ToBitmap();
        if (File.Exists(_sett.AppPath + @"\images\about.ico")) this.toolStripMain_About.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\about.ico", 48, 48).ToBitmap();

        this.toolStripMain_Disconnect.Enabled = false;
        this.toolStripMain_Connect.Enabled = false;
        this.toolStripMain_Open.Enabled = true; // maybe set as default in the WinForms designer
        this.toolStripMain_Save.Enabled = false;

        return;
    }

    /// <summary>
    /// Initialize the MenuStrip component
    /// </summary>
    private void InitializeMenuStrip()
    {
        if (File.Exists(_sett.AppPath + @"\images\openfolder.ico")) this.mnuMainFrm_File_Open.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\openfolder.ico", 16, 16).ToBitmap();
        if (File.Exists(_sett.AppPath + @"\images\save.ico")) this.mnuMainFrm_File_Save.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\save.ico", 16, 16).ToBitmap();
        if (File.Exists(_sett.AppPath + @"\images\exit.ico")) this.mnuMainFrm_File_Exit.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\exit.ico", 16, 16).ToBitmap();

        if (File.Exists(_sett.AppPath + @"\images\connect.ico")) this.mnuMainFrm_Tools_Connect.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\connect.ico", 16, 16).ToBitmap();
        if (File.Exists(_sett.AppPath + @"\images\disconnect.ico")) this.mnuMainFrm_Tools_Disconnect.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\disconnect.ico", 16, 16).ToBitmap();
        if (File.Exists(_sett.AppPath + @"\images\settings.ico")) this.mnuMainFrm_Tools_Settings.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\settings.ico", 16, 16).ToBitmap();

        if (File.Exists(_sett.AppPath + @"\images\about.ico")) this.mnuMainFrm_Help_About.Image = new System.Drawing.Icon(_sett.AppPath + @"\images\about.ico", 16, 16).ToBitmap();

        // Initialize the menu checked items
        this.mnuMainFrm_View_Menu.Checked = true;
        this.mnuMainFrm_View_Toolbar.Checked = true;
        this.mnuMainFrm_View_Raw.Checked = _sett.Plot_ShowRawData;
        this.mnuMainFrm_View_Distribution.Checked = _sett.Plot_ShowDistribution;
        this.mnuMainFrm_View_Average.Checked = _sett.Plot_ShowAverage;
        this.mnuMainFrm_View_Ratio.Checked = _sett.Plot_ShowRatios;

        // Initialize enable status
        this.mnuMainFrm_Tools_Connect.Enabled = false;
        this.mnuMainFrm_Tools_Disconnect.Enabled = false;
        this.toolStripMain_Disconnect.Enabled = false;
        this.toolStripMain_Connect.Enabled = false;

        return;
    }

    /// <summary>
    /// Initialize the StatusStrip component
    /// </summary>
    private void InitializeStatusStrip()
    {
        //if (File.Exists(_sett.AppPath + @"\images\close.ico")) this.statusStripIconOpen.Image = new Icon(_sett.AppPath + @"\images\close.ico", 16, 16).ToBitmap();
        statusStripIconOpen.Image = _sett.Icon_Close;

        statusStripLabelRaw.CheckedChanged += new System.EventHandler(this.statusStripLabelPlots_CheckedChanged);
        statusStripLabelRadar.CheckedChanged += new System.EventHandler(this.statusStripLabelPlots_CheckedChanged);
        statusStripLabelMax.CheckedChanged += new System.EventHandler(this.statusStripLabelPlots_CheckedChanged);
        statusStripLabelRatio.CheckedChanged += new System.EventHandler(this.statusStripLabelPlots_CheckedChanged);

        InitializeStatusStripLabelsStatus();

        //statusStrip.Renderer = new customRenderer<ToolStripLabel>(Brushes.SteelBlue, Brushes.LightSkyBlue);
        return;
    }

    /// <summary>
    /// Sets the labels checked status based on the values stored in <see cref="ClassSettings"/>.
    /// </summary>
    private void InitializeStatusStripLabelsStatus()
    {
        statusStripLabelRaw.Checked = _sett.Plot_ShowRawData;
        statusStripLabelRadar.Checked = _sett.Plot_ShowDistribution;
        statusStripLabelMax.Checked = _sett.Plot_ShowAverage;
        statusStripLabelRatio.Checked = _sett.Plot_ShowRatios;
    }

    /// <summary>
    /// Initialize plots: titles, labels, grids, colors, and other visual stuff.
    /// It does not modify axes. That's done elsewhere (when updating the charts with values).
    /// </summary>
    private void InitializePlots()
    {
        // Starting from v 4.1.18 Refresh() must be called manually at least once
        formsPlot1.Refresh();
        formsPlot2.Refresh();
        formsPlot3.Refresh();
        formsPlot4.Refresh();

        //formsPlot1.plt.AxisAutoX(margin: 0);
        formsPlot1.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0, yMax: 1000);

        // customize styling
        formsPlot1.Plot.Palette = ScottPlot.Drawing.Palette.Category10;
        formsPlot1.Plot.Title("Illuminance");
        formsPlot1.Plot.YLabel("Lux");
        formsPlot1.Plot.XLabel("Time (seconds)");
        formsPlot1.Plot.Grid(enable: false);

        formsPlot1.MouseDown += new System.Windows.Forms.MouseEventHandler(formsPlot_MouseDown);

        // Customize the Distribution plot
        formsPlot2.Plot.Grid(enable: false);
        formsPlot2.Plot.Title("Illuminance distribution");
        formsPlot2.Plot.XAxis.Ticks(false);
        formsPlot2.Plot.YAxis.Ticks(false);

        // Customize the Average plot
        formsPlot3.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0, yMax: 1000);

        formsPlot3.Plot.Palette = ScottPlot.Drawing.Palette.Nord;
        formsPlot3.Plot.Title("Max, average, min");
        formsPlot3.Plot.YLabel("Lux");
        formsPlot3.Plot.XLabel("Time (seconds)");
        formsPlot3.Plot.Grid(enable: false);

        formsPlot3.MouseDown += new System.Windows.Forms.MouseEventHandler(formsPlot_MouseDown);

        // Customize the Ratio plot
        //formsPlot4.plt.AxisAuto(horizontalMargin: 0);
        formsPlot4.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0, yMax: 1);

        formsPlot4.Plot.Palette = ScottPlot.Drawing.Palette.OneHalf;
        formsPlot4.Plot.Title("Illuminance ratios");
        formsPlot4.Plot.YLabel("Ratio");
        formsPlot4.Plot.XLabel("Time (seconds)");
        formsPlot4.Plot.Grid(enable: false);

        formsPlot4.MouseDown += new System.Windows.Forms.MouseEventHandler(formsPlot_MouseDown);

        // Set the colorsets
        InitializePlotsPalette();
    }

    /// <summary>
    /// Function to initialize the plots' color palettes
    /// </summary>
    private void InitializePlotsPalette()
    {
        // Define colorsets
        if (_sett.Plot_DistIsRadar)
        {
            formsPlot1.Plot.Palette = ScottPlot.Drawing.Palette.Category10;
            formsPlot2.Plot.Palette = ScottPlot.Drawing.Palette.OneHalfDark;
        }
        else
        {
            formsPlot1.Plot.Palette = ScottPlot.Drawing.Palette.Microcharts;
            formsPlot2.Plot.Palette = ScottPlot.Drawing.Palette.Microcharts;
        }

        formsPlot3.Plot.Palette = ScottPlot.Drawing.Palette.Nord;
        formsPlot4.Plot.Palette = ScottPlot.Drawing.Palette.OneHalf;
    }

    /// <summary>
    /// Function to initialize the illuminance distribution plot
    /// </summary>
    private void InitializePlotDistribution()
    {
        formsPlot2.Plot.XAxis2.Hide(false);
        formsPlot2.Plot.XAxis2.Ticks(false);
        formsPlot2.Plot.XAxis.Hide(false);
        formsPlot2.Plot.XAxis.Ticks(false);
        formsPlot2.Plot.YAxis2.Hide(false);
        formsPlot2.Plot.YAxis2.Ticks(false);
        formsPlot2.Plot.YAxis.Hide(false);
        formsPlot2.Plot.YAxis.Ticks(false);

    }
    /// <summary>
    /// Initializes the data arrays using <see cref="ClassSettings"/> properties T10_NumberOfPoints and ArrayFixedColumns
    /// </summary>
    private void InitializeArrays()
    {
        _plotRadialGauge = new double[_sett.T10_NumberOfSensors];
        _plotRadar = new double[2, _sett.T10_NumberOfSensors];
        _plotData = new double[_sett.T10_NumberOfSensors + _sett.ArrayFixedColumns][];
        for (int i = 0; i < _sett.T10_NumberOfSensors + _sett.ArrayFixedColumns; i++)
            _plotData[i] = new double[_sett.Plot_ArrayPoints];
    }
}
