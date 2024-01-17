﻿using FTD2XX_NET;
using System.Globalization;

namespace ErgoLux;

public partial class FrmSettings : Form
{
    private CultureInfo _culture = CultureInfo.CurrentCulture;
    private readonly AppSettings? Settings;
    private readonly string _baseName = "ErgoLux.localization.strings";

    public bool ModifyPlots { get; private set; } = false;
    public bool ModifyArrays { get; private set; } = false;
    public bool ModifyDevice { get; private set; } = false;

    public FrmSettings()
    {
        InitializeComponent();

        InitializaControls();
        PopulateFTDIDevices();
        FillDefinedCultures(_baseName, typeof(FrmMain).Assembly);
    }

    public FrmSettings(AppSettings settings)
        : this()
    {
        Settings = settings;
        _culture = settings.AppCulture;
        UpdateControls(settings);
    }

    private void PopulateSerialPortDevices()
    {
        List<SerialPort> ports = SerialPorts.GetSerialPorts();

        //// Populate the control
        //ListViewItem item;
        //for (int i = 0; i < ports.Count; i++)
        //{
        //    item = new ListViewItem(
        //        new string[]
        //        {
        //                    i.ToString(),
        //                    String.Format("{0:X}", ports[i].flags),
        //                    ports[i].providerType,
        //                    String.Format("{0:X}", ports[i].flags),
        //                    String.Format("{0:X}", ftdiDeviceList[i].LocId),
        //                    ftdiDeviceList[i].SerialNumber.ToString(),
        //                    ftdiDeviceList[i].Description.ToString()
        //        });
        //    if (i++ % 2 == 1)
        //    {
        //        item.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
        //        item.UseItemStyleForSubItems = true;
        //    }
        //    viewDevices.Items.Add(item);
        //}

    }

