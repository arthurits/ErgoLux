using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ScottPlot.Drawing;

// Lighten or darken color
// https://stackoverflow.com/questions/801406/c-create-a-lighter-darker-color-based-on-a-system-color
// https://www.pvladov.com/2012/09/make-color-lighter-or-darker.html
// https://gist.github.com/zihotki/09fc41d52981fb6f93a81ebf20b35cd5

// Circular Segment
// https://github.com/falahati/CircularProgressBar/blob/master/CircularProgressBar/CircularProgressBar.cs
// https://github.com/aalitor/AltoControls/blob/on-development/AltoControls/Controls/Circular%20Progress%20Bar.cs

// http://csharphelper.com/blog/2015/02/draw-lines-with-custom-end-caps-in-c/

// Text on path
// http://csharphelper.com/blog/2018/02/draw-text-on-a-circle-in-c/
// http://csharphelper.com/blog/2016/01/draw-text-on-a-curve-in-c/

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
        private double[] Norm;

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
        /// Gets or sets the size (in pixels) of each gauge. If <0, then its will be calculated from the available space.
        /// </summary>
        public float LineWidth = -1;

        /// <summary>
        /// True if the gauges are to be drawn anti-clockwise (conter clockwise). False otherwise.
        /// </summary>
        public bool AntiClockWise = false;

        /// <summary>
        /// Angle (in degrees) at which the gauges start: -90 for North, 0 for East, 90 for South, 180 for West, and so on
        /// </summary>
        public float StartingAngle = -90f;


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

        public RadialGaugePlot(double[] values, Color[] lineColors, Color[] fillColors, bool independentAxes, double [] maxValues = null)
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
        public void Update(double[] values, bool independentAxes = false, double [] maxValues = null)
        {
            IndependentAxes = independentAxes;
            Norm = new double[values.GetLength(0)];
            Array.Copy(values, 0, Norm, 0, values.Length);

            if (IndependentAxes)
                NormMax = NormalizeInPlace(Norm, maxValues);
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
        private double NormalizeInPlace(double[] input, double[] maxValues = null)
        {
            double max;
            if (maxValues != null && maxValues.Length == 1)
            {
                max = maxValues[0];
            }
            else
            {
                max = input[0];
                for (int i = 0; i < input.GetLength(0); i++)
                    max = Math.Max(max, input[i]);
            }

            for (int i = 0; i < input.GetLength(0); i++)
                    input[i] /= max;

            return max;
        }

        /// <summary>
        /// Normalize each row of a 2D array independently by dividing all values by the maximum value.
        /// </summary>
        /// <returns>maximum value in each row of the array before normalization</returns>
        //private double[] NormalizeSeveralInPlace(double[] input, double[] maxValues = null)
        //{
        //    double[] maxes;
        //    if (maxValues != null && input.GetLength(1) == maxValues.Length)
        //    {
        //        maxes = maxValues;
        //    }
        //    else
        //    {
        //        maxes = new double[input.GetLength(1)];
        //        for (int i = 0; i < input.GetLength(1); i++)
        //        {
        //            double max = input[0, i];
        //            for (int j = 0; j < input.GetLength(0); j++)
        //            {
        //                max = Math.Max(input[j, i], max);
        //            }
        //            maxes[i] = max;
        //        }
        //    }

        //    for (int i = 0; i < input.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < input.GetLength(1); j++)
        //        {
        //            input[i, j] /= maxes[j];
        //        }
        //    }

        //    return maxes;
        //}

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
            //int numCategories = Norm.GetUpperBound(1) + 1;
            //double sweepAngle = 2 * Math.PI / numCategories;
            float sweepAngle = 0;
            double minScale = new double[] { dims.PxPerUnitX, dims.PxPerUnitX }.Min();
            PointF origin = new PointF(dims.GetPixelX(0), dims.GetPixelY(0));
            //double[] radii = new double[] { 0.25 * minScale, 0.5 * minScale, 1 * minScale };
            //double[] radii = new double[numCategories];

            //for (int i = 0; i < numGroups; i++)
            //{
            //    radii[i] = minScale * (i + 1) / numGroups;
            //}

            using Graphics gfx = GDI.Graphics(bmp, dims, lowQuality);
            using Pen pen = GDI.Pen(WebColor);
            using Pen penCircle = GDI.Pen(WebColor);
            //using Brush brush = GDI.Brush(Color.Black);
            using StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center };
            using StringFormat sf2 = new StringFormat();
            using System.Drawing.Font font = GDI.Font(Font);
            using Brush fontBrush = GDI.Brush(Font.Color);


            lock (this)
            {
                float lineWidth = (LineWidth < 0) ? (float)(minScale / ((numGroups) * 2)) : LineWidth;
                float radiusSpace = lineWidth * 2;
                float entryRadius = 0;
                float maxAngle = (float)Norm.Max() * 360f;

                pen.Width = (float)lineWidth;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Triangle;
                penCircle.Width = (float)lineWidth;
                penCircle.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                penCircle.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                for (int i = 0; i < numGroups; i++)
                {
                    sweepAngle = AntiClockWise ? -(float)(360f * Norm[i]) : (float)(360f * Norm[i]);
                    entryRadius = (i + 1) * radiusSpace;

                    pen.Color = LineColors[i];
                    penCircle.Color = LightenBy(LineColors[i], 90);

                    gfx.DrawArc(penCircle, (origin.X - entryRadius), (origin.Y - entryRadius), (entryRadius * 2), (entryRadius * 2), StartingAngle, maxAngle);
                    gfx.DrawArc(pen, (origin.X - entryRadius), (origin.Y - entryRadius), (entryRadius * 2), (entryRadius * 2), StartingAngle, sweepAngle);

                    if (ShowAxisValues)
                    {
                        DrawTextOnCircle(gfx,
                            font,
                            fontBrush,
                            new RectangleF(dims.DataOffsetX, dims.DataOffsetY, dims.DataWidth, dims.DataHeight),
                            entryRadius,
                            StartingAngle,
                            origin.X,
                            origin.Y,
                            Norm[i].ToString("#.00"));
                    }

                }

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

        /// <summary>Creates color with corrected brightness.</summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>Corrected <see cref="Color"/> structure.</returns>
        /// <seealso cref="https://gist.github.com/zihotki/09fc41d52981fb6f93a81ebf20b35cd5"/>
        private Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        private Color LightenBy(Color color, float percent)
        {
            return ChangeColorBrightness(color, percent / 100f);
        }

        private Color DarkenBy(Color color, float percent)
        {
            return ChangeColorBrightness(color, -1f * percent / 100f);
        }

        /// <summary>
        /// Draw text centered on the top and bottom of the circle.
        /// </summary>
        /// <param name="gfx"><see langword="keyword">Graphic</see> object used to draw</param>
        /// <param name="font"><see langword="keyword">Font</see> used to draw the text</param>
        /// <param name="brush"><see langword="keyword">Brush</see> used to draw the text</param>
        /// <param name="clientRectangle"><see langword="keyword">Rectangle</see> of the ScottPlot control</param>
        /// <param name="radius">Radius of the circle in pixels</param>
        /// <param name="cx">The x-coordinate of the circle centre</param>
        /// <param name="cy">The y-coordinate of the circle centre</param>
        /// <param name="text">String to be drawn</param>
        /// <seealso cref="http://csharphelper.com/blog/2018/02/draw-text-on-a-circle-in-c/"/>
        private void DrawTextOnCircle(Graphics gfx, System.Drawing.Font font,
            Brush brush, RectangleF clientRectangle, float radius, float anglePos, float cx, float cy,
            string text)
        {
            // Use a StringFormat to draw the middle
            // top of each character at (0, 0).
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Center;

                // Used to scale from radians to degrees.
                double radians_to_degrees = 180.0 / Math.PI;

                // **********************
                // * Draw the top text. *
                // **********************
                // Measure the characters.
                List<RectangleF> rects =
                    MeasureCharacters(gfx, font, clientRectangle, text);

                // Use LINQ to add up the character widths.
                var width_query = from RectangleF rect in rects
                                  select rect.Width;
                float text_width = width_query.Sum();

                // Find the starting angle.
                double width_to_angle = 1 / radius;
                double start_angle = -Math.PI / 2 -
                    text_width / 2 * width_to_angle;
                double theta = start_angle + (anglePos * Math.PI / 180);

                // Draw the characters.
                for (int i = 0; i < text.Length; i++)
                {
                    // See where this character goes.
                    if (anglePos > 180)
                        theta += rects[i].Width / 2 * width_to_angle;
                    else
                        theta -= rects[i].Width / 2 * width_to_angle;
                    double x = cx + radius * Math.Cos(theta);
                    double y = cy + radius * Math.Sin(theta);

                    // Transform to position the character.
                    gfx.RotateTransform((float)(radians_to_degrees *
                        (theta + Math.PI / 2)));
                    gfx.TranslateTransform((float)x, (float)y,
                        System.Drawing.Drawing2D.MatrixOrder.Append);

                    // Draw the character.
                    gfx.DrawString(text[i].ToString(), font, brush,
                        0, 0, string_format);
                    gfx.ResetTransform();

                    // Increment theta.
                    theta += rects[i].Width / 2 * width_to_angle;
                }

                
            }
        }

        /// <summary>
        /// Measure the characters in a string with no more than 32 characters.
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="font"></param>
        /// <param name="clientRectangle"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<RectangleF> MeasureCharactersInWord(
            Graphics gfx, System.Drawing.Font font, RectangleF clientRectangle, string text)
        {
            List<RectangleF> result = new List<RectangleF>();

            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Near;
                string_format.LineAlignment = StringAlignment.Near;
                string_format.Trimming = StringTrimming.None;
                string_format.FormatFlags =
                    StringFormatFlags.MeasureTrailingSpaces;

                CharacterRange[] ranges = new CharacterRange[text.Length];
                for (int i = 0; i < text.Length; i++)
                {
                    ranges[i] = new CharacterRange(i, 1);
                }
                string_format.SetMeasurableCharacterRanges(ranges);

                // Find the character ranges.
                RectangleF rect = new RectangleF(0, 0, 10000, 100);
                Region[] regions =
                    gfx.MeasureCharacterRanges(
                        text, font, clientRectangle,
                        string_format);

                // Convert the regions into rectangles.
                foreach (Region region in regions)
                    result.Add(region.GetBounds(gfx));
            }

            return result;
        }

        /// <summary>
        /// Measure the characters in the string.
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="font"></param>
        /// <param name="clientRectangle"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<RectangleF> MeasureCharacters(Graphics gfx,
            System.Drawing.Font font, RectangleF clientRectangle, string text)
        {
            List<RectangleF> results = new List<RectangleF>();

            // The X location for the next character.
            float x = 0;

            // Get the character sizes 31 characters at a time.
            for (int start = 0; start < text.Length; start += 32)
            {
                // Get the substring.
                int len = 32;
                if (start + len >= text.Length) len = text.Length - start;
                string substring = text.Substring(start, len);

                // Measure the characters.
                List<RectangleF> rects =
                    MeasureCharactersInWord(gfx, font, clientRectangle, substring);

                // Remove lead-in for the first character.
                if (start == 0) x += rects[0].Left;

                // Save all but the last rectangle.
                for (int i = 0; i < rects.Count + 1 - 1; i++)
                {
                    RectangleF new_rect = new RectangleF(
                        x, rects[i].Top,
                        rects[i].Width, rects[i].Height);
                    results.Add(new_rect);

                    // Move to the next character's X position.
                    x += rects[i].Width;
                }
            }

            // Return the results.
            return results;
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
        public ScottPlot.Plottable.RadialGaugePlot AddRadialGauge(double[] values, bool independentAxes = false, double[] maxValues = null, bool disableFrameAndGrid = true)
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
