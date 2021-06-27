using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErgoLux
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();

            double[] values = {78, 83, 84, 76, 43 };



            formsPlot1.Plot.Palette = ScottPlot.Drawing.Palette.Nord;

            Color[] colors = Enumerable.Range(0, values.Length)
                                       .Select(i => formsPlot1.Plot.GetSettings(false).PlottablePalette.GetColor(i))   // modify later
                                       .ToArray();
;
            //ScottPlot.Plottable.RadialGaugePlot plottable = new(values, colors, false, new double []{ values.Max() * 4 / 3 });
            ScottPlot.Plottable.RadialGaugePlot plottable = new(values, colors, false);
            plottable.CategoryLabels = new string [] { "C #1", "C #2", "C #3", "C #4", "C #5" };
            plottable.GroupLabels = new string[] { "G #1", "G #2", "G #3", "G #4", "G #5" };
            //plottable.StartingAngle = 150;
            //plottable.AntiClockWise = true;
            //plottable.GaugeMode = ScottPlot.RadialGaugeMode.SingleGauge;
            formsPlot1.Plot.Add(plottable);
            formsPlot1.Plot.Frameless();
            formsPlot1.Plot.Grid(enable: false);
            //formsPlot1.Plot.XAxis2.Label("Radial gauge plot");
            formsPlot1.Plot.Title("Radial gauge plot");
            formsPlot1.Plot.Legend(enable: true, ScottPlot.Alignment.LowerRight);

        }
    }
}
