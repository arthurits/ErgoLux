namespace ErgoLux;

public partial class Form2 : Form
{
    public Form2()
    {
        InitializeComponent();

        //double[] dataX = new double[] { 1, 2, 3, 4, 5 };
        //double[] dataY = new double[] { 1, 4, 9, 16, 25 };
        //formsPlot1.Plot.AddScatter(dataX, dataY);
        //formsPlot1.Refresh();

        formsPlot1.Plot.AddSignal(ScottPlot.DataGen.Sin(51));
        formsPlot1.Plot.AddSignal(ScottPlot.DataGen.Cos(51));
        var ch = formsPlot1.Plot.AddCrosshair(42, 0.48);
        ch.VerticalLine.DragEnabled = true;
        ch.HorizontalLine.DragEnabled = true;

        formsPlot1.Plot.Title("Crosshair Demo");
        formsPlot1.Plot.XLabel("Horizontal Axis");
        formsPlot1.Plot.YLabel("Vertical Axis");

        formsPlot1.Refresh();
    }

}

