namespace ErgoLux
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            formsPlot1.Plot.Palette = ScottPlot.Palette.Microcharts;
            double[] values = { 100, 80, 65, 45, 20 };
            var plt = formsPlot1.Plot.AddRadialGauge(values);
            plt.Labels = new string[] { "alpha", "beta", "gamma", "delta", "epsilon" };
            plt.SpaceFraction = 0.1;
            formsPlot1.Plot.Title("Test title");
            formsPlot1.Plot.Legend(true);
            formsPlot1.Plot.XAxis2.Hide(false);
            formsPlot1.Plot.XAxis2.Ticks(false);
            formsPlot1.Plot.XAxis.Hide(false);
            formsPlot1.Plot.XAxis.Ticks(false);
            formsPlot1.Plot.YAxis2.Hide(false);
            formsPlot1.Plot.YAxis2.Ticks(false);
            formsPlot1.Plot.YAxis.Hide(false);
            formsPlot1.Plot.YAxis.Ticks(false);
            formsPlot1.Render();

            pictureBox1.Image = formsPlot1.Plot.RenderLegend();

            var plt2 = formsPlot2.Plot.AddRadar(new double[2, 5] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 } });
            formsPlot2.Plot.Title("Radar title");
            //formsPlot2.Plot.XAxis2.Hide(false);
            //formsPlot2.Plot.XAxis2.Ticks(false);
            //formsPlot2.Plot.XAxis.Hide(false);
            //formsPlot2.Plot.XAxis.Ticks(false);
            //formsPlot2.Plot.YAxis2.Hide(false);
            //formsPlot2.Plot.YAxis2.Ticks(false);
            //formsPlot2.Plot.YAxis.Hide(false);
            //formsPlot2.Plot.YAxis.Ticks(false);
            formsPlot2.Render();
        }
    }
}
