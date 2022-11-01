using System.Linq;

namespace ScottPlot;

public class FormsPlotCrossHair : ScottPlot.FormsPlotCulture
{
    private System.Windows.Forms.ToolStripMenuItem crossHairMenuItem = new();

    /// <summary>
    /// Event fired whenever the vertical line is dragged.
    /// </summary>
    public event EventHandler<LineDragEventArgs>? VLineDragged;
    /// <summary>
    /// Event fired whenever the horizontal line is dragged.
    /// </summary>
    public event EventHandler<LineDragEventArgs>? HLineDragged;

    public ScottPlot.Plottable.VLine? VerticalLine { get; private set; }
    public ScottPlot.Plottable.HLine? HorizontalLine { get; private set; }

    private readonly System.Resources.ResourceManager StringsRM;

    public bool ShowCrossHair
    {
        get => this.crossHairMenuItem.Checked;
        set
        {
            if (value)
            {
                this.ShowCrossHairLines(true, true);
                this.crossHairMenuItem.Checked = value;
            }
            else
            {
                this.DeleteCrossHairLines();
                this.crossHairMenuItem.Checked = value;
            }
        }
    }
    public bool ShowCrossHairVertical { get; set; } = false;
    public bool ShowCrossHairHorizontal { get; set; } = false;

    public bool SnapToPoint { get; set; } = false;

    /// <summary>
    /// Sets color for horizontal and vertical lines and their position label backgrounds
    /// </summary>
    public System.Drawing.Color CrossHairColor
    {
        set
        {
            if (this.VerticalLine is not null)
            {
                this.VerticalLine.Color = value;
                this.VerticalLine.PositionLabelBackground = System.Drawing.Color.FromArgb(200, value);
            }
            if (this.HorizontalLine is not null)
            {
                this.HorizontalLine.Color = value;
                this.HorizontalLine.PositionLabelBackground = System.Drawing.Color.FromArgb(200, value);
            }
        }
        get => this.VerticalLine?.Color ?? System.Drawing.Color.Red;
    }

    public FormsPlotCrossHair()
        : base()
    {
        DoubleClick += this.OnDoubleClick;
        this.StringsRM = new("ScottPlot.FormsPlotCrossHair", typeof(FormsPlotCrossHair).Assembly);
    }

    public FormsPlotCrossHair(System.Globalization.CultureInfo? culture = null)
        : this()
    {
        this.CultureUI = culture ?? System.Globalization.CultureInfo.CurrentCulture;
        //this.Refresh();
    }

    protected override void InitilizeContextMenu()
    {
        base.InitilizeContextMenu();

        int item = this.ContextMenu.Items.Add(new ToolStripSeparator());

        item = this.ContextMenu.Items.Add(new ToolStripMenuItem("Show crosshair", null, new EventHandler(this.RightClickMenu_CrossHair)));
        this.crossHairMenuItem = (ToolStripMenuItem)this.ContextMenu.Items[item];
        this.crossHairMenuItem.Name = "CrossHair";
    }

    /// <summary>
    /// Add vertical and horizontal plottable lines.
    /// Subscribe to the line's dragged events.
    /// </summary>
    private void CreateCrossHairLines()
    {
        this.VerticalLine = this.Plot.AddVerticalLine(0.0, style: ScottPlot.LineStyle.Dash);
        this.VerticalLine.IsVisible = false;
        this.VerticalLine.PositionLabel = true;
        this.VerticalLine.DragEnabled = true;
        this.VerticalLine.Dragged += new System.EventHandler(this.OnDraggedVertical);

        this.HorizontalLine = this.Plot.AddHorizontalLine(0.0, color: System.Drawing.Color.Red, width: 1, style: ScottPlot.LineStyle.Dash);
        this.HorizontalLine.IsVisible = false;
        this.HorizontalLine.PositionLabel = true;
        this.HorizontalLine.DragEnabled = true;
        this.HorizontalLine.Dragged += new System.EventHandler(this.OnDraggedHorizontal);

        this.CrossHairColor = System.Drawing.Color.FromArgb(200, System.Drawing.Color.Red);
    }

    private void ShowCrossHairLines(bool showVertical = false, bool showHorizontal = false)
    {
        if (!showVertical && !showHorizontal) return;

        if (this.Plot.GetPlottables().Where(x => x is Plottable.VLine || x is Plottable.HLine).Any()) return;

        // There should be at last one plottable added, otherwise
        if (this.Plot.GetPlottables().Length >= 1)
            this.CreateCrossHairLines();

        if (showVertical && this.VerticalLine is not null)
        {
            this.VerticalLine.IsVisible = true;
            var axis = this.Plot.GetPlottables()[0].GetAxisLimits();
            this.VerticalLine.X = axis.XCenter;
            //SnapLinesToPoint(ToX: true);
        }

        if (showHorizontal && this.HorizontalLine is not null)
        {
            this.HorizontalLine.IsVisible = true;
            var axis = this.Plot.GetPlottables()[0].GetAxisLimits();
            this.HorizontalLine.Y = axis.YCenter;
            //SnapLinesToPoint(ToY: true);
        }
    }

    /// <summary>
    /// Delete vertical and horizontal plottable lines.
    /// Unsubscribe to the line's dragged events.
    /// </summary>
    private void DeleteCrossHairLines()
    {
        if (this.VerticalLine is not null)
        {
            this.VerticalLine.Dragged -= new System.EventHandler(this.OnDraggedVertical);
            this.VerticalLine = null;
            this.Plot.Clear(typeof(Plottable.VLine));
        }

        if (this.HorizontalLine is not null)
        {
            this.HorizontalLine.Dragged -= new System.EventHandler(this.OnDraggedHorizontal);
            this.HorizontalLine = null;
            this.Plot.Clear(typeof(Plottable.HLine));
        }
    }

    /// <summary>
    /// Move the vertical and horizontal lines to the nearest point.
    /// </summary>
    /// <param name="ToX"><see langword="True"/> if the lines are moved according to the nearest X point.</param>
    /// <param name="ToY"><see langword="True"/> if the lines are moved according to the nearest Y point.</param>
    /// <returns>The closest X/Y coordinate as well as the array index of the closest point.</returns>
    private (double? pointX, double? pointY, int? pointIndex) SnapLinesToPoint(bool ToX = false, bool ToY = false)
    {
        double? pointX = null;
        double? pointY = null;
        int? pointIndex = null;

        var plot = this.Plot.GetPlottables().First();
        var plotType = (this.Plot.GetPlottables().First()).GetType();
        System.Reflection.MethodInfo? plotMethod = null;
        if (ToX)
            plotMethod = plotType.GetMethod("GetPointNearestX");
        else if (ToY)
            plotMethod = plotType.GetMethod("GetPointNearestY");

        if (plotMethod is null || this.VerticalLine is null || this.HorizontalLine is null) return (null, null, null);

        if (this.VerticalLine.IsVisible || this.HorizontalLine.IsVisible)
        {
            (double mouseCoordX, double mouseCoordY) = this.GetMouseCoordinates();
            var param = new object[1];
            if (ToX)
                param[0] = mouseCoordX;
            else if (ToY)
                param[0] = mouseCoordY;

            var result = plotMethod.Invoke(plot, param);
            if (result is not null)
            {
                (pointX, pointY, pointIndex) = ((double, double, int))result;
                this.VerticalLine.X = pointX.Value;
                this.HorizontalLine.Y = pointY.Value;
            }
        }
        return (pointX, pointY, pointIndex);
    }

    private void OnDoubleClick(object? sender, EventArgs e)
    {
        this.ShowCrossHair = !this.ShowCrossHair;
    }

