using System.Collections.Generic;
using System.Linq;
using FTD2XX_NET;

namespace ErgoLux;

public partial class FrmSettings : Form
{
    private string _deviceType = String.Empty;
    private string _deviceID = String.Empty;
    private ClassSettings _settings = new();
    private readonly System.Resources.ResourceManager StringsRM = new("ErgoLux.localization.strings", typeof(FrmSettings).Assembly);

    public string GetDeviceType { get => _deviceType; }
    public string GetDeviceID { get => _deviceID; }

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

        //
        FillDefinedCultures("ErgoLux.localization.strings", typeof(FrmMain).Assembly);
        UpdateUI_Language();
    }

    public FrmSettings(ClassSettings settings)
        : this()
    {
        UpdateControls(settings);
    }

    /// <summary>
    /// Populates ListView control with data from the FTDI devices connected
    /// </summary<
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
            viewDevices.Items.Add(item);
        }
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
        groupBox1.Enabled = chkShowDistribution.Checked;
    }

    private void Accept_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.None;

        // Check that a device has been selected from the list
        if (viewDevices.SelectedIndices.Count > 0)
        {
            _deviceType = viewDevices.SelectedItems[0].SubItems[2].Text;
            _deviceID = viewDevices.SelectedItems[0].SubItems[3].Text;
            _settings.T10_LocationID = Convert.ToInt32(viewDevices.SelectedItems[0].SubItems[4].Text, 16);
        }

        // Check that all texboxes have valid values
        if (!Validation.IsValidRange<int>(txtBaudRate.Text, 0, 9600, true, this)) { txtBaudRate.Focus(); txtBaudRate.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(txtOn.Text, 0, 255, true, this)) { txtOn.Focus(); txtOn.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(txtOff.Text, 0, 255, true, this)) { txtOff.Focus(); txtOff.SelectAll(); return; }
        if (!Validation.IsValidRange<double>(txtHz.Text, 0, 1000, true, this)) { txtHz.Focus(); txtHz.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(txtArrayPoints.Text, 1, Int32.MaxValue, true, this)) { txtArrayPoints.Focus(); txtArrayPoints.SelectAll(); return; }
        if (!Validation.IsValidRange<int>(txtPlotWindow.Text, 20, Int32.MaxValue, true, this)) { txtPlotWindow.Focus(); txtPlotWindow.SelectAll(); return; }

        // Save to class settings
        //_settings.T10_LocationID = Convert.ToInt32(viewDevices.SelectedItems[0].SubItems[4].Text, 16);
        _settings.T10_NumberOfSensors = (int)updSensors.Value;
        _settings.T10_BaudRate = Convert.ToInt32(txtBaudRate.Text);
        _settings.T10_DataBits = ((KeyValuePair<string, int>)cboDataBits.SelectedItem).Value;
        _settings.T10_StopBits = ((KeyValuePair<string, int>)cboStopBits.SelectedItem).Value;
        _settings.T10_Parity = ((KeyValuePair<string, int>)cboParity.SelectedItem).Value;
        _settings.T10_FlowControl = ((KeyValuePair<string, int>)cboFlowControl.SelectedItem).Value;
        _settings.T10_CharOn = Convert.ToInt32(txtOn.Text);
        _settings.T10_CharOff = Convert.ToInt32(txtOff.Text);
        _settings.T10_Frequency = Convert.ToDouble(txtHz.Text);

        _settings.Plot_ArrayPoints = Convert.ToInt32(txtArrayPoints.Text);
        _settings.Plot_WindowPoints = Convert.ToInt32(txtPlotWindow.Text);
        _settings.Plot_ShowRawData = chkShowRaw.Checked;
        _settings.Plot_ShowDistribution = chkShowDistribution.Checked;
        _settings.Plot_ShowAverage = chkShowAverage.Checked;
        _settings.Plot_ShowRatios = chkShowRatio.Checked;
        _settings.Plot_DistIsRadar = radRadar.Checked;

        //if (radCurrentCulture.Checked) _settings.AppCulture = System.Globalization.CultureInfo.CurrentCulture;
        //else if (radInvariantCulture.Checked) _settings.AppCulture = System.Globalization.CultureInfo.InvariantCulture;
        //else if (radUserCulture.Checked) _settings.AppCulture = System.Globalization.CultureInfo.CreateSpecificCulture((string)cboAllCultures.SelectedValue);
        _settings.RememberFileDialogPath = chkDlgPath.Checked;
        _settings.DataFormat = txtDataFormat.Text;

        this.DialogResult = DialogResult.OK;
    }

    private void Reset_Click(object sender, EventArgs e)
    {
        DialogResult result;
        using (new CenterWinDialog(this))
        {
            result = MessageBox.Show(StringsRM.GetString("strMsgBoxReset", _settings.AppCulture) ?? "Do you want to reset all fields" + Environment.NewLine + "to their default values?",
                StringsRM.GetString("strMsgBoxResetTitle", _settings.AppCulture) ?? "Reset?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }

        if (result == DialogResult.Yes)
        {
            UpdateControls(new ClassSettings());
            //SetDefaultValues();
        }
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void CurrentCulture_CheckedChanged(object sender, EventArgs e)
    {
        if (radCurrentCulture.Checked)
        {
            _settings.AppCulture = System.Globalization.CultureInfo.CurrentCulture;
            UpdateUI_Language();
        }
    }

    private void InvariantCulture_CheckedChanged(object sender, EventArgs e)
    {
        if (radInvariantCulture.Checked)
        {
            _settings.AppCulture = System.Globalization.CultureInfo.InvariantCulture;
            UpdateUI_Language();
        }
    }

    private void UserCulture_CheckedChanged(object sender, EventArgs e)
    {
        cboAllCultures.Enabled = radUserCulture.Checked;
        if (cboAllCultures.Enabled)
        {
            _settings.AppCulture = new((string)cboAllCultures.SelectedValue);
            UpdateUI_Language();
        }
    }

    private void AllCultures_SelectedValueChanged(object sender, EventArgs e)
    {
        var cbo = sender as ComboBox;
        if (cbo is not null && cbo.Items.Count > 0 && cbo.SelectedValue is not null)
        {
            _settings.AppCulture = new((string)cbo.SelectedValue ?? String.Empty);
            UpdateUI_Language();
        }
    }

    /// <summary>
    /// Updates the form's controls with values from the settings class
    /// </summary>
    /// <param name="settings">Class containing the values to show on the form's controls</param>
    private void UpdateControls(ClassSettings settings)
    {
        _settings = settings;

        cboDataBits.SelectedValue = _settings.T10_DataBits;
        cboStopBits.SelectedValue = _settings.T10_StopBits;
        cboParity.SelectedValue = _settings.T10_Parity;
        cboFlowControl.SelectedValue = _settings.T10_FlowControl;

        txtBaudRate.Text = _settings.T10_BaudRate.ToString();
        txtOn.Text = _settings.T10_CharOn.ToString();
        txtOff.Text = _settings.T10_CharOff.ToString();
        txtHz.Text = _settings.T10_Frequency.ToString();

        updSensors.Value = _settings.T10_NumberOfSensors;

        txtArrayPoints.Text = _settings.Plot_ArrayPoints.ToString();
        txtPlotWindow.Text = _settings.Plot_WindowPoints.ToString();

        chkShowRaw.Checked = _settings.Plot_ShowRawData;
        chkShowDistribution.Checked = _settings.Plot_ShowDistribution;
        chkShowAverage.Checked = _settings.Plot_ShowAverage;
        chkShowRatio.Checked = _settings.Plot_ShowRatios;
        if (_settings.Plot_DistIsRadar)
            radRadar.Checked = true;
        else
            radRadial.Checked = true;

        cboAllCultures.Enabled = false;
        if (_settings.AppCultureName == string.Empty)
            radInvariantCulture.Checked = true;
        else if (_settings.AppCultureName == System.Globalization.CultureInfo.CurrentCulture.Name)
            radCurrentCulture.Checked = true;
        else
            radUserCulture.Checked = true;
        cboAllCultures.SelectedValue = _settings.AppCultureName;

        chkDlgPath.Checked = _settings.RememberFileDialogPath;
        txtDataFormat.Text = _settings.DataFormat;
    }

    /// <summary>
    /// Databind only the cultures found in .resources files for a given type
    /// </summary>
    /// <param name="type">A type from which the resource manager derives all information for finding .resources files</param>
    private void FillDefinedCultures(string baseName, System.Reflection.Assembly assembly)
    {
        var cultures = System.Globalization.GlobalizationUtilities.GetAvailableCultures(baseName, assembly);
        cboAllCultures.DisplayMember = "DisplayName";
        cboAllCultures.ValueMember = "Name";
        cboAllCultures.DataSource = cultures.ToArray();
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language()
    {
        UpdateUI_Language(_settings.AppCulture);
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        this.Text = StringsRM.GetString("strFrmSettings", culture) ?? "Settings";

        this.tabDevice.Text = StringsRM.GetString("strTabDevice", culture) ?? "T-10A";
        this.tabPlots.Text = StringsRM.GetString("strTabPlots", culture) ?? "Plotting";
        this.tabGUI.Text = StringsRM.GetString("strTabGUI", culture) ?? "User interface";

        this.grpCulture.Text = StringsRM.GetString("strGrpCulture", culture) ?? "UI and data format";
        this.radCurrentCulture.Text = (StringsRM.GetString("strRadCurrentCulture", culture) ?? "Current culture formatting") + $" ({System.Globalization.CultureInfo.CurrentCulture.Name})";
        this.radInvariantCulture.Text = StringsRM.GetString("strRadInvariantCulture", culture) ?? "Invariant culture formatting";
        this.radUserCulture.Text = StringsRM.GetString("strRadUserCulture", culture) ?? "Select culture";
        this.chkDlgPath.Text = StringsRM.GetString("strChkDlgPath", culture) ?? "Remember open/save dialog previous path";
        this.lblDataFormat.Text = StringsRM.GetString("strLblDataFormat", culture) ?? "Numeric data-formatting string";

        this.btnReset.Text = StringsRM.GetString("strBtnReset", culture) ?? "&Reset";
        this.btnCancel.Text = StringsRM.GetString("strBtnCancel", culture) ?? "&Cancel";
        this.btnAccept.Text = StringsRM.GetString("strBtnAccept", culture) ?? "&Accept";
    }

}
