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
        ClassT10 cT10;

        public FrmMain()
        {
            InitializeComponent();
            cT10 = new ClassT10();

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

            // Set up device data parameters
            // Set Baud rate to 9600
            ftStatus = myFtdiDevice.SetBaudRate(9600);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set Baud rate (error " + ftStatus.ToString() + ")");
                Console.ReadKey();
                return;
            }

            // Set data characteristics - Data bits, Stop bits, Parity
            ftStatus = myFtdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_7, FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_EVEN);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set data characteristics (error " + ftStatus.ToString() + ")");
                Console.ReadKey();
                return;
            }

            // Set flow control - set RTS/CTS flow control
            ftStatus = myFtdiDevice.SetFlowControl(FTDI.FT_FLOW_CONTROL.FT_FLOW_XON_XOFF, 0x11, 0x13);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set flow control (error " + ftStatus.ToString() + ")");
                Console.ReadKey();
                return;
            }

            // Set read timeout to 5 seconds, write timeout to infinite
            ftStatus = myFtdiDevice.SetTimeouts(500, 0);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set timeouts (error " + ftStatus.ToString() + ")");
                Console.ReadKey();
                return;
            }



            // Close our device
            ftStatus = myFtdiDevice.Close();


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
