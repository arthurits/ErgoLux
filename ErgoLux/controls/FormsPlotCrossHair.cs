﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottPlot
{
    class FormsPlotCrossHair : ScottPlot.FormsPlot
    {
        public event EventHandler<VLineDragEventArgs> VLineDragged;
        private ScottPlot.Plottable.VLine vLine;
        public ScottPlot.Plottable.VLine GetVLine { get; }

        //private bool ShowVLine;
        public bool ShowVLine
        {
            get => ShowVLine;
            set
            {
                ShowVLine = value;
                vLine.IsVisible = value;
            }
        }
        
        public FormsPlotCrossHair()
            :base()
        {
            vLine = this.Plot.AddVerticalLine(0.0, color: System.Drawing.Color.Red, width: 3, style: ScottPlot.LineStyle.Dash);
            ShowVLine = false;
            //vLine.IsVisible = false;
            vLine.DragEnabled = true;
            vLine.Dragged += OnDragged;
        }

        

        private void OnDragged(object sender, EventArgs e)
        {
            // If we are reading from the sensor, then exit
            if (!ShowVLine) return;

            (double mouseCoordX, double mouseCoordY) = this.GetMouseCoordinates();
            //double xyRatio = formsPlot1.Plot.XAxis.Dims.PxPerUnit / formsPlot1.Plot.YAxis.Dims.PxPerUnit;
            (double pointX, double pointY, int pointIndex) = ((ScottPlot.Plottable.SignalPlot)(this.Plot.GetPlottables()[1])).GetPointNearestX(mouseCoordX);

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
