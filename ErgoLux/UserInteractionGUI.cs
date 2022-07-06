using FTD2XX_NET;

namespace ErgoLux;

partial class FrmMain
{
    private void Exit_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void Save_CheckedChanged(object sender, EventArgs e)
    {
        this.mnuMainFrm_File_Save.Enabled = toolStripMain_Save.Checked;
    }

    private void Save_Click(object sender, EventArgs e)
    {
        DialogResult result;
        string filePath;

        // Exit if no data has been received or the matrices are still un-initialized
        if (_nPoints == 0 || _plotData == null)
        {
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(StringsRM.GetString("strMsgBoxNoData", _settings.AppCulture) ?? "There is no data available to be saved.",
                    StringsRM.GetString("strMsgBoxNoDataTitle", _settings.AppCulture) ?? "No data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            return;
        }

        // Displays a SaveFileDialog, so the user can save the data into a file  
        SaveFileDialog SaveDlg = new()
        {
            DefaultExt = "*.elux",
            Filter = StringsRM.GetString("strSaveDlgFilter", _settings.AppCulture) ?? "ErgoLux file (*.elux)|*.elux|Text file (*.txt)|*.txt|Binary file (*.bin)|*.bin|All files (*.*)|*.*",
            FilterIndex = 1,
            Title = StringsRM.GetString("strSaveDlgTitle", _settings.AppCulture) ?? "Save illuminance data",
            OverwritePrompt = true,
            InitialDirectory = _settings.RememberFileDialogPath ? _settings.UserSavePath : _settings.DefaultSavePath,
        };

        using (new CenterWinDialog(this))
            result = SaveDlg.ShowDialog(this.Parent);

        var cursor = Cursor.Current;
        // If the file name is not an empty string, call the corresponding routine to save the data into a file.  
        if (result == DialogResult.OK && SaveDlg.FileName != "")
        {
            // Show waiting cursor
            Cursor.Current = Cursors.WaitCursor;

            //Get the path of specified file and store the directory for future calls
            filePath = SaveDlg.FileName;
            if (_settings.RememberFileDialogPath) _settings.UserSavePath = Path.GetDirectoryName(filePath) ?? string.Empty;

            switch (Path.GetExtension(SaveDlg.FileName).ToLower())
            {
                case ".elux":
                    SaveELuxData(SaveDlg.FileName);
                    break;
                case ".txt":
                    SaveTextData(SaveDlg.FileName);
                    break;
                case ".bin":
                    SaveBinaryData(SaveDlg.FileName);
                    break;
                default:
                    SaveDefaultData(SaveDlg.FileName);
                    break;
            }

            SetFormTitle(this, SaveDlg.FileName);

            // Restore cursor
            Cursor.Current = cursor;
        }
    }

    private void Open_Click(object sender, EventArgs e)
    {
        DialogResult result;
        string filePath;

        OpenFileDialog OpenDlg = new()
        {
            DefaultExt = "*.elux",
            Filter = StringsRM.GetString("strOpenDlgFilter", _settings.AppCulture) ?? "ErgoLux file (*.elux)|*.elux|Text file (*.txt)|*.txt|Binary file (*.bin)|*.bin|All files (*.*)|*.*",
            FilterIndex = 1,
            Title = StringsRM.GetString("strOpenDlgTitle", _settings.AppCulture) ?? "Open illuminance data",
            InitialDirectory = _settings.RememberFileDialogPath ? _settings.UserOpenPath : _settings.DefaultOpenPath
        };

        using (new CenterWinDialog(this))
            result = OpenDlg.ShowDialog(this);

        // If the file name is not an empty string open it for saving.
        var cursor = Cursor.Current;
        bool readOK = false;
        if (result == DialogResult.OK && OpenDlg.FileName != "")
        {
            // Show waiting cursor
            Cursor.Current = Cursors.WaitCursor;

            //Get the path of specified file and store the directory for future calls
            filePath = OpenDlg.FileName;
            if (_settings.RememberFileDialogPath) _settings.UserOpenPath = Path.GetDirectoryName(filePath) ?? string.Empty;

            // Read the data file in the corresponding format
            switch (Path.GetExtension(OpenDlg.FileName).ToLower())
            {
                case ".elux":
                    readOK = OpenELuxData(OpenDlg.FileName);
                    break;
                case ".txt":
                    readOK = OpenTextData(OpenDlg.FileName);
                    break;
                case ".bin":
                    readOK = OpenBinaryData(OpenDlg.FileName);
                    break;
                default:
                    //OpenDefaultData(OpenDlg.FileName);
                    break;
            }
        }

        if (readOK)
        {
            SetFormTitle(this, OpenDlg.FileName);
            // Show data into plots
            Plots_FetchData();
        }

        // Restore cursor
        Cursor.Current = cursor;
    }

    private void Connect_CheckedChanged(object sender, EventArgs e)
    {
        if (toolStripMain_Connect.Checked == true)
        {
            // Although unnecessary because the ToolStripButton should be disabled, make sure the device is already open
            if (myFtdiDevice == null || myFtdiDevice.IsOpen == false)
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("The device is closed. Please, go to\n'Settings' to open the device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                toolStripMain_Connect.Checked = false;
                mnuMainFrm_Tools_Connect.Enabled = false;
                return;
            }

            mnuMainFrm_Tools_Disconnect.Enabled = true;
            toolStripMain_Disconnect.Enabled = true;
            toolStripMain_Open.Enabled = false;
            toolStripMain_Save.Enabled = false;
            toolStripMain_Settings.Enabled = false;
            toolStripMain_About.Enabled = false;
            this.statusStripIconExchange.Image = _settings.Icon_Data;
            SetFormTitle(this);
            _reading = true;

            myFtdiDevice.DataReceived += OnDataReceived;
            if (myFtdiDevice.Write(ClassT10.Command_54))
            {
                _timeStart = DateTime.Now;
                m_timer.Start();
                m_timer.Stop();
            }
        }
        else if (toolStripMain_Connect.Checked == false)
        {
            mnuMainFrm_Tools_Disconnect.Enabled = false;
            toolStripMain_Disconnect.Enabled = false;
            toolStripMain_Open.Enabled = true;
            toolStripMain_Save.Enabled = true;
            toolStripMain_Settings.Enabled = true;
            toolStripMain_About.Enabled = true;
            this.statusStripIconExchange.Image = null;
            _reading = false;
        }
    }

    private void Disconnect_Click(object sender, EventArgs e)
    {
        // Stop receiving data
        m_timer.Stop();
        _timeEnd = DateTime.Now;
        if (myFtdiDevice is not null) myFtdiDevice.DataReceived -= OnDataReceived;

        // Update GUI
        toolStripMain_Connect.Checked = false;
        Plots_ShowFull();
    }

    private void Settings_Click(object sender, EventArgs e)
    {
        FTDI.FT_STATUS result;

        FrmSettings frm = new(_settings);
        frm.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        frm.ShowDialog();

        if (frm.DialogResult == DialogResult.OK)
        {
            UpdateUI_Language(_settings.T10_NumberOfSensors + _settings.ArrayFixedColumns);

            // If a device is selected, then set up the parameters
            if (_settings.T10_LocationID > 0)
            {
                this.toolStripMain_Connect.Enabled = true;

                if (myFtdiDevice != null && myFtdiDevice.IsOpen)
                    myFtdiDevice.Close();

                myFtdiDevice = new FTDISample();
                result = myFtdiDevice.OpenDevice(location: (uint)_settings.T10_LocationID,
                    baud: _settings.T10_BaudRate,
                    dataBits: _settings.T10_DataBits,
                    stopBits: _settings.T10_StopBits,
                    parity: _settings.T10_Parity,
                    flowControl: _settings.T10_FlowControl,
                    xOn: _settings.T10_CharOn,
                    xOff: _settings.T10_CharOff,
                    readTimeOut: 0,
                    writeTimeOut: 0);

                if (result == FTDI.FT_STATUS.FT_OK)
                {
                    // Set the timer interval according to the sampling frecuency
                    m_timer.Interval = 1000 / _settings.T10_Frequency;

                    // Update the status strip with information
                    this.statusStripLabelLocation.Text = (StringsRM.GetString("strStatusLocation", _settings.AppCulture) ?? "Location ID") + $": {_settings.T10_LocationID:X}";
                    this.statusStripLabelType.Text = (StringsRM.GetString("strStatusType", _settings.AppCulture) ?? "Device type") + $": {frm.GetDeviceType}";
                    this.statusStripLabelID.Text = (StringsRM.GetString("strStatusID", _settings.AppCulture) ?? "Device ID") + $": {frm.GetDeviceID}";
                    this.statusStripIconOpen.Image = _settings.Icon_Open;

                    InitializeStatusStripLabelsStatus();
                    InitializeArrays();     // Initialize the arrays containing the data
                    Plots_Clear();          // First, clear all data (if any) in the plots
                    Plots_DataBinding();    // Bind the arrays to the plots
                    Plots_ShowLegends();    // Show the legends in the picture boxes
                }
                else
                {
                    this.statusStripIconOpen.Image = _settings.Icon_Close;
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show(StringsRM.GetString("strMsgBoxErrorOpenDevice", _settings.AppCulture) ?? "Could not open the device",
                            StringsRM.GetString("strMsgBoxErrorOpenDeviceTitle", _settings.AppCulture) ?? "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

            } // End _sett.T10_LocationID
            else
            {
                InitializeStatusStripLabelsStatus();

                if (_plotData.Length > 0 && _plotRadar.Length > 0 && _plotRadialGauge.Length > 0)
                    Plots_FetchData();
            }

        }   // End DialogResult.OK

    }

    private void About_Click(object sender, EventArgs e)
    {
        var frm = new FrmAbout();
        frm.ShowDialog();
    }

    private void Language_Click(object sender, EventArgs e)
    {
        FrmLanguage frm = new(_settings);
        frm.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        frm.ShowDialog();

        if (frm.DialogResult == DialogResult.OK)
            UpdateUI_Language(_plotData.Length);
    }

    private void statusStripLabelEx_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is ToolStripStatusLabelEx)
        {
            var label = (ToolStripStatusLabelEx)sender;

            // Change the text color
            if (label.Checked)
                label.ForeColor = Color.Black;
            else
                label.ForeColor = Color.LightGray;
        }
    }

    private void statusStripLabelPlots_Click(object sender, EventArgs e)
    {
        if (sender is ToolStripStatusLabelEx)
        {
            var label = (ToolStripStatusLabelEx)sender;
            label.Checked = !label.Checked;

            // Update the settings
            switch (label.Text)
            {
                case "W":
                    _settings.Plot_ShowRawData = label.Checked;
                    mnuMainFrm_View_Raw.Checked = label.Checked;
                    break;
                case "D":
                    _settings.Plot_ShowDistribution = label.Checked;
                    mnuMainFrm_View_Distribution.Checked = label.Checked;
                    break;
                case "A":
                    _settings.Plot_ShowAverage = label.Checked;
                    mnuMainFrm_View_Average.Checked = label.Checked;
                    break;
                case "R":
                    _settings.Plot_ShowRatios = label.Checked;
                    mnuMainFrm_View_Ratio.Checked = label.Checked;
                    break;
            }
        }
    }

    private void statusStripLabelCross_Click(object sender, EventArgs e)
    {
        if (sender is ToolStripStatusLabelEx)
        {
            var label = (ToolStripStatusLabelEx)sender;
            if (plotData.Plot.GetPlottables().Length > 0 && plotStats.Plot.GetPlottables().Length > 0 && plotRatio.Plot.GetPlottables().Length > 0)
            {
                label.Checked = !label.Checked;
                plotData.ShowCrossHair = label.Checked;
                plotData.Refresh();
                plotStats.ShowCrossHair = label.Checked;
                plotStats.Refresh();
                plotRatio.ShowCrossHair = label.Checked;
                plotRatio.Refresh();
            }
        }
    }

    private void mnuMainFrm_View_Menu_Click(object sender, EventArgs e)
    {
        bool status = !this.mnuMainFrm_View_Menu.Checked;
        this.mnuMainFrm_View_Menu.Checked = status;
        this.mnuMainFrm.Visible = status;
    }
    private void mnuMainFrm_View_Toolbar_Click(object sender, EventArgs e)
    {
        bool status = !this.mnuMainFrm_View_Toolbar.Checked;
        this.mnuMainFrm_View_Toolbar.Checked = status;
        this.toolStripMain.Visible = status;
    }
    private void mnuMainFrm_View_Raw_Click(object sender, EventArgs e)
    {
        bool status = !this.mnuMainFrm_View_Raw.Checked;
        this.mnuMainFrm_View_Raw.Checked = status;
        this.statusStripLabelRaw.Checked = status;
        _settings.Plot_ShowRawData = status;
    }
    private void mnuMainFrm_View_Radial_Click(object sender, EventArgs e)
    {
        bool status = !this.mnuMainFrm_View_Distribution.Checked;
        this.mnuMainFrm_View_Distribution.Checked = status;
        this.statusStripLabelRadar.Checked = status;
        _settings.Plot_ShowDistribution = status;
    }
    private void mnuMainFrm_View_Average_Click(object sender, EventArgs e)
    {
        bool status = !this.mnuMainFrm_View_Average.Checked;
        this.mnuMainFrm_View_Average.Checked = status;
        this.statusStripLabelMax.Checked = status;
        _settings.Plot_ShowAverage = status;
    }
    private void mnuMainFrm_View_Ratio_Click(object sender, EventArgs e)
    {
        bool status = !this.mnuMainFrm_View_Ratio.Checked;
        this.mnuMainFrm_View_Ratio.Checked = status;
        this.statusStripLabelRatio.Checked = status;
        _settings.Plot_ShowRatios = status;
    }
    private void mnuMainFrm_Tools_Connect_Click(object sender, EventArgs e)
    {
        bool status = !this.toolStripMain_Connect.Checked;
        this.mnuMainFrm_Tools_Connect.Checked = status;
        this.toolStripMain_Connect.Checked = status;
    }

}

