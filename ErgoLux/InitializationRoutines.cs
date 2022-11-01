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
        this.tspTop.Join(this.toolStripMain);
        this.tspTop.Join(this.mnuMainFrm);
        this.tspBottom.Join(this.statusStrip);
    }

    /// <summary>
    /// Initialize the ToolStrip component
    /// </summary>
    private void InitializeToolStrip()
    {
        this.toolStripMain.Renderer = new CustomRenderer<ToolStripButton>(System.Drawing.Brushes.SteelBlue, System.Drawing.Brushes.LightSkyBlue);

        this.toolStripMain_Exit.Image = GraphicsResources.LoadIcon(GraphicsResources.IconExit, 48);
        this.toolStripMain_Connect.Image = GraphicsResources.LoadIcon(GraphicsResources.IconConnect, 48);
        this.toolStripMain_Disconnect.Image = GraphicsResources.LoadIcon(GraphicsResources.IconDisconnect, 48);
        this.toolStripMain_Save.Image = GraphicsResources.LoadIcon(GraphicsResources.IconSave, 48);
        this.toolStripMain_Open.Image = GraphicsResources.LoadIcon(GraphicsResources.IconOpen, 48);
        this.toolStripMain_Settings.Image = GraphicsResources.LoadIcon(GraphicsResources.IconSettings, 48);
        this.toolStripMain_About.Image = GraphicsResources.LoadIcon(GraphicsResources.IconAbout, 48);

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
        this.mnuMainFrm_File_Open.Image = GraphicsResources.LoadIcon(GraphicsResources.IconOpen, 16);
        this.mnuMainFrm_File_Save.Image = GraphicsResources.LoadIcon(GraphicsResources.IconSave, 16);
        this.mnuMainFrm_File_Exit.Image = GraphicsResources.LoadIcon(GraphicsResources.IconExit, 16);

        this.mnuMainFrm_Tools_Connect.Image = GraphicsResources.LoadIcon(GraphicsResources.IconConnect, 16);
        this.mnuMainFrm_Tools_Disconnect.Image = GraphicsResources.LoadIcon(GraphicsResources.IconDisconnect, 16);
        this.mnuMainFrm_Tools_Settings.Image = GraphicsResources.LoadIcon(GraphicsResources.IconSettings, 16);

        this.mnuMainFrm_Help_About.Image = GraphicsResources.LoadIcon(GraphicsResources.IconAbout, 16);

        // Initialize the menu checked items
        this.mnuMainFrm_View_Menu.Checked = true;
        this.mnuMainFrm_View_Toolbar.Checked = true;
        this.mnuMainFrm_View_Raw.Checked = this._settings.Plot_ShowRawData;
        this.mnuMainFrm_View_Distribution.Checked = this._settings.Plot_ShowDistribution;
        this.mnuMainFrm_View_Average.Checked = this._settings.Plot_ShowAverage;
        this.mnuMainFrm_View_Ratio.Checked = this._settings.Plot_ShowRatios;

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
        this.statusStrip.Renderer = new CustomRenderer<ToolStripStatusLabelEx>(System.Drawing.Brushes.SteelBlue, System.Drawing.Brushes.LightSkyBlue);

        this.statusStripIconOpen.Image = this._settings.Icon_Close;
        this.statusStripLabelCross.Checked = false;

        this.InitializeStatusStripLabelsStatus();
    }

    /// <summary>
    /// Sets the labels checked status based on the values stored in <see cref="ClassSettings"/>.
    /// </summary>
    private void InitializeStatusStripLabelsStatus()
    {
        this.statusStripLabelRaw.Checked = this._settings.Plot_ShowRawData;
        this.statusStripLabelRadar.Checked = this._settings.Plot_ShowDistribution;
        this.statusStripLabelMax.Checked = this._settings.Plot_ShowAverage;
        this.statusStripLabelRatio.Checked = this._settings.Plot_ShowRatios;
    }

    /// <summary>
    /// Initialize plots: titles, labels, grids, colors, and other visual stuff.
    /// It does not modify axes. That's done elsewhere (when updating the charts with values).
    /// </summary>
    private void InitializePlots()
    {
        // Starting from v 4.1.18 Refresh() must be called manually at least once
        this.plotData.Refresh();
        this.plotDistribution.Refresh();
        this.plotStats.Refresh();
        this.plotRatio.Refresh();

        //formsPlot1.plt.AxisAutoX(margin: 0);
        this.plotData.Plot.SetAxisLimits(xMin: 0, xMax: this._settings.Plot_WindowPoints, yMin: 0, yMax: 1000);

        // Customize styling
        this.plotData.Plot.Palette = ScottPlot.Palette.Category10;
        this.plotData.Plot.Title(StringResources.PlotRawTitle);
        this.plotData.Plot.YLabel(StringResources.PlotRawYLabel);
        this.plotData.Plot.XLabel(StringResources.PlotRawXLabel);
        this.plotData.Plot.Grid(enable: false);
        this.plotData.SnapToPoint = true;

        // Customize the Distribution plot
        this.plotDistribution.Plot.Grid(enable: false);
        this.plotDistribution.Plot.Title(StringResources.PlotDistributionTitle);
        this.plotDistribution.Plot.XAxis.Ticks(false);
        this.plotDistribution.Plot.YAxis.Ticks(false);

        // Customize the Average plot
        this.plotStats.Plot.SetAxisLimits(xMin: 0, xMax: this._settings.Plot_WindowPoints, yMin: 0, yMax: 1000);

        this.plotStats.Plot.Palette = ScottPlot.Palette.Nord;
        this.plotStats.Plot.Title(StringResources.PlotAverageTitle);
        this.plotStats.Plot.YLabel(StringResources.PlotAverageYLabel);
        this.plotStats.Plot.XLabel(StringResources.PlotAverageXLabel);
        this.plotStats.Plot.Grid(enable: false);
        this.plotStats.SnapToPoint = true;

        // Customize the Ratio plot
        //formsPlot4.plt.AxisAuto(horizontalMargin: 0);
        this.plotRatio.Plot.SetAxisLimits(xMin: 0, xMax: this._settings.Plot_WindowPoints, yMin: 0, yMax: 1);

        this.plotRatio.Plot.Palette = ScottPlot.Palette.OneHalf;
        this.plotRatio.Plot.Title(StringResources.PlotRatiosTitle);
        this.plotRatio.Plot.YLabel(StringResources.PlotRatiosYLabel);
        this.plotRatio.Plot.XLabel(StringResources.PlotRatiosXLabel);
        this.plotRatio.Plot.Grid(enable: false);
        this.plotRatio.SnapToPoint = true;

        // Subscribe to the events
        this.plotData.VLineDragged += this.formsPlot_PlottableDragged;
        this.plotStats.VLineDragged += this.formsPlot_PlottableDragged;
        this.plotRatio.VLineDragged += this.formsPlot_PlottableDragged;

        // Set the colorsets
        this.InitializePlotsPalette();
    }

    /// <summary>
    /// Function to assing color palettes to each of the plot controls
    /// </summary>
    private void InitializePlotsPalette()
    {
        // Define colorsets
        if (this._settings.Plot_DistIsRadar)
        {
            this.plotData.Plot.Palette = ScottPlot.Palette.Category10;
            this.plotDistribution.Plot.Palette = ScottPlot.Palette.OneHalfDark;
        }
        else
        {
            this.plotData.Plot.Palette = ScottPlot.Palette.Microcharts;
            this.plotDistribution.Plot.Palette = ScottPlot.Palette.Microcharts;
        }

        this.plotStats.Plot.Palette = ScottPlot.Palette.Nord;
        this.plotRatio.Plot.Palette = ScottPlot.Palette.OneHalf;
    }

    /// <summary>
    /// Function to initialize the illuminance distribution plot
    /// </summary>
    private void InitializePlotDistribution()
    {
        this.plotDistribution.Plot.XAxis2.Hide(false);
        this.plotDistribution.Plot.XAxis2.Ticks(false);
        this.plotDistribution.Plot.XAxis.Hide(false);
        this.plotDistribution.Plot.XAxis.Ticks(false);
        this.plotDistribution.Plot.YAxis2.Hide(false);
        this.plotDistribution.Plot.YAxis2.Ticks(false);
        this.plotDistribution.Plot.YAxis.Hide(false);
        this.plotDistribution.Plot.YAxis.Ticks(false);

    }

    /// <summary>
    /// Initializes the data arrays using <see cref="ClassSettings.T10_NumberOfSensors"/> and <see cref="ClassSettings.ArrayFixedColumns"/>.
    /// </summary>
    private void InitializeArrays()
    {
        // Create new labels and fill in the corresponding text strings based on the culture selected
        this._seriesLabels = new string[this._settings.T10_NumberOfSensors + this._settings.ArrayFixedColumns];
        this.UpdateUI_Series();

        this._plotRadialGauge = new double[this._settings.T10_NumberOfSensors];
        this._plotRadar = new double[2, this._settings.T10_NumberOfSensors];
        this._plotData = new double[this._settings.T10_NumberOfSensors + this._settings.ArrayFixedColumns][];
        for (int i = 0; i < this._settings.T10_NumberOfSensors + this._settings.ArrayFixedColumns; i++)
            this._plotData[i] = new double[this._settings.Plot_ArrayPoints];

    }

}