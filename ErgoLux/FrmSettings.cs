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
    public partial class FrmSettings : Form
    {
        private int[] _data = new int[9];

        public int[] GetData { get => _data; }

        public FrmSettings()
        {
            InitializeComponent();

            // Initializa ListView
            viewDevices.View = View.Details;
            viewDevices.GridLines = true;
            viewDevices.FullRowSelect = true;

            //Add column header
            viewDevices.Columns.Add("Device index", 50);
            viewDevices.Columns.Add("Flags", 100);
            viewDevices.Columns.Add("Type", 100);
            viewDevices.Columns.Add("ID", 70);
            viewDevices.Columns.Add("Location ID", 70);
            viewDevices.Columns.Add("Serial number", 100);
            viewDevices.Columns.Add("Description", 100);
            
            PopulateDevices();

            // Populate cboDataBits
            Dictionary<string, int> cboList = new Dictionary<string, int>();
            cboList.Add("7 bits", 7);
            cboList.Add("8 bits", 8);

            cboDataBits.DataSource = new BindingSource(cboList, null);
            cboDataBits.DisplayMember = "Key";
            cboDataBits.ValueMember = "Value";
            cboDataBits.SelectedIndex = 0;

            // Populate cboStop
            cboList.Clear();
            cboList.Add("1 stop bit", 0);
            cboList.Add("2 stop bits", 2);

            cboStopBits.DataSource = new BindingSource(cboList, null);
            cboStopBits.DisplayMember = "Key";
            cboStopBits.ValueMember = "Value";
            cboStopBits.SelectedIndex = 0;

            // Populate cboParity
            cboList.Clear();
            cboList.Add("None", 0);
            cboList.Add("Odd", 1);
            cboList.Add("Even", 2);
            cboList.Add("Mark", 3);

            cboParity.DataSource = new BindingSource(cboList, null);
            cboParity.DisplayMember = "Key";
            cboParity.ValueMember = "Value";
            cboParity.SelectedIndex = 2;

            // Populate cboFlowControl
            cboList.Clear();
            cboList.Add("None", 0);
            cboList.Add("RTS / CTS", 100);
            cboList.Add("DTR / DSR", 200);
            cboList.Add("Xon / Xoff", 400);

            cboFlowControl.DataSource = new BindingSource(cboList, null);
            cboFlowControl.DisplayMember = "Key";
            cboFlowControl.ValueMember = "Value";
            cboFlowControl.SelectedIndex = 3;

            txtBaudRate.Text = "9600";
            txtOn.Text = "10";
            txtOff.Text = "13";

        }

        /// <summary>
        /// Populates ListView control with data from the FTDI devices connected
        /// </summary>
        private void PopulateDevices()
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
            if (ftStatus != FTDI.FT_STATUS.FT_OK) return;

            // Populate the control
            ListViewItem item;
            for (UInt32 i = 0; i < ftdiDeviceCount; i++)
            {
                item = new ListViewItem(
                    new string[]
                    {
                            i.ToString(),
                            String.Format("{0:x}", ftdiDeviceList[i].Flags),
                            ftdiDeviceList[i].Type.ToString(),
                            String.Format("{0:x}", ftdiDeviceList[i].ID),
                            String.Format("{0:x}", ftdiDeviceList[i].LocId),
                            ftdiDeviceList[i].SerialNumber.ToString(),
                            ftdiDeviceList[i].Description.ToString()
                    });
                if (i++ % 2 == 1)
                {
                    item.BackColor = Color.FromArgb(240, 240, 240);
                    item.UseItemStyleForSubItems = true;
                }
                viewDevices.Items.Add(item);
            }
        }

        private void cboFlowControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboFlowControl.SelectedIndex==3)
            {
                txtOn.Enabled = true;
                txtOff.Enabled = true;
            }
            else
            {
                txtOn.Enabled = false;
                txtOff.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            if (viewDevices.SelectedIndices.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Please select one of the devices from the list.",
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            else
                _data[0] = Convert.ToInt32(viewDevices.SelectedItems[0].SubItems[4].Text);  //Location ID

            _data[1] = (int)updSensors.Value;
            _data[2] = Convert.ToInt32(txtBaudRate.Text);
            _data[3] = ((KeyValuePair<string, int>)cboDataBits.SelectedItem).Value;
            _data[4] = ((KeyValuePair<string, int>)cboStopBits.SelectedItem).Value;
            _data[5] = ((KeyValuePair<string, int>)cboParity.SelectedItem).Value;
            _data[6] = ((KeyValuePair<string, int>)cboFlowControl.SelectedItem).Value;
            _data[7] = Convert.ToInt32(txtOn.Text);
            _data[8] = Convert.ToInt32(txtOff.Text);

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
