using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottPlot
{
    public class FormsPlotCrossHair : ScottPlot.FormsPlot
    {
        public event EventHandler<VLineDragEventArgs> VLineDragged;
        private ScottPlot.Plottable.Crosshair crossHair;
        private ScottPlot.Plottable.VLine vLine;
        private ScottPlot.Plottable.HLine hLine;
        public ScottPlot.Plottable.VLine GetVerticalLine => crossHair.VerticalLine;
        public ScottPlot.Plottable.HLine GetHorizontalLine => crossHair.HorizontalLine;

        private bool _verticalLine;
        public bool VerticalLine
        {
            get => _verticalLine;
            set
            {
                _verticalLine = value;
                //vLine.IsVisible = value;
            }
        }

        private bool _horizontalLine;
        public bool HorizontalLine
        {
            get => _horizontalLine;
            set
            {
                _horizontalLine = value;
                //hLine.IsVisible = value;
            }
        }

        public FormsPlotCrossHair()
            :base()
        {
            crossHair = this.Plot.AddCrosshair(0.0, 0.0);
            crossHair.IsVisible = true;
            
            //vLine = this.Plot.AddVerticalLine(0.0, color: System.Drawing.Color.Red, width: 3, style: ScottPlot.LineStyle.Dash);
            //VerticalLine = true;
            ////vLine.IsVisible = false;
            //vLine.PositionLabel = true;
            //vLine.DragEnabled = true;
            //vLine.Dragged += OnDraggedVertical;
            crossHair.VerticalLine.DragEnabled = true;
            crossHair.VerticalLine.Dragged += OnDraggedVertical;

            //hLine = this.Plot.AddHorizontalLine(0.0, color: System.Drawing.Color.Red, width: 3, style: ScottPlot.LineStyle.Dash);
            //HorizontalLine = true;
            //hLine.DragEnabled = true;
            crossHair.HorizontalLine.DragEnabled = true;
            crossHair.HorizontalLine.Dragged += OnDraggedHorizontal;
            crossHair.HorizontalLine.Dragged += new EventHandler(OnDraggedHorizontal);
            //hLine.Dragged += OnDraggedHorizontal;

            //this.Click += OnClick;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(OnClick);


        }

        private void OnClick(object sender, EventArgs e)
        {
            crossHair.IsVisible = !crossHair.IsVisible;
        }

        private void OnDraggedVertical(object sender, EventArgs e)
        {
            // If we are reading from the sensor, then exit
            if (!_verticalLine) return;

            (double mouseCoordX, double mouseCoordY) = this.GetMouseCoordinates();
            //double xyRatio = formsPlot1.Plot.XAxis.Dims.PxPerUnit / formsPlot1.Plot.YAxis.Dims.PxPerUnit;
            (double pointX, double pointY, int pointIndex) = ((ScottPlot.Plottable.ScatterPlot)(this.Plot.GetPlottables()[1])).GetPointNearestX(mouseCoordX);
            crossHair.VerticalLine.X = pointX;
            crossHair.HorizontalLine.Y = pointY;
            
            // Raise the custom event for the subscribers
            OnVLineDragged(new VLineDragEventArgs(pointX, pointY, pointIndex));
            //EventHandler<VLineDragEventArgs> handler = VLineDragged;
            //handler?.Invoke(this, new VLineDragEventArgs(pointX, pointY, pointIndex));

        }

        private void OnDraggedHorizontal(object sender, EventArgs e)
        {
            // If we are reading from the sensor, then exit
            if (!HorizontalLine) return;

            (double mouseCoordX, double mouseCoordY) = this.GetMouseCoordinates();
            //double xyRatio = formsPlot1.Plot.XAxis.Dims.PxPerUnit / formsPlot1.Plot.YAxis.Dims.PxPerUnit;
            (double pointX, double pointY, int pointIndex) = ((ScottPlot.Plottable.ScatterPlot)(this.Plot.GetPlottables()[1])).GetPointNearestY(mouseCoordY);

            crossHair.VerticalLine.X = pointX;
            crossHair.HorizontalLine.Y = pointY;

            // Raise the custom event for the subscribers
            OnVLineDragged(new VLineDragEventArgs(pointX, pointY, pointIndex));
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
            var algo = this.Plot.GetPlottables().Where(x => !(x is Plottable.VLine));

            //foreach (var plot in this.Plot.GetPlottables())
            //{
            //    if (plot.GetType() != typeof(Plottable.VLine))
            //    {
            //        plots.Add(plot);
            //    }
            //}

            return algo.ToArray();
        }

        // Wrap event invocations inside a protected virtual method to allow derived classes to override the event invocation behavior
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-publish-events-that-conform-to-net-framework-guidelines
        protected virtual void OnVLineDragged(VLineDragEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<VLineDragEventArgs> raiseEvent = VLineDragged;

            // Event will be null if there are no subscribers
            if (raiseEvent != null)
            {
                // Format arguments if needed
                //e.pointX = 0.0;
                //e.pointY = 0.0;
                //e.pointIndex = 0;

                // Call to raise the event.
                raiseEvent(this, e);
            }
        }


        public void ClearPlottablesWithData()
        {
            for (int i = this.Plot.GetPlottables().Length - 1; i > 0; i--)
            {
                this.Plot.RemoveAt(i);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormsPlotCrossHair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.Name = "FormsPlotCrossHair";
            this.ResumeLayout(false);

        }

    }

    public class VLineDragEventArgs : EventArgs
    {
        public VLineDragEventArgs(double X, double Y, int Index)
        {
            pointX = X;
            pointY = Y;
            pointIndex = Index;
        }

        public double pointX { get; set; }
        public double pointY { get; set; }
        public int pointIndex { get; set; }

    }

}
