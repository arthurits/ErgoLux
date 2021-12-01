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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            double[] dataX = new double[] { 1, 2, 3, 4, 5 };
            double[] dataY = new double[] { 1, 4, 9, 16, 25 };
            formsPlot1.Plot.AddScatter(dataX, dataY);
            formsPlot1.Refresh();
        }

    }
}
