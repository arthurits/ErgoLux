using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System.IO.Ports;

using FTD2XX_NET;

namespace ErgoLux
{
    public partial class FrmMain : Form
    {
        private readonly System.Timers.Timer m_timer;
        private readonly string _path;
        private ClassSettings _sett;
        private FTDISample myFtdiDevice;
        private double[][] _plotData;
        private double _max = 0;
        private double _min = 0;
        private double _average = 0;
        private double[,] _plotRadar;
        private double[] _plotRadialGauge;
        private int _nPoints = 0;
        private DateTime _timeStart;
        private DateTime _timeEnd;
        private bool _reading = false;   // this controls whether clicking the plots is allowed or not
        private int PxBetweenLegends = 10;    // the number of pixels between two legends

        public FrmMain()
        {
            // Load settings. This has to go before custom initialization, since some routines depends on this
            _path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            _sett = new ClassSettings(_path);
            LoadProgramSettingsJSON();

            // Set form icon
            if (File.Exists(_path + @"\images\logo.ico")) this.Icon = new Icon(_path + @"\images\logo.ico");

            // Initialize components and GUI
            InitializeComponent();
            InitializeToolStripPanel();
            InitializeToolStrip();
            InitializeStatusStrip();
            InitializeMenuStrip();
            InitializePlots();

            // Initialize the internal timer
            m_timer = new System.Timers.Timer() { Interval = 500, AutoReset = true };
            m_timer.Elapsed += OnTimedEvent;
            m_timer.Enabled = false;

            // To be deleted: just for testing purposes
            //_sett.T10_NumberOfSensors = 8;
            //_sett.T10_Frequency = 1;
            //_sett.Plot_ArrayPoints = 10;
            //_sett.Plot_DistIsRadar = false;

            //InitializeStatusStripLabelsStatus();
            //InitializeArrays();     // Initialize the arrays containing the data
            //Plots_Clear();          // First, clear all data (if any) in the plots
            //Plots_DataBinding();    // Bind the arrays to the plots
            //Plots_ShowLegends();    // Show the legends in the picture boxes

            //Plots_Update(0, 550);
            //Plots_Update(1, 560);
            //Plots_Update(2, 540);
            //Plots_Update(3, 545);
            //Plots_Update(4, 555);
            //Plots_Update(5, 565);
            //Plots_Update(6, 530);
            //Plots_Update(7, 535);

            //Plots_Update(0, 510);
            //Plots_Update(1, 520);
            //Plots_Update(2, 515);
            //Plots_Update(3, 525);
            //Plots_Update(4, 520);
            //Plots_Update(5, 530);
            //Plots_Update(6, 535);
            //Plots_Update(7, 540);

            //Plots_Update(0, 510);
            //Plots_Update(1, 505);
            //Plots_Update(2, 520);
            //Plots_Update(3, 515);
            //Plots_Update(4, 510);
            //Plots_Update(5, 525);
            //Plots_Update(6, 530);
            //Plots_Update(7, 0);
            
            //Plots_Update(0, 505);
            //Plots_Update(1, 515);
            //Plots_Update(2, 510);
            //Plots_Update(3, 520);
            //Plots_Update(4, 515);
            //Plots_Update(5, 510);
            //Plots_Update(6, 0);
            //Plots_Update(7, 0);

            //Plots_Update(0, 525);
            //Plots_Update(1, 535);
            //Plots_Update(2, 515);
            //Plots_Update(3, 505);
            //Plots_Update(4, 510);
            //Plots_Update(5, 0);
            //Plots_Update(6, 0);
            //Plots_Update(7, 0);

            //Plots_Update(0, 555);
            //Plots_Update(1, 560);
            //Plots_Update(2, 540);
            //Plots_Update(3, 530);
            //Plots_Update(4, 0);
            //Plots_Update(5, 0);
            //Plots_Update(6, 0);
            //Plots_Update(7, 0);

            //Plots_Update(0, 595);
            //Plots_Update(1, 540);
            //Plots_Update(2, 570);
            //Plots_Update(3, 0);
            //Plots_Update(4, 0);
            //Plots_Update(5, 0);
            //Plots_Update(6, 0);
            //Plots_Update(7, 0);

            //Plots_Update(0, 505);
            //Plots_Update(1, 590);
            //Plots_Update(2, 0.005);
            //Plots_Update(3, 0.005);
            //Plots_Update(4, 0.0005);
            //Plots_Update(5, 0.005);
            //Plots_Update(6, 0.005);
            //Plots_Update(7, 0.0005);

            //Plots_Update(0, 0.1);
            //Plots_Update(1, 0);
            //Plots_Update(2, 0);
            //Plots_Update(3, 0);
            //Plots_Update(4, 0);
            //Plots_Update(5, 0);
            //Plots_Update(6, 0);
            //Plots_Update(7, 0);
        }

