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
        private Timer m_timer;
        //private ClassT10 cT10;
        private FTDISample myFtdiDevice;
        private double[] _plotData = new double[7200];
        private int _dataN = 0;
        Random rand = new Random(0);
        ScottPlot.PlottableSignal sigPlot;

        public FrmMain()
        {
            InitializeComponent();
            //cT10 = new ClassT10();

            m_timer = new Timer { Interval = 500 };
            m_timer.Tick += timer_Tick;
            m_timer.Enabled = true;

            // plot the data array only once and we can updates its values later
            sigPlot = formsPlot1.plt.PlotSignal(_plotData, 2);
            formsPlot1.plt.AxisAutoX(margin: 0);
            formsPlot1.plt.Axis(y1: 0);

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
            //strEncoded = algo.EncodeCommand("02110200");
            //strEncoded = algo.EncodeCommand("03110200");
            //strEncoded = algo.EncodeCommand("04110200");
            //strEncoded = algo.EncodeCommand("05110200");
            //strEncoded = algo.EncodeCommand("06110200");
            //strEncoded = algo.EncodeCommand("07110200");
            //strEncoded = algo.EncodeCommand("08110200");
            //strEncoded = algo.EncodeCommand("09110200");
            //strEncoded = algo.EncodeCommand("10110200");
            //strEncoded = algo.EncodeCommand("11110200");
            //strEncoded = algo.EncodeCommand("12110200");
            //strEncoded = algo.EncodeCommand("13110200");
            //strEncoded = algo.EncodeCommand("14110200");
            //strEncoded = algo.EncodeCommand("15110200");
            //strEncoded = algo.EncodeCommand("16110200");
            //strEncoded = algo.EncodeCommand("17110200");
            //strEncoded = algo.EncodeCommand("18110200");
            //strEncoded = algo.EncodeCommand("19110200");
            //strEncoded = algo.EncodeCommand("20110200");
            //strEncoded = algo.EncodeCommand("21110200");
            //strEncoded = algo.EncodeCommand("22110200");
            //strEncoded = algo.EncodeCommand("23110200");
            //strEncoded = algo.EncodeCommand("24110200");
            //strEncoded = algo.EncodeCommand("25110200");
            //strEncoded = algo.EncodeCommand("26110200");
            //strEncoded = algo.EncodeCommand("27110200");
            //strEncoded = algo.EncodeCommand("28110200");
            //strEncoded = algo.EncodeCommand("29110200");
            //strEncoded = algo.EncodeCommand("30110200");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // signal the native process (that launched us) to close the splash screen
            using (var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent"))
            {
                closeSplashEvent.Set();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            string readData;
            UInt32 numBytesRead = 0;

            //var result = false;
            //for (int i = 0; i < NumSensors.Value; i++)
            //{
            //    while (!result)
            //        result = myFtdiDevice.Write(ClassT10.ReceptorsSingle[i]);

            //    result = false;
            //}

            // Note that the Read method is overloaded, so can read string or byte array data
            //ftStatus = myFtdiDevice.Read(out readData, numBytesAvailable, ref numBytesRead);

            // add the condition checking here to validate that the readData in not empty.
            //OnReadingAvailable(readData);


            _plotData[_dataN] = rand.NextDouble();
            
            sigPlot.maxRenderIndex = _dataN;
            //sigPlot.minRenderIndex = _dataN > 20 ? _dataN - 20 : 0;
            ++_dataN;
            formsPlot1.plt.AxisAuto();
            formsPlot1.Render(skipIfCurrentlyRendering: true);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            // FTDI connection code
            UInt32 ftdiDeviceCount = 0;
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

            // Create new instance of the FTDI device class
            FTDISample myFtdiDevice = new FTDISample();

            // Determine the number of FTDI devices connected to the machine
            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

            // Allocate storage for device info list
            FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

            // Populate our device list
            ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);

            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                for (UInt32 i = 0; i < ftdiDeviceCount; i++)
                {
                    richTextBox1.AppendText(System.Environment.NewLine + "Device index: " + i.ToString());

                    richTextBox1.AppendText(System.Environment.NewLine + "Flags: " + String.Format("{0:x}", ftdiDeviceList[i].Flags));
                    richTextBox1.AppendText(System.Environment.NewLine + "Type: " + ftdiDeviceList[i].Type.ToString());
                    richTextBox1.AppendText(System.Environment.NewLine + "ID: " + String.Format("{0:x}", ftdiDeviceList[i].ID));
                    richTextBox1.AppendText(System.Environment.NewLine + "Location ID: " + String.Format("{0:x}", ftdiDeviceList[i].LocId));
                    richTextBox1.AppendText(System.Environment.NewLine + "Serial number: " + ftdiDeviceList[i].SerialNumber.ToString());
                    richTextBox1.AppendText(System.Environment.NewLine + "Description: " + ftdiDeviceList[i].Description.ToString());
                    richTextBox1.AppendText(System.Environment.NewLine);
                }
            }


            //Konica Test = new Konica();
            //Test.StartMeasurement();
            //Test.CmdSend("00541   ");

            myFtdiDevice = new FTDISample();
            myFtdiDevice.OpenDevice(location: ftdiDeviceList[0].LocId);
            //myFtdiDevice.SetKonicaT10();
            myFtdiDevice.DataReceived += OnDataReceived;
            
            //System.Threading.Thread.Sleep(1000);

            //string strConn = Char.ConvertFromUtf32((Convert.ToInt32("02", 16))) +
            //    "00541   " +
            //    Char.ConvertFromUtf32((Convert.ToInt32("03", 16))) +
            //    Char.ConvertFromUtf32((Convert.ToInt32("10", 16))) +
            //    "\r\n";

            if (myFtdiDevice.Write(ClassT10.Command54.Value))
                m_timer.Start();

        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            m_timer.Stop();
        }

        private void OnDataReceived (object sender, DataReceivedEventArgs e)
        {
            string str = System.Text.Encoding.UTF8.GetString(e.DataReceived, 0, e.DataReceived.Length);
            if (e.StrDataReceived.Length > 14)
            {
                var result = ClassT10.DecodeCommand(e.StrDataReceived);
            }

            System.Diagnostics.Debug.Print("Data received");

            // https://github.com/ScottPlot/ScottPlot/blob/096062f5dfde8fd5f1e2eb2e15e0e7ce9b17a54b/src/ScottPlot.Demo.WinForms/WinFormsDemos/LiveDataUpdate.cs#L14-L91
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the time it it's still running
            if (m_timer.Enabled) m_timer.Stop();

            // Close the device if it's still open
            if (myFtdiDevice!=null && myFtdiDevice.IsOpen)
                myFtdiDevice.Close();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            var frm = new FrmSettings();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                BtnConnect.Enabled = true;
        }
    }
}
