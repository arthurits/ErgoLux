using System.Globalization;
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
        this.mnuMainFrm_File_Save.Enabled = this.toolStripMain_Save.Checked;
    }

    private void Save_Click(object sender, EventArgs e)
    {
        DialogResult result;
        string filePath;

        // Exit if no data has been received or the matrices are still un-initialized
        if (this._nPoints == 0 || this._plotData == null)
        {
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(StringResources.MsgBoxNoData,
                    StringResources.MsgBoxNoDataTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            return;
        }

        // Displays a SaveFileDialog, so the user can save the data into a file  
        SaveFileDialog SaveDlg = new()
        {
            DefaultExt = "*.elux",
            Filter = StringResources.SaveDlgFilter,
            FilterIndex = 1,
            Title = StringResources.SaveDlgTitle,
            OverwritePrompt = true,
            InitialDirectory = this._settings.RememberFileDialogPath ? this._settings.UserSavePath : this._settings.DefaultSavePath,
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
            if (this._settings.RememberFileDialogPath) this._settings.UserSavePath = Path.GetDirectoryName(filePath) ?? string.Empty;

            switch (Path.GetExtension(SaveDlg.FileName).ToLower())
            {
                case ".elux":
                    this.SaveELuxData(SaveDlg.FileName);
                    break;
                case ".txt":
                    this.SaveTextData(SaveDlg.FileName);
                    break;
                case ".bin":
                    this.SaveBinaryData(SaveDlg.FileName);
                    break;
                default:
                    this.SaveDefaultData(SaveDlg.FileName);
                    break;
            }

            this.SetFormTitle(this, SaveDlg.FileName);

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
            Filter = StringResources.OpenDlgFilter,
            FilterIndex = 1,
            Title = StringResources.OpenDlgTitle,
            InitialDirectory = this._settings.RememberFileDialogPath ? this._settings.UserOpenPath : this._settings.DefaultOpenPath
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
            if (this._settings.RememberFileDialogPath) this._settings.UserOpenPath = Path.GetDirectoryName(filePath) ?? string.Empty;

            // Read the data file in the corresponding format
            switch (Path.GetExtension(OpenDlg.FileName).ToLower())
            {
                case ".elux":
                    readOK = this.OpenELuxData(OpenDlg.FileName);
                    break;
                case ".txt":
                    readOK = this.OpenTextData(OpenDlg.FileName);
                    break;
                case ".bin":
                    readOK = this.OpenBinaryData(OpenDlg.FileName);
                    break;
                default:
                    //OpenDefaultData(OpenDlg.FileName);
                    break;
            }
        }

        if (readOK)
        {
            this.SetFormTitle(this, OpenDlg.FileName);

            // Show data into plots
            this.Plots_FetchData();

            // Show measuring time in the status bar
            this.UpdateUI_MeasuringTime();
        }

        // Restore cursor
        Cursor.Current = cursor;
    }

    private void Connect_CheckedChanged(object sender, EventArgs e)
    {
        if (this.toolStripMain_Connect.Checked == true)
        {
            // Although unnecessary because the ToolStripButton should be disabled, make sure the device is already open
            if (this.myFtdiDevice == null || this.myFtdiDevice.IsOpen == false)
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show(StringResources.MsgBoxErrorDeviceClosed,
                        StringResources.MsgBoxErrorDeviceClosedTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                this.toolStripMain_Connect.Checked = false;
                this.mnuMainFrm_Tools_Connect.Enabled = false;
                return;
            }

            this.mnuMainFrm_Tools_Disconnect.Enabled = true;
            this.toolStripMain_Disconnect.Enabled = true;
            this.toolStripMain_Open.Enabled = false;
            this.toolStripMain_Save.Enabled = false;
            this.toolStripMain_Settings.Enabled = false;
            this.toolStripMain_About.Enabled = false;
            this.statusStripIconExchange.Image = this._settings.Icon_Data;
            this.SetFormTitle(this);
            this._reading = true;

            this.myFtdiDevice.DataReceived += this.OnDataReceived;
            if (this.myFtdiDevice.Write(ClassT10.Command_54))
            {
                this._timeStart = DateTime.Now;
                this.m_timer.Start();
                //m_timer.Stop();
            }
        }
        else if (this.toolStripMain_Connect.Checked == false)
        {
            this.mnuMainFrm_Tools_Disconnect.Enabled = false;
            this.toolStripMain_Disconnect.Enabled = false;
            this.toolStripMain_Open.Enabled = true;
            this.toolStripMain_Save.Enabled = true;
            this.toolStripMain_Settings.Enabled = true;
            this.toolStripMain_About.Enabled = true;
            this.statusStripIconExchange.Image = null;
            this._reading = false;
        }
    }

    private void Disconnect_Click(object sender, EventArgs e)
    {
        // Stop receiving data
        this.m_timer.Stop();
        this._timeEnd = DateTime.Now;
        if (this.myFtdiDevice is not null) this.myFtdiDevice.DataReceived -= this.OnDataReceived;

        // Update GUI
        this.toolStripMain_Connect.Checked = false;
        this.Plots_ShowFull();
        this.Plots_Refresh();
        this.UpdateUI_MeasuringTime();
    }

    private void Settings_Click(object sender, EventArgs e)
    {
        FTDI.FT_STATUS result = FTDI.FT_STATUS.FT_DEVICE_NOT_OPENED;
        int _locationID = this._settings.T10_LocationID;
        int _baudRate = this._settings.T10_BaudRate;
        int _dataBits = this._settings.T10_DataBits;
        int _stopBits = this._settings.T10_StopBits;
        int _parity = this._settings.T10_Parity;
        int _flowControl = this._settings.T10_FlowControl;
        int _charOn = this._settings.T10_CharOn;
        int _charOff = this._settings.T10_CharOff;
        //bool ModifyDevice = false;
        int _numberOfSensors = this._settings.T10_NumberOfSensors;
        double _frequency = this._settings.T10_Frequency;
        int _windowPoints = this._settings.Plot_WindowPoints;
        bool _isRadar = this._settings.Plot_DistIsRadar;
        int _arrayPoints = this._settings.Plot_ArrayPoints;
        bool ModifyPlots = false;
        bool ModifyArrays = false;
        bool InitializeArrays = false;
        string _cultureName = this._settings.AppCultureName;


        FrmSettings frm = new(this._settings);
        frm.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        frm.ShowDialog();

        if (frm.DialogResult == DialogResult.OK)
        {
            //ModifyDevice = (_locationID == _settings.T10_LocationID) &&
            //                (_baudRate == _settings.T10_BaudRate) &&
            //                (_dataBits == _settings.T10_DataBits) &&
            //                (_stopBits == _settings.T10_StopBits) &&
            //                (_parity == _settings.T10_Parity) &&
            //                (_flowControl == _settings.T10_FlowControl) &&
            //                (_charOn == _settings.T10_CharOn) &&
            //                (_charOff == _settings.T10_CharOff);
            //ModifyDevice = !ModifyDevice;

            ModifyPlots = (_numberOfSensors == this._settings.T10_NumberOfSensors) &&
                (_frequency == this._settings.T10_Frequency) &&
                (_windowPoints == this._settings.Plot_WindowPoints) &&
                (_isRadar == this._settings.Plot_DistIsRadar) &&
                (_arrayPoints == this._settings.Plot_ArrayPoints);
            ModifyPlots = !ModifyPlots;

            ModifyArrays = (_numberOfSensors == this._settings.T10_NumberOfSensors) && (_arrayPoints == this._settings.Plot_ArrayPoints);
            ModifyArrays = !ModifyArrays;

            InitializeArrays = (this._plotData.Length > 0) && (this._plotRadar.Length > 0) && (this._plotRadialGauge.Length > 0) && (this._seriesLabels.Length > 0);
            InitializeArrays = !InitializeArrays;

            if (this._settings.T10_LocationID > 0 && _locationID != this._settings.T10_LocationID)
            {
                if (this.myFtdiDevice != null && this.myFtdiDevice.IsOpen)
                    this.myFtdiDevice.Close();

                this.myFtdiDevice = new FTDISample();
                result = this.myFtdiDevice.OpenDevice(location: (uint)this._settings.T10_LocationID,
                    baud: this._settings.T10_BaudRate,
                    dataBits: this._settings.T10_DataBits,
                    stopBits: this._settings.T10_StopBits,
                    parity: this._settings.T10_Parity,
                    flowControl: this._settings.T10_FlowControl,
                    xOn: this._settings.T10_CharOn,
                    xOff: this._settings.T10_CharOff,
                    readTimeOut: 0,
                    writeTimeOut: 0);

                if (result == FTDI.FT_STATUS.FT_OK)
                {
                    this.toolStripMain_Connect.Enabled = true;

                    // Update the status strip with information
                    this.statusStripLabelLocation.Text = StringResources.StatusLocation + $": {this._settings.T10_LocationID:X}";
                    this.statusStripLabelType.Text = StringResources.StatusType + $": {this._settings.T10_DeviceType}";
                    this.statusStripLabelID.Text = StringResources.StatusID + $": {this._settings.T10_DevideID.ToString("0:X")}";
                    this.statusStripIconOpen.Image = this._settings.Icon_Open;

                    // Check the number of sensors
                    //CheckSensors();

                    if (InitializeArrays)
                    {
                        this.InitializeArrays();
                        ModifyArrays = false;
                    }

                    this.Plots_FetchData(false, false);
                    //Plots_Clear();          // First, clear all data (if any) in the plots
                    //Plots_DataBinding();    // Bind the arrays to the plots
                    //Plots_ShowLegends();    // Show the legends in the picture boxes
                    ModifyPlots = false;
                }
                else
                {
                    this.toolStripMain_Connect.Enabled = false;
                    this.statusStripIconOpen.Image = this._settings.Icon_Close;

                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show(this,
                            StringResources.MsgBoxErrorOpenDevice,
                            StringResources.MsgBoxErrorOpenDeviceTitle,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (_baudRate != this._settings.T10_BaudRate)
                    this.myFtdiDevice.SetBaudRate((uint)this._settings.T10_BaudRate);
                if (_dataBits != this._settings.T10_DataBits || _stopBits != this._settings.T10_StopBits || _parity != this._settings.T10_Parity)
                    this.myFtdiDevice.SetDataCharacteristics((byte)this._settings.T10_DataBits, (byte)this._settings.T10_StopBits, (byte)this._settings.T10_Parity);
                if (_flowControl != this._settings.T10_FlowControl || _charOn != this._settings.T10_CharOn || _charOff != this._settings.T10_CharOff)
                    this.myFtdiDevice.SetFlowControl((ushort)this._settings.T10_FlowControl, (byte)this._settings.T10_CharOn, (byte)this._settings.T10_CharOff);
            }

            if (ModifyArrays)
                this.InitializeArrays();

            if (ModifyPlots)
            {
                if (this._plotData.Length > 0 && this._plotRadar.Length > 0 && this._plotRadialGauge.Length > 0)
                    this.Plots_FetchData(false, true);
            }

            if (_cultureName != this._settings.AppCultureName)
                this.UpdateUI_Language(this._settings.T10_NumberOfSensors + this._settings.ArrayFixedColumns);

            this.InitializeStatusStripLabelsStatus();

            // Set the timer interval according to the sampling frecuency
            this.m_timer.Interval = 1000 / this._settings.T10_Frequency;



            //// If a device is selected and settings have changed, then set up the new parameters for the device
            //if (_settings.T10_LocationID > 0 && !_equalSettings)
            //{
            //    this.toolStripMain_Connect.Enabled = true;

            //    if (myFtdiDevice != null && myFtdiDevice.IsOpen)
            //        myFtdiDevice.Close();

            //    myFtdiDevice = new FTDISample();
            //    result = myFtdiDevice.OpenDevice(location: (uint)_settings.T10_LocationID,
            //        baud: _settings.T10_BaudRate,
            //        dataBits: _settings.T10_DataBits,
            //        stopBits: _settings.T10_StopBits,
            //        parity: _settings.T10_Parity,
            //        flowControl: _settings.T10_FlowControl,
            //        xOn: _settings.T10_CharOn,
            //        xOff: _settings.T10_CharOff,
            //        readTimeOut: 0,
            //        writeTimeOut: 0);

            //    if (result == FTDI.FT_STATUS.FT_OK)
            //    {
            //        // Check the number of sensors
            //        //CheckSensors();

            //        // Set the timer interval according to the sampling frecuency
            //        m_timer.Interval = 1000 / _settings.T10_Frequency;

            //        // Update the status strip with information
            //        this.statusStripLabelLocation.Text = StringResources.StatusLocation + $": {_settings.T10_LocationID:X}";
            //        this.statusStripLabelType.Text = StringResources.StatusType + $": {_settings.T10_DeviceType}";
            //        this.statusStripLabelID.Text = StringResources.StatusID + $": {_settings.T10_DevideID.ToString("0:X")}";
            //        this.statusStripIconOpen.Image = _settings.Icon_Open;

            //        InitializeStatusStripLabelsStatus();
            //        InitializeArrays();     // Initialize the arrays containing the data
            //        //Plots_FetchData();    // Needs verification in substitution of the next 3 calls
            //        Plots_Clear();          // First, clear all data (if any) in the plots
            //        Plots_DataBinding();    // Bind the arrays to the plots
            //        Plots_ShowLegends();    // Show the legends in the picture boxes
            //    }
            //    else
            //    {
            //        this.statusStripIconOpen.Image = _settings.Icon_Close;
            //        using (new CenterWinDialog(this))
            //        {
            //            MessageBox.Show(StringResources.MsgBoxErrorOpenDevice,
            //                StringResources.MsgBoxErrorOpenDeviceTitle,
            //                MessageBoxButtons.OK,
            //                MessageBoxIcon.Error);
            //        }
            //    }

            //} // End setting new device parameters
            //else
            //{
            //    InitializeStatusStripLabelsStatus();

            //    if (_plotData.Length > 0 && _plotRadar.Length > 0 && _plotRadialGauge.Length > 0)
            //        Plots_FetchData();
            //}

        }   // End DialogResult.OK

    }

    private void About_Click(object sender, EventArgs e)
    {
        var frm = new FrmAbout();
        frm.ShowDialog();
    }

    private void Language_Click(object sender, EventArgs e)
    {
        FrmLanguage frm = new(this._settings);
        frm.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);
        frm.ShowDialog();

        if (frm.DialogResult == DialogResult.OK)
        {
            this.UpdateUI_Language(this._plotData.Length);
        }
    }

    private void LabelExPlots_Click(object sender, EventArgs e)
    {
        if (sender is not null && sender is ToolStripStatusLabelEx LabelEx)
        {
            //var label = (ToolStripStatusLabelEx)sender;
            LabelEx.Checked = !LabelEx.Checked;

            // Update the settings
            switch (LabelEx.Text)
            {
                case "W":
                    this._settings.Plot_ShowRawData = LabelEx.Checked;
                    this.mnuMainFrm_View_Raw.Checked = LabelEx.Checked;
                    break;
                case "D":
                    this._settings.Plot_ShowDistribution = LabelEx.Checked;
                    this.mnuMainFrm_View_Distribution.Checked = LabelEx.Checked;
                    break;
                case "A":
                    this._settings.Plot_ShowAverage = LabelEx.Checked;
                    this.mnuMainFrm_View_Average.Checked = LabelEx.Checked;
                    break;
                case "R":
                    this._settings.Plot_ShowRatios = LabelEx.Checked;
                    this.mnuMainFrm_View_Ratio.Checked = LabelEx.Checked;
                    break;
            }
        }
    }

    private void LabelExCross_Click(object sender, EventArgs e)
    {
        if (sender is not null && sender is ToolStripStatusLabelEx LabelEx)
        {
            //var label = (ToolStripStatusLabelEx)sender;
            if (this.plotData.Plot.GetPlottables().Length > 0 && this.plotStats.Plot.GetPlottables().Length > 0 && this.plotRatio.Plot.GetPlottables().Length > 0)
            {
                LabelEx.Checked = !LabelEx.Checked;
                this.plotData.ShowCrossHair = LabelEx.Checked;
                this.plotData.Refresh();
                this.plotStats.ShowCrossHair = LabelEx.Checked;
                this.plotStats.Refresh();
                this.plotRatio.ShowCrossHair = LabelEx.Checked;
                this.plotRatio.Refresh();
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
        this._settings.Plot_ShowRawData = status;
    }
    private void mnuMainFrm_View_Radial_Click(object sender, EventArgs e)
    {
        bool status = !this.mnuMainFrm_View_Distribution.Checked;
        this.mnuMainFrm_View_Distribution.Checked = status;
        this.statusStripLabelRadar.Checked = status;
        this._settings.Plot_ShowDistribution = status;
    }
    private void mnuMainFrm_View_Average_Click(object sender, EventArgs e)
    {
        bool status = !this.mnuMainFrm_View_Average.Checked;
        this.mnuMainFrm_View_Average.Checked = status;
        this.statusStripLabelMax.Checked = status;
        this._settings.Plot_ShowAverage = status;
    }
    private void mnuMainFrm_View_Ratio_Click(object sender, EventArgs e)
    {
        bool status = !this.mnuMainFrm_View_Ratio.Checked;
        this.mnuMainFrm_View_Ratio.Checked = status;
        this.statusStripLabelRatio.Checked = status;
        this._settings.Plot_ShowRatios = status;
    }
    private void mnuMainFrm_Tools_Connect_Click(object sender, EventArgs e)
    {
        bool status = !this.toolStripMain_Connect.Checked;
        this.mnuMainFrm_Tools_Connect.Checked = status;
        this.toolStripMain_Connect.Checked = status;
    }

}