using System.Globalization;
using FTD2XX_NET;

namespace ErgoLux;

public partial class FrmSettings : Form
{
    private CultureInfo _culture = CultureInfo.CurrentCulture;
    private readonly ClassSettings? Settings;

    public FrmSettings()
    {
        this.InitializeComponent();

        // Initializa ListView
        this.viewDevices.View = View.Details;
        this.viewDevices.GridLines = true;
        this.viewDevices.FullRowSelect = true;

        //Add column header
        this.viewDevices.Columns.Add("Device index", 50);
        this.viewDevices.Columns.Add("Flags", 100);
        this.viewDevices.Columns.Add("Type", 100);
        this.viewDevices.Columns.Add("ID", 70);
        this.viewDevices.Columns.Add("Location ID", 70);
        this.viewDevices.Columns.Add("Serial number", 100);
        this.viewDevices.Columns.Add("Description", 100);

        this.PopulateDevices();

        // Populate cboDataBits
        Dictionary<string, int> cboList = new Dictionary<string, int>();
        cboList.Add("7 bits", FTDI.FT_DATA_BITS.FT_BITS_7);
        cboList.Add("8 bits", FTDI.FT_DATA_BITS.FT_BITS_8);

        this.cboDataBits.DataSource = new BindingSource(cboList, null);
        this.cboDataBits.DisplayMember = "Key";
        this.cboDataBits.ValueMember = "Value";

        // Populate cboStop
        cboList.Clear();
        cboList.Add("1 bit", FTDI.FT_STOP_BITS.FT_STOP_BITS_1);
        cboList.Add("2 bits", FTDI.FT_STOP_BITS.FT_STOP_BITS_2);

        this.cboStopBits.DataSource = new BindingSource(cboList, null);
        this.cboStopBits.DisplayMember = "Key";
        this.cboStopBits.ValueMember = "Value";

        // Populate cboParity
        cboList.Clear();
        cboList.Add("None", FTDI.FT_PARITY.FT_PARITY_NONE);
        cboList.Add("Odd", FTDI.FT_PARITY.FT_PARITY_ODD);
        cboList.Add("Even", FTDI.FT_PARITY.FT_PARITY_EVEN);
        cboList.Add("Mark", FTDI.FT_PARITY.FT_PARITY_MARK);
        cboList.Add("Space", FTDI.FT_PARITY.FT_PARITY_SPACE);

        this.cboParity.DataSource = new BindingSource(cboList, null);
        this.cboParity.DisplayMember = "Key";
        this.cboParity.ValueMember = "Value";

        // Populate cboFlowControl
        cboList.Clear();
        cboList.Add("None", FTDI.FT_FLOW_CONTROL.FT_FLOW_NONE);
        cboList.Add("RTS / CTS", FTDI.FT_FLOW_CONTROL.FT_FLOW_RTS_CTS);
        cboList.Add("DTR / DSR", FTDI.FT_FLOW_CONTROL.FT_FLOW_DTR_DSR);
        cboList.Add("Xon / Xoff", FTDI.FT_FLOW_CONTROL.FT_FLOW_XON_XOFF);

        this.cboFlowControl.DataSource = new BindingSource(cboList, null);
        this.cboFlowControl.DisplayMember = "Key";
        this.cboFlowControl.ValueMember = "Value";

        // Set control's default input-values
        //SetDefaultValues();

        this.FillDefinedCultures("ErgoLux.localization.strings", typeof(FrmMain).Assembly);
    }