    /// <summary>
    /// Populates ListView control with data from the FTDI devices connected
    /// </summary>
    private void PopulateFTDIDevices()
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
            viewDevices.Items.Add(item);
        }
    }

    /// <summary>
    /// Initializes and populates with data combobox DataBits, Stop, Parity, and FlowControl
    /// </summary>
    private void InitializaControls()
    {
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

        // Populate cboDataBits
        Dictionary<string, int> cboList = new Dictionary<string, int>();
        cboList.Add("7 bits", FTDI.FT_DATA_BITS.FT_BITS_7);
        cboList.Add("8 bits", FTDI.FT_DATA_BITS.FT_BITS_8);

        cboDataBits.DataSource = new BindingSource(cboList, null);
        cboDataBits.DisplayMember = "Key";
        cboDataBits.ValueMember = "Value";

        // Populate cboStop
        cboList.Clear();
        cboList.Add("1 bit", FTDI.FT_STOP_BITS.FT_STOP_BITS_1);
        cboList.Add("2 bits", FTDI.FT_STOP_BITS.FT_STOP_BITS_2);

        cboStopBits.DataSource = new BindingSource(cboList, null);
        cboStopBits.DisplayMember = "Key";
        cboStopBits.ValueMember = "Value";

        // Populate cboParity
        cboList.Clear();
        cboList.Add("None", FTDI.FT_PARITY.FT_PARITY_NONE);
        cboList.Add("Odd", FTDI.FT_PARITY.FT_PARITY_ODD);
        cboList.Add("Even", FTDI.FT_PARITY.FT_PARITY_EVEN);
        cboList.Add("Mark", FTDI.FT_PARITY.FT_PARITY_MARK);
        cboList.Add("Space", FTDI.FT_PARITY.FT_PARITY_SPACE);

        cboParity.DataSource = new BindingSource(cboList, null);
        cboParity.DisplayMember = "Key";
        cboParity.ValueMember = "Value";

        // Populate cboFlowControl
        cboList.Clear();
        cboList.Add("None", FTDI.FT_FLOW_CONTROL.FT_FLOW_NONE);
        cboList.Add("RTS / CTS", FTDI.FT_FLOW_CONTROL.FT_FLOW_RTS_CTS);
        cboList.Add("DTR / DSR", FTDI.FT_FLOW_CONTROL.FT_FLOW_DTR_DSR);
        cboList.Add("Xon / Xoff", FTDI.FT_FLOW_CONTROL.FT_FLOW_XON_XOFF);

        cboFlowControl.DataSource = new BindingSource(cboList, null);
        cboFlowControl.DisplayMember = "Key";
        cboFlowControl.ValueMember = "Value";

        // Set control's default input-values
        //SetDefaultValues();
    }

    private void cboFlowControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboFlowControl.SelectedIndex == 3)
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

    private void chkShowDistribution_CheckedChanged(object sender, EventArgs e)
    {
        grpPlot.Enabled = chkShowDistribution.Checked;
    }

    private void SetModificationFields(AppSettings oldSettings)
    {
        if (Settings is null) return;

        // Compute modification fields
        ModifyPlots = (Settings.T10_NumberOfSensors == oldSettings.T10_NumberOfSensors) &&
                (Settings.T10_Frequency == oldSettings.T10_Frequency) &&
                (Settings.Plot_WindowPoints == oldSettings.Plot_WindowPoints) &&
                (Settings.Plot_DistIsRadar == oldSettings.Plot_DistIsRadar) &&
                (Settings.Plot_ArrayPoints == oldSettings.Plot_ArrayPoints);
        ModifyPlots = !ModifyPlots;

        ModifyArrays = (Settings.T10_NumberOfSensors == oldSettings.T10_NumberOfSensors) &&
                        (Settings.Plot_ArrayPoints == oldSettings.Plot_ArrayPoints);
        ModifyArrays = !ModifyArrays;

        ModifyDevice = (Settings.T10_LocationID == oldSettings.T10_LocationID) &&
                        (Settings.T10_BaudRate == oldSettings.T10_BaudRate) &&
                        (Settings.T10_DataBits == oldSettings.T10_DataBits) &&
                        (Settings.T10_StopBits == oldSettings.T10_StopBits) &&
                        (Settings.T10_Parity == oldSettings.T10_Parity) &&
                        (Settings.T10_FlowControl == oldSettings.T10_FlowControl) &&
                        (Settings.T10_CharOn == oldSettings.T10_CharOn) &&
                        (Settings.T10_CharOff == oldSettings.T10_CharOff);
        ModifyDevice = !ModifyDevice;
    }

    private void Accept_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.None;
        if (Settings is null) return;

        AppSettings oldSettings = new(Settings);

        // Check that a device has been selected from the list
        if (viewDevices.SelectedIndices.Count > 0)
        {
            Settings.T10_DeviceType = viewDevices.SelectedItems[0].SubItems[2].Text;
            Settings.T10_DevideID = Convert.ToInt32(viewDevices.SelectedItems[0].SubItems[3].Text, 16);
            Settings.T10_LocationID = Convert.ToInt32(viewDevices.SelectedItems[0].SubItems[4].Text, 16);
        }
        else
        {
            if (viewDevices.Items.Count == 1)
            {
                Settings.T10_DeviceType = viewDevices.Items[0].SubItems[2].Text;
                Settings.T10_DevideID = Convert.ToInt32(viewDevices.Items[0].SubItems[3].Text, 16);
                Settings.T10_LocationID = Convert.ToInt32(viewDevices.Items[0].SubItems[4].Text, 16);
            }
        }

        // Check that all texboxes have valid values
        if (!Validation.IsValidRange<int>(txtBaudRate.Text, 0, 9600, true, this)) { txtBaudRate.Focus(); txtBaudRate.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(txtOn.Text, 0, 255, true, this)) { txtOn.Focus(); txtOn.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(txtOff.Text, 0, 255, true, this)) { txtOff.Focus(); txtOff.SelectAll(); return; }
        if (!Validation.IsValidRange<double>(txtHz.Text, 0, 1000, true, this)) { txtHz.Focus(); txtHz.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(txtArrayPoints.Text, 1, Int32.MaxValue, true, this)) { txtArrayPoints.Focus(); txtArrayPoints.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(txtPlotWindow.Text, 20, Int32.MaxValue, true, this)) { txtPlotWindow.Focus(); txtPlotWindow.SelectAll(); return; }

        // Save to class settings
        Settings.T10_NumberOfSensors = (int)updSensors.Value;
        Settings.T10_BaudRate = Convert.ToInt32(txtBaudRate.Text);
        //Settings.T10_DataBits = ((KeyValuePair<string, int>)cboDataBits.SelectedItem).Value;
        //Settings.T10_StopBits = ((KeyValuePair<string, int>)cboStopBits.SelectedItem).Value;
        //Settings.T10_Parity = ((KeyValuePair<string, int>)cboParity.SelectedItem).Value;
        //Settings.T10_FlowControl = ((KeyValuePair<string, int>)cboFlowControl.SelectedItem).Value;
        Settings.T10_DataBits = Convert.ToInt32(cboDataBits.SelectedValue);
        Settings.T10_StopBits = Convert.ToInt32(cboStopBits.SelectedValue);
        Settings.T10_Parity = Convert.ToInt32(cboParity.SelectedValue);
        Settings.T10_FlowControl = Convert.ToInt32(cboFlowControl.SelectedValue);
        Settings.T10_CharOn = Convert.ToInt32(txtOn.Text);
        Settings.T10_CharOff = Convert.ToInt32(txtOff.Text);
        Settings.T10_Frequency = Convert.ToDouble(txtHz.Text);

        Settings.Plot_ArrayPoints = Convert.ToInt32(txtArrayPoints.Text);
        Settings.Plot_WindowPoints = Convert.ToInt32(txtPlotWindow.Text);
        Settings.Plot_ShowRawData = chkShowRaw.Checked;
        Settings.Plot_ShowDistribution = chkShowDistribution.Checked;
        Settings.Plot_ShowAverage = chkShowAverage.Checked;
        Settings.Plot_ShowRatios = chkShowRatio.Checked;
        Settings.Plot_DistIsRadar = radRadar.Checked;

        Settings.AppCulture = _culture;
        Settings.WindowPosition = chkWindowPos.Checked;
        Settings.RememberFileDialogPath = chkDlgPath.Checked;
        Settings.DataFormat = txtDataFormat.Text;

        //SetModificationFields(oldSettings);
        // Compute modification fields
        ModifyPlots = (Settings.T10_NumberOfSensors != oldSettings.T10_NumberOfSensors) ||
                (Settings.T10_Frequency != oldSettings.T10_Frequency) ||
                (Settings.Plot_WindowPoints != oldSettings.Plot_WindowPoints) ||
                (Settings.Plot_DistIsRadar != oldSettings.Plot_DistIsRadar) ||
                (Settings.Plot_ArrayPoints != oldSettings.Plot_ArrayPoints);

        ModifyArrays = (Settings.T10_NumberOfSensors != oldSettings.T10_NumberOfSensors) ||
                        (Settings.Plot_ArrayPoints != oldSettings.Plot_ArrayPoints);

        ModifyDevice = (Settings.T10_LocationID != oldSettings.T10_LocationID) ||
                        (Settings.T10_BaudRate != oldSettings.T10_BaudRate) ||
                        (Settings.T10_DataBits != oldSettings.T10_DataBits) ||
                        (Settings.T10_StopBits != oldSettings.T10_StopBits) ||
                        (Settings.T10_Parity != oldSettings.T10_Parity) ||
                        (Settings.T10_FlowControl != oldSettings.T10_FlowControl) ||
                        (Settings.T10_CharOn != oldSettings.T10_CharOn) ||
                        (Settings.T10_CharOff != oldSettings.T10_CharOff);

        DialogResult = DialogResult.OK;
    }

    private void Reset_Click(object sender, EventArgs e)
    {
        DialogResult result;
        using (new CenterWinDialog(this))
        {
            result = MessageBox.Show(this,
                StringResources.MsgBoxReset,
                StringResources.MsgBoxResetTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }

        if (result == DialogResult.Yes)
        {
            UpdateControls(new AppSettings());
        }
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
        if (Settings is not null)
            StringResources.Culture = Settings.AppCulture;
        else
            StringResources.Culture = CultureInfo.InvariantCulture;

        DialogResult = DialogResult.Cancel;
    }

    private void Label_Click(object sender, EventArgs e)
    {
        switch ((sender as Label)?.Name)
        {
            case "lblChkShowRaw":
                chkShowRaw.Checked = true;
                break;
            case "lblChkShowDistribution":
                chkShowDistribution.Checked = true;
                break;
            case "lblChkShowAverage":
                chkShowAverage.Checked = true;
                break;
            case "lblChkShowRatio":
                chkShowRatio.Checked = true;
                break;
        }
    }

    private void CurrentCulture_CheckedChanged(object sender, EventArgs e)
    {
        if (radCurrentCulture.Checked)
        {
            _culture = System.Globalization.CultureInfo.CurrentCulture;
            UpdateUI_Language();

            int index = cboAllCultures.SelectedIndex;
            FillDefinedCultures(_baseName, typeof(FrmLanguage).Assembly);
            cboAllCultures.SelectedIndex = index;
        }
    }

    private void InvariantCulture_CheckedChanged(object sender, EventArgs e)
    {
        if (radInvariantCulture.Checked)
        {
            _culture = System.Globalization.CultureInfo.InvariantCulture;
            UpdateUI_Language();

            int index = cboAllCultures.SelectedIndex;
            FillDefinedCultures(_baseName, typeof(FrmLanguage).Assembly);
            cboAllCultures.SelectedIndex = index;
        }
    }

    private void UserCulture_CheckedChanged(object sender, EventArgs e)
    {
        cboAllCultures.Enabled = radUserCulture.Checked;
        if (cboAllCultures.Enabled && cboAllCultures.SelectedValue is not null)
        {
            _culture = new((string)cboAllCultures.SelectedValue);
            if (_culture.Name != string.Empty)
                UpdateUI_Language();

            int index = cboAllCultures.SelectedIndex;
            FillDefinedCultures(_baseName, typeof(FrmLanguage).Assembly);
            cboAllCultures.SelectedIndex = index;
        }
    }

    private void AllCultures_SelectionChangeCommitted(object sender, EventArgs e)
    {
        var cbo = sender as ComboBox;
        if (cbo is not null && cbo.Items.Count > 0 && cbo.SelectedValue is not null)
        {
            _culture = new((string)cbo.SelectedValue);
            UpdateUI_Language();
            FillDefinedCultures(_baseName, typeof(FrmLanguage).Assembly);
        }
    }

    /// <summary>
    /// Updates the form's controls with values from the settings class
    /// </summary>
    /// <param name="settings">Class containing the values to show on the form's controls</param>
    private void UpdateControls(AppSettings settings)
    {
        cboDataBits.SelectedValue = settings.T10_DataBits;
        cboStopBits.SelectedValue = settings.T10_StopBits;
        cboParity.SelectedValue = settings.T10_Parity;
        cboFlowControl.SelectedValue = settings.T10_FlowControl;

        txtBaudRate.Text = settings.T10_BaudRate.ToString();
        txtOn.Text = settings.T10_CharOn.ToString();
        txtOff.Text = settings.T10_CharOff.ToString();
        txtHz.Text = settings.T10_Frequency.ToString();

        updSensors.Value = settings.T10_NumberOfSensors;

        txtArrayPoints.Text = settings.Plot_ArrayPoints.ToString();
        txtPlotWindow.Text = settings.Plot_WindowPoints.ToString();

        chkShowRaw.Checked = settings.Plot_ShowRawData;
        chkShowDistribution.Checked = settings.Plot_ShowDistribution;
        chkShowAverage.Checked = settings.Plot_ShowAverage;
        chkShowRatio.Checked = settings.Plot_ShowRatios;
        if (settings.Plot_DistIsRadar)
            radRadar.Checked = true;
        else
            radRadial.Checked = true;

        cboAllCultures.Enabled = false;
        if (_culture.Name == string.Empty)
            radInvariantCulture.Checked = true;
        else if (_culture.Name == System.Globalization.CultureInfo.CurrentCulture.Name)
            radCurrentCulture.Checked = true;
        else
        {
            cboAllCultures.SelectedValue = _culture.Name;
            radUserCulture.Checked = true;
        }

        chkWindowPos.Checked = settings.WindowPosition;
        chkDlgPath.Checked = settings.RememberFileDialogPath;
        txtDataFormat.Text = settings.DataFormat;
    }

    /// <summary>
    /// Databind only the cultures found in .resources files for a given type
    /// </summary>
    /// <param name="type">A type from which the resource manager derives all information for finding .resources files</param>
    private void FillDefinedCultures(string baseName, System.Reflection.Assembly assembly)
    {
        string cultureName = _culture.Name;
        //string _cultureUI = CultureInfo.CurrentUICulture.Name;

        // Retrieve the culture list using the culture currently selected. The UI culture needs to be temporarily changed
        CultureInfo.CurrentUICulture = new CultureInfo(cultureName);
        var cultures = System.Globalization.GlobalizationUtilities.GetAvailableCultures(baseName, assembly);

        cboAllCultures.DisplayMember = "DisplayName";
        cboAllCultures.ValueMember = "Name";
        cboAllCultures.DataSource = cultures.ToArray();
        cboAllCultures.SelectedValue = cultureName;

        // Reset the UI culture to its previous value
        //CultureInfo.CurrentUICulture = new(_cultureUI);
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language()
    {
        UpdateUI_Language(_culture);
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
        this.chkWindowPos.Text = StringResources.ChkWindowPos;
        //this.chkDlgPath.Text = StringResources.ChkDlgPath;
        this.lblDlgPath.Text = StringResources.ChkDlgPath;
        this.lblDataFormat.Text = StringResources.LblDataFormat;

        // Relocate controls
        RelocateControls();
    }

    /// <summary>
    /// Relocate controls to compensate for the culture text length in labels
    /// </summary>
    private void RelocateControls()
    {
        this.SuspendLayout();

        // Tab T-10A
        int nBaud = this.lblBaudRate.Left + this.lblBaudRate.Width + this.txtBaudRate.Width;
        int nData = this.lblDataBits.Left + this.lblDataBits.Width + this.cboDataBits.Width;
        int nParity = this.lblParity.Left + this.lblParity.Width + this.cboParity.Width;
        int nSensors = this.lblSensors.Left + this.lblSensors.Width + this.updSensors.Width;
        int nMaxRight = Math.Max(Math.Max(nBaud, nData), Math.Max(nParity, nSensors)) + 3;
        this.lblBaudRate.Top = this.txtBaudRate.Top + (txtBaudRate.Height - lblBaudRate.Height) / 2;
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
        this.cboAllCultures.Width = Math.Min(190, grpCulture.Width - 2 * this.radUserCulture.Left - 5 - this.radUserCulture.Width);
        this.cboAllCultures.Left = 5 + this.radUserCulture.Left + this.radUserCulture.Width;
        this.chkDlgPath.Top = 1 + this.lblDlgPath.Top + (lblDlgPath.Height - chkDlgPath.Height) / 2;
        this.txtDataFormat.Left = 5 + this.lblDataFormat.Left + this.lblDataFormat.Width;
        this.lblDataFormat.Top = this.txtDataFormat.Top + (txtDataFormat.Height - lblDataFormat.Height) / 2;

        this.ResumeLayout(false);
    }

}
