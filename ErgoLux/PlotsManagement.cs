namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Clears all data in the plots and sets the private variable _nPoints to 0
    /// </summary>
    private void Plots_Clear()
    {
        this._nPoints = 0;
        this._max = 0;
        this._min = 0;

        this.plotData.ShowCrossHair = false;
        this.plotStats.ShowCrossHair = false;
        this.plotRatio.ShowCrossHair = false;

        this.plotData.Plot.Clear();
        this.plotDistribution.Plot.Clear();
        this.plotStats.Plot.Clear();
        this.plotRatio.Plot.Clear();

        this.InitializePlotsPalette();

        // to do: this is unnecessary. Verify it in order to delete it.
        //InitializePlots();
    }

    /// <summary>
    /// Binds the data arrays to the plots. Both _plotData and _plotRadar should be initialized, otherwise this function will throw an error.
    /// </summary>
    private void Plots_DataBinding()
    {
        // Binding for Plot Raw Data
        for (int i = 0; i < this._settings.T10_NumberOfSensors; i++)
        {
            var plot = this.plotData.Plot.AddSignal(this._plotData[i], sampleRate: this._settings.T10_Frequency, label: this._seriesLabels[i]);
            //formsPlot1.Refresh();
            plot.MinRenderIndex = 0;
            plot.MaxRenderIndex = 0;
        }
        this.plotData.Plot.SetAxisLimits(xMin: 0, xMax: this._settings.Plot_WindowPoints, yMin: 0);

        // Binding for Distribution plot
        if (this._settings.Plot_DistIsRadar)
        {
            string[] labels = new string[this._settings.T10_NumberOfSensors];
            for (int i = 0; i < this._settings.T10_NumberOfSensors; i++)
                labels[i] = $"#{i:#0}";
            var plt = this.plotDistribution.Plot.AddRadar(this._plotRadar, disableFrameAndGrid: false);
            plt.FillColors[0] = Color.FromArgb(100, plt.LineColors[0]);
            plt.FillColors[1] = Color.FromArgb(150, plt.LineColors[1]);
            //formsPlot2.Plot.Grid(enable: false);

            plt.AxisType = ScottPlot.RadarAxis.Polygon;
            plt.ShowAxisValues = false;
            plt.CategoryLabels = labels;
            plt.GroupLabels = new string[] { $"{StringResources.FileHeader15}",
                                            $"{StringResources.FileHeader16}" };
        }
        else
        {
            var plt = this.plotDistribution.Plot.AddRadialGauge(this._plotRadialGauge);
            var strLabels = new string[this._settings.T10_NumberOfSensors];
            for (int i = 0; i < this._settings.T10_NumberOfSensors; i++)
                strLabels[i] = this._seriesLabels[i];
            plt.Labels = strLabels;
            plt.StartingAngle = 180;
        }
        this.InitializePlotDistribution();

        // Binding for Plot Average
        for (int i = this._settings.T10_NumberOfSensors; i < this._settings.T10_NumberOfSensors + 3; i++)
        {
            //var plot = formsPlot3.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: (i == _sett.T10_NumberOfSensors ? "Max" : (i == (_sett.T10_NumberOfSensors + 1) ? "Average" : "Min")));
            var plot = this.plotStats.Plot.AddSignal(this._plotData[i], sampleRate: this._settings.T10_Frequency, label: this._seriesLabels[i]);
            plot.MinRenderIndex = 0;
            plot.MaxRenderIndex = 0;
        }
        this.plotStats.Plot.SetAxisLimits(xMin: 0, xMax: this._settings.Plot_WindowPoints, yMin: 0);

        // Binding for Plot Ratio
        for (int i = this._settings.T10_NumberOfSensors + 3; i < this._settings.T10_NumberOfSensors + this._settings.ArrayFixedColumns; i++)
        {
            //var plot = formsPlot4.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: (i == (_sett.T10_NumberOfSensors + 3) ? "Min/Average" : (i == (_sett.T10_NumberOfSensors + 4) ? "Min/Max" : "Average/Max")));
            var plot = this.plotRatio.Plot.AddSignal(this._plotData[i], sampleRate: this._settings.T10_Frequency, label: this._seriesLabels[i]);
            plot.MinRenderIndex = 0;
            plot.MaxRenderIndex = 0;
        }
        this.plotRatio.Plot.SetAxisLimits(xMin: 0, xMax: this._settings.Plot_WindowPoints, yMin: 0, yMax: 1);
    }

    /// <summary>
    /// Fetchs data into the plots
    /// </summary>
    /// <param name="resetPlotPoints"><see langword="True"/> if <see cref="_nPoints"/> is set equal to <see cref="_settings.Plot_ArrayPoints"/>, <see langword="False">false</see> otherwise </param>
    /// <param name="showAllData">True to fit data into the plots, false otherwise</param>
    private void Plots_FetchData(bool resetPlotPoints = true, bool showAllData = true)
    {
        int points = resetPlotPoints ? this._settings.Plot_ArrayPoints : this._nPoints;

        this.Plots_Clear();  // This sets _nPoints = 0, so we need to reset it now

        this._nPoints = points;

        this.Plots_DataBinding();    // Bind the arrays to the plots
        this.Plots_ShowLegends();    // Show the legends in the picture boxes

        if (showAllData)
            this.Plots_ShowFull();   // Show all data (fit data)

        this.Plots_Refresh();
    }

    /// <summary>
    /// Refreshes all plot controls
    /// </summary>
    private void Plots_Refresh()
    {
        this.plotData.Refresh(skipIfCurrentlyRendering: true);
        this.plotDistribution.Refresh(skipIfCurrentlyRendering: true);
        this.plotStats.Refresh(skipIfCurrentlyRendering: true);
        this.plotRatio.Refresh(skipIfCurrentlyRendering: true);
    }

    /// <summary>
    /// Shows plots's full range data
    /// </summary>
    private void Plots_ShowFull()
    {
        if (this._settings.Plot_ShowRawData)
        {
            foreach (var plot in this.plotData.Plot.GetPlottables())
                ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = (this._nPoints > 0) ? (this._nPoints - 1) : 0;
            this.plotData.Plot.AxisAuto();
            this.plotData.Plot.SetAxisLimits(xMin: 0, yMin: 0);
        }

        this.plotDistribution.Refresh();

        if (this._settings.Plot_ShowAverage)
        {
            foreach (var plot in this.plotStats.Plot.GetPlottables())
                ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = (this._nPoints > 0) ? (this._nPoints - 1) : 0;
            this.plotStats.Plot.AxisAuto();
            this.plotStats.Plot.SetAxisLimits(xMin: 0, yMin: 0);
        }

        if (this._settings.Plot_ShowRatios)
        {
            foreach (var plot in this.plotRatio.Plot.GetPlottables())
                ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = (this._nPoints > 0) ? (this._nPoints - 1) : 0;
            this.plotRatio.Plot.AxisAuto();
            this.plotRatio.Plot.SetAxisLimits(xMin: 0, yMin: 0, yMax: 1);
        }
    }

    /// <summary>
    /// Show plots legends in picture boxes
    /// </summary>
    private void Plots_ShowLegends()
    {
        bool showLegA = this._settings.Plot_DistIsRadar;
        bool showLegB = !this._settings.Plot_DistIsRadar || this._settings.T10_NumberOfSensors >= 2;
        Bitmap legendA;
        Bitmap legendB;
        Bitmap bitmap;
        Size szLegend = new();

        // Combine legends from Plot1 and Plot2 and draw a black border around each legend
        legendA = showLegA ? this.plotData.Plot.RenderLegend() : new(1, 1);
        legendB = showLegB ? this.plotDistribution.Plot.RenderLegend() : new(1, 1);
        szLegend.Width = Math.Max(showLegA ? legendA.Width : 0, showLegB ? legendB.Width : 0);
        szLegend.Width += 2;    // black border drawing
        szLegend.Height = (showLegA ? legendA.Height + 2 : 0);
        szLegend.Height += (showLegB ? legendB.Height + 2 : 0);
        szLegend.Height += (showLegA && showLegB ? this._settings.PxBetweenLegends : 0);
        bitmap = new Bitmap(szLegend.Width, szLegend.Height);
        using Graphics GraphicsA = Graphics.FromImage(bitmap);

        if (showLegA)
        {
            GraphicsA.DrawRectangle(new Pen(Color.Black, 1),
                                (bitmap.Width - legendA.Width - 2) / 2,
                                0,
                                legendA.Width + 1,
                                legendA.Height + 1);
            GraphicsA.DrawImage(legendA, (bitmap.Width - legendA.Width - 2) / 2 + 1, 1);
        }
        if (showLegB)
        {
            GraphicsA.DrawRectangle(new Pen(Color.Black, 1),
                                (bitmap.Width - legendB.Width - 2) / 2,
                                (showLegA ? legendA.Height + this._settings.PxBetweenLegends + 1 : 0),
                                legendB.Width + 1,
                                legendB.Height + 1);
            GraphicsA.DrawImage(legendB, (bitmap.Width - legendB.Width - 2) / 2 + 1, (showLegA ? legendA.Height + this._settings.PxBetweenLegends + 2 : 1));
        }
        this.pictureBox1.Image = bitmap;

        // Combine legends from Plot3 and Plot4 and draw a black border around each legend
        legendA = this.plotStats.Plot.RenderLegend();
        legendB = this.plotRatio.Plot.RenderLegend();
        bitmap = new Bitmap(Math.Max(legendA.Width, legendB.Width) + 2, legendA.Height + legendB.Height + this._settings.PxBetweenLegends + 4);
        using Graphics GraphicsB = Graphics.FromImage(bitmap);
        GraphicsB.DrawRectangle(new Pen(Color.Black),
                                (bitmap.Width - legendA.Width - 2) / 2,
                                0,
                                legendA.Width + 1,
                                legendA.Height + 1);
        GraphicsB.DrawImage(legendA, (bitmap.Width - legendA.Width - 2) / 2 + 1, 1);
        GraphicsB.DrawRectangle(new Pen(Color.Black),
                                (bitmap.Width - legendB.Width - 2) / 2,
                                legendA.Height + this._settings.PxBetweenLegends + 1,
                                legendB.Width + 1,
                                legendB.Height + 2);
        GraphicsB.DrawImage(legendB, (bitmap.Width - legendB.Width - 2) / 2 + 1, legendA.Height + this._settings.PxBetweenLegends + 2);
        this.pictureBox2.Image = bitmap;
    }

    /// <summary>
    /// Updates plots with new values
    /// </summary>
    /// <param name="sensor">Sensor number</param>
    /// <param name="value">New illuminance value</param>
    private void Plots_Update(int sensor, double value)
    {
        // Resize arrays if necessary
        int factor = (this._nPoints + 10) / this._settings.Plot_ArrayPoints;
        factor *= this._settings.Plot_ArrayPoints;
        if (factor > this._plotData[0].Length - 1)
        {
            //System.Diagnostics.Debug.WriteLine($"Current _plotData[i] points: {_nPoints}, array size: {_plotData[0].Length}, new size: {_settings.Plot_ArrayPoints + factor}");
            this._settings.Plot_ArrayPoints += factor;
            for (int i = 0; i < this._plotData.Length; i++)
            {
                Array.Resize<double>(ref this._plotData[i], this._settings.Plot_ArrayPoints);
            }

            // https://github.com/ScottPlot/ScottPlot/discussions/1042
            // https://swharden.com/scottplot/faq/version-4.1/
            // Update array reference in the plots. The Update() method doesn't allow a bigger array, so we need to modify Ys property
            int j = 0;
            foreach (var plot in this.plotData.Plot.GetPlottables())
            {
                ((ScottPlot.Plottable.SignalPlot)plot).Ys = this._plotData[j];
                j++;
            }
            foreach (var plot in this.plotStats.Plot.GetPlottables())
            {
                ((ScottPlot.Plottable.SignalPlot)plot).Ys = this._plotData[j];
                j++;
            }
            foreach (var plot in this.plotRatio.Plot.GetPlottables())
            {
                ((ScottPlot.Plottable.SignalPlot)plot).Ys = this._plotData[j];
                j++;
            }
        }

        // Data computation
        this._plotData[sensor][this._nPoints] = value;
        this._plotRadar[1, sensor] = value;
        this._plotRadialGauge[sensor] = value;

        this._max = sensor == 0 ? value : (value > this._max ? value : this._max);
        this._min = sensor == 0 ? value : (value < this._min ? value : this._min);
        this._average += value;

        // Only render when the last sensor value is received
        if (sensor == this._settings.T10_NumberOfSensors - 1)
        {
            // Compute data
            this._average /= this._settings.T10_NumberOfSensors;
            this._plotData[this._settings.T10_NumberOfSensors][this._nPoints] = this._max;
            this._plotData[this._settings.T10_NumberOfSensors + 1][this._nPoints] = this._average;
            this._plotData[this._settings.T10_NumberOfSensors + 2][this._nPoints] = this._min;
            this._plotData[this._settings.T10_NumberOfSensors + 3][this._nPoints] = this._average > 0 ? this._min / this._average : 0;
            this._plotData[this._settings.T10_NumberOfSensors + 4][this._nPoints] = this._max > 0 ? this._min / this._max : 0;
            this._plotData[this._settings.T10_NumberOfSensors + 5][this._nPoints] = this._min > 0 ? this._average / this._max : 0;

            // Adjust the plots's axis
            this.plotData.Plot.AxisAutoY();
            this.plotStats.Plot.AxisAutoY();
            this.plotData.Plot.SetAxisLimits(yMin: 0);
            this.plotStats.Plot.SetAxisLimits(yMin: 0);
            if (this._nPoints / this._settings.T10_Frequency >= this.plotData.Plot.GetAxisLimits().XMax)
            {
                int xMin = (int)((this._nPoints / this._settings.T10_Frequency) - this._settings.Plot_WindowPoints * 1 / 5);
                int xMax = xMin + this._settings.Plot_WindowPoints;
                this.plotData.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                this.plotStats.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                this.plotRatio.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                //formsPlot3.Plot.SetAxisLimits(xMin: (_nPoints - _sett.Plot_WindowPoints) / _sett.T10_Frequency, xMax: (_nPoints + _sett.Plot_WindowPoints) / _sett.T10_Frequency);
                //formsPlot4.Plot.SetAxisLimits(xMin: (_nPoints - _sett.Plot_WindowPoints) / _sett.T10_Frequency, xMax: (_nPoints + _sett.Plot_WindowPoints) / _sett.T10_Frequency);
            }

            // Update first plot
            if (this._settings.Plot_ShowRawData)
            {
                foreach (var plot in this.plotData.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = this._nPoints;
                    //((ScottPlot.PlottableSignal)plot).minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                    //sigPlot.maxRenderIndex = _dataN;
                    //sigPlot.minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                }
            }

            // Update radar plot
            if (this._settings.Plot_ShowDistribution)
            {
                // We always store the RadarPlot data in case the user wants to use it later
                for (int i = 0; i < this._settings.T10_NumberOfSensors; i++)
                    this._plotRadar[0, i] = this._average;

                // Update the distribution plot
                if (this._settings.Plot_DistIsRadar)
                {
                    ((ScottPlot.Plottable.RadarPlot)this.plotDistribution.Plot.GetPlottables()[0]).Update(this._plotRadar, false);
                }
                else
                {
                    var plot = (ScottPlot.Plottable.RadialGaugePlot)this.plotDistribution.Plot.GetPlottables()[0];
                    var maxAngle = this._average > 0 ? 180 * this._plotRadialGauge.Max() / this._average : 0.0;
                    plot.MaximumAngle = maxAngle > 360 ? 360.0 : maxAngle;
                    plot.Update(this._plotRadialGauge);
                    //plot.MaximumAngle = 180 * _plotRadialGauge.Max() / _average;
                    //if (plot.MaximumAngle > 360) plot.MaximumAngle = 360.0;
                }
            }

            // Update max, average, and min plot
            if (this._settings.Plot_ShowAverage)
            {
                foreach (var plot in this.plotStats.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = this._nPoints;
                }
            }

            // Update ratios plot
            if (this._settings.Plot_ShowRatios)
            {
                foreach (var plot in this.plotRatio.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = this._nPoints;
                }
            }

            this.Plots_Refresh();

            // Modify internal numeric variables
            ++this._nPoints;
            this._max = 0.0;
            this._min = 0.0;
            this._average = 0.0;
        }

        // https://github.com/ScottPlot/ScottPlot/blob/096062f5dfde8fd5f1e2eb2e15e0e7ce9b17a54b/src/ScottPlot.Demo.WinForms/WinFormsDemos/LiveDataUpdate.cs#L14-L91
    }

    /// <summary>
    /// Called when the user drags the any plot crosshair 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void formsPlot_PlottableDragged(object? sender, ScottPlot.LineDragEventArgs e)
    {
        // If we are reading from the sensor, then exit
        if (this._reading) return;

        //var MyPlot = ((ScottPlot.FormsPlot)sender);
        if (sender?.GetType() != typeof(ScottPlot.FormsPlotCrossHair)) return;

        var MyPlot = (ScottPlot.FormsPlotCrossHair)sender;

        // Set the data to be shown on the distribution plot (Radar or RadialGauge)
        for (int i = 0; i < this._settings.T10_NumberOfSensors; i++)
        {
            this._plotRadialGauge[i] = this._plotData[i][e.PointIndex];
            this._plotRadar[1, i] = this._plotData[i][e.PointIndex];
            this._plotRadar[0, i] = this._plotData[this._settings.T10_NumberOfSensors + 1][e.PointIndex];
        }

        // Update the distribution plot
        if (this._settings.Plot_DistIsRadar)
        {
            ((ScottPlot.Plottable.RadarPlot)this.plotDistribution.Plot.GetPlottables()[0]).Update(this._plotRadar, false);
        }
        else
        {
            var plot = ((ScottPlot.Plottable.RadialGaugePlot)this.plotDistribution.Plot.GetPlottables()[0]);
            plot.Update(this._plotRadialGauge);
            plot.MaximumAngle = 180 * this._plotRadialGauge.Max() / this._plotRadialGauge.Average();
            if (plot.MaximumAngle > 360) plot.MaximumAngle = 360.0;
        }
        this.plotDistribution.Refresh(skipIfCurrentlyRendering: true);

        // Update the rest of the plots accordingly
        if (MyPlot.Name == "plotData")
        {
            this.plotData.Refresh(skipIfCurrentlyRendering: true);
            if (this.plotStats.ShowCrossHair)
            {
                if (this.plotStats.VerticalLine is not null) this.plotStats.VerticalLine.X = e.PointX;
                if (this.plotStats.HorizontalLine is not null) this.plotStats.HorizontalLine.Y = this._plotData[this._settings.T10_NumberOfSensors][e.PointIndex];
                this.plotStats.Refresh(skipIfCurrentlyRendering: true);
            }

            if (this.plotRatio.ShowCrossHair)
            {
                if (this.plotRatio.VerticalLine is not null) this.plotRatio.VerticalLine.X = e.PointX;
                if (this.plotRatio.HorizontalLine is not null) this.plotRatio.HorizontalLine.Y = this._plotData[this._settings.T10_NumberOfSensors + 3][e.PointIndex];
                this.plotRatio.Refresh(skipIfCurrentlyRendering: true);
            }
        }
        else if (MyPlot.Name == "plotStats")
        {
            this.plotStats.Refresh(skipIfCurrentlyRendering: true);
            if (this.plotData.ShowCrossHair)
            {
                if (this.plotData.VerticalLine is not null) this.plotData.VerticalLine.X = e.PointX;
                if (this.plotData.HorizontalLine is not null) this.plotData.HorizontalLine.Y = this._plotData[this._settings.T10_NumberOfSensors - 1][e.PointIndex];
                this.plotData.Refresh(skipIfCurrentlyRendering: true);
            }
            if (this.plotRatio.ShowCrossHair)
            {
                if (this.plotRatio.VerticalLine is not null) this.plotRatio.VerticalLine.X = e.PointX;
                if (this.plotRatio.HorizontalLine is not null) this.plotRatio.HorizontalLine.Y = this._plotData[this._settings.T10_NumberOfSensors + 3][e.PointIndex];
                this.plotRatio.Refresh(skipIfCurrentlyRendering: true);
            }
        }
        else if (MyPlot.Name == "plotRatio")
        {
            this.plotRatio.Refresh(skipIfCurrentlyRendering: true);
            if (this.plotData.ShowCrossHair)
            {
                if (this.plotData.VerticalLine is not null) this.plotData.VerticalLine.X = e.PointX;
                if (this.plotData.HorizontalLine is not null) this.plotData.HorizontalLine.Y = this._plotData[this._settings.T10_NumberOfSensors - 1][e.PointIndex];
                this.plotData.Refresh(skipIfCurrentlyRendering: true);
            }
            if (this.plotStats.ShowCrossHair)
            {
                if (this.plotStats.VerticalLine is not null) this.plotStats.VerticalLine.X = e.PointX;
                if (this.plotStats.HorizontalLine is not null) this.plotStats.HorizontalLine.Y = this._plotData[this._settings.T10_NumberOfSensors][e.PointIndex];
                this.plotStats.Refresh(skipIfCurrentlyRendering: true);
            }
        }
    }
}