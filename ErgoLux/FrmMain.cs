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

namespace ErgoL
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // signal the native process (that launched us) to close the splash screen
            using (var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent"))
            {
                closeSplashEvent.Set();
            }

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


            FTDISample control = new FTDISample(ftdiDeviceList[0].SerialNumber.ToString());

        }
    }
}
