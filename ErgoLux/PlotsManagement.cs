namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Clears all data in the plots and sets the private variable _nPoints to 0
    /// </summary>
    private void Plots_Clear()
    {
        _nPoints = 0;
        _max = 0;
        _min = 0;

        plotData.ShowCrossHair = false;
        plotStats.ShowCrossHair = false;
        plotRatio.ShowCrossHair = false;

        statusStripLabelCross.Checked = false;

        plotData.Plot.Clear();
        plotDistribution.Plot.Clear();
        plotStats.Plot.Clear();
        plotRatio.Plot.Clear();

        InitializePlotsPalette();

        // to do: this is unnecessary. Verify it in order to delete it.
        //InitializePlots();
    }

    /// <summary>
    /// Binds the data arrays to the plots. Both _plotData and _plotRadar should be initialized, otherwise this function will throw an error.
    /// </summary>
    private void Plots_DataBinding()
    {
        // Binding for Plot Raw Data
        for (int i = 0; i < _settings.T10_NumberOfSensors; i++)
        {
            var plot = plotData.Plot.AddSignal(_plotData[i], sampleRate: _settings.T10_Frequency, label: _seriesLabels[i]);
            //formsPlot1.Refresh();
            plot.MinRenderIndex = 0;
            plot.MaxRenderIndex = 0;
        }
        plotData.Plot.SetAxisLimits(xMin: 0, xMax: _settings.Plot_WindowPoints, yMin: 0, yMax: 1000);

        // Binding for Distribution plot
        if (_settings.Plot_DistIsRadar)
        {
            string[] labels = new string[_settings.T10_NumberOfSensors];
            for (int i = 0; i < _settings.T10_NumberOfSensors; i++)
                labels[i] = $"#{i:#0}";
            var plt = plotDistribution.Plot.AddRadar(_plotRadar, disableFrameAndGrid: false);
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
            var plt = plotDistribution.Plot.AddRadialGauge(_plotRadialGauge);
            var strLabels = new string[_settings.T10_NumberOfSensors];
            for (int i = 0; i < _settings.T10_NumberOfSensors; i++)
                strLabels[i] = _seriesLabels[i];
            plt.Labels = strLabels;
            plt.StartingAngle = 180;
        }
        InitializePlotDistribution();

        // Binding for Plot Average
        for (int i = _settings.T10_NumberOfSensors; i < _settings.T10_NumberOfSensors + 3; i++)
        {
            //var plot = formsPlot3.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: (i == _sett.T10_NumberOfSensors ? "Max" : (i == (_sett.T10_NumberOfSensors + 1) ? "Average" : "Min")));
            var plot = plotStats.Plot.AddSignal(_plotData[i], sampleRate: _settings.T10_Frequency, label: _seriesLabels[i]);
            plot.MinRenderIndex = 0;
            plot.MaxRenderIndex = 0;
        }
        plotStats.Plot.SetAxisLimits(xMin: 0, xMax: _settings.Plot_WindowPoints, yMin: 0, yMax: 1000);

        // Binding for Plot Ratio
        for (int i = _settings.T10_NumberOfSensors + 3; i < _settings.T10_NumberOfSensors + _settings.ArrayFixedColumns; i++)
        {
            //var plot = formsPlot4.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: (i == (_sett.T10_NumberOfSensors + 3) ? "Min/Average" : (i == (_sett.T10_NumberOfSensors + 4) ? "Min/Max" : "Average/Max")));
            var plot = plotRatio.Plot.AddSignal(_plotData[i], sampleRate: _settings.T10_Frequency, label: _seriesLabels[i]);
            plot.MinRenderIndex = 0;
            plot.MaxRenderIndex = 0;
        }
        plotRatio.Plot.SetAxisLimits(xMin: 0, xMax: _settings.Plot_WindowPoints, yMin: 0, yMax: 1);
    }

    /// <summary>
    /// Fetchs data into the plots. This calls <see cref="Plots_Clear"/>, <see cref="Plots_DataBinding"/>, <see cref="Plots_ShowLegends"/>, <see cref="Plots_ShowFull"/>, and <see cref="Plots_Refresh"/>.
    /// </summary>
    /// <param name="resetPlotPoints"><see langword="True"/> if <see cref="_nPoints"/> is set equal to <see cref="_settings.Plot_ArrayPoints"/>, <see langword="False">false</see> otherwise </param>
    /// <param name="showAllData">True to fit data into the plots and call <see cref="Plots_ShowFull"/>, false otherwise</param>
    private void Plots_FetchData(bool resetPlotPoints = true, bool showAllData = true)
    {
        int points = resetPlotPoints ? _settings.Plot_ArrayPoints : _nPoints;
        Plots_Clear();  // This sets _nPoints = 0, so we need to reset it now
        _nPoints = points;

        Plots_DataBinding();    // Bind the arrays to the plots
        Plots_ShowLegends();    // Show the legends in the picture boxes
        
        if (showAllData)
            Plots_ShowFull();   // Show all data (fit data)
        
        Plots_Refresh();
    }

    /// <summary>
    /// Refreshes all plot controls
    /// </summary>
    private void Plots_Refresh()
    {
        plotData.Refresh(skipIfCurrentlyRendering: true);
        plotDistribution.Refresh(skipIfCurrentlyRendering: true);
        plotStats.Refresh(skipIfCurrentlyRendering: true);
        plotRatio.Refresh(skipIfCurrentlyRendering: true);
    }

    /// <summary>
    /// Shows plots's full range data
    /// </summary>
    private void Plots_ShowFull()
    {
        if (_settings.Plot_ShowRawData)
        {
            foreach (var plot in plotData.Plot.GetPlottables())
                ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = (_nPoints > 0) ? (_nPoints - 1) : 0;
            plotData.Plot.AxisAuto();
            plotData.Plot.SetAxisLimits(xMin: 0, yMin: 0);
        }

        plotDistribution.Refresh();

        if (_settings.Plot_ShowAverage)
        {
            foreach (var plot in plotStats.Plot.GetPlottables())
                ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = (_nPoints > 0) ? (_nPoints - 1) : 0;
            plotStats.Plot.AxisAuto();
            plotStats.Plot.SetAxisLimits(xMin: 0, yMin: 0);
        }

        if (_settings.Plot_ShowRatios)
        {
            foreach (var plot in plotRatio.Plot.GetPlottables())
                ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = (_nPoints > 0) ? (_nPoints - 1) : 0;
            plotRatio.Plot.AxisAuto();
            plotRatio.Plot.SetAxisLimits(xMin: 0, yMin: 0, yMax: 1);
        }
    }

    /// <summary>
    /// Show plots legends in picture boxes
    /// </summary>
    private void Plots_ShowLegends()
    {
        bool showLegA = _settings.Plot_DistIsRadar;
        bool showLegB = !_settings.Plot_DistIsRadar || _settings.T10_NumberOfSensors >= 2;
        Bitmap legendA;
        Bitmap legendB;
        Bitmap bitmap;
        Size szLegend = new();

        // Combine legends from Plot1 and Plot2 and draw a black border around each legend
        legendA = showLegA ? plotData.Plot.RenderLegend() : new(1,1);
        legendB = showLegB ? plotDistribution.Plot.RenderLegend() : new(1, 1);
        szLegend.Width = Math.Max(showLegA ? legendA.Width : 0, showLegB ? legendB.Width : 0);
        szLegend.Width += 2;    // black border drawing
        szLegend.Height = (showLegA ? legendA.Height + 2 : 0);
        szLegend.Height += (showLegB ? legendB.Height + 2 : 0);
        szLegend.Height += (showLegA && showLegB ? _settings.PxBetweenLegends : 0);
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
                                (showLegA ? legendA.Height + _settings.PxBetweenLegends + 1 : 0),
                                legendB.Width + 1,
                                legendB.Height + 1);
            GraphicsA.DrawImage(legendB, (bitmap.Width - legendB.Width - 2) / 2 + 1, (showLegA ? legendA.Height + _settings.PxBetweenLegends + 2 : 1));
        }
        pictureBox1.Image = bitmap;
        if (bitmap.Width > pictureBox1.Width || bitmap.Height > pictureBox1.Height)
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        else
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;

        // Combine legends from Plot3 and Plot4 and draw a black border around each legend
        legendA = plotStats.Plot.RenderLegend();
        legendB = plotRatio.Plot.RenderLegend();
        bitmap = new Bitmap(Math.Max(legendA.Width, legendB.Width) + 2, legendA.Height + legendB.Height + _settings.PxBetweenLegends + 4);
        using Graphics GraphicsB = Graphics.FromImage(bitmap);
        GraphicsB.DrawRectangle(new Pen(Color.Black),
                                (bitmap.Width - legendA.Width - 2) / 2,
                                0,
                                legendA.Width + 1,
                                legendA.Height + 1);
        GraphicsB.DrawImage(legendA, (bitmap.Width - legendA.Width - 2) / 2 + 1, 1);
        GraphicsB.DrawRectangle(new Pen(Color.Black),
                                (bitmap.Width - legendB.Width - 2) / 2,
                                legendA.Height + _settings.PxBetweenLegends + 1,
                                legendB.Width + 1,
                                legendB.Height + 2);
        GraphicsB.DrawImage(legendB, (bitmap.Width - legendB.Width - 2) / 2 + 1, legendA.Height + _settings.PxBetweenLegends + 2);
        pictureBox2.Image = bitmap;
        if (bitmap.Width > pictureBox2.Width || bitmap.Height > pictureBox2.Height)
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        else
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
    }

    /// <summary>
    /// Updates plots with new values
    /// </summary>
    /// <param name="sensor">Sensor number</param>
    /// <param name="value">New illuminance value</param>
    private void Plots_Update(int sensor, double value)
    {
        // Resize arrays if necessary
        int factor = (_nPoints + 10) / _settings.Plot_ArrayPoints;
        factor *= _settings.Plot_ArrayPoints;
        if (factor > _plotData[0].Length - 1)
        {
            //System.Diagnostics.Debug.WriteLine($"Current _plotData[i] points: {_nPoints}, array size: {_plotData[0].Length}, new size: {_settings.Plot_ArrayPoints + factor}");
            _settings.Plot_ArrayPoints += factor;
            for (int i = 0; i < _plotData.Length; i++)
            {
                Array.Resize<double>(ref _plotData[i], _settings.Plot_ArrayPoints);
            }
            
            // https://github.com/ScottPlot/ScottPlot/discussions/1042
            // https://swharden.com/scottplot/faq/version-4.1/
            // Update array reference in the plots. The Update() method doesn't allow a bigger array, so we need to modify Ys property
            int j = 0;
            foreach (var plot in plotData.Plot.GetPlottables())
            {
                ((ScottPlot.Plottable.SignalPlot)plot).Ys = _plotData[j];
                j++;
            }
            foreach (var plot in plotStats.Plot.GetPlottables())
            {
                ((ScottPlot.Plottable.SignalPlot)plot).Ys = _plotData[j];
                j++;
            }
            foreach (var plot in plotRatio.Plot.GetPlottables())
            {
                ((ScottPlot.Plottable.SignalPlot)plot).Ys = _plotData[j];
                j++;
            }
        }

        // Data computation
        _plotData[sensor][_nPoints] = value;
        _plotRadar[1, sensor] = value;
        _plotRadialGauge[sensor] = value;

        _max = sensor == 0 ? value : (value > _max ? value : _max);
        _min = sensor == 0 ? value : (value < _min ? value : _min);
        _average += value;

        // Only render when the last sensor value is received
        if (sensor == _settings.T10_NumberOfSensors - 1)
        {
            // Compute data
            _average /= _settings.T10_NumberOfSensors;
            _plotData[_settings.T10_NumberOfSensors][_nPoints] = _max;
            _plotData[_settings.T10_NumberOfSensors + 1][_nPoints] = _average;
            _plotData[_settings.T10_NumberOfSensors + 2][_nPoints] = _min;
            _plotData[_settings.T10_NumberOfSensors + 3][_nPoints] = _average > 0 ? _min / _average : 0;
            _plotData[_settings.T10_NumberOfSensors + 4][_nPoints] = _max > 0 ? _min / _max : 0;
            _plotData[_settings.T10_NumberOfSensors + 5][_nPoints] = _min > 0 ? _average / _max : 0;

            // Adjust the plots's axis
            plotData.Plot.AxisAutoY();
            plotStats.Plot.AxisAutoY();
            plotData.Plot.SetAxisLimits(yMin: 0);
            plotStats.Plot.SetAxisLimits(yMin: 0);
            if (_nPoints / _settings.T10_Frequency >= plotData.Plot.GetAxisLimits().XMax)
            {
                int xMin = (int)((_nPoints / _settings.T10_Frequency) - _settings.Plot_WindowPoints * 1 / 5);
                int xMax = xMin + _settings.Plot_WindowPoints;
                plotData.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                plotStats.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                plotRatio.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                //formsPlot3.Plot.SetAxisLimits(xMin: (_nPoints - _sett.Plot_WindowPoints) / _sett.T10_Frequency, xMax: (_nPoints + _sett.Plot_WindowPoints) / _sett.T10_Frequency);
                //formsPlot4.Plot.SetAxisLimits(xMin: (_nPoints - _sett.Plot_WindowPoints) / _sett.T10_Frequency, xMax: (_nPoints + _sett.Plot_WindowPoints) / _sett.T10_Frequency);
            }

            // Update first plot
            if (_settings.Plot_ShowRawData)
            {
                foreach (var plot in plotData.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints;
                    //((ScottPlot.PlottableSignal)plot).minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                    //sigPlot.maxRenderIndex = _dataN;
                    //sigPlot.minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                }
            }

            // Update radar plot
            if (_settings.Plot_ShowDistribution)
            {
                // We always store the RadarPlot data in case the user wants to use it later
                for (int i = 0; i < _settings.T10_NumberOfSensors; i++)
                    _plotRadar[0, i] = _average;

                // Update the distribution plot
                if (_settings.Plot_DistIsRadar)
                {
                    ((ScottPlot.Plottable.RadarPlot)plotDistribution.Plot.GetPlottables()[0]).Update(_plotRadar, false);
                }
                else
                {
                    var plot = (ScottPlot.Plottable.RadialGaugePlot)plotDistribution.Plot.GetPlottables()[0];
                    var maxAngle = _average > 0 ? 180 * _plotRadialGauge.Max() / _average : 0.0;
                    plot.MaximumAngle = maxAngle > 360 ? 360.0 : maxAngle;
                    plot.Update(_plotRadialGauge);
                    //plot.MaximumAngle = 180 * _plotRadialGauge.Max() / _average;
                    //if (plot.MaximumAngle > 360) plot.MaximumAngle = 360.0;
                }
            }

            // Update max, average, and min plot
            if (_settings.Plot_ShowAverage)
            {
                foreach (var plot in plotStats.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints;
                }
            }

            // Update ratios plot
            if (_settings.Plot_ShowRatios)
            {
                foreach (var plot in plotRatio.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints;
                }
            }

            Plots_Refresh();

            // Modify internal numeric variables
            ++_nPoints;
            _max = 0.0;
            _min = 0.0;
            _average = 0.0;
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
        if (_reading) return;

        //var MyPlot = ((ScottPlot.FormsPlot)sender);
        if (sender?.GetType() != typeof(ScottPlot.FormsPlotCrossHair)) return;

        var MyPlot = (ScottPlot.FormsPlotCrossHair)sender;

        // Set the data to be shown on the distribution plot (Radar or RadialGauge)
        for (int i = 0; i < _settings.T10_NumberOfSensors; i++)
        {
            _plotRadialGauge[i] = _plotData[i][e.PointIndex];
            _plotRadar[1, i] = _plotData[i][e.PointIndex];
            _plotRadar[0, i] = _plotData[_settings.T10_NumberOfSensors + 1][e.PointIndex];
        }

        // Update the distribution plot
        if (_settings.Plot_DistIsRadar)
        {
            ((ScottPlot.Plottable.RadarPlot)plotDistribution.Plot.GetPlottables()[0]).Update(_plotRadar, false);
        }
        else
        {
            var plot = ((ScottPlot.Plottable.RadialGaugePlot)plotDistribution.Plot.GetPlottables()[0]);
            plot.Update(_plotRadialGauge);
            plot.MaximumAngle = 180 * _plotRadialGauge.Max() / _plotRadialGauge.Average();
            if (plot.MaximumAngle > 360) plot.MaximumAngle = 360.0;
        }
        plotDistribution.Refresh(skipIfCurrentlyRendering: true);

        // Update the rest of the plots accordingly
        if (MyPlot.Name == "plotData")
        {
            plotData.Refresh(skipIfCurrentlyRendering: true);
            if (plotStats.ShowCrossHair)
            {
                if (plotStats.VerticalLine is not null) plotStats.VerticalLine.X = e.PointX;
                if (plotStats.HorizontalLine is not null) plotStats.HorizontalLine.Y = _plotData[_settings.T10_NumberOfSensors][e.PointIndex];
                plotStats.Refresh(skipIfCurrentlyRendering: true);
            }

            if (plotRatio.ShowCrossHair)
            {
                if (plotRatio.VerticalLine is not null) plotRatio.VerticalLine.X = e.PointX;
                if (plotRatio.HorizontalLine is not null) plotRatio.HorizontalLine.Y = _plotData[_settings.T10_NumberOfSensors + 3][e.PointIndex];
                plotRatio.Refresh(skipIfCurrentlyRendering: true);
            }
        }
        else if (MyPlot.Name == "plotStats")
        {
            plotStats.Refresh(skipIfCurrentlyRendering: true);
            if (plotData.ShowCrossHair)
            {
                if (plotData.VerticalLine is not null) plotData.VerticalLine.X = e.PointX;
                if (plotData.HorizontalLine is not null) plotData.HorizontalLine.Y = _plotData[_settings.T10_NumberOfSensors-1][e.PointIndex];
                plotData.Refresh(skipIfCurrentlyRendering: true);
            }
            if (plotRatio.ShowCrossHair)
            {
                if (plotRatio.VerticalLine is not null) plotRatio.VerticalLine.X = e.PointX;
                if (plotRatio.HorizontalLine is not null) plotRatio.HorizontalLine.Y = _plotData[_settings.T10_NumberOfSensors + 3][e.PointIndex];
                plotRatio.Refresh(skipIfCurrentlyRendering: true);
            }
        }
        else if (MyPlot.Name == "plotRatio")
        {
            plotRatio.Refresh(skipIfCurrentlyRendering: true);
            if (plotData.ShowCrossHair)
            {
                if (plotData.VerticalLine is not null) plotData.VerticalLine.X = e.PointX;
                if (plotData.HorizontalLine is not null) plotData.HorizontalLine.Y = _plotData[_settings.T10_NumberOfSensors - 1][e.PointIndex];
                plotData.Refresh(skipIfCurrentlyRendering: true);
            }
            if (plotStats.ShowCrossHair)
            {
                if (plotStats.VerticalLine is not null) plotStats.VerticalLine.X = e.PointX;
                if (plotStats.HorizontalLine is not null) plotStats.HorizontalLine.Y = _plotData[_settings.T10_NumberOfSensors][e.PointIndex];
                plotStats.Refresh(skipIfCurrentlyRendering: true);
            }
        }
    }
}
