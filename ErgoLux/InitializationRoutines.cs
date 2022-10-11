using System.Windows.Forms;

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
        tspBottom.Join(statusStrip);
    }

    /// <summary>
    /// Initialize the ToolStrip component
    /// </summary>
    private void InitializeToolStrip()
    {
        toolStripMain.Renderer = new CustomRenderer<ToolStripButton>(System.Drawing.Brushes.SteelBlue, System.Drawing.Brushes.LightSkyBlue);

        this.toolStripMain_Exit.Image = new System.Drawing.Icon(GraphicsResources.IconExit,48,48).ToBitmap();
        this.toolStripMain_Connect.Image = new System.Drawing.Icon(GraphicsResources.IconConnect, 48, 48).ToBitmap();
        this.toolStripMain_Disconnect.Image = new System.Drawing.Icon(GraphicsResources.IconDisconnect, 48, 48).ToBitmap();
        this.toolStripMain_Save.Image = new System.Drawing.Icon(GraphicsResources.IconSave, 48, 48).ToBitmap();
        this.toolStripMain_Open.Image = new System.Drawing.Icon(GraphicsResources.IconOpen, 48, 48).ToBitmap();
        this.toolStripMain_Settings.Image = new System.Drawing.Icon(GraphicsResources.IconSettings, 48, 48).ToBitmap();
        this.toolStripMain_About.Image = new System.Drawing.Icon(GraphicsResources.IconAbout, 48, 48).ToBitmap();

        this.toolStripMain_Disconnect.Enabled = false;
        this.toolStripMain_Connect.Enabled = false;
        this.toolStripMain_Open.Enabled = true; // maybe set as default in the WinForms designer
        this.toolStripMain_Save.Enabled = true;
    }

    /// <summary>
    /// Initialize the MenuStrip component
    /// </summary>
    private void InitializeMenuStrip()
    {
        this.mnuMainFrm_File_Open.Image = new System.Drawing.Icon(GraphicsResources.IconOpen, 16, 16).ToBitmap();
        this.mnuMainFrm_File_Save.Image = new System.Drawing.Icon(GraphicsResources.IconSave, 16, 16).ToBitmap();
        this.mnuMainFrm_File_Exit.Image = new System.Drawing.Icon(GraphicsResources.IconExit, 16, 16).ToBitmap();

        this.mnuMainFrm_Tools_Connect.Image = new System.Drawing.Icon(GraphicsResources.IconConnect, 16, 16).ToBitmap();
        this.mnuMainFrm_Tools_Disconnect.Image = new System.Drawing.Icon(GraphicsResources.IconDisconnect, 16, 16).ToBitmap();
        this.mnuMainFrm_Tools_Settings.Image = new System.Drawing.Icon(GraphicsResources.IconSettings, 16, 16).ToBitmap();

        this.mnuMainFrm_Help_About.Image = new System.Drawing.Icon(GraphicsResources.IconAbout, 16, 16).ToBitmap();

        // Initialize the menu checked items
        this.mnuMainFrm_View_Menu.Checked = true;
        this.mnuMainFrm_View_Toolbar.Checked = true;
        this.mnuMainFrm_View_Raw.Checked = _settings.Plot_ShowRawData;
        this.mnuMainFrm_View_Distribution.Checked = _settings.Plot_ShowDistribution;
        this.mnuMainFrm_View_Average.Checked = _settings.Plot_ShowAverage;
        this.mnuMainFrm_View_Ratio.Checked = _settings.Plot_ShowRatios;

        // Initialize enable status
        this.mnuMainFrm_Tools_Connect.Enabled = false;
        this.mnuMainFrm_Tools_Disconnect.Enabled = false;
        this.toolStripMain_Disconnect.Enabled = false;
        this.toolStripMain_Connect.Enabled = false;
    }

    /// <summary>
    /// Initialize the StatusStrip component
    /// </summary>
    private void InitializeStatusStrip()
    {
        //statusStrip.Renderer = new CustomRenderer<ToolStripStatusLabelEx>(System.Drawing.Brushes.SteelBlue, System.Drawing.Brushes.LightSkyBlue);

        statusStripIconOpen.Image = _settings.Icon_Close;
        statusStripLabelCross.Checked = false;

        InitializeStatusStripLabelsStatus();
    }

    /// <summary>
    /// Sets the labels checked status based on the values stored in <see cref="ClassSettings"/>.
    /// </summary>
    private void InitializeStatusStripLabelsStatus()
    {
        statusStripLabelRaw.Checked = _settings.Plot_ShowRawData;
        statusStripLabelRadar.Checked = _settings.Plot_ShowDistribution;
        statusStripLabelMax.Checked = _settings.Plot_ShowAverage;
        statusStripLabelRatio.Checked = _settings.Plot_ShowRatios;
    }

    /// <summary>
    /// Initialize plots: titles, labels, grids, colors, and other visual stuff.
    /// It does not modify axes. That's done elsewhere (when updating the charts with values).
    /// </summary>
    private void InitializePlots()
    {
        // Starting from v 4.1.18 Refresh() must be called manually at least once
        plotData.Refresh();
        plotDistribution.Refresh();
        plotStats.Refresh();
        plotRatio.Refresh();

        //formsPlot1.plt.AxisAutoX(margin: 0);
        plotData.Plot.SetAxisLimits(xMin: 0, xMax: _settings.Plot_WindowPoints, yMin: 0, yMax: 1000);

        // Customize styling
        plotData.Plot.Palette = ScottPlot.Palette.Category10;
        plotData.Plot.Title(StringResources.PlotRawTitle);
        plotData.Plot.YLabel(StringResources.PlotRawYLabel);
        plotData.Plot.XLabel(StringResources.PlotRawXLabel);
        plotData.Plot.Grid(enable: false);
        plotData.SnapToPoint = true;

        // Customize the Distribution plot
        plotDistribution.Plot.Grid(enable: false);
        plotDistribution.Plot.Title(StringResources.PlotDistributionTitle);
        plotDistribution.Plot.XAxis.Ticks(false);
        plotDistribution.Plot.YAxis.Ticks(false);

        // Customize the Average plot
        plotStats.Plot.SetAxisLimits(xMin: 0, xMax: _settings.Plot_WindowPoints, yMin: 0, yMax: 1000);

        plotStats.Plot.Palette = ScottPlot.Palette.Nord;
        plotStats.Plot.Title(StringResources.PlotAverageTitle);
        plotStats.Plot.YLabel(StringResources.PlotAverageYLabel);
        plotStats.Plot.XLabel(StringResources.PlotAverageXLabel);
        plotStats.Plot.Grid(enable: false);
        plotStats.SnapToPoint = true;

        // Customize the Ratio plot
        //formsPlot4.plt.AxisAuto(horizontalMargin: 0);
        plotRatio.Plot.SetAxisLimits(xMin: 0, xMax: _settings.Plot_WindowPoints, yMin: 0, yMax: 1);

        plotRatio.Plot.Palette = ScottPlot.Palette.OneHalf;
        plotRatio.Plot.Title(StringResources.PlotRatiosTitle);
        plotRatio.Plot.YLabel(StringResources.PlotRatiosYLabel);
        plotRatio.Plot.XLabel(StringResources.PlotRatiosXLabel);
        plotRatio.Plot.Grid(enable: false);
        plotRatio.SnapToPoint = true;

        // Subscribe to the events
        plotData.VLineDragged += formsPlot_PlottableDragged;
        plotStats.VLineDragged += formsPlot_PlottableDragged;
        plotRatio.VLineDragged += formsPlot_PlottableDragged;

        // Set the colorsets
        InitializePlotsPalette();
    }

    /// <summary>
    /// Function to initialize the plots' color palettes
    /// </summary>
    private void InitializePlotsPalette()
    {
        // Define colorsets
        if (_settings.Plot_DistIsRadar)
        {
            plotData.Plot.Palette = ScottPlot.Palette.Category10;
            plotDistribution.Plot.Palette = ScottPlot.Palette.OneHalfDark;
        }
        else
        {
            plotData.Plot.Palette = ScottPlot.Palette.Microcharts;
            plotDistribution.Plot.Palette = ScottPlot.Palette.Microcharts;
        }

        plotStats.Plot.Palette = ScottPlot.Palette.Nord;
        plotRatio.Plot.Palette = ScottPlot.Palette.OneHalf;
    }

    /// <summary>
    /// Function to initialize the illuminance distribution plot
    /// </summary>
    private void InitializePlotDistribution()
    {
        plotDistribution.Plot.XAxis2.Hide(false);
        plotDistribution.Plot.XAxis2.Ticks(false);
        plotDistribution.Plot.XAxis.Hide(false);
        plotDistribution.Plot.XAxis.Ticks(false);
        plotDistribution.Plot.YAxis2.Hide(false);
        plotDistribution.Plot.YAxis2.Ticks(false);
        plotDistribution.Plot.YAxis.Hide(false);
        plotDistribution.Plot.YAxis.Ticks(false);

    }

    /// <summary>
    /// Initializes the data arrays using <see cref="ClassSettings.T10_NumberOfSensors"/> and <see cref="ClassSettings.ArrayFixedColumns"/>.
    /// </summary>
    private void InitializeArrays()
    {
        _seriesLabels = new string[_settings.T10_NumberOfSensors + _settings.ArrayFixedColumns];
        UpdateUI_Series();
        _plotRadialGauge = new double[_settings.T10_NumberOfSensors];
        _plotRadar = new double[2, _settings.T10_NumberOfSensors];
        _plotData = new double[_settings.T10_NumberOfSensors + _settings.ArrayFixedColumns][];
        for (int i = 0; i < _settings.T10_NumberOfSensors + _settings.ArrayFixedColumns; i++)
            _plotData[i] = new double[_settings.Plot_ArrayPoints];

    }

}
