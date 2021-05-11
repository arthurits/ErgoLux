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
    public class FTDISample : FTDI
    {
        private bool _receivedBuffer = false;
        private AutoResetEvent receivedDataEvent;
        private BackgroundWorker dataReceivedHandler;

        public event EventHandler<DataReceivedEventArgs> DataReceived;

        public FTDISample()
            : base()
        {
            receivedDataEvent = new AutoResetEvent(false);
            FTDI.FT_STATUS ftStatus = base.SetEventNotification(FTDI.FT_EVENTS.FT_EVENT_RXCHAR, receivedDataEvent);
            
            dataReceivedHandler = new BackgroundWorker();
            dataReceivedHandler.DoWork += ReadData2;
            if (!dataReceivedHandler.IsBusy)
            {
                dataReceivedHandler.RunWorkerAsync();
            }
        }

        ~FTDISample()
        {
            if (base.IsOpen)
                base.Close();
        }
        
        protected virtual void OnDataReceived(DataReceivedEventArgs e)
        {
            EventHandler<DataReceivedEventArgs> handler = DataReceived;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Custom-generalized function to initialize the device
        /// </summary>
        /// <param name="description">Description of the device to open.</param>
        /// <param name="index">Index of the device to open. Note that this cannot be guaranteed to open a specific device.</param>
        /// <param name="location">Location of the device to open.</param>
        /// <param name="serialNumber">Serial number of the device to open.</param>
        /// <param name="baud">Baud rate (typically 9600)</param>
        /// <param name="dataBits">7 bits (7) or 8 bits (8)</param>
        /// <param name="stopBits">1 bit (0) or 2 bits (2)</param>
        /// <param name="parity">None(0), odd(1), even(2), mark(3), space (4)</param>
        /// <param name="flowControl">None (0), RTS/CTS (100), DTR/DSR (200), Xon/Xoff (400)</param>
        /// <param name="xOn">On Ascii char value</param>
        /// <param name="xOff">Off Ascii char value</param>
        /// <param name="readTimeOut">Read timeout value in ms. A value of 0 indicates an infinite timeout.</param>
        /// <param name="writeTimeOut">Write timeout value in ms. A value of 0 indicates an infinite timeout.</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool OpenDevice(string description = null, uint? index = null, uint? location = null, string serialNumber = null, int? baud = 9600, int? dataBits = 7, int? stopBits = 1, int? parity = 2, int? flowControl = 400, int? xOn = 10, int? xOff = 13, uint readTimeOut = 0, uint writeTimeOut = 0)
        {
            // FTDI connection code
            UInt32 ftdiDeviceCount = 0;
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

            // First check if there is al least 1 device connected to the machine
            ftStatus = base.GetNumberOfDevices(ref ftdiDeviceCount);
            // Check status
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                System.Windows.Forms.MessageBox.Show("Failed to open device\nError: " + ftStatus.ToString(),
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                //Console.WriteLine("Failed to open device (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return false;
            }

            // If no devices available, return
            if (ftdiDeviceCount == 0)
            {
                // Wait for a key press
                System.Windows.Forms.MessageBox.Show("Failed to open device\nError: " + ftStatus.ToString(),
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                //Console.WriteLine("Failed to open device (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return false;
            }


            // Open the device
            if (!string.IsNullOrEmpty(description) && !base.IsOpen)
                ftStatus = base.OpenByDescription(description);
            if (index.HasValue && !base.IsOpen)
                ftStatus = base.OpenByIndex(index.Value);
            if (location.HasValue && !base.IsOpen)
                ftStatus = base.OpenByLocation(location.Value);
            if (!string.IsNullOrEmpty(serialNumber) && !base.IsOpen)
                ftStatus = base.OpenBySerialNumber(serialNumber);

            // Set the T10 device paramters
            SetKonicaT10((uint)baud, (byte)dataBits, (byte)stopBits, (byte)parity, (ushort)flowControl, (byte)xOn, (byte)xOff, readTimeOut, writeTimeOut);

            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                System.Windows.Forms.MessageBox.Show("Failed to open device\nError: " + ftStatus.ToString(),
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                //Console.WriteLine("Failed to open device (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sets the FTDI configuration values in order to read data from illuminance meter Konica T10-A
        /// 9600 baud rate, 7 data bits, 1 stop bit, even parity (2) and on/off flow control (400)
        /// </summary>
        /// <param name="baud">Baud rate (typically 9600)</param>
        /// <param name="dataBits">7 bits (7) or 8 bits (8)</param>
        /// <param name="stopBits">1 bit (0) or 2 bits (2)</param>
        /// <param name="parity">None(0), odd(1), even(2), mark(3), space (4)</param>
        /// <param name="flow">None (0), RTS/CTS (100), DTR/DSR (200), Xon/Xoff (400)</param>
        /// <param name="xOn">On Ascii char value</param>
        /// <param name="xOff">Off Ascii char value</param>
        /// <param name="readTimeOut">Read timeout value in ms. A value of 0 indicates an infinite timeout.</param>
        /// <param name="writeTimeOut">Write timeout value in ms. A value of 0 indicates an infinite timeout.</param>
        /// <returns>True if all parameters could be set, false otherwise. FT_STATUS value from FT_SetTimeouts in FTD2XX.DLL</returns>
        public bool SetKonicaT10(uint baud, byte dataBits, byte stopBits, byte parity, ushort flow, byte xOn, byte xOff, uint readTimeOut, uint writeTimeOut)
        {
            FTDI.FT_STATUS ftStatus;

            // Set up device data parameters
            // Set Baud rate to 9600
            //ftStatus = base.SetBaudRate(9600);
            ftStatus = base.SetBaudRate(baud);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                System.Windows.Forms.MessageBox.Show("Failed to set Baud rate\nError: " + ftStatus.ToString(),
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                //Console.WriteLine("Failed to set Baud rate (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return false;
            }

            // Set data characteristics - Data bits, Stop bits, Parity
            //ftStatus = base.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_7, FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_EVEN);
            ftStatus = base.SetDataCharacteristics(dataBits, stopBits, parity);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                System.Windows.Forms.MessageBox.Show("Failed to set data characteristics\nError: " + ftStatus.ToString(),
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                //Console.WriteLine("Failed to set data characteristics (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return false;
            }

            // Set flow control - set RTS/CTS flow control
            //ftStatus = base.SetFlowControl(FTDI.FT_FLOW_CONTROL.FT_FLOW_XON_XOFF, 0x10, 0x13);
            ftStatus = base.SetFlowControl(flow, 0x10, 0x13);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                System.Windows.Forms.MessageBox.Show("Failed to set flow control\nError: " + ftStatus.ToString(),
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                //Console.WriteLine("Failed to set flow control (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return false;
            }

            // Set read and write timeouts
            ftStatus = base.SetTimeouts(readTimeOut, writeTimeOut);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                System.Windows.Forms.MessageBox.Show("Failed to set timeouts\nError: " + ftStatus.ToString(),
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                //Console.WriteLine("Failed to set timeouts (error " + ftStatus.ToString() + ")");
                //Console.ReadKey();
                return false;
            }

            return true;
        }

        private void ReadData(object pSender, DoWorkEventArgs pEventArgs)
        {
            FTDI.FT_STATUS status;
            UInt32 nrOfBytesAvailable = 0;
            while (true)
            {
                System.Diagnostics.Debug.WriteLine("ReadData event");
                // wait until event is fired
                this.receivedDataEvent.WaitOne();

                //FTDI.FT_STATUS status = FTDI.FT_STATUS.FT_IO_ERROR;
                //while (status != FTDI.FT_STATUS.FT_OK)
                //    status = base.GetRxBytesAvailable(ref nrOfBytesAvailable);

                // try to recieve data now
                status = base.GetRxBytesAvailable(ref nrOfBytesAvailable);
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
                    status = base.Read(readData, nrOfBytesAvailable, ref numBytesRead);
                    
                    // invoke your own event handler for data received...
                    OnDataReceived(new DataReceivedEventArgs(readData, numBytesRead, nrOfBytesAvailable));
                    _receivedBuffer = true;
                }
            }
        }

        private void ReadData2(object pSender, DoWorkEventArgs pEventArgs)
        {
            FTDI.FT_STATUS status;
            UInt32 nrOfBytesAvailable = 0;
            UInt32 numBytesRead = 0;
            string strReadData = string.Empty;
            
            this.receivedDataEvent.WaitOne();
            
            while (true)
            {
                status = base.GetRxBytesAvailable(ref nrOfBytesAvailable);
                if (status != FTDI.FT_STATUS.FT_OK)
                {
                    return;
                }
                if (nrOfBytesAvailable > 0)
                {
                    byte[] readData = new byte[nrOfBytesAvailable];
                    numBytesRead = 0;
                    status = base.Read(readData, nrOfBytesAvailable, ref numBytesRead);
                    strReadData += System.Text.Encoding.UTF8.GetString(readData);
                    if (readData[readData.Length - 1] == 10)
                    {
                        // invoke your own event handler for data received...
                        OnDataReceived(new DataReceivedEventArgs(Encoding.ASCII.GetBytes(strReadData), numBytesRead, nrOfBytesAvailable));
                        strReadData = string.Empty;
                        this.receivedDataEvent.WaitOne();
                    }
                }
            }

            
        }

        public bool Write(string data)
        {
            UInt32 numBytesWritten = 0;
            ASCIIEncoding enconding = new ASCIIEncoding();
            byte[] bytes = enconding.GetBytes(data);
            FTDI.FT_STATUS status = base.Write(bytes, bytes.Length, ref numBytesWritten);
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

        public bool Write (params string[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (!Write(list[i])) return false;
            }
            return true;
        }


        /// <summary>
        /// For testing purposes. It tries to clear the reading buffer.
        /// </summary>
        public void ClearBuffer()
        {
            this.receivedDataEvent.Set();
        }


        /// <summary>
        /// For testing purposes
        /// </summary>
        public void GetReceiveBuffer()
        {
            _receivedBuffer = false;
            while (!_receivedBuffer)
                this.receivedDataEvent.Set();
            return;
        }
    }

    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(byte[] data, uint bytesRead, uint bytesAvailable)
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

            BytesRead = bytesRead;

            BytesAvailable = BytesAvailable;
        }

        public byte[] DataReceived { get; }
        public uint BytesRead { get; }
        public uint BytesAvailable { get; }
        public string StrDataReceived { get; }
    }
}
