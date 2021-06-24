using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ScottPlot.Drawing;


namespace ScottPlot.Plottable
{
    /// <summary>
    /// A radar chart is a graphical method of displaying multivariate data in the form of 
    /// a two-dimensional chart of three or more quantitative variables represented on axes 
    /// starting from the same point.
    /// 
    /// Data is managed using 2D arrays where groups (colored shapes) are rows and categories (arms of the web) are columns.
    /// </summary>
    public class RadialGaugePlot : IPlottable
    {
        /// <summary>
        /// Values for every group (rows) and category (columns) normalized from 0 to 1.
        /// </summary>
        private double[,] Norm;

        /// <summary>
        /// Single value to normalize all values against for all groups/categories.
        /// </summary>
        private double NormMax;

        /// <summary>
        /// Individual values (one per category) to use for normalization.
        /// Length must be equal to the number of columns (categories) in the original data.
        /// </summary>
        private double[] NormMaxes;

        /// <summary>
        /// Labels for each category.
        /// Length must be equal to the number of columns (categories) in the original data.
        /// </summary>
        public string[] CategoryLabels;

        /// <summary>
        /// Labels for each group.
        /// Length must be equal to the number of rows (groups) in the original data.
        /// </summary>
        public string[] GroupLabels;

        /// <summary>
        /// Colors (typically semi-transparent) to shade the inner area of each group.
        /// Length must be equal to the number of rows (groups) in the original data.
        /// </summary>
        public Color[] FillColors;

        /// <summary>
        /// Colors to outline the shape for each group.
        /// Length must be equal to the number of rows (groups) in the original data.
        /// </summary>
        public Color[] LineColors;

        /// <summary>
        /// Color of the axis lines and concentric circles representing ticks
        /// </summary>
        public Color WebColor = Color.Gray;

        /// <summary>
        /// Controls if values along each category axis are scaled independently or uniformly across all axes.
        /// </summary>
        public bool IndependentAxes;

        /// <summary>
        /// Font used for labeling values on the plot
        /// </summary>
        public Drawing.Font Font = new();

        /// <summary>
        /// If true, each value will be written in text on the plot.
        /// </summary>
        public bool ShowAxisValues { get; set; } = true;

        /// <summary>
        /// Controls rendering style of the concentric circles (ticks) of the web
        /// </summary>
        public RadarAxis AxisType { get; set; } = RadarAxis.Circle;

        public bool IsVisible { get; set; } = true;
        public int XAxisIndex { get; set; } = 0;
        public int YAxisIndex { get; set; } = 0;

        public RadialGaugePlot(double[,] values, Color[] lineColors, Color[] fillColors, bool independentAxes, double[] maxValues = null)
        {
            LineColors = lineColors;
            FillColors = fillColors;
            IndependentAxes = independentAxes;
            Update(values, independentAxes, maxValues);
        }

        public override string ToString() =>
            $"PlottableRadialGauge with {PointCount} points and {Norm.GetUpperBound(1) + 1} categories.";

        /// <summary>
        /// Replace the data values with new ones.
        /// </summary>
        /// <param name="values">2D array of groups (rows) of values for each category (columns)</param>
        /// <param name="independentAxes">Controls if values along each category axis are scaled independently or uniformly across all axes</param>
        /// <param name="maxValues">If provided, these values will be used to normalize each category (columns)</param>
        public void Update(double[,] values, bool independentAxes = false, double[] maxValues = null)
        {
            IndependentAxes = independentAxes;
            Norm = new double[values.GetLength(0), values.GetLength(1)];
            Array.Copy(values, 0, Norm, 0, values.Length);

            if (IndependentAxes)
                NormMaxes = NormalizeSeveralInPlace(Norm, maxValues);
            else
                NormMax = NormalizeInPlace(Norm, maxValues);
        }

        public void ValidateData(bool deep = false)
        {
            if (GroupLabels != null && GroupLabels.Length != Norm.GetLength(0))
                throw new InvalidOperationException("group names must match size of values");

            if (CategoryLabels != null && CategoryLabels.Length != Norm.GetLength(1))
                throw new InvalidOperationException("category names must match size of values");
        }

