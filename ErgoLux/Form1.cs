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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            formsPlot1.Plot.Palette = ScottPlot.Drawing.Palette.Microcharts;
            double[] values = { 100, 80, 65, 45, 20 };
            var plt = formsPlot1.Plot.AddRadialGauge(values);
            plt.Labels = new string[] { "alpha", "beta", "gamma", "delta", "epsilon" };
            plt.SpaceFraction = 0.1;
            formsPlot1.Plot.Legend(true);
            formsPlot1.Render();

            pictureBox1.Image = formsPlot1.Plot.RenderLegend();
        }
    }
}
