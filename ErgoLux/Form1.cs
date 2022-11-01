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
            this.InitializeComponent();

            this.formsPlot1.Plot.Palette = ScottPlot.Palette.Microcharts;
            double[] values = { 100, 80, 65, 45, 20 };
            var plt = this.formsPlot1.Plot.AddRadialGauge(values);
            plt.Labels = new string[] { "alpha", "beta", "gamma", "delta", "epsilon" };
            plt.SpaceFraction = 0.1;
            this.formsPlot1.Plot.Title("Test title");
            this.formsPlot1.Plot.Legend(true);
            this.formsPlot1.Plot.XAxis2.Hide(false);
            this.formsPlot1.Plot.XAxis2.Ticks(false);
            this.formsPlot1.Plot.XAxis.Hide(false);
            this.formsPlot1.Plot.XAxis.Ticks(false);
            this.formsPlot1.Plot.YAxis2.Hide(false);
            this.formsPlot1.Plot.YAxis2.Ticks(false);
            this.formsPlot1.Plot.YAxis.Hide(false);
            this.formsPlot1.Plot.YAxis.Ticks(false);
            this.formsPlot1.Render();

            this.pictureBox1.Image = this.formsPlot1.Plot.RenderLegend();

            var plt2 = this.formsPlot2.Plot.AddRadar(new double[2, 5] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 } });
            this.formsPlot2.Plot.Title("Radar title");
            //formsPlot2.Plot.XAxis2.Hide(false);
            //formsPlot2.Plot.XAxis2.Ticks(false);
            //formsPlot2.Plot.XAxis.Hide(false);
            //formsPlot2.Plot.XAxis.Ticks(false);
            //formsPlot2.Plot.YAxis2.Hide(false);
            //formsPlot2.Plot.YAxis2.Ticks(false);
            //formsPlot2.Plot.YAxis.Hide(false);
            //formsPlot2.Plot.YAxis.Ticks(false);
            this.formsPlot2.Render();
        }
    }
}