        /// <summary>
        /// Normalize a 2D array by dividing all values by the maximum value.
        /// </summary>
        /// <returns>maximum value in the array before normalization</returns>
        private double NormalizeInPlace(double[,] input, double[] maxValues = null)
        {
            double max;
            if (maxValues != null && maxValues.Length == 1)
            {
                max = maxValues[0];
            }
            else
            {
                max = input[0, 0];
                for (int i = 0; i < input.GetLength(0); i++)
                    for (int j = 0; j < input.GetLength(1); j++)
                        max = Math.Max(max, input[i, j]);
            }

            for (int i = 0; i < input.GetLength(0); i++)
                for (int j = 0; j < input.GetLength(1); j++)
                    input[i, j] /= max;

            return max;
        }

        /// <summary>
        /// Normalize each row of a 2D array independently by dividing all values by the maximum value.
        /// </summary>
        /// <returns>maximum value in each row of the array before normalization</returns>
        private double[] NormalizeSeveralInPlace(double[,] input, double[] maxValues = null)
        {
            double[] maxes;
            if (maxValues != null && input.GetLength(1) == maxValues.Length)
            {
                maxes = maxValues;
            }
            else
            {
                maxes = new double[input.GetLength(1)];
                for (int i = 0; i < input.GetLength(1); i++)
                {
                    double max = input[0, i];
                    for (int j = 0; j < input.GetLength(0); j++)
                    {
                        max = Math.Max(input[j, i], max);
                    }
                    maxes[i] = max;
                }
            }

            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    input[i, j] /= maxes[j];
                }
            }

