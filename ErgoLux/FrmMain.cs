using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FTD2XX_NET;

namespace ErgoLux
{
    public partial class FrmMain : Form
    {
        private System.Timers.Timer m_timer;
        //private ClassT10 cT10;
        private FTDISample myFtdiDevice;
        private double[][] _plotData;
        private int[] _settings = new int[]
        {
            0,                                  // Location ID
            1,                                  // Number of sensors
            9600,                               // Baud rate
            FTDI.FT_DATA_BITS.FT_BITS_7,        // Data bits
            FTDI.FT_STOP_BITS.FT_STOP_BITS_1,   // Stop bits
            FTDI.FT_PARITY.FT_PARITY_EVEN,      // Parity
            FTDI.FT_FLOW_CONTROL.FT_FLOW_NONE,  // Flow
            11,                                 // On char
            13,                                 // Off char
            2                                   // Frequency (Herz)
        };
        private int _dataN = 0;
        Random rand = new Random(0);
        ScottPlot.PlottableSignal sigPlot;

        private const int ArraySize = 7200;
        private const int PlotRangeX = 20;

        public FrmMain()
        {
            InitializeComponent();
            //cT10 = new ClassT10();

            m_timer = new System.Timers.Timer() { Interval = 500, AutoReset = true };
            m_timer.Elapsed += OnTimedEvent;
            m_timer.Enabled = true;
            
            // plot the data array only once and we can updates its value later
            _plotData = new double[2][];
            _plotData[0] = new double[ArraySize];
            _plotData[1] = new double[ArraySize];
            sigPlot = formsPlot1.plt.PlotSignal(_plotData[0], sampleRate: 2);
            formsPlot1.plt.PlotSignal(_plotData[1], sampleRate: 2);
            //sigPlot = formsPlot1.plt.PlotSignal(_plotData, 2);
            formsPlot1.plt.AxisAutoX(margin: 0);
            formsPlot1.plt.Axis(x1: 0, x2: PlotRangeX, y1: 0);

            // customize styling
            formsPlot1.plt.Title("Illuminance");
            formsPlot1.plt.YLabel("Lux");
            formsPlot1.plt.XLabel("Time (seconds)");
            formsPlot1.plt.Grid(false);


            //var algo = new ClassT10("00541   ");
            //var strEncoded = algo.EncodeCommand();

            var strEncoded = ClassT10.EncodeCommand("00541   ");
            //string test = new string(new char[] { (char)2, '0', '0', '5', '4', '1', ' ', ' ', ' ', (char)3, '1', '3', (char)13, (char)10 });
            strEncoded = ClassT10.EncodeCommand("00110200");

            //System.Diagnostics.Debug.Print(strEncoded);
            //System.Diagnostics.Debug.Print(strEncoded.ToCharArray().ToString());
            //strEncoded = algo.EncodeCommand("01110200");
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // signal the native process (that launched us) to close the splash screen
            using (var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent"))
            {
                closeSplashEvent.Set();
            }
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            string readData;
            UInt32 numBytesRead = 0;

            //var result = false;
            //for (int i = 0; i < _settings[1]; i++)
            //{
            //    while (!result)
            //        result = myFtdiDevice.Write(ClassT10.ReceptorsSingle[i]);

            //    result = false;
            //}

            // Note that the Read method is overloaded, so can read string or byte array data
            //ftStatus = myFtdiDevice.Read(out readData, numBytesAvailable, ref numBytesRead);

            // add the condition checking here to validate that the readData in not empty.
            //OnReadingAvailable(readData);

            int factor = ArraySize * (_dataN + 10) / ArraySize;
            if (factor > _plotData[0].Length)
            {
                for (int i = 0; i < _plotData.Length; i++)
                    Array.Resize<double>(ref _plotData[i], factor * ArraySize);
                //Array.Copy(_plotData[0], 1, _plotData[0], 0, _plotData[0].Length - 1);
            }
            
            _plotData[0][_dataN] = rand.NextDouble();
            _plotData[1][_dataN] = rand.NextDouble();

            foreach (var plot in formsPlot1.plt.GetPlottables())
            {
                ((ScottPlot.PlottableSignal)plot).maxRenderIndex = _dataN;
                //((ScottPlot.PlottableSignal)plot).minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                //sigPlot.maxRenderIndex = _dataN;
                //sigPlot.minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
            }

            if (_dataN / 2 >= formsPlot1.plt.Axis()[1])
                formsPlot1.plt.Axis(x1: _dataN / 2 - 10, x2: _dataN / 2 + 10);
            ++_dataN;
            formsPlot1.Render(skipIfCurrentlyRendering: true);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            
            myFtdiDevice.DataReceived += OnDataReceived;
            if (myFtdiDevice.Write(ClassT10.Command54))
                m_timer.Start();

        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            m_timer.Stop();
            //myFtdiDevice.DataReceived -= OnDataReceived;

            //foreach (var plot in formsPlot1.plt.GetPlottables())
            //{
            //    ((ScottPlot.PlottableSignal)plot).minRenderIndex = 0;
            //}

            //sigPlot.minRenderIndex = 0;
            //formsPlot1.plt.Axis(x1: 0);
            formsPlot1.plt.AxisAuto(horizontalMargin: 0.05);
            formsPlot1.Render();
        }

        private void OnDataReceived (object sender, DataReceivedEventArgs e)
        {
            (int Sensor, double Iluminance, double Increment, double Percent) result = (0, 0, 0, 0);

            string str = System.Text.Encoding.UTF8.GetString(e.DataReceived, 0, e.DataReceived.Length);
            if (e.StrDataReceived.Length > 14)
            {
                result = ClassT10.DecodeCommand(e.StrDataReceived);
                System.Diagnostics.Debug.Print(result.ToString());
            }


            // Plot data
            _plotData[result.Sensor][_dataN] = result.Iluminance;

            sigPlot.maxRenderIndex = _dataN;
            ////sigPlot.minRenderIndex = _dataN > 20 ? _dataN - 20 : 0;
            if (result.Sensor == 0) ++_dataN;
            formsPlot1.plt.AxisAuto();
            formsPlot1.Render(skipIfCurrentlyRendering: true);

            // https://github.com/ScottPlot/ScottPlot/blob/096062f5dfde8fd5f1e2eb2e15e0e7ce9b17a54b/src/ScottPlot.Demo.WinForms/WinFormsDemos/LiveDataUpdate.cs#L14-L91
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the time it it's still running
            if (m_timer.Enabled) m_timer.Stop();
            
            m_timer.Dispose();
            
            // Close the device if it's still open
            if (myFtdiDevice!=null && myFtdiDevice.IsOpen)
                myFtdiDevice.Close();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            var frm = new FrmSettings(_settings);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                BtnConnect.Enabled = true;
                _settings = frm.GetData;

                myFtdiDevice = new FTDISample();
                myFtdiDevice.OpenDevice(location: (uint)_settings[0],
                    baud: _settings[2],
                    dataBits: _settings[3],
                    stopBits: _settings[4],
                    parity: _settings[5],
                    flowControl: _settings[6],
                    xOn: _settings[7],
                    xOff: _settings[8]);

                _plotData = new double[_settings[1]][];
                for (int i = 0; i < _settings[1]; i++)
                    _plotData[i] = new double[ArraySize];

                sigPlot = formsPlot1.plt.PlotSignal(_plotData[0], sampleRate: _settings[9]);

            }
        }
    }
}
