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

            double[,] values = {{78,  83, 84, 76, 43 },
                                { 100, 50, 70, 60, 90 }
                                };

            

            Color[] colors = Enumerable.Range(0, values.Length)
                                       .Select(i => formsPlot1.Plot.GetSettings(false).PlottablePalette.GetColor(i))   // modify later
                                       .ToArray();

            Color[] fills = colors.Select(x => Color.FromArgb(50, x)).ToArray();

            ScottPlot.Plottable.RadialGaugePlot plottable = new(values, colors, fills, false);
            formsPlot1.Plot.Add(plottable);
            //formsPlot1.Frameless();
            formsPlot1.Plot.Grid(enable: false);

        }
    }
}