    private void OnDraggedVertical(object? sender, EventArgs e)
    {
        // If we are reading from the sensor, then exit
        if (this.VerticalLine is null || !this.VerticalLine.IsVisible || !this.SnapToPoint) return;

        var (pointX, pointY, pointIndex) = this.SnapLinesToPoint(ToX: true);

        // Raise the custom event for the subscribers
        this.OnVLineDragged(new LineDragEventArgs(pointX, pointY, pointIndex));
        //EventHandler<VLineDragEventArgs> handler = VLineDragged;
        //handler?.Invoke(this, new VLineDragEventArgs(pointX, pointY, pointIndex));

    }

    private void OnDraggedHorizontal(object? sender, EventArgs e)
    {
        // If we are reading from the sensor, then exit
        if (this.HorizontalLine is null || !this.HorizontalLine.IsVisible || !this.SnapToPoint) return;

        var (pointX, pointY, pointIndex) = this.SnapLinesToPoint(ToY: true);

        // Raise the custom event for the subscribers
        this.OnHLineDragged(new LineDragEventArgs(pointX, pointY, pointIndex));
        //EventHandler<VLineDragEventArgs> handler = VLineDragged;
        //handler?.Invoke(this, new VLineDragEventArgs(pointX, pointY, pointIndex));

    }

    /// <summary>
    /// Gets the plottables that represent data. Therefore, no VLine nor other auxiliar plottables are returned
    /// </summary>
    /// <returns></returns>
    public Plottable.IPlottable[] GetDataCurves()
    {
        //System.Collections.ObjectModel.ObservableCollection<ScottPlot.Plottable.IPlottable> plots = new();
        var dataPlots = this.Plot.GetPlottables().Where(x => x is not Plottable.VLine && x is not Plottable.HLine);

        //foreach (var plot in this.Plot.GetPlottables())
        //{
        //    if (plot.GetType() != typeof(Plottable.VLine))
        //    {
        //        plots.Add(plot);
        //    }
        //}

        return dataPlots.ToArray();
    }

    // Wrap event invocations inside a protected virtual method to allow derived classes to override the event invocation behavior
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-publish-events-that-conform-to-net-framework-guidelines
    protected virtual void OnVLineDragged(LineDragEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately after the null check and before the event is raised.
        EventHandler<LineDragEventArgs>? raiseEvent = VLineDragged;

        // Event will be null if there are no subscribers
        if (raiseEvent is not null)
        {
            // Call to raise the event.
            raiseEvent(this, e);
        }
    }

    protected virtual void OnHLineDragged(LineDragEventArgs e)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately after the null check and before the event is raised.
        EventHandler<LineDragEventArgs>? raiseEvent = HLineDragged;

        // Event will be null if there are no subscribers
        if (raiseEvent is not null)
        {
            // Call to raise the event.
            raiseEvent(this, e);
        }
    }

    /// <summary>
    /// Override Clear method.
    /// </summary>
    public void Clear()
    {
        Plottable.IPlottable[] plottables = this.Plot.GetPlottables();
        for (int i = plottables.Length - 1; i >= 0; i--)
        {
            //if (plottables[i] is not Plottable.VLine && plottables[i] is not Plottable.HLine)
            this.Plot.RemoveAt(i);
        }
    }


    /// <summary>
    /// Launch the default right-click menu.
    /// </summary>
    protected override void CustomRightClickEvent(object? sender, EventArgs e)
    {
        //detachLegendMenuItem.Visible = Plot.Legend(null).Count > 0;
        this.crossHairMenuItem.Enabled = this.Plot.GetPlottables().Length > 0;
        //customMenu.Show(System.Windows.Forms.Cursor.Position);
        base.CustomRightClickEvent(sender, e);
    }

    private void RightClickMenu_CrossHair(object? sender, EventArgs e)
    {
        if (sender is not null)
        {
            this.ShowCrossHair = !this.ShowCrossHair;
            this.Refresh();
        }
    }

}

public class LineDragEventArgs : EventArgs
{
    public LineDragEventArgs(double? X, double? Y, int? Index)
    {
        this.PointX = X ?? default;
        this.PointY = Y ?? default;
        this.PointIndex = Index ?? default;
    }

    public double PointX { get; set; }
    public double PointY { get; set; }
    public int PointIndex { get; set; }

}