            return maxes;
        }

        public LegendItem[] GetLegendItems()
        {
            if (GroupLabels is null)
                return null;

            List<LegendItem> legendItems = new List<LegendItem>();
            for (int i = 0; i < GroupLabels.Length; i++)
            {
                var item = new LegendItem()
                {
                    label = GroupLabels[i],
                    color = FillColors[i],
                    lineWidth = 10,
                    markerShape = MarkerShape.none
                };
                legendItems.Add(item);
            }

            return legendItems.ToArray();
        }

        public AxisLimits GetAxisLimits() =>
            (GroupLabels != null) ? new AxisLimits(-3.5, 3.5, -3.5, 3.5) : new AxisLimits(-2.5, 2.5, -2.5, 2.5);

        public int PointCount { get => Norm.Length; }

        /// <summary>
        /// This is where the drawing of the plot is done
        /// </summary>
        /// <param name="dims"></param>
        /// <param name="bmp"></param>
        /// <param name="lowQuality"></param>
        public virtual void Render(PlotDimensions dims, Bitmap bmp, bool lowQuality = false)
        {
            int numGroups = Norm.GetUpperBound(0) + 1;
            int numCategories = Norm.GetUpperBound(1) + 1;
            double sweepAngle = 2 * Math.PI / numCategories;
            double minScale = new double[] { dims.PxPerUnitX, dims.PxPerUnitX }.Min();
            PointF origin = new PointF(dims.GetPixelX(0), dims.GetPixelY(0));
            //double[] radii = new double[] { 0.25 * minScale, 0.5 * minScale, 1 * minScale };
            double[] radii = new double[numCategories];

            for (int i=0; i<numGroups; i++)
            {
                radii[i] = minScale * (i + 1) / numGroups;
            }

            using Graphics gfx = GDI.Graphics(bmp, dims, lowQuality);
            using Pen pen = GDI.Pen(WebColor);
            using Brush brush = GDI.Brush(Color.Black);
            using StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center };
            using StringFormat sf2 = new StringFormat();
            using System.Drawing.Font font = GDI.Font(Font);
            using Brush fontBrush = GDI.Brush(Font.Color);


            lock(this)
            {
                for (int i = 0; i < numGroups; i++)
                {
                    ((SolidBrush)brush).Color = FillColors[0];
                    pen.Color = LineColors[0];
                    gfx.DrawEllipse(pen, (int)(origin.X - radii[i]), (int)(origin.Y - radii[i]), (int)(radii[i] * 2), (int)(radii[i] * 2));
                    gfx.FillEllipse(brush, (int)(origin.X - radii[i]), (int)(origin.Y - radii[i]), (int)(radii[i] * 2), (int)(radii[i] * 2));
                }

                //gfx.TextRenderingHint = TextRenderingHint.AntiAlias;
                //gfx.SmoothingMode = SmoothingMode.AntiAlias;
                //var point = Point.Add(Point.Empty, new Size(2, 2));
                //var size = Size.add(Size, new Size(-2 * 2, -2 * 2) );

                //if (OuterWidth + OuterMargin < 0)
                //{
                //    var offset = Math.Abs(OuterWidth + OuterMargin);
                //    point = AddPoint(Point.Empty, offset);
                //    size = AddSize(Size, -2 * offset);
                //}

                //if (OuterColor != Color.Empty && OuterColor != Color.Transparent && OuterWidth != 0)
                //{
                //    g.FillEllipse(new SolidBrush(OuterColor), new RectangleF(point, size));

                //    if (OuterWidth >= 0)
                //    {
                //        point = AddPoint(point, OuterWidth);
                //        size = AddSize(size, -2 * OuterWidth);
                //        g.FillEllipse(_backBrush, new RectangleF(point, size));
                //    }
                //}

                //point = AddPoint(point, OuterMargin);
                //size = AddSize(size, -2 * OuterMargin);

                //gfx.FillPie(
                //    new SolidBrush(ProgressColor),
                //    ToRectangle(new RectangleF(point, size)),
                //    _animatedStartAngle ?? StartAngle,
                //    ((_animatedValue ?? Value) - Minimum) / (Maximum - Minimum) * 360);

                //if (ProgressWidth >= 0)
                //{
                //    point = AddPoint(point, ProgressWidth);
                //    size = AddSize(size, -2 * ProgressWidth);
                //    g.FillEllipse(_backBrush, new RectangleF(point, size));
                //}

                //point = AddPoint(point, InnerMargin);
                //size = AddSize(size, -2 * InnerMargin);

                //if (InnerColor != Color.Empty && InnerColor != Color.Transparent && InnerWidth != 0)
                //{
                //    g.FillEllipse(new SolidBrush(InnerColor), new RectangleF(point, size));

                //    if (InnerWidth >= 0)
                //    {
                //        point = AddPoint(point, InnerWidth);
                //        size = AddSize(size, -2 * InnerWidth);
                //        g.FillEllipse(_backBrush, new RectangleF(point, size));
                //    }
                //}
            }


            //for (int i = 0; i < radii.Length; i++)
            //{
            //    double hypotenuse = (radii[i] / radii[radii.Length - 1]);

            //    if (AxisType == RadarAxis.Circle)
            //    {
            //        gfx.DrawEllipse(pen, (int)(origin.X - radii[i]), (int)(origin.Y - radii[i]), (int)(radii[i] * 2), (int)(radii[i] * 2));
            //    }
            //    else if (AxisType == RadarAxis.Polygon)
            //    {
            //        PointF[] points = new PointF[numCategories];
            //        for (int j = 0; j < numCategories; j++)
            //        {
            //            float x = (float)(hypotenuse * Math.Cos(sweepAngle * j - Math.PI / 2) * minScale + origin.X);
            //            float y = (float)(hypotenuse * Math.Sin(sweepAngle * j - Math.PI / 2) * minScale + origin.Y);

            //            points[j] = new PointF(x, y);
            //        }
            //        gfx.DrawPolygon(pen, points);
            //    }
            //    if (ShowAxisValues)
            //    {
            //        if (IndependentAxes)
            //        {
            //            for (int j = 0; j < numCategories; j++)
            //            {
            //                float x = (float)(hypotenuse * Math.Cos(sweepAngle * j - Math.PI / 2) * minScale + origin.X);
            //                float y = (float)(hypotenuse * Math.Sin(sweepAngle * j - Math.PI / 2) * minScale + origin.Y);

            //                sf2.Alignment = x < origin.X ? StringAlignment.Far : StringAlignment.Near;
            //                sf2.LineAlignment = y < origin.Y ? StringAlignment.Far : StringAlignment.Near;

            //                double val = NormMaxes[j] * radii[i] / minScale;
            //                gfx.DrawString($"{val:f1}", font, fontBrush, x, y, sf2);
            //            }
            //        }
            //        else
            //        {
            //            double val = NormMax * radii[i] / minScale;
            //            gfx.DrawString($"{val:f1}", font, fontBrush, origin.X, (float)(-radii[i] + origin.Y), sf2);
            //        }
            //    }
            //}

            //for (int i = 0; i < numCategories; i++)
            //{
            //    PointF destination = new PointF((float)(1.1 * Math.Cos(sweepAngle * i - Math.PI / 2) * minScale + origin.X), (float)(1.1 * Math.Sin(sweepAngle * i - Math.PI / 2) * minScale + origin.Y));
            //    gfx.DrawLine(pen, origin, destination);

            //    if (CategoryLabels != null)
            //    {
            //        PointF textDestination = new PointF(
            //            (float)(1.3 * Math.Cos(sweepAngle * i - Math.PI / 2) * minScale + origin.X),
            //            (float)(1.3 * Math.Sin(sweepAngle * i - Math.PI / 2) * minScale + origin.Y));

            //        if (Math.Abs(textDestination.X - origin.X) < 0.1)
            //            sf.Alignment = StringAlignment.Center;
            //        else
            //            sf.Alignment = dims.GetCoordinateX(textDestination.X) < 0 ? StringAlignment.Far : StringAlignment.Near;
            //        gfx.DrawString(CategoryLabels[i], font, fontBrush, textDestination, sf);
            //    }
            //}

            //for (int i = 0; i < numGroups; i++)
            //{
            //    PointF[] points = new PointF[numCategories];
            //    for (int j = 0; j < numCategories; j++)
            //        points[j] = new PointF(
            //            (float)(Norm[i, j] * Math.Cos(sweepAngle * j - Math.PI / 2) * minScale + origin.X),
            //            (float)(Norm[i, j] * Math.Sin(sweepAngle * j - Math.PI / 2) * minScale + origin.Y));

            //    ((SolidBrush)brush).Color = FillColors[i];
            //    pen.Color = LineColors[i];
            //    gfx.FillPolygon(brush, points);
            //    gfx.DrawPolygon(pen, points);
            //}
        }
    }
}


namespace ScottPlot
{
    public partial class PlotExt: ScottPlot.Plot
    {
        /// <summary>
        /// Add a radar plot (a two-dimensional chart of three or more quantitative variables represented on axes starting from the same point)
        /// </summary>
        /// <param name="values">2D array containing categories (columns) and groups (rows)</param>
        /// <param name="independentAxes">if true, axis (category) values are scaled independently</param>
        /// <param name="maxValues">if provided, each category (column) is normalized to these values</param>
        /// <param name="disableFrameAndGrid">also make the plot frameless and disable its grid</param>
        /// <returns>the radar plot that was just created and added to the plot</returns>
        public ScottPlot.Plottable.RadialGaugePlot AddRadialGauge(double[,] values, bool independentAxes = false, double[] maxValues = null, bool disableFrameAndGrid = true)
        {

            Color[] colors = Enumerable.Range(0, values.Length)
                                       .Select(i => this.GetSettings(false).PlottablePalette.GetColor(i))   // modify later
                                       .ToArray();

            Color[] fills = colors.Select(x => Color.FromArgb(50, x)).ToArray();

            ScottPlot.Plottable.RadialGaugePlot plottable = new(values, colors, fills, independentAxes, maxValues);
            Add(plottable);

            if (disableFrameAndGrid)
            {
                Frameless();
                Grid(enable: false);
            }

            return plottable;
        }
    }
}