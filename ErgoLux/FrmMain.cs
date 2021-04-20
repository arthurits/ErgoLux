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
        private ClassT10 cT10;
        private FTDISample myFtdiDevice;

        public FrmMain()
        {
            InitializeComponent();
            cT10 = new ClassT10();

            m_timer = new Timer { Interval = 1000 };
            m_timer.Tick += timer_Tick;
            m_timer.Enabled = false;


            //var algo = new ClassT10("00541   ");
            //var strEncoded = algo.EncodeCommand();

            //strEncoded = algo.EncodeCommand("00541   ");
            //strEncoded = algo.EncodeCommand("00110200");
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

            var result = false;
            for (int i = 0; i < NumSensors.Value; i++)
            {
                while (!result)
                    result = myFtdiDevice.Write(cT10.ReceptorsSingle[i]);
                
                result = false;
            }

            // Note that the Read method is overloaded, so can read string or byte array data
            //ftStatus = myFtdiDevice.Read(out readData, numBytesAvailable, ref numBytesRead);

            // add the condition checking here to validate that the readData in not empty.
            //OnReadingAvailable(readData);
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
                    richTextBox1.AppendText(System.Environment.NewLine + "Device Index: " + i.ToString());

                    richTextBox1.AppendText(System.Environment.NewLine + "Flags: " + String.Format("{0:x}", ftdiDeviceList[i].Flags));
                    richTextBox1.AppendText(System.Environment.NewLine + "Type: " + ftdiDeviceList[i].Type.ToString());
                    richTextBox1.AppendText(System.Environment.NewLine + "ID: " + String.Format("{0:x}", ftdiDeviceList[i].ID));
                    richTextBox1.AppendText(System.Environment.NewLine + "Location ID: " + String.Format("{0:x}", ftdiDeviceList[i].LocId));
                    richTextBox1.AppendText(System.Environment.NewLine + "Serial Number: " + ftdiDeviceList[i].SerialNumber.ToString());
                    richTextBox1.AppendText(System.Environment.NewLine + "Description: " + ftdiDeviceList[i].Description.ToString());
                    richTextBox1.AppendText(System.Environment.NewLine);
                }
            }


            Konica Test = new Konica();
            //Test.StartMeasurement();
            Test.CmdSend("00541   ");

            myFtdiDevice = new FTDISample();
            myFtdiDevice.OpenDevice(location: ftdiDeviceList[0].LocId);
            myFtdiDevice.SetKonicaT10();
            myFtdiDevice.DataReceived += OnDataReceived;
            
            //System.Threading.Thread.Sleep(1000);

            //string strConn = Char.ConvertFromUtf32((Convert.ToInt32("02", 16))) +
            //    "00541   " +
            //    Char.ConvertFromUtf32((Convert.ToInt32("03", 16))) +
            //    Char.ConvertFromUtf32((Convert.ToInt32("10", 16))) +
            //    "\r\n";

            if (myFtdiDevice.Write(cT10.Commands[0]))
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
                var result = cT10.DecodeCommand(e.StrDataReceived);
            }

            System.Diagnostics.Debug.Print("Data received");

            // https://github.com/ScottPlot/ScottPlot/blob/096062f5dfde8fd5f1e2eb2e15e0e7ce9b17a54b/src/ScottPlot.Demo.WinForms/WinFormsDemos/LiveDataUpdate.cs#L14-L91
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the device if it's still open
            if (myFtdiDevice.IsOpen)
                myFtdiDevice.Close();
        }

        
    }
}