        #region Form events
        private void Form1_Shown(object sender, EventArgs e)
        {
            // signal the native process (that launched us) to close the splash screen
            using var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent");
            closeSplashEvent.Set();
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            var result = false;
            result = myFtdiDevice.Write(ClassT10.ReceptorsSingle[0]);
        }

        private void OnDataReceived (object sender, DataReceivedEventArgs e)
        {
            //_data = true;
            (int Sensor, double Iluminance, double Increment, double Percent) result = (0, 0, 0, 0);
            //string str = System.Text.Encoding.UTF8.GetString(e.DataReceived, 0, e.DataReceived.Length);

            if (e.StrDataReceived.Length == ClassT10.LongBytesLength)
            {
                result = ClassT10.DecodeCommand(e.StrDataReceived);
                //System.Diagnostics.Debug.Print("Daata: {0} — TimeStamp: {1}",
                //                            result.ToString(),
                //                            DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture));
                if (result.Sensor < _sett.T10_NumberOfSensors - 1)
                {
                    myFtdiDevice.Write(ClassT10.ReceptorsSingle[result.Sensor + 1]);
                }
            }
            else if (e.StrDataReceived.Length == ClassT10.ShortBytesLength)
            {
                return;
            }

            Plots_Update(result.Sensor, result.Iluminance);
            
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (new CenterWinDialog(this))
            {
                if (DialogResult.No == MessageBox.Show(this,
                                                        "Are you sure you want to exit\nthe application?",
                                                        "Exit?",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button2))
                {
                    // Cancel
                    e.Cancel = true;
                }
            }

            // Stop the time if it's still running
            if (m_timer.Enabled) m_timer.Stop();
            
            m_timer.Dispose();

            // Close the device if it's still open
            if (myFtdiDevice != null && myFtdiDevice.IsOpen)
                myFtdiDevice.Close();

            // Save settings data
            SaveProgramSettingsJSON();
        }

        #endregion Form events

        #region mnuMainFrm events
        private void mnuMainFrm_File_Exit_Click(object sender, EventArgs e)
        {
            this.toolStripMain_Exit_Click(sender, e);
        }
        private void mnuMainFrm_File_Open_Click(object sender, EventArgs e)
        {
            this.toolStripMain_Open_Click(sender, e);
        }

        private void mnuMainFrm_File_Save_Click(object sender, EventArgs e)
        {
            this.toolStripMain_Save_Click(sender, e);
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
            _sett.Plot_ShowRawData = status;
        }

        private void mnuMainFrm_View_Radial_Click(object sender, EventArgs e)
        {
            bool status = !this.mnuMainFrm_View_Radial.Checked;
            this.mnuMainFrm_View_Radial.Checked = status;
            this.statusStripLabelRadar.Checked = status;
            _sett.Plot_ShowDistribution = status;
        }

        private void mnuMainFrm_View_Average_Click(object sender, EventArgs e)
        {
            bool status = !this.mnuMainFrm_View_Average.Checked;
            this.mnuMainFrm_View_Average.Checked = status;
            this.statusStripLabelMax.Checked = status;
            _sett.Plot_ShowAverage = status;
        }

        private void mnuMainFrm_View_Ratio_Click(object sender, EventArgs e)
        {
            bool status = !this.mnuMainFrm_View_Ratio.Checked;
            this.mnuMainFrm_View_Ratio.Checked = status;
            this.statusStripLabelRatio.Checked = status;
            _sett.Plot_ShowRatios = status;
        }
        private void mnuMainFrm_Tools_Connect_Click(object sender, EventArgs e)
        {
            bool status = !this.toolStripMain_Connect.Checked;
            this.mnuMainFrm_Tools_Connect.Checked = status;
            this.toolStripMain_Connect.Checked = status;
        }

        private void mnuMainFrm_Tools_Disconnect_Click(object sender, EventArgs e)
        {
            this.toolStripMain_Disconnect_Click(sender, e);
        }

        private void mnuMainFrm_Tools_Settings_Click(object sender, EventArgs e)
        {
            this.toolStripMain_Settings_Click(sender, e);
        }
        private void mnuMainFrm_Help_About_Click(object sender, EventArgs e)
        {
            this.toolStripMain_About_Click(sender, e);
        }
        #endregion mnuMainFrm events

