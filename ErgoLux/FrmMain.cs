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
        public FrmMain()
        {
            InitializeComponent();

            var algo = new ClassT10("00541   ");
            var strEncoded = algo.EncodeCommand();
            
            //strEncoded = algo.EncodeCommand("00541   ");
            //strEncoded = algo.EncodeCommand("00100200");
            //strEncoded = algo.EncodeCommand("01100200");
            //strEncoded = algo.EncodeCommand("02100200");
            //strEncoded = algo.EncodeCommand("03100200");
            //strEncoded = algo.EncodeCommand("04100200");
            //strEncoded = algo.EncodeCommand("05100200");
            //strEncoded = algo.EncodeCommand("06100200");
            //strEncoded = algo.EncodeCommand("07100200");
            //strEncoded = algo.EncodeCommand("08100200");
            //strEncoded = algo.EncodeCommand("09100200");
            //strEncoded = algo.EncodeCommand("10100200");
            //strEncoded = algo.EncodeCommand("11100200");
            //strEncoded = algo.EncodeCommand("12100200");
            //strEncoded = algo.EncodeCommand("13100200");
            //strEncoded = algo.EncodeCommand("14100200");
            //strEncoded = algo.EncodeCommand("15100200");
            //strEncoded = algo.EncodeCommand("16100200");
            //strEncoded = algo.EncodeCommand("17100200");
            //strEncoded = algo.EncodeCommand("18100200");
            //strEncoded = algo.EncodeCommand("19100200");
            //strEncoded = algo.EncodeCommand("20100200");
            //strEncoded = algo.EncodeCommand("21100200");
            //strEncoded = algo.EncodeCommand("22100200");
            //strEncoded = algo.EncodeCommand("23100200");
            //strEncoded = algo.EncodeCommand("24100200");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // signal the native process (that launched us) to close the splash screen
            using (var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent"))
            {
                closeSplashEvent.Set();
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            // FTDI connection code
            UInt32 ftdiDeviceCount = 0;
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

            // Create new instance of the FTDI device class
            FTDI myFtdiDevice = new FTDI();

            // Determine the number of FTDI devices connected to the machine
            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);
            // Check status
            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                richTextBox1.AppendText(System.Environment.NewLine + "Number of FTDI devices: " + ftdiDeviceCount.ToString());
                richTextBox1.AppendText(System.Environment.NewLine);
            }
            else
            {
                // Wait for a key press
                richTextBox1.AppendText(System.Environment.NewLine + "Failed to get number of devices (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return;
            }

            // If no devices available, return
            if (ftdiDeviceCount == 0)
            {
                // Wait for a key press
                richTextBox1.AppendText(System.Environment.NewLine + "Failed to get number of devices (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return;
            }

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

            FTDISample control = new FTDISample(ftdiDeviceList[0].SerialNumber.ToString());
            control.DataReceived += OnDataReceived;

            //System.Threading.Thread.Sleep(1000);

            //string strConn = Char.ConvertFromUtf32((Convert.ToInt32("02", 16))) +
            //    "00541   " +
            //    Char.ConvertFromUtf32((Convert.ToInt32("03", 16))) +
            //    Char.ConvertFromUtf32((Convert.ToInt32("10", 16))) +
            //    "\r\n";

            var result = control.Write(Test.CmdSend("00541   "));
            result = control.Write(Test.CmdSend("00100200"));
            System.Threading.Thread.Sleep(3000);
            result = control.Write(Test.CmdSend("00100200"));



            //result = control.Write("00541   ");     // switch connection mode
            //result = control.Write("00100200");         // set measuremente conditions
            //System.Threading.Thread.Sleep(3000);
            //result = control.Write("00100200");         // set measuremente conditions

        }

        private void OnDataReceived (object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Print("Data received");
        }
    }
}
