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

            Color[] fills = colors.Select(x => Color.FromArgb(50, x)).ToArray();

            ScottPlot.Plottable.RadialGaugePlot plottable = new(values, colors, fills, false, new double []{ values.Max() * 4 / 3 });
            formsPlot1.Plot.Add(plottable);
            formsPlot1.Plot.Frameless();
            formsPlot1.Plot.Grid(enable: false);
            //formsPlot1.Plot.XAxis2.Label("Radial gauge plot");
            formsPlot1.Plot.Title("Radial gauge plot");

        }
    }
}