        #region toolStripMain events
        private void toolStripMain_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMain_Save_Click(object sender, EventArgs e)
        {
            // Exit if no data has been received or the matrices are still un-initialized
            if (_nPoints == 0 || _plotData == null)
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("There is no data available to be saved.", "No data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            // Displays a SaveFileDialog, so the user can save the data into a file  
            SaveFileDialog SaveDlg = new()
            {
                DefaultExt = "*.elux",
                Filter = "ErgoLux file (*.elux)|*.elux|Text file (*.txt)|*.txt|Binary file (*.bin)|*.bin|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save illuminance data",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            };

            DialogResult result;
            using (new CenterWinDialog(this))
                result = SaveDlg.ShowDialog(this.Parent);

            // If the file name is not an empty string, call the corresponding routine to save the data into a file.  
            if (result == DialogResult.OK && SaveDlg.FileName != "")
            {
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
            }
            
        }

        private void toolStripMain_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDlg = new()
            {
                DefaultExt = "*.elux",
                Filter = "ErgoLux file (*.elux)|*.elux|Text file (*.txt)|*.txt|Binary file (*.bin)|*.bin|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Open illuminance data",
                InitialDirectory = _path + @"\Examples"
            };

            DialogResult result;
            using (new CenterWinDialog(this))
                result = OpenDlg.ShowDialog(this);

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && OpenDlg.FileName != "")
            {
                switch (Path.GetExtension(OpenDlg.FileName).ToLower())
                {
                    case ".elux":
                        OpenELuxData(OpenDlg.FileName);
                        break;
                    case ".txt":
                        OpenTextData(OpenDlg.FileName);
                        break;
                    case ".bin":
                        OpenBinaryData(OpenDlg.FileName);
                        break;
                    default:
                        //OpenDefaultData(OpenDlg.FileName);
                        break;
                }

                // Show data into plots
                Plots_FetchData();
            }
        }

        private void toolStripMain_Connect_CheckedChanged(object sender, EventArgs e)
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
                this.statusStripIconExchange.Image = _sett.Icon_Data;
                _reading = true;

