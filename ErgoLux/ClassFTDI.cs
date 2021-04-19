using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FTD2XX_NET;

namespace ErgoLux
{
    public class FTDISample
    {
        private AutoResetEvent receivedDataEvent;
        private BackgroundWorker dataReceivedHandler;
        private FTDI myFtdiDevice;
        //private FTDI mFTDI;

        public event EventHandler<DataReceivedEventArgs> DataReceived;

        public FTDISample(string serialNumber)
        {
            myFtdiDevice = new FTDI();
            //FTDI.FT_STATUS status = ftdi.OpenBySerialNumber(serialNumber);
            FTDI.FT_STATUS ftStatus = myFtdiDevice.OpenByIndex(0);

            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to open device (error " + ftStatus.ToString() + ")");
                Console.ReadKey();
                return;
            }

            receivedDataEvent = new AutoResetEvent(false);
            //status = mFTDI.SetEventNotification(FTDI.FT_EVENTS.FT_EVENT_RXCHAR, receivedDataEvent);
            ftStatus = myFtdiDevice.SetEventNotification(FTDI.FT_EVENTS.FT_EVENT_RXCHAR, receivedDataEvent);
            dataReceivedHandler = new BackgroundWorker();
            dataReceivedHandler.DoWork += ReadData;
            if (!dataReceivedHandler.IsBusy)
            {
                dataReceivedHandler.RunWorkerAsync();
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
            ftStatus = myFtdiDevice.SetFlowControl(FTDI.FT_FLOW_CONTROL.FT_FLOW_XON_XOFF, 0x10, 0x13);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set flow control (error " + ftStatus.ToString() + ")");
                Console.ReadKey();
                return;
            }

            // Set read timeout to 0.5 seconds, write timeout to infinite
            ftStatus = myFtdiDevice.SetTimeouts(500, 0);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set timeouts (error " + ftStatus.ToString() + ")");
                Console.ReadKey();
                return;
            }

        }

        ~FTDISample()
        {
            if (myFtdiDevice.IsOpen)
                myFtdiDevice.Close();
        }

        protected virtual void OnDataReceived(DataReceivedEventArgs e)
        {
            EventHandler<DataReceivedEventArgs> handler = DataReceived;
            handler?.Invoke(this, e);
        }

        private void ReadData(object pSender, DoWorkEventArgs pEventArgs)
        {
            UInt32 nrOfBytesAvailable = 0;
            while (true)
            {
                System.Diagnostics.Debug.WriteLine("ReadData event");
                // wait until event is fired
                this.receivedDataEvent.WaitOne();

                // try to recieve data now
                FTDI.FT_STATUS status = myFtdiDevice.GetRxBytesAvailable(ref nrOfBytesAvailable);
                System.Diagnostics.Debug.WriteLine("Bytes read: " + nrOfBytesAvailable.ToString());
                if (status != FTDI.FT_STATUS.FT_OK)
                {
                    break;
                }
                if (nrOfBytesAvailable > 0)
                {
                    byte[] readData = new byte[nrOfBytesAvailable];
                    UInt32 numBytesRead = 0;
                    //status = mFTDI.Read(readData, nrOfBytesAvailable, ref numBytesRead);
                    status = myFtdiDevice.Read(readData, nrOfBytesAvailable, ref numBytesRead);

                    // invoke your own event handler for data received...
                    OnDataReceived(new DataReceivedEventArgs(readData));
                }
            }
        }

        public bool Write(string data)
        {
            UInt32 numBytesWritten = 0;
            ASCIIEncoding enconding = new ASCIIEncoding();
            byte[] bytes = enconding.GetBytes(data);
            FTDI.FT_STATUS status = myFtdiDevice.Write(bytes, bytes.Length, ref numBytesWritten);
            if (status != FTDI.FT_STATUS.FT_OK)
            {
                System.Diagnostics.Debug.WriteLine("FTDI Write Status ERROR: " + status);
                return false;
            }
            if (numBytesWritten < data.Length)
            {
                System.Diagnostics.Debug.WriteLine("FTDI Write Length ERROR: " + status + " length " + data.Length +
                            " written " + numBytesWritten);
                return false;
            }
            this.receivedDataEvent.Set();
            return true;
        }
    }

    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(byte[] data)
        {
            DataReceived = data;

            string str = string.Empty;
            foreach (byte i in data)
            {
                str += Char.ConvertFromUtf32(i);
            }

            //string convert = "This is the string to be converted";

            //// From string to byte array
            //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(convert);

            //// From byte array to string
            StrDataReceived = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
        }

        public byte[] DataReceived { get; }
        public string StrDataReceived { get; }
    }
}
