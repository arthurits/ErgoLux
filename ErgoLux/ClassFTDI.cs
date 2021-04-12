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
        private FTDI ftdi;
        //private FTDI mFTDI;

        public event EventHandler DataReceived;

        public FTDISample(string serialNumber)
        {
            ftdi = new FTDI();
            //FTDI.FT_STATUS status = ftdi.OpenBySerialNumber(serialNumber);
            FTDI.FT_STATUS status = ftdi.OpenByIndex(0);

            receivedDataEvent = new AutoResetEvent(false);
            //status = mFTDI.SetEventNotification(FTDI.FT_EVENTS.FT_EVENT_RXCHAR, receivedDataEvent);
            status = ftdi.SetEventNotification(FTDI.FT_EVENTS.FT_EVENT_RXCHAR, receivedDataEvent);
            dataReceivedHandler = new BackgroundWorker();
            dataReceivedHandler.DoWork += ReadData;
            if (!dataReceivedHandler.IsBusy)
            {
                dataReceivedHandler.RunWorkerAsync();
            }
        }

        ~FTDISample()
        {
            ftdi.Close();
        }

        protected virtual void OnDataReceived(EventArgs e)
        {
            EventHandler handler = DataReceived;
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
                FTDI.FT_STATUS status = ftdi.GetRxBytesAvailable(ref nrOfBytesAvailable);
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
                    status = ftdi.Read(readData, nrOfBytesAvailable, ref numBytesRead);

                    // invoke your own event handler for data received...
                    OnDataReceived(new EventArgs());
                }
            }
        }

        public bool Write(string data)
        {
            UInt32 numBytesWritten = 0;
            ASCIIEncoding enconding = new ASCIIEncoding();
            byte[] bytes = enconding.GetBytes(data);
            FTDI.FT_STATUS status = ftdi.Write(bytes, bytes.Length, ref numBytesWritten);
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
}