                myFtdiDevice.DataReceived += OnDataReceived;
                if (myFtdiDevice.Write(ClassT10.Command_54))
                {
                    _timeStart = DateTime.Now;
                    m_timer.Start();
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

        private void toolStripMain_Disconnect_Click(object sender, EventArgs e)
        {
            // Stop receiving data
            m_timer.Stop();
            _timeEnd = DateTime.Now;
            myFtdiDevice.DataReceived -= OnDataReceived;

            // Update GUI
            toolStripMain_Connect.Checked = false;
            Plots_ShowFull();
        }

        private void toolStripMain_Settings_Click(object sender, EventArgs e)
        {
            FTDI.FT_STATUS result;

            var frm = new FrmSettings(_sett);
            // Set form icon
            if (File.Exists(_path + @"\images\logo.ico")) frm.Icon = new Icon(_path + @"\images\logo.ico");
            frm.ShowDialog();
            
            if (frm.DialogResult == DialogResult.OK)
            {
                // If a device is selected, then set up the parameters
                if (_sett.T10_LocationID > 0)
                {
                    this.toolStripMain_Connect.Enabled = true;

                    if (myFtdiDevice != null && myFtdiDevice.IsOpen)
                        myFtdiDevice.Close();

                    myFtdiDevice = new FTDISample();
                    result = myFtdiDevice.OpenDevice(location: (uint)_sett.T10_LocationID,
                        baud: _sett.T10_BaudRate,
                        dataBits: _sett.T10_DataBits,
                        stopBits: _sett.T10_StopBits,
                        parity: _sett.T10_Parity,
                        flowControl: _sett.T10_FlowControl,
                        xOn: _sett.T10_CharOn,
                        xOff: _sett.T10_CharOff,
                        readTimeOut: 0,
                        writeTimeOut: 0);

                    if (result == FTDI.FT_STATUS.FT_OK)
                    {
                        // Set the timer interval according to the sampling frecuency
                        m_timer.Interval = 1000 / _sett.T10_Frequency;

                        // Update the status strip with information
                        this.statusStripLabelLocation.Text = "Location ID: " + String.Format("{0:X}", _sett.T10_LocationID);
                        this.statusStripLabelType.Text = "Device type: " + frm.GetDeviceType;
                        this.statusStripLabelID.Text = "Device ID: " + frm.GetDeviceID;
                        this.statusStripIconOpen.Image = _sett.Icon_Open;

                        InitializeStatusStripLabelsStatus();
                        InitializeArrays();     // Initialize the arrays containing the data
                        Plots_Clear();          // First, clear all data (if any) in the plots
                        Plots_DataBinding();    // Bind the arrays to the plots
                        Plots_ShowLegends();    // Show the legends in the picture boxes
                    }
                    else
                    {
                        this.statusStripIconOpen.Image = _sett.Icon_Close;
                        using (new CenterWinDialog(this))
                        {
                            MessageBox.Show("Could not open the device", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                } // End _sett.T10_LocationID
                else
                {
                    InitializeStatusStripLabelsStatus();

                    if (_plotData != null && _plotRadar != null && _plotRadialGauge != null)
                    {
                        Plots_FetchData();
                    }
                }

            }   // End DialogResult.OK

        }
        
        private void toolStripMain_About_Click(object sender, EventArgs e)
        {
            var frm = new FrmAbout();
            frm.ShowDialog();
        }

        #endregion toolStripMain events

        #region statusSrip events
        
        private void statusStripLabelPlots_CheckedChanged (object sender, EventArgs e)
        {
            if (sender is ToolStripStatusLabelEx)
            {
                var label = sender as ToolStripStatusLabelEx;

                // Change the text color
                if (label.Checked)
                    label.ForeColor = Color.Black;
                else
                    label.ForeColor = Color.LightGray;

                // Update the View menu
                switch (label.Text)
                {
                    case "W":
                        //mnuMainFrm_View_Raw.Checked = label.Checked;
                        break;
                    case "D":
                        //mnuMainFrm_View_Radial.Checked = label.Checked;
                        break;
                    case "A":
                        //mnuMainFrm_View_Average.Checked = label.Checked;
                        break;
                    case "R":
                        //mnuMainFrm_View_Ratio.Checked = label.Checked;
                        break;
                }
            }
        }

        private void statusStripLabelPlots_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripStatusLabelEx)
            {
                var label = sender as ToolStripStatusLabelEx;
                label.Checked = !label.Checked;

                // Update the settings
                switch (label.Text)
                {
                    case "W":
                        _sett.Plot_ShowRawData = label.Checked;
                        mnuMainFrm_View_Raw.Checked = label.Checked;
                        break;
                    case "D":
                        _sett.Plot_ShowDistribution = label.Checked;
                        mnuMainFrm_View_Radial.Checked = label.Checked;
                        break;
                    case "A":
                        _sett.Plot_ShowAverage = label.Checked;
                        mnuMainFrm_View_Average.Checked = label.Checked;
                        break;
                    case "R":
                        _sett.Plot_ShowRatios = label.Checked;
                        mnuMainFrm_View_Ratio.Checked = label.Checked;
                        break;
                }
            }
        }

        #endregion statusSrip events

        #region Plot custom methods

        /// <summary>
        /// Fetchs data into the plots
        /// </summary>
        private void Plots_FetchData()
        {
            Plots_Clear();  // This sets _nPoints = 0, so we need to reset it now
            _nPoints = _sett.Plot_ArrayPoints;
            
            Plots_DataBinding();    // Bind the arrays to the plots
            Plots_ShowLegends();    // Show the legends in the picture boxes
            Plots_ShowFull();       // Show all data (fit data)
        }
        
        /// <summary>
        /// Binds the data arrays to the plots. Both _plotData and _plotRadar should be initialized, otherwise this function will throw an error.
        /// </summary>
        private void Plots_DataBinding()
        {
            // Binding for Plot Raw Data
            for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
            {
                var plot = formsPlot1.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: "Sensor #" + i.ToString("#0"));
                //formsPlot1.Refresh();
                plot.MinRenderIndex = 0;
                plot.MaxRenderIndex = 0;
            }
            formsPlot1.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0);

            // Binding for Distribution plot
            if (_sett.Plot_DistIsRadar)
            {
                string[] labels = new string[_sett.T10_NumberOfSensors];
                for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                    labels[i] = "#" + i.ToString("#0");
                var plt = formsPlot2.Plot.AddRadar(_plotRadar, disableFrameAndGrid: false);
                plt.FillColors[0] = Color.FromArgb(100, plt.LineColors[0]);
                plt.FillColors[1] = Color.FromArgb(150, plt.LineColors[1]);
                //formsPlot2.Plot.Grid(enable: false);

                plt.AxisType = ScottPlot.RadarAxis.Polygon;
                plt.ShowAxisValues = false;
                plt.CategoryLabels = labels;
                plt.GroupLabels = new string[] { "Average", "Illuminance" };
            }
            else
            {
                var plt = formsPlot2.Plot.AddRadialGauge(_plotRadialGauge);
                var strLabels = new string[_sett.T10_NumberOfSensors];
                for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                    strLabels[i] = "Sensor #" + i.ToString("#0");
                plt.Labels = strLabels;
                plt.StartingAngle = 180;
            }
            InitializePlotDistribution();

            // Binding for Plot Average
            for (int i = _sett.T10_NumberOfSensors; i < _sett.T10_NumberOfSensors + 3; i++)
            {
                var plot = formsPlot3.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: (i == _sett.T10_NumberOfSensors ? "Max" : (i == (_sett.T10_NumberOfSensors + 1) ? "Average" : "Min")));
                plot.MinRenderIndex = 0;
                plot.MaxRenderIndex = 0;
            }
            formsPlot3.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0);

            // Binding for Plot Ratio
            for (int i = _sett.T10_NumberOfSensors + 3; i < _sett.T10_NumberOfSensors + _sett.ArrayFixedColumns; i++)
            {
                var plot = formsPlot4.Plot.AddSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: (i == (_sett.T10_NumberOfSensors + 3) ? "Min/Average" : (i == (_sett.T10_NumberOfSensors + 4) ? "Min/Max" : "Average/Max")));
                plot.MinRenderIndex = 0;
                plot.MaxRenderIndex = 0;
            }
            formsPlot4.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0, yMax: 1);
        }

        /// <summary>
        /// Shows plots's full range data
        /// </summary>
        private void Plots_ShowFull()
        {
            if (_sett.Plot_ShowRawData)
            {
                foreach (var plot in formsPlot1.Plot.GetPlottables())
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints - 1;
                formsPlot1.Plot.AxisAuto();
                formsPlot1.Plot.SetAxisLimits(xMin: 0, yMin: 0);
                formsPlot1.Refresh();
            }

            formsPlot2.Refresh();

            if (_sett.Plot_ShowAverage)
            {
                foreach (var plot in formsPlot3.Plot.GetPlottables())
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints - 1;
                formsPlot3.Plot.AxisAuto();
                formsPlot3.Plot.SetAxisLimits(xMin: 0, yMin: 0);
                formsPlot3.Refresh();
            }

            if (_sett.Plot_ShowRatios)
            {
                foreach (var plot in formsPlot4.Plot.GetPlottables())
                    ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints - 1;
                formsPlot4.Plot.AxisAuto();
                formsPlot4.Plot.SetAxisLimits(xMin: 0, yMin: 0, yMax: 1);
                formsPlot4.Refresh();
            }
        }

        /// <summary>
        /// Show plots legends in picture boxes
        /// </summary>
        private void Plots_ShowLegends()
        {
            bool showLegA = _sett.Plot_DistIsRadar;
            bool showLegB = !_sett.Plot_DistIsRadar || _sett.T10_NumberOfSensors >= 2;
            Bitmap legendA;
            Bitmap legendB;
            Bitmap bitmap;
            Size szLegend = new ();

            // Combine legends from Plot1 and Plot2 and draw a black border around each legend
            legendA = showLegA ? formsPlot1.Plot.RenderLegend() : null;
            legendB = showLegB ? formsPlot2.Plot.RenderLegend() : null;
            szLegend.Width = Math.Max(showLegA ? legendA.Width : 0, showLegB ? legendB.Width : 0);
            szLegend.Width += 2;    // black border drawing
            szLegend.Height = (showLegA ? legendA.Height + 2 : 0);
            szLegend.Height += (showLegB ? legendB.Height + 2 : 0);
            szLegend.Height += (showLegA && showLegB ? PxBetweenLegends : 0);
            bitmap = new Bitmap(szLegend.Width, szLegend.Height);
            using Graphics GraphicsA = Graphics.FromImage(bitmap);

            if (showLegA)
            {
                GraphicsA.DrawRectangle(new Pen(Color.Black, 1),
                                    (bitmap.Width - legendA.Width - 2) / 2,
                                    0,
                                    legendA.Width + 1,
                                    legendA.Height + 1);
                GraphicsA.DrawImage(legendA, (bitmap.Width - legendA.Width - 2) / 2 + 1, 1);
            }
            if (showLegB)
            {
                GraphicsA.DrawRectangle(new Pen(Color.Black, 1),
                                    (bitmap.Width - legendB.Width - 2) / 2,
                                    (showLegA ? legendA.Height + PxBetweenLegends + 1 : 0),
                                    legendB.Width + 1,
                                    legendB.Height + 1);
                GraphicsA.DrawImage(legendB, (bitmap.Width - legendB.Width - 2) / 2 + 1, (showLegA ? legendA.Height + PxBetweenLegends + 2 : 1));
            }
            pictureBox1.Image = bitmap;

            // Combine legends from Plot3 and Plot4 and draw a black border around each legend
            legendA = formsPlot3.Plot.RenderLegend();
            legendB = formsPlot4.Plot.RenderLegend();
            bitmap = new Bitmap(Math.Max(legendA.Width, legendB.Width) + 2, legendA.Height + legendB.Height + PxBetweenLegends + 4);
            using Graphics GraphicsB = Graphics.FromImage(bitmap);
            GraphicsB.DrawRectangle(new Pen(Color.Black),
                                    (bitmap.Width - legendA.Width - 2) / 2,
                                    0,
                                    legendA.Width + 1,
                                    legendA.Height + 1);
            GraphicsB.DrawImage(legendA, (bitmap.Width - legendA.Width - 2) / 2 + 1, 1);
            GraphicsB.DrawRectangle(new Pen(Color.Black),
                                    (bitmap.Width - legendB.Width - 2) / 2,
                                    legendA.Height + PxBetweenLegends + 1,
                                    legendB.Width + 1,
                                    legendB.Height + 2);
            GraphicsB.DrawImage(legendB, (bitmap.Width - legendB.Width - 2) / 2 + 1, legendA.Height + PxBetweenLegends + 2);
            pictureBox2.Image = bitmap;
        }

        /// <summary>
        /// Clears all data in the plots and sets the private variable _nPoints to 0
        /// </summary>
        private void Plots_Clear()
        {
            _nPoints = 0;
            _max = 0;
            _min = 0;

            formsPlot1.Plot.Clear();
            formsPlot2.Plot.Clear();
            formsPlot3.Plot.Clear();
            formsPlot4.Plot.Clear();

            InitializePlotsPalette();

            // to do: this is unnecessary. Verify it in order to delete it.
            //InitializePlots();
        }

        /// <summary>
        /// Updates the plots with new values
        /// </summary>
        /// <param name="sensor">Sensor number</param>
        /// <param name="value">New illuminance value</param>
        private void Plots_Update(int sensor, double value)
        {
            // Resize arrays if necessary
            int factor = (_nPoints + 10) / _sett.Plot_ArrayPoints;
            factor *= _sett.Plot_ArrayPoints;
            if (factor > _plotData[0].Length-1)
            {
                _sett.Plot_ArrayPoints += factor;
                for (int i = 0; i < _plotData.Length; i++)
                {
                    Array.Resize<double>(ref _plotData[i], _sett.Plot_ArrayPoints);
                }

                // https://github.com/ScottPlot/ScottPlot/discussions/1042
                // https://swharden.com/scottplot/faq/version-4.1/
                // Update array reference in the plots. The Update() method doesn't allow a bigger array, so we need to modify Ys property
                int j = 0;
                foreach (var plot in formsPlot1.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).Ys = _plotData[j];
                    j++;
                }
                foreach (var plot in formsPlot3.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).Ys = _plotData[j];
                    j++;
                }
                foreach (var plot in formsPlot4.Plot.GetPlottables())
                {
                    ((ScottPlot.Plottable.SignalPlot)plot).Ys = _plotData[j];
                    j++;
                }
            }

            // Data computation
            _plotData[sensor][_nPoints] = value;
            _plotRadar[1, sensor] = value;
            _plotRadialGauge[sensor] = value;
            
            _max = sensor == 0 ? value : (value > _max ? value : _max);
            _min = sensor == 0 ? value : (value < _min ? value : _min);
            _average += value;
            
            // Only render when the last sensor value is received
            if (sensor == _sett.T10_NumberOfSensors - 1)
            {
                // Compute data
                _average /= _sett.T10_NumberOfSensors;
                _plotData[_sett.T10_NumberOfSensors][_nPoints] = _max;
                _plotData[_sett.T10_NumberOfSensors + 1][_nPoints] = _average;
                _plotData[_sett.T10_NumberOfSensors + 2][_nPoints] = _min;
                _plotData[_sett.T10_NumberOfSensors + 3][_nPoints] = _average > 0 ? _min / _average : 0;
                _plotData[_sett.T10_NumberOfSensors + 4][_nPoints] = _max > 0 ?_min / _max : 0;
                _plotData[_sett.T10_NumberOfSensors + 5][_nPoints] = _min > 0 ? _average / _max : 0;

                // Adjust the plots's axis
                formsPlot1.Plot.AxisAutoY();
                formsPlot3.Plot.AxisAutoY();
                formsPlot1.Plot.SetAxisLimits(yMin: 0);
                formsPlot3.Plot.SetAxisLimits(yMin: 0);
                if (_nPoints / _sett.T10_Frequency >= formsPlot1.Plot.GetAxisLimits().XMax)
                {
                    int xMin = (int)((_nPoints / _sett.T10_Frequency) - _sett.Plot_WindowPoints * 1 / 5);
                    int xMax = xMin + _sett.Plot_WindowPoints;
                    formsPlot1.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                    formsPlot3.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                    formsPlot4.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
                    //formsPlot3.Plot.SetAxisLimits(xMin: (_nPoints - _sett.Plot_WindowPoints) / _sett.T10_Frequency, xMax: (_nPoints + _sett.Plot_WindowPoints) / _sett.T10_Frequency);
                    //formsPlot4.Plot.SetAxisLimits(xMin: (_nPoints - _sett.Plot_WindowPoints) / _sett.T10_Frequency, xMax: (_nPoints + _sett.Plot_WindowPoints) / _sett.T10_Frequency);
                }

                // Update first plot
                if (_sett.Plot_ShowRawData)
                {
                    foreach (var plot in formsPlot1.Plot.GetPlottables())
                    {
                        ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints;
                        //((ScottPlot.PlottableSignal)plot).minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                        //sigPlot.maxRenderIndex = _dataN;
                        //sigPlot.minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                    }
                    formsPlot1.Refresh(skipIfCurrentlyRendering: true);
                }
                
                // Update radar plot
                if (_sett.Plot_ShowDistribution)
                {
                    // We always store the RadarPlot data in case the user wants to use it later
                    for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                        _plotRadar[0, i] = _average;

                    // Update the distribution plot
                    if (_sett.Plot_DistIsRadar)
                    {    
                        ((ScottPlot.Plottable.RadarPlot)formsPlot2.Plot.GetPlottables()[0]).Update(_plotRadar, false);
                    }
                    else
                    {
                        var plot = (ScottPlot.Plottable.RadialGaugePlot)formsPlot2.Plot.GetPlottables()[0];
                        var maxAngle = _average > 0 ? 180 * _plotRadialGauge.Max() / _average : 0.0;
                        plot.MaximumAngle = maxAngle > 360 ? 360.0 : maxAngle;
                        plot.Update(_plotRadialGauge);
                        //plot.MaximumAngle = 180 * _plotRadialGauge.Max() / _average;
                        //if (plot.MaximumAngle > 360) plot.MaximumAngle = 360.0;
                    }
                    formsPlot2.Refresh(skipIfCurrentlyRendering: true);
                }

                // Update max, average, and min plot
                if (_sett.Plot_ShowAverage)
                {
                    foreach (var plot in formsPlot3.Plot.GetPlottables())
                    {
                        ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints;
                    }
                    formsPlot3.Refresh(skipIfCurrentlyRendering: true);
                }

                // Update ratios plot
                if (_sett.Plot_ShowRatios)
                {
                    foreach (var plot in formsPlot4.Plot.GetPlottables())
                    {
                        ((ScottPlot.Plottable.SignalPlot)plot).MaxRenderIndex = _nPoints;
                    }
                    formsPlot4.Refresh(skipIfCurrentlyRendering: true);
                }

                // Modify internal numeric variables
                ++_nPoints;
                _max = 0.0;
                _min = 0.0;
                _average = 0.0;
            }

            // https://github.com/ScottPlot/ScottPlot/blob/096062f5dfde8fd5f1e2eb2e15e0e7ce9b17a54b/src/ScottPlot.Demo.WinForms/WinFormsDemos/LiveDataUpdate.cs#L14-L91
        }

        #endregion Plots functions

        

        private void formsPlot_MouseDown(object sender, MouseEventArgs e)
        {
            
            // If we are reading from the sensor, then exit
            if (_reading) return;

            if (e.Button != MouseButtons.Left) return;

            var MyPlot = ((ScottPlot.FormsPlot)sender);
            if (MyPlot.Name != "formsPlot1") return;
            if (MyPlot.Plot.GetPlottables().Length == 0) return;
            (double mouseCoordX, double mouseCoordY) = MyPlot.GetMouseCoordinates();
            (double pointX, double pointY, int pointIndex) = ((ScottPlot.Plottable.SignalPlot)(MyPlot.Plot.GetPlottables()[0])).GetPointNearestX(mouseCoordX);
            if (MyPlot.Plot.GetPlottables().Length == _sett.T10_NumberOfSensors)
            {
                var VLine = MyPlot.Plot.AddVerticalLine(pointX, color: Color.Red, width: 3, style: ScottPlot.LineStyle.Dash);
                VLine.DragEnabled = true;
            }
            else if (MyPlot.Plot.GetPlottables().Length == _sett.T10_NumberOfSensors + 1)
            {
                ((ScottPlot.Plottable.VLine)MyPlot.Plot.GetPlottables().Last()).X = pointX;
            }

            // Some information:
            // https://swharden.com/scottplot/faq/mouse-position/#highlight-the-data-point-near-the-cursor
            // https://github.com/ScottPlot/ScottPlot/discussions/862
            // https://github.com/ScottPlot/ScottPlot/discussions/645
            // https://github.com/ScottPlot/ScottPlot/issues/1090
            // frmWRmodel.cs chart_MouseClicked

        }

        private void formsPlot_PlottableDragged(object sender, EventArgs e)
        {
            // If we are reading from the sensor, then exit
            if (_reading) return;
            
            //var MyPlot = ((ScottPlot.FormsPlot)sender);
            if (sender.GetType() != typeof(ScottPlot.Plottable.VLine)) return;

            var MyPlot = ((ScottPlot.Plottable.VLine)sender);

            //(double mouseCoordX, double mouseCoordY) = formsPlot1.GetMouseCoordinates();
            ////double xyRatio = formsPlot1.Plot.XAxis.Dims.PxPerUnit / formsPlot1.Plot.YAxis.Dims.PxPerUnit;
            (double pointX, double pointY, int pointIndex) = ((ScottPlot.Plottable.SignalPlot)(formsPlot1.Plot.GetPlottables()[0])).GetPointNearestX(MyPlot.X);
            ////Text = $"Point index {pointIndex} at ({pointX}, {pointY})";


            for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
            {
                _plotRadialGauge[i] = _plotData[i][pointIndex];
                _plotRadar[1, i] = _plotData[i][pointIndex];
                _plotRadar[0, i] = _plotData[_sett.T10_NumberOfSensors + 1][pointIndex];
            }

            // Update the distribution plot
            if (_sett.Plot_DistIsRadar)
            {
                ((ScottPlot.Plottable.RadarPlot)formsPlot2.Plot.GetPlottables()[0]).Update(_plotRadar, false);
            }
            else
            {
                var plot = ((ScottPlot.Plottable.RadialGaugePlot)formsPlot2.Plot.GetPlottables()[0]);
                plot.Update(_plotRadialGauge);
                plot.MaximumAngle = 180 * _plotRadialGauge.Max() / _plotRadialGauge.Average();
                if (plot.MaximumAngle > 360) plot.MaximumAngle = 360.0;
            }
            formsPlot2.Refresh(skipIfCurrentlyRendering: true);

        }

    }
}

