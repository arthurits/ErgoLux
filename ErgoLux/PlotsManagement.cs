using System.Drawing;
using System.Linq;

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
        for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
        {
            var plot = plotData.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: _seriesLabels[i]);
            //formsPlot1.Refresh();
            plot.MinRenderIndex = 0;
            plot.MaxRenderIndex = 0;
        }
        plotData.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0);

        // Binding for Distribution plot
        if (_sett.Plot_DistIsRadar)
        {
            string[] labels = new string[_sett.T10_NumberOfSensors];
            for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                labels[i] = $"#{i:#0}";
            var plt = plotDistribution.Plot.AddRadar(_plotRadar, disableFrameAndGrid: false);
            plt.FillColors[0] = Color.FromArgb(100, plt.LineColors[0]);
            plt.FillColors[1] = Color.FromArgb(150, plt.LineColors[1]);
            //formsPlot2.Plot.Grid(enable: false);

            plt.AxisType = ScottPlot.RadarAxis.Polygon;
            plt.ShowAxisValues = false;
            plt.CategoryLabels = labels;
            plt.GroupLabels = new string[] { $"{(StringsRM.GetString("strFileHeader15", _sett.AppCulture) ?? "Average")}",
                                            $"{(StringsRM.GetString("strFileHeader16", _sett.AppCulture) ?? "Illuminance")}" };
        }
        else
        {
            var plt = plotDistribution.Plot.AddRadialGauge(_plotRadialGauge);
            var strLabels = new string[_sett.T10_NumberOfSensors];
            for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                strLabels[i] = _seriesLabels[i];
            plt.Labels = strLabels;
            plt.StartingAngle = 180;
        }
        InitializePlotDistribution();

        // Binding for Plot Average
        for (int i = _sett.T10_NumberOfSensors; i < _sett.T10_NumberOfSensors + 3; i++)
        {
            //var plot = formsPlot3.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: (i == _sett.T10_NumberOfSensors ? "Max" : (i == (_sett.T10_NumberOfSensors + 1) ? "Average" : "Min")));
            var plot = plotStats.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: _seriesLabels[i]);
            plot.MinRenderIndex = 0;
            plot.MaxRenderIndex = 0;
        }
        plotStats.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0);

        // Binding for Plot Ratio
        for (int i = _sett.T10_NumberOfSensors + 3; i < _sett.T10_NumberOfSensors + _sett.ArrayFixedColumns; i++)
        {
            //var plot = formsPlot4.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: (i == (_sett.T10_NumberOfSensors + 3) ? "Min/Average" : (i == (_sett.T10_NumberOfSensors + 4) ? "Min/Max" : "Average/Max")));
            var plot = plotRatio.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: _seriesLabels[i]);
            plot.MinRenderIndex = 0;
            plot.MaxRenderIndex = 0;
        }
        plotRatio.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0, yMax: 1);
    }

    /// <summary>
    /// Fetchs data into the plots
    /// </summary>
    private void Plots_FetchData()
    {
        Plots_Clear();  // This sets _nPoints = 0, so we need to reset it now
        _nPoints = _sett.Plot_ArrayPoints;

        Plots_DataBinding();    // Bind the arrays to the plots
        Plots_ShowLegends();    // Show the legends in the picture boxes
        Plots_ShowFull();       // Show all data (fit data)
    }

    /// <summary>
    /// Shows plots's full range data
    /// </summary>
    private void Plots_ShowFull()
    {
        if (_sett.Plot_ShowRawData)
        {
            foreach (var plot in plotData.Plot.GetPlottables())
                ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints - 1;
            plotData.Plot.AxisAuto();
            plotData.Plot.SetAxisLimits(xMin: 0, yMin: 0);
            plotData.Refresh();
        }

        plotDistribution.Refresh();

        if (_sett.Plot_ShowAverage)
        {
            foreach (var plot in plotStats.Plot.GetPlottables())
                ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints - 1;
            plotStats.Plot.AxisAuto();
            plotStats.Plot.SetAxisLimits(xMin: 0, yMin: 0);
            plotStats.Refresh();
        }

        if (_sett.Plot_ShowRatios)
        {
            foreach (var plot in plotRatio.Plot.GetPlottables())
                ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints - 1;
            plotRatio.Plot.AxisAuto();
            plotRatio.Plot.SetAxisLimits(xMin: 0, yMin: 0, yMax: 1);
            plotRatio.Refresh();
        }
    }

    /// <summary>
    /// Show plots legends in picture boxes
    /// </summary>
    private void Plots_ShowLegends()
    {
        bool showLegA = _sett.Plot_DistIsRadar;
        bool showLegB = !_sett.Plot_DistIsRadar || _sett.T10_NumberOfSensors >= 2;
        Bitmap legendA;
        Bitmap legendB;
        Bitmap bitmap;
        Size szLegend = new();

        // Combine legends from Plot1 and Plot2 and draw a black border around each legend
        legendA = showLegA ? plotData.Plot.RenderLegend() : null;
        legendB = showLegB ? plotDistribution.Plot.RenderLegend() : null;
        szLegend.Width = Math.Max(showLegA ? legendA.Width : 0, showLegB ? legendB.Width : 0);
        szLegend.Width += 2;    // black border drawing
        szLegend.Height = (showLegA ? legendA.Height + 2 : 0);
        szLegend.Height += (showLegB ? legendB.Height + 2 : 0);
        szLegend.Height += (showLegA && showLegB ? _sett.PxBetweenLegends : 0);
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
                                (showLegA ? legendA.Height + _sett.PxBetweenLegends + 1 : 0),
                                legendB.Width + 1,
                                legendB.Height + 1);
            GraphicsA.DrawImage(legendB, (bitmap.Width - legendB.Width - 2) / 2 + 1, (showLegA ? legendA.Height + _sett.PxBetweenLegends + 2 : 1));
        }
        pictureBox1.Image = bitmap;

        // Combine legends from Plot3 and Plot4 and draw a black border around each legend
        legendA = plotStats.Plot.RenderLegend();
        legendB = plotRatio.Plot.RenderLegend();
        bitmap = new Bitmap(Math.Max(legendA.Width, legendB.Width) + 2, legendA.Height + legendB.Height + _sett.PxBetweenLegends + 4);
        using Graphics GraphicsB = Graphics.FromImage(bitmap);
        GraphicsB.DrawRectangle(new Pen(Color.Black),
                                (bitmap.Width - legendA.Width - 2) / 2,
                                0,
                                legendA.Width + 1,
                                legendA.Height + 1);
        GraphicsB.DrawImage(legendA, (bitmap.Width - legendA.Width - 2) / 2 + 1, 1);
        GraphicsB.DrawRectangle(new Pen(Color.Black),
                                (bitmap.Width - legendB.Width - 2) / 2,
                                legendA.Height + _sett.PxBetweenLegends + 1,
                                legendB.Width + 1,
                                legendB.Height + 2);
        GraphicsB.DrawImage(legendB, (bitmap.Width - legendB.Width - 2) / 2 + 1, legendA.Height + _sett.PxBetweenLegends + 2);
        pictureBox2.Image = bitmap;
    }

    /// <summary>
    /// Updates plots with new values
    /// </summary>
    /// <param name="sensor">Sensor number</param>
    /// <param name="value">New illuminance value</param>
    private void Plots_Update(int sensor, double value)
    {
        // Resize arrays if necessary
        int factor = (_nPoints + 10) / _sett.Plot_ArrayPoints;
        factor *= _sett.Plot_ArrayPoints;
        if (factor > _plotData[0].Length - 1)
        {
            _sett.Plot_ArrayPoints += factor;
            for (int i = 0; i < _plotData.Length; i++)
            {
                Array.Resize<double>(ref _plotData[i], _sett.Plot_ArrayPoints);
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
        if (sensor == _sett.T10_NumberOfSensors - 1)
        {
            // Compute data
            _average /= _sett.T10_NumberOfSensors;
            _plotData[_sett.T10_NumberOfSensors][_nPoints] = _max;
            _plotData[_sett.T10_NumberOfSensors + 1][_nPoints] = _average;
            _plotData[_sett.T10_NumberOfSensors + 2][_nPoints] = _min;
            _plotData[_sett.T10_NumberOfSensors + 3][_nPoints] = _average > 0 ? _min / _average : 0;
            _plotData[_sett.T10_NumberOfSensors + 4][_nPoints] = _max > 0 ? _min / _max : 0;
            _plotData[_sett.T10_NumberOfSensors + 5][_nPoints] = _min > 0 ? _average / _max : 0;

            // Adjust the plots's axis
            plotData.Plot.AxisAutoY();
            plotStats.Plot.AxisAutoY();
            plotData.Plot.SetAxisLimits(yMin: 0);
            plotStats.Plot.SetAxisLimits(yMin: 0);
            if (_nPoints / _sett.T10_Frequency >= plotData.Plot.GetAxisLimits().XMax)
            {
                int xMin = (int)((_nPoints / _sett.T10_Frequency) - _sett.Plot_WindowPoints * 1 / 5);
                int xMax = xMin + _sett.Plot_WindowPoints;
                plotData.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                plotStats.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                plotRatio.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                //formsPlot3.Plot.SetAxisLimits(xMin: (_nPoints - _sett.Plot_WindowPoints) / _sett.T10_Frequency, xMax: (_nPoints + _sett.Plot_WindowPoints) / _sett.T10_Frequency);
                //formsPlot4.Plot.SetAxisLimits(xMin: (_nPoints - _sett.Plot_WindowPoints) / _sett.T10_Frequency, xMax: (_nPoints + _sett.Plot_WindowPoints) / _sett.T10_Frequency);
            }

            // Update first plot
            if (_sett.Plot_ShowRawData)
            {
                foreach (var plot in plotData.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints;
                    //((ScottPlot.PlottableSignal)plot).minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                    //sigPlot.maxRenderIndex = _dataN;
                    //sigPlot.minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                }
                plotData.Refresh(skipIfCurrentlyRendering: true);
            }

            // Update radar plot
            if (_sett.Plot_ShowDistribution)
            {
                // We always store the RadarPlot data in case the user wants to use it later
                for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                    _plotRadar[0, i] = _average;

                // Update the distribution plot
                if (_sett.Plot_DistIsRadar)
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
                plotDistribution.Refresh(skipIfCurrentlyRendering: true);
            }

            // Update max, average, and min plot
            if (_sett.Plot_ShowAverage)
            {
                foreach (var plot in plotStats.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints;
                }
                plotStats.Refresh(skipIfCurrentlyRendering: true);
            }

            // Update ratios plot
            if (_sett.Plot_ShowRatios)
            {
                foreach (var plot in plotRatio.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints;
                }
                plotRatio.Refresh(skipIfCurrentlyRendering: true);
            }

            // Modify internal numeric variables
            ++_nPoints;
            _max = 0.0;
            _min = 0.0;
            _average = 0.0;
        }

        // https://github.com/ScottPlot/ScottPlot/blob/096062f5dfde8fd5f1e2eb2e15e0e7ce9b17a54b/src/ScottPlot.Demo.WinForms/WinFormsDemos/LiveDataUpdate.cs#L14-L91
    }


    //private void formsPlot_MouseDown(object sender, MouseEventArgs e)
    //{

    //    // If we are reading from the sensor, then exit
    //    if (_reading) return;

    //    if (e.Button != MouseButtons.Left) return;

    //    var MyPlot = ((ScottPlot.FormsPlot)sender);
    //    if (MyPlot.Name != "formsPlot1") return;
    //    if (MyPlot.Plot.GetPlottables().Length == 0) return;
    //    (double mouseCoordX, double mouseCoordY) = MyPlot.GetMouseCoordinates();
    //    (double pointX, double pointY, int pointIndex) = ((ScottPlot.Plottable.SignalPlot)(MyPlot.Plot.GetPlottables()[0])).GetPointNearestX(mouseCoordX);
    //    if (MyPlot.Plot.GetPlottables().Length == _sett.T10_NumberOfSensors)
    //    {
    //        var VLine = MyPlot.Plot.AddVerticalLine(pointX, color: Color.Red, width: 3, style: ScottPlot.LineStyle.Dash);
    //        VLine.DragEnabled = true;
    //    }
    //    else if (MyPlot.Plot.GetPlottables().Length == _sett.T10_NumberOfSensors + 1)
    //    {
    //        ((ScottPlot.Plottable.VLine)MyPlot.Plot.GetPlottables().Last()).X = pointX;
    //    }

    //    // Some information:
    //    // https://swharden.com/scottplot/faq/mouse-position/#highlight-the-data-point-near-the-cursor
    //    // https://github.com/ScottPlot/ScottPlot/discussions/862
    //    // https://github.com/ScottPlot/ScottPlot/discussions/645
    //    // https://github.com/ScottPlot/ScottPlot/issues/1090
    //    // frmWRmodel.cs chart_MouseClicked

    //}

    private void formsPlot_PlottableDragged(object sender, ScottPlot.LineDragEventArgs e)
    {
        // If we are reading from the sensor, then exit
        if (_reading) return;

        //var MyPlot = ((ScottPlot.FormsPlot)sender);
        if (sender.GetType() != typeof(ScottPlot.FormsPlotCrossHair)) return;

        var MyPlot = sender as ScottPlot.FormsPlotCrossHair;

        //(double mouseCoordX, double mouseCoordY) = formsPlot1.GetMouseCoordinates();
        ////double xyRatio = formsPlot1.Plot.XAxis.Dims.PxPerUnit / formsPlot1.Plot.YAxis.Dims.PxPerUnit;
        //(double pointX, double pointY, int pointIndex) = ((ScottPlot.Plottable.SignalPlot)(formsPlot1.Plot.GetPlottables()[0])).GetPointNearestX(MyPlot.X);
        ////Text = $"Point index {pointIndex} at ({pointX}, {pointY})";


        for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
        {
            _plotRadialGauge[i] = _plotData[i][e.PointIndex];
            _plotRadar[1, i] = _plotData[i][e.PointIndex];
            _plotRadar[0, i] = _plotData[_sett.T10_NumberOfSensors + 1][e.PointIndex];
        }

        // Update the distribution plot
        if (_sett.Plot_DistIsRadar)
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

        if (MyPlot.Name == "plotData")
        {
            plotData.Refresh(skipIfCurrentlyRendering: true);
            if (plotStats.ShowCrossHair)
            {
                plotStats.VerticalLine.X = e.PointX;
                plotStats.Refresh(skipIfCurrentlyRendering: true);
            }

            if (plotRatio.ShowCrossHair)
            {
                plotRatio.VerticalLine.X = e.PointX;
                plotRatio.Refresh(skipIfCurrentlyRendering: true);
            }
        }
        else if (MyPlot.Name == "plotStats")
        {
            plotStats.Refresh(skipIfCurrentlyRendering: true);
            if (plotData.ShowCrossHair)
            {
                plotData.VerticalLine.X = e.PointX;
                plotData.Refresh(skipIfCurrentlyRendering: true);
            }
            if (plotRatio.ShowCrossHair)
            {
                plotRatio.VerticalLine.X = e.PointX;
                plotRatio.Refresh(skipIfCurrentlyRendering: true);
            }
        }
        else if (MyPlot.Name == "plotRatio")
        {
            plotRatio.Refresh(skipIfCurrentlyRendering: true);
            if (plotData.ShowCrossHair)
            {
                plotData.VerticalLine.X = e.PointX;
                plotData.Refresh(skipIfCurrentlyRendering: true);
            }
            if (plotStats.ShowCrossHair)
            {
                plotStats.VerticalLine.X = e.PointX;
                plotStats.Refresh(skipIfCurrentlyRendering: true);
            }
        }
    }
}