    public FrmSettings(ClassSettings settings)
        : this()
    {
        this.Settings = settings;
        this._culture = settings.AppCulture;
        this.UpdateControls(settings);
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
                            String.Format("{0:X}", ftdiDeviceList[i].Flags),
                            ftdiDeviceList[i].Type.ToString(),
                            String.Format("{0:X}", ftdiDeviceList[i].ID),
                            String.Format("{0:X}", ftdiDeviceList[i].LocId),
                            ftdiDeviceList[i].SerialNumber.ToString(),
                            ftdiDeviceList[i].Description.ToString()
                });
            if (i++ % 2 == 1)
            {
                item.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                item.UseItemStyleForSubItems = true;
            }
            this.viewDevices.Items.Add(item);
        }
    }

    private void cboFlowControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.cboFlowControl.SelectedIndex == 3)
        {
            this.txtOn.Enabled = true;
            this.txtOff.Enabled = true;
        }
        else
        {
            this.txtOn.Enabled = false;
            this.txtOff.Enabled = false;
        }
    }

    private void chkShowDistribution_CheckedChanged(object sender, EventArgs e)
    {
        this.grpPlot.Enabled = this.chkShowDistribution.Checked;
    }

    private void Accept_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.None;
        if (this.Settings is null) return;

        // Check that a device has been selected from the list
        if (this.viewDevices.SelectedIndices.Count > 0)
        {
            this.Settings.T10_DeviceType = this.viewDevices.SelectedItems[0].SubItems[2].Text;
            this.Settings.T10_DevideID = Convert.ToInt32(this.viewDevices.SelectedItems[0].SubItems[3].Text, 16);
            this.Settings.T10_LocationID = Convert.ToInt32(this.viewDevices.SelectedItems[0].SubItems[4].Text, 16);
        }
        else
        {
            if (this.viewDevices.Items.Count == 1)
            {
                this.Settings.T10_DeviceType = this.viewDevices.Items[0].SubItems[2].Text;
                this.Settings.T10_DevideID = Convert.ToInt32(this.viewDevices.Items[0].SubItems[3].Text, 16);
                this.Settings.T10_LocationID = Convert.ToInt32(this.viewDevices.Items[0].SubItems[4].Text, 16);
            }
        }

        // Check that all texboxes have valid values
        if (!Validation.IsValidRange<int>(this.txtBaudRate.Text, 0, 9600, true, this)) { this.txtBaudRate.Focus(); this.txtBaudRate.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(this.txtOn.Text, 0, 255, true, this)) { this.txtOn.Focus(); this.txtOn.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(this.txtOff.Text, 0, 255, true, this)) { this.txtOff.Focus(); this.txtOff.SelectAll(); return; }
        if (!Validation.IsValidRange<double>(this.txtHz.Text, 0, 1000, true, this)) { this.txtHz.Focus(); this.txtHz.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(this.txtArrayPoints.Text, 1, Int32.MaxValue, true, this)) { this.txtArrayPoints.Focus(); this.txtArrayPoints.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(this.txtPlotWindow.Text, 20, Int32.MaxValue, true, this)) { this.txtPlotWindow.Focus(); this.txtPlotWindow.SelectAll(); return; }

        // Save to class settings
        this.Settings.T10_NumberOfSensors = (int)this.updSensors.Value;
        this.Settings.T10_BaudRate = Convert.ToInt32(this.txtBaudRate.Text);
        //Settings.T10_DataBits = ((KeyValuePair<string, int>)cboDataBits.SelectedItem).Value;
        //Settings.T10_StopBits = ((KeyValuePair<string, int>)cboStopBits.SelectedItem).Value;
        //Settings.T10_Parity = ((KeyValuePair<string, int>)cboParity.SelectedItem).Value;
        //Settings.T10_FlowControl = ((KeyValuePair<string, int>)cboFlowControl.SelectedItem).Value;
        this.Settings.T10_DataBits = Convert.ToInt32(this.cboDataBits.SelectedValue);
        this.Settings.T10_StopBits = Convert.ToInt32(this.cboStopBits.SelectedValue);
        this.Settings.T10_Parity = Convert.ToInt32(this.cboParity.SelectedValue);
        this.Settings.T10_FlowControl = Convert.ToInt32(this.cboFlowControl.SelectedValue);
        this.Settings.T10_CharOn = Convert.ToInt32(this.txtOn.Text);
        this.Settings.T10_CharOff = Convert.ToInt32(this.txtOff.Text);
        this.Settings.T10_Frequency = Convert.ToDouble(this.txtHz.Text);

        this.Settings.Plot_ArrayPoints = Convert.ToInt32(this.txtArrayPoints.Text);
        this.Settings.Plot_WindowPoints = Convert.ToInt32(this.txtPlotWindow.Text);
        this.Settings.Plot_ShowRawData = this.chkShowRaw.Checked;
        this.Settings.Plot_ShowDistribution = this.chkShowDistribution.Checked;
        this.Settings.Plot_ShowAverage = this.chkShowAverage.Checked;
        this.Settings.Plot_ShowRatios = this.chkShowRatio.Checked;
        this.Settings.Plot_DistIsRadar = this.radRadar.Checked;

        this.Settings.AppCulture = this._culture;
        this.Settings.RememberFileDialogPath = this.chkDlgPath.Checked;
        this.Settings.DataFormat = this.txtDataFormat.Text;

        this.DialogResult = DialogResult.OK;
    }

    private void Reset_Click(object sender, EventArgs e)
    {
        DialogResult result;
        using (new CenterWinDialog(this))
        {
            result = MessageBox.Show(StringResources.MsgBoxReset,
                StringResources.MsgBoxResetTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }

        if (result == DialogResult.Yes)
        {
            this.UpdateControls(new ClassSettings());
        }
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
    }

    private void Label_Click(object sender, EventArgs e)
    {
        switch ((sender as Label)?.Name)
        {
            case "lblChkShowRaw":
                this.chkShowRaw.Checked = true;
                break;
            case "lblChkShowDistribution":
                this.chkShowDistribution.Checked = true;
                break;
            case "lblChkShowAverage":
                this.chkShowAverage.Checked = true;
                break;
            case "lblChkShowRatio":
                this.chkShowRatio.Checked = true;
                break;
        }
    }

    private void CurrentCulture_CheckedChanged(object sender, EventArgs e)
    {
        if (this.radCurrentCulture.Checked)
        {
            this._culture = System.Globalization.CultureInfo.CurrentCulture;
            this.UpdateUI_Language();
        }
    }

    private void InvariantCulture_CheckedChanged(object sender, EventArgs e)
    {
        if (this.radInvariantCulture.Checked)
        {
            this._culture = System.Globalization.CultureInfo.InvariantCulture;
            this.UpdateUI_Language();
        }
    }

    private void UserCulture_CheckedChanged(object sender, EventArgs e)
    {
        this.cboAllCultures.Enabled = this.radUserCulture.Checked;
        if (this.cboAllCultures.Enabled)
        {
            this._culture = new((string)this.cboAllCultures.SelectedValue);
            this.UpdateUI_Language();
        }
    }

    private void AllCultures_SelectedValueChanged(object sender, EventArgs e)
    {
        var cbo = sender as ComboBox;
        if (cbo is not null && cbo.Items.Count > 0 && cbo.SelectedValue is not null)
        {
            this._culture = new((string)cbo.SelectedValue ?? String.Empty);
            this.UpdateUI_Language();
        }
    }

    /// <summary>
    /// Updates the form's controls with values from the settings class
    /// </summary>
    /// <param name="settings">Class containing the values to show on the form's controls</param>
    private void UpdateControls(ClassSettings settings)
    {
        this.cboDataBits.SelectedValue = settings.T10_DataBits;
        this.cboStopBits.SelectedValue = settings.T10_StopBits;
        this.cboParity.SelectedValue = settings.T10_Parity;
        this.cboFlowControl.SelectedValue = settings.T10_FlowControl;

        this.txtBaudRate.Text = settings.T10_BaudRate.ToString();
        this.txtOn.Text = settings.T10_CharOn.ToString();
        this.txtOff.Text = settings.T10_CharOff.ToString();
        this.txtHz.Text = settings.T10_Frequency.ToString();

        this.updSensors.Value = settings.T10_NumberOfSensors;

        this.txtArrayPoints.Text = settings.Plot_ArrayPoints.ToString();
        this.txtPlotWindow.Text = settings.Plot_WindowPoints.ToString();

        this.chkShowRaw.Checked = settings.Plot_ShowRawData;
        this.chkShowDistribution.Checked = settings.Plot_ShowDistribution;
        this.chkShowAverage.Checked = settings.Plot_ShowAverage;
        this.chkShowRatio.Checked = settings.Plot_ShowRatios;
        if (settings.Plot_DistIsRadar)
            this.radRadar.Checked = true;
        else
            this.radRadial.Checked = true;

        this.cboAllCultures.Enabled = false;
        if (this._culture.Name == string.Empty)
        {
            this.radInvariantCulture.Checked = true;
        }
        else if (this._culture.Name == System.Globalization.CultureInfo.CurrentCulture.Name)
        {
            this.radCurrentCulture.Checked = true;
        }
        else
        {
            this.cboAllCultures.SelectedValue = this._culture.Name;
            this.radUserCulture.Checked = true;
        }

        this.chkDlgPath.Checked = settings.RememberFileDialogPath;
        this.txtDataFormat.Text = settings.DataFormat;
    }

    /// <summary>
    /// Databind only the cultures found in .resources files for a given type
    /// </summary>
    /// <param name="type">A type from which the resource manager derives all information for finding .resources files</param>
    private void FillDefinedCultures(string baseName, System.Reflection.Assembly assembly)
    {
        string cultureName = this._culture.Name;
        var cultures = System.Globalization.GlobalizationUtilities.GetAvailableCultures(baseName, assembly);
        this.cboAllCultures.DisplayMember = "DisplayName";
        this.cboAllCultures.ValueMember = "Name";
        this.cboAllCultures.DataSource = cultures.ToArray();
        this.cboAllCultures.SelectedValue = cultureName;
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language()
    {
        this.UpdateUI_Language(this._culture);
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        StringResources.Culture = culture;

        // Form
        this.Text = StringResources.FrmSettings;

        this.tabDevice.Text = StringResources.TabDevice;
        this.tabPlots.Text = StringResources.TabPlots;
        this.tabGUI.Text = StringResources.TabGUI;

        this.btnReset.Text = StringResources.BtnReset;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnAccept.Text = StringResources.BtnAccept;

        // Tab T-10A
        this.lblDevices.Text = StringResources.LblDeviceList;
        this.lblBaudRate.Text = StringResources.LblBaudRate;
        this.lblDataBits.Text = StringResources.LblDataBits;
        this.lblParity.Text = StringResources.LblParity;
        this.lblSensors.Text = StringResources.LblSensors;
        this.lblFrequency.Text = StringResources.LblFrequency;
        this.lblStopBits.Text = StringResources.LblStopBits;
        this.lblFlowControl.Text = StringResources.LblFlow;
        this.lblOn.Text = StringResources.LblOn;
        this.lblOff.Text = StringResources.LblOff;

        this.viewDevices.Columns[0].Text = StringResources.GridDevice;
        this.viewDevices.Columns[1].Text = StringResources.GridFlags;
        this.viewDevices.Columns[2].Text = StringResources.GridType;
        this.viewDevices.Columns[3].Text = StringResources.GridID;
        this.viewDevices.Columns[4].Text = StringResources.GridLocation;
        this.viewDevices.Columns[5].Text = StringResources.GridSerial;
        this.viewDevices.Columns[6].Text = StringResources.GridDescription;

        // Tab Plots
        this.lblChkShowRaw.Text = StringResources.ChkPlotRaw;
        this.lblChkShowDistribution.Text = StringResources.ChkPlotDistribution;
        this.lblChkShowAverage.Text = StringResources.ChkPlotAverage;
        this.lblChkShowRatio.Text = StringResources.ChkPlotRatios;
        this.grpPlot.Text = StringResources.GrpPlot;
        this.radRadar.Text = StringResources.RadRadar;
        this.radRadial.Text = StringResources.RadRadialGauge;
        this.lblArrayPoints.Text = StringResources.LblArrayPoints;
        this.lblPlotWindow.Text = StringResources.LblPlotWindow;

        // Tab Interface
        this.grpCulture.Text = StringResources.GrpCulture;
        this.radCurrentCulture.Text = StringResources.RadCurrentCulture + $" ({System.Globalization.CultureInfo.CurrentCulture.Name})";
        this.radInvariantCulture.Text = StringResources.RadInvariantCulture;
        this.radUserCulture.Text = StringResources.RadUserCulture;
        //this.chkDlgPath.Text = StringResources.ChkDlgPath;
        this.lblDlgPath.Text = StringResources.ChkDlgPath;
        this.lblDataFormat.Text = StringResources.LblDataFormat;

        // Reposition controls to compensate for the culture text length in labels
        // Tab T-10A
        int nBaud = this.lblBaudRate.Left + this.lblBaudRate.Width + this.txtBaudRate.Width;
        int nData = this.lblDataBits.Left + this.lblDataBits.Width + this.cboDataBits.Width;
        int nParity = this.lblParity.Left + this.lblParity.Width + this.cboParity.Width;
        int nSensors = this.lblSensors.Left + this.lblSensors.Width + this.updSensors.Width;
        int nMaxRight = Math.Max(Math.Max(nBaud, nData), Math.Max(nParity, nSensors)) + 3;
        this.txtBaudRate.Left = nMaxRight - this.txtBaudRate.Width;
        this.cboDataBits.Left = nMaxRight - this.cboDataBits.Width;
        this.cboParity.Left = nMaxRight - this.cboParity.Width;
        this.updSensors.Left = nMaxRight - this.updSensors.Width;

        int nFreq = this.txtHz.Left - this.lblFrequency.Width;
        int nStop = this.cboStopBits.Left - this.lblStopBits.Width;
        int nFlow = this.cboFlowControl.Left - this.lblFlowControl.Width;
        int minLeft = Math.Min(Math.Min(nFreq, nStop), nFlow) - 3;
        this.lblFrequency.Left = minLeft;
        this.lblStopBits.Left = minLeft;
        this.lblFlowControl.Left = minLeft;

        this.lblOff.Left = this.txtOff.Left - this.lblOff.Width - 3;
        this.txtOn.Left = this.lblOff.Left - this.txtOn.Width - 7;
        this.lblOn.Left = this.txtOn.Left - this.lblOn.Width - 3;

        // Tab plots
        this.lblChkShowRaw.Top = this.chkShowRaw.Top + (this.chkShowRaw.Height - this.lblChkShowRaw.Height) / 2;
        this.lblChkShowDistribution.Top = this.chkShowDistribution.Top + (this.chkShowDistribution.Height - this.lblChkShowDistribution.Height) / 2;
        this.lblChkShowAverage.Top = this.chkShowAverage.Top + (this.chkShowAverage.Height - this.lblChkShowAverage.Height) / 2;
        this.lblChkShowRatio.Top = this.chkShowRatio.Top + (this.chkShowRatio.Height - this.lblChkShowRatio.Height) / 2;

        int width = Math.Max(this.lblArrayPoints.Width, this.lblPlotWindow.Width);
        this.txtArrayPoints.Left = this.lblArrayPoints.Left + width;
        this.txtPlotWindow.Left = this.lblPlotWindow.Left + width;

        // Tab Interface
        this.chkDlgPath.Top = 1 + this.lblDlgPath.Top + (this.lblDlgPath.Height - this.chkDlgPath.Height) / 2;

        this.txtDataFormat.Left = this.lblDataFormat.Left + this.lblDataFormat.Width;
        this.lblDataFormat.Top = this.txtDataFormat.Top + (this.txtDataFormat.Height - this.lblDataFormat.Height) / 2;
    }


}