/* Alternative and easier connection method using System.IO.Ports.SerialPort (.NET Platform Extensions 5) instead of FTD2.dll and ClassFTDI
 * The only disadvantage is that we need to know the port name the deviced is connected into.
 *
    private void BtnConnect_Click(object sender, EventArgs e)
        {
            // Sets the serial port parameters and opens it
            _serialPort = new SerialPort("COM5", 9600, Parity.Even, 7, StopBits.One);
            _serialPort.Handshake = Handshake.None;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            //_serialPort.ReadTimeout = 500;
            //_serialPort.NewLine = "\n";
            _serialPort.Open();

            if (_serialPort.IsOpen)
            {
                // Initialize the arrays containing the data
                InitializeArrays();

                // First, clear all data (if any) in the plots
                Plots_Clear();

                // Bind the arrays to the plots
                Plots_DataBinding();

                // Show the legends in the picture boxes
                Plots_ShowLegends();

                _serialPort.Write(ClassT10.Command_54);

                m_timer.Start();
            }
        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            m_timer.Stop();
            _serialPort.DataReceived -= sp_DataReceived;

            // Shows plots's full data
            Plots_ShowFull();
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            _serialPort.Write(ClassT10.ReceptorsSingle[0]);
        }

        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            (int Sensor, double Iluminance, double Increment, double Percent) result;
            string data = _serialPort.ReadLine() + _serialPort.NewLine;     //ReadLine deletes the string defined in _serialPort.NewLine
            if (data.Length == ClassT10.LongBytesLength)
            {
                result = ClassT10.DecodeCommand(data);
                if (result.Sensor < _sett.T10_NumberOfSensors - 1)
                    _serialPort.Write(ClassT10.ReceptorsSingle[result.Sensor + 1]);
                Plots_Update(result.Sensor, result.Iluminance);
            }
        }
 * 
 */
