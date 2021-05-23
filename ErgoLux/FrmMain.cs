using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

using FTD2XX_NET;
using System.Text.Json;

namespace ErgoLux
{
    public partial class FrmMain : Form
    {
        private SerialPort _serialPort;
        private readonly System.Timers.Timer m_timer;
        private readonly string _path;
        private ClassSettings _sett;
        private FTDISample myFtdiDevice;
        private double[][] _plotData;
        private double _max = 0;
        private double _min = 0;
        private double _average = 0;
        private double[,] _plotRadar;
        private int _nPoints = 0;

        public FrmMain()
        {
            // Load settings. This has to go before custom initialization, since some routines depends on this
            _path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            _sett = new ClassSettings(_path);
            LoadProgramSettingsJSON();

            // Initilizate components and GUI
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

            // Set form icon
            if (File.Exists(_path + @"\images\logo.ico")) this.Icon = new Icon(_path + @"\images\logo.ico");
        }

        #region Initialization routines

        /// <summary>
        /// Initialize the ToolStripPanel component: add the child components to it
        /// </summary>
        private void InitializeToolStripPanel()
        {
            //tspTop = new ToolStripPanel();
            //tspBottom = new ToolStripPanel();
            tspTop.Join(toolStripMain);
            tspTop.Join(mnuMainFrm);
            tspBottom.Join(this.statusStrip);

            // Exit the method
            return;
        }

        /// <summary>
        /// Initialize the ToolStrip component
        /// </summary>
        private void InitializeToolStrip()
        {

            //ToolStripNumericUpDown c = new ToolStripNumericUpDown();
            //this.toolStripMain.Items.Add((ToolStripItem)c);

            toolStripMain.Renderer = new customRenderer<ToolStripButton>(Brushes.SteelBlue, Brushes.LightSkyBlue);

            //var path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(_path + @"\images\exit.ico")) this.toolStripMain_Exit.Image = new Icon(_path + @"\images\exit.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\connect.ico")) this.toolStripMain_Connect.Image = new Icon(_path + @"\images\connect.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\disconnect.ico")) this.toolStripMain_Disconnect.Image = new Icon(_path + @"\images\disconnect.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\cinema.ico")) this.toolStripMain_Video.Image = new Icon(path + @"\images\cinema.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\save.ico")) this.toolStripMain_Save.Image = new Icon(_path + @"\images\save.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\openfolder.ico")) this.toolStripMain_Open.Image = new Icon(_path + @"\images\openfolder.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\picture.ico")) this.toolStripMain_Picture.Image = new Icon(path + @"\images\picture.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\reflect-horizontal.ico")) this.toolStripMain_Mirror.Image = new Icon(path + @"\images\reflect-horizontal.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\plot.ico")) this.toolStripMain_Plots.Image = new Icon(path + @"\images\plot.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\settings.ico")) this.toolStripMain_Settings.Image = new Icon(_path + @"\images\settings.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\about.ico")) this.toolStripMain_About.Image = new Icon(_path + @"\images\about.ico", 48, 48).ToBitmap();

            this.toolStripMain_Disconnect.Enabled = false;
            this.toolStripMain_Connect.Enabled = false;
            this.toolStripMain_Open.Enabled = false;

            /*
            using (Graphics g = Graphics.FromImage(this.toolStripMain_Skeleton.Image))
            {
                g.Clear(Color.PowderBlue);
            }
            */

            return;
        }

        /// <summary>
        /// Initialize the MenuStrip component
        /// </summary>
        private void InitializeMenuStrip()
        {
            return;
        }

        /// <summary>
        /// Initialize the StatusStrip component
        /// </summary>
        private void InitializeStatusStrip()
        {
            //if (File.Exists(_path + @"\images\close.ico")) this.statusStripIconOpen.Image = new Icon(_path + @"\images\close.ico", 16, 16).ToBitmap();
            statusStripIconOpen.Image = _sett.Icon_Close;
            
            statusStripLabelRaw.CheckedChanged += new System.EventHandler(this.statusStripLabelPlots_CheckedChanged);
            statusStripLabelRadar.CheckedChanged += new System.EventHandler(this.statusStripLabelPlots_CheckedChanged);
            statusStripLabelMax.CheckedChanged += new System.EventHandler(this.statusStripLabelPlots_CheckedChanged);            
            statusStripLabelRatio.CheckedChanged += new System.EventHandler(this.statusStripLabelPlots_CheckedChanged);

            InitializeStatusStripLabelsStatus();

            statusStrip.Renderer = new customRenderer<ToolStripLabel>(Brushes.SteelBlue, Brushes.LightSkyBlue);
            return;
        }

        /// <summary>
        /// Sets the labels checked status based on the values stored in <see cref="ClassSettings"/>.
        /// </summary>
        private void InitializeStatusStripLabelsStatus()
        {
            statusStripLabelRaw.Checked = _sett.Plot_ShowRawData;
            statusStripLabelRadar.Checked = _sett.Plot_ShowRadar;
            statusStripLabelMax.Checked = _sett.Plot_ShowAverage;
            statusStripLabelRatio.Checked = _sett.Plot_ShowRatios;
        }

        private void InitializePlots()
        {
            
            //formsPlot1.plt.AxisAutoX(margin: 0);
            //formsPlot1.plt.Axis(x1: 0, x2: _sett.Plot_WindowPoints, y1: 0);

            // customize styling
            formsPlot1.plt.Title("Illuminance");
            formsPlot1.plt.YLabel("Lux");
            formsPlot1.plt.XLabel("Time (seconds)");
            formsPlot1.plt.Grid(false);

            // Customize the Radar plot
            formsPlot2.plt.Grid(false);
            formsPlot2.plt.Frame(false);
            formsPlot2.plt.Ticks(false, false);
            //formsPlot2.plt.Colorset(ScottPlot.Drawing.Colorset.OneHalf);

            // Customize the Average plot
            //formsPlot3.plt.AxisAuto(horizontalMargin: 0);
            //formsPlot3.plt.Axis(x1: 0, x2: _sett.Plot_WindowPoints, y1: 0);

            formsPlot3.plt.Title("Average, max, min");
            formsPlot3.plt.YLabel("Lux");
            formsPlot3.plt.XLabel("Time (seconds)");
            formsPlot3.plt.Grid(false);
            formsPlot3.plt.Colorset(ScottPlot.Drawing.Colorset.Nord);

            // Customize the Ratio plot
            //formsPlot4.plt.AxisAuto(horizontalMargin: 0);
            //formsPlot4.plt.Axis(x1: 0, x2: _sett.Plot_WindowPoints, y1: 0);

            formsPlot4.plt.Title("Illuminance ratios");
            formsPlot4.plt.YLabel("Ratio");
            formsPlot4.plt.XLabel("Time (seconds)");
            formsPlot4.plt.Grid(false);
            formsPlot4.plt.Colorset(ScottPlot.Drawing.Colorset.OneHalf);
            //formsPlot4.plt.AxisAuto(horizontalMargin: 0);
            //formsPlot4.plt.Axis(x1: 0, x2: _sett.Plot_WindowPoints, y1: 0);
            //formsPlot4.plt.Colorset(ScottPlot.Drawing.Colorset.Aurora);
        }

        #endregion Initialization routines 

        private void Form1_Shown(object sender, EventArgs e)
        {
            // signal the native process (that launched us) to close the splash screen
            using (var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent"))
            {
                closeSplashEvent.Set();
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            _serialPort = new SerialPort("COM5", 9600, Parity.Even, 7, StopBits.One);
            _serialPort.Handshake = Handshake.None;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            //_serialPort.ReadTimeout = 500;
            //_serialPort.NewLine = string.Empty;
            // Opens serial port   
            _serialPort.Open();
            //_serialPort.WriteLine(ClassT10.Command54.TrimEnd( (char)10, (char)13));
            //_serialPort.WriteLine(ClassT10.ReceptorsSingle[0].TrimEnd((char)10, (char)13));
            _serialPort.Write(ClassT10.Command_54);
            System.Threading.Thread.Sleep(500);
            _serialPort.Write(ClassT10.ReceptorsSingle[0]);
            _serialPort.Write(ClassT10.ReceptorsSingle[1]);


        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            m_timer.Stop();
            myFtdiDevice.DataReceived -= OnDataReceived;

            //foreach (var plot in formsPlot1.plt.GetPlottables())
            //{
            //    ((ScottPlot.PlottableSignal)plot).minRenderIndex = 0;
            //}

            //sigPlot.minRenderIndex = 0;
            //formsPlot1.plt.Axis(x1: 0);
            formsPlot1.plt.AxisAuto(horizontalMargin: 0.05);
            formsPlot1.Render();
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            //m_timer.Stop();
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
                //System.Diagnostics.Debug.Print(result.ToString());
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

        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadLine();
            System.Diagnostics.Debug.Print("Received data is {0} and length is {1}", data, data.Length);
            if(data.Length==13)
            {
                _serialPort.Write(ClassT10.ReceptorsSingle[0]);
            }
            else if (data.Length==31)
            {
                _serialPort.Write(ClassT10.ReceptorsSingle[1]);
            }
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


        #region toolStripMain events
        private void toolStripMain_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMain_Save_Click(object sender, EventArgs e)
        {
            // Exit if no data has been received or the matrix is still un-initialized
            if (_nPoints == 0 || _plotData == null)
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("There is no data available to be saved.", "No data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            // Displays a SaveFileDialog so the user can save the Image  
            SaveFileDialog SaveDlg = new SaveFileDialog
            {
                DefaultExt = "*.elux",
                Filter = "ErgoLux file (*.elux)|*.elux|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Save illuminance data",
                OverwritePrompt = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            DialogResult result;
            using (new CenterWinDialog(this))
            {
                result = SaveDlg.ShowDialog(this.Parent);
            }

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && SaveDlg.FileName != "")
            {
                //object writer;

                //switch(Path.GetExtension(SaveDlg.FileName).ToLower())
                //{
                //    case ".elux":
                //        writer = new BinaryWriter(SaveDlg.OpenFile());
                //        break;
                //    case ".txt":
                //    default:
                //        writer = new StreamWriter(SaveDlg.OpenFile());
                //        break;
                //}

                // Convert array data into CSV format.
                using var outfile = new StreamWriter(SaveDlg.OpenFile());
                
                // Save the header text into the file
                string content = string.Empty;
                outfile.WriteLine("ErgoLux data");
                outfile.WriteLine("Data exported on: {0}", DateTime.Now.ToString("F"));
                outfile.WriteLine("Number of sensors: {0}", _sett.T10_NumberOfSensors.ToString());
                outfile.WriteLine("Number of data points: {0}", _nPoints.ToString());
                outfile.WriteLine();
                for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                {
                    content += "Sensor #" + i.ToString("00") + "\t";
                }
                content += "Maximum" + "\t" + "Average" + "\t" + "Minimum" + "\t" + "Min/Average" + "\t" + "Min/Max" + "\t" + "Average/Max";
                outfile.WriteLineAsync(content);

                // Save the numerical values
                for (int j = 0; j < _nPoints; j++)
                {
                    content = string.Empty;
                    for (int i = 0; i < _plotData.Length; i++)
                    {
                        content += _plotData[i][j].ToString(i < _sett.T10_NumberOfSensors + 3 ? "#0.0" : "0.000") + "\t";
                    }
                    //trying to write data to csv
                    outfile.WriteLineAsync(content);   
                }
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
                        MessageBox.Show("The device is closed. Please, go to\n'Settings' to open the device. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    toolStripMain_Connect.Checked = false;
                    return;
                }

                toolStripMain_Disconnect.Enabled = true;
                myFtdiDevice.DataReceived += OnDataReceived;
                if (myFtdiDevice.Write(ClassT10.Command_54))
                {
                    //System.Threading.Thread.Sleep(500);
                    //myFtdiDevice.ClearBuffer();
                    //this.statusStripIconExchange.Image = _sett.Icon_Data;
                    m_timer.Start();
                }
            }
            else if (toolStripMain_Connect.Checked == false)
            {
                toolStripMain_Disconnect.Enabled = false;
                this.statusStripIconExchange.Image = null;
            }
        }

        private void toolStripMain_Disconnect_Click(object sender, EventArgs e)
        {
            m_timer.Stop();
            toolStripMain_Connect.Checked = false;
            myFtdiDevice.DataReceived -= OnDataReceived;

            // Shows plots full data
            formsPlot1.plt.AxisAuto(horizontalMargin: 0.05);
            formsPlot1.plt.Axis(x1: 0, y1: 0);
            formsPlot1.Render();

            formsPlot3.plt.AxisAuto(horizontalMargin: 0.05);
            formsPlot3.plt.Axis(x1: 0, y1: 0);
            formsPlot3.Render();

            formsPlot4.plt.AxisAuto(horizontalMargin: 0.05);
            formsPlot4.plt.Axis(x1: 0, y1: 0, y2: 1);
            formsPlot4.Render();
        }
        private void toolStripMain_Settings_Click(object sender, EventArgs e)
        {
            bool result;
            
            var frm = new FrmSettings(_sett);
            // Set form icon
            if (File.Exists(_path + @"\images\logo.ico")) frm.Icon = new Icon(_path + @"\images\logo.ico");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
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
                
                if (result == true)
                {
                    // Update the status strip with information
                    this.statusStripLabelLocation.Text = "Location ID: " + String.Format("{0:X}", _sett.T10_LocationID);
                    this.statusStripLabelType.Text = "Device type: " + frm.GetDeviceType;
                    this.statusStripLabelID.Text = "Device ID: " + frm.GetDeviceID;
                    this.statusStripIconOpen.Image = _sett.Icon_Open;

                    InitializeStatusStripLabelsStatus();

                    // Initialize the arrays containing the data
                    _plotData = new double[_sett.T10_NumberOfSensors + _sett.ArrayFixedColumns][];
                    _plotRadar = new double[2, _sett.T10_NumberOfSensors];

                    // First, clear all data (if any) in the plots
                    Plots_Clear();

                    // Bind the arrays into the plots
                    Plots_DataBinding();

                    // Show the legends into the picture boxes
                    Plots_ShowLegends();
                }
                else
                {
                    this.statusStripIconOpen.Image = _sett.Icon_Close;
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Could not open the device", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
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
                        break;
                    case "D":
                        _sett.Plot_ShowRadar = label.Checked;
                        break;
                    case "A":
                        _sett.Plot_ShowAverage = label.Checked;
                        break;
                    case "R":
                        _sett.Plot_ShowRatios = label.Checked;
                        break;
                }
            }
        }

        #endregion statusSrip events

        #region Plot custom methods

        /// <summary>
        /// Binds the data arrays to the plots
        /// </summary>
        private void Plots_DataBinding()
        {
            // Binding for Plot Raw Data
            for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
            {
                _plotData[i] = new double[_sett.Plot_ArrayPoints];
                formsPlot1.plt.PlotSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: "Sensor #" + i.ToString("#0"));
            }
            formsPlot1.plt.AxisAutoX(margin: 0);
            formsPlot1.plt.Axis(x1: 0, x2: _sett.Plot_WindowPoints, y1: 0);

            // Binding for Plot Radar
            string[] labels = new string[_sett.T10_NumberOfSensors];
            for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
            {
                labels[i] = "Sensor #" + i.ToString("#0");
            }
            formsPlot2.plt.PlotRadar(_plotRadar, categoryNames: labels, groupNames: new string[] { "Average", "Illuminance" });

            // Binding for Plot Average
            //_plotAverage = new double[3][];
            for (int i = _sett.T10_NumberOfSensors; i < _sett.T10_NumberOfSensors + 3; i++)
            {
                //_plotAverage[i] = new double[_sett.Plot_ArrayPoints];
                _plotData[i] = new double[_sett.Plot_ArrayPoints];
                formsPlot3.plt.PlotSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: (i == _sett.T10_NumberOfSensors ? "Max" : (i == (_sett.T10_NumberOfSensors + 1) ? "Average" : "Min")));
            }
            formsPlot3.plt.AxisAuto(horizontalMargin: 0);
            formsPlot3.plt.Axis(x1: 0, x2: _sett.Plot_WindowPoints, y1: 0);

            // Binding for Plot Ratio
            //_plotRatio = new double[3][];
            for (int i = _sett.T10_NumberOfSensors + 3; i < _sett.T10_NumberOfSensors + _sett.ArrayFixedColumns; i++)
            {
                //_plotRatio[i] = new double[_sett.Plot_ArrayPoints];
                _plotData[i] = new double[_sett.Plot_ArrayPoints];
                formsPlot4.plt.PlotSignal(_plotData[i], sampleRate: _sett.T10_Frequency, label: (i == (_sett.T10_NumberOfSensors + 3) ? "Min/Average" : (i == (_sett.T10_NumberOfSensors + 4) ? "Min/Max" : "Average/Max")));
            }
            formsPlot4.plt.AxisAuto(horizontalMargin: 0);
            formsPlot4.plt.Axis(x1: 0, x2: _sett.Plot_WindowPoints, y1: 0, y2: 1);
        }

        /// <summary>
        /// Show plots legends in picture boxes
        /// </summary>
        private void Plots_ShowLegends()
        {
            int nVertDist = 10;

            // Combine legends from Plot1 and Plot2
            var legendA = formsPlot1.plt.GetLegendBitmap();
            var legendB = formsPlot2.plt.GetLegendBitmap();
            var bitmap = new Bitmap(Math.Max(legendA.Width, legendB.Width), legendA.Height + legendB.Height + nVertDist);
            using Graphics GraphicsA = Graphics.FromImage(bitmap);
            GraphicsA.DrawImage(legendA, 0, 0);
            GraphicsA.DrawImage(legendB, 0, legendA.Height + nVertDist);
            pictureBox1.Image = bitmap;

            // Combine legends from Plot3 and Plot4
            legendA = formsPlot3.plt.GetLegendBitmap();
            legendB = formsPlot4.plt.GetLegendBitmap();
            bitmap = new Bitmap(Math.Max(legendA.Width, legendB.Width), legendA.Height + legendB.Height + nVertDist);
            using Graphics GraphicsB = Graphics.FromImage(bitmap);
            GraphicsB.DrawImage(legendA, 0, 0);
            GraphicsB.DrawImage(legendB, 0, legendA.Height + nVertDist);
            pictureBox2.Image = bitmap;
        }

        /// <summary>
        /// Clears all data in the plots and sets the private variable _nPoints to 0
        /// </summary>
        private void Plots_Clear()
        {
            _nPoints = 0;
            formsPlot1.plt.Clear();
            formsPlot2.plt.Clear();
            formsPlot3.plt.Clear();
            formsPlot4.plt.Clear();
        }

        /// <summary>
        /// Updates the plots with a new value
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
                System.Diagnostics.Debug.Print("The current factor is: {0}", factor);
                System.Diagnostics.Debug.Print("The new factor is: {0}", factor + _sett.Plot_ArrayPoints);
                for (int i = 0; i < _plotData.Length; i++)
                {
                    Array.Resize<double>(ref _plotData[i], factor + _sett.Plot_ArrayPoints);
                }
                //Array.Copy(_plotData[0], 1, _plotData[0], 0, _plotData[0].Length - 1);
                _sett.Plot_ArrayPoints *= factor;
            }

            // Data computation
            _plotData[sensor][_nPoints] = value;
            _plotRadar[1, sensor] = value;
            _max = value > _max ? value : _max;
            _min = value < _min ? value : _min;
            _average += value;

            // Only render when the last sensor value is received
            if (sensor == _sett.T10_NumberOfSensors - 1)
            {
                // Compute data
                _average /= _sett.T10_NumberOfSensors;
                
                _plotData[_sett.T10_NumberOfSensors][_nPoints] = _max;
                _plotData[_sett.T10_NumberOfSensors + 1][_nPoints] = _average;
                _plotData[_sett.T10_NumberOfSensors + 2][_nPoints] = _min;
                
                _plotData[_sett.T10_NumberOfSensors + 3][_nPoints] = _min / _average;
                _plotData[_sett.T10_NumberOfSensors + 4][_nPoints] = _min / _max;
                _plotData[_sett.T10_NumberOfSensors + 5][_nPoints] = _average / _max;



                formsPlot1.plt.AxisAutoY();
                formsPlot3.plt.AxisAutoY();
                formsPlot4.plt.AxisAutoY();
                formsPlot1.plt.Axis(y1: 0);
                formsPlot3.plt.Axis(y1: 0);
                formsPlot4.plt.Axis(y1: 0);
                if (_nPoints / _sett.T10_Frequency >= formsPlot1.plt.Axis()[1])
                {
                    formsPlot1.plt.Axis(x1: (_nPoints - _sett.Plot_WindowPoints) / 2, x2: (_nPoints + _sett.Plot_WindowPoints) / 2, y1: 0);
                    formsPlot3.plt.Axis(x1: (_nPoints - _sett.Plot_WindowPoints) / 2, x2: (_nPoints + _sett.Plot_WindowPoints) / 2, y1: 0);
                    formsPlot4.plt.Axis(x1: (_nPoints - _sett.Plot_WindowPoints) / 2, x2: (_nPoints + _sett.Plot_WindowPoints) / 2, y1: 0);
                }

                if (_sett.Plot_ShowRawData)
                {
                    foreach (var plot in formsPlot1.plt.GetPlottables())
                    {
                        ((ScottPlot.PlottableSignal)plot).maxRenderIndex = _nPoints;
                        //((ScottPlot.PlottableSignal)plot).minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                        //sigPlot.maxRenderIndex = _dataN;
                        //sigPlot.minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                    }
                    formsPlot1.Render(skipIfCurrentlyRendering: true);
                }

                //var average = _plotData[0][_nPoints];
                //var min = _plotData[0][_nPoints];
                //var max = _plotData[0][_nPoints];

                //for (int i = 1; i < _sett.T10_NumberOfSensors; i++)
                //{
                //    min = _plotData[i][_nPoints] < min ? _plotData[i][_nPoints] : min;
                //    max = _plotData[i][_nPoints] > max ? _plotData[i][_nPoints] : max;
                //    average += _plotData[i][_nPoints];
                //}
                //average /= _sett.T10_NumberOfSensors;

                

                if (_sett.Plot_ShowRadar)
                {
                    for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                    {
                        _plotRadar[0, i] = _average;
                    }
                    formsPlot2.plt.Clear();
                    formsPlot2.plt.PlotRadar(_plotRadar, fillAlpha: 0.5);
                    formsPlot2.Render(skipIfCurrentlyRendering: true);
                }

                
                if (_sett.Plot_ShowAverage)
                {
                    foreach (var plot in formsPlot3.plt.GetPlottables())
                    {
                        ((ScottPlot.PlottableSignal)plot).maxRenderIndex = _nPoints;
                    }
                    formsPlot3.Render(skipIfCurrentlyRendering: true);
                }


                if (_sett.Plot_ShowRatios)
                {
                    foreach (var plot in formsPlot4.plt.GetPlottables())
                    {
                        ((ScottPlot.PlottableSignal)plot).maxRenderIndex = _nPoints;
                    }
                    formsPlot4.Render(skipIfCurrentlyRendering: true);
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

        #region Program settings

        /// <summary>
        /// Loads all settings from file _sett.FileName into class instance _sett
        /// Shows MessageBox error if unsuccessful
        /// </summary>
        private void LoadProgramSettingsJSON()
        {
            try
            {
                var jsonString = File.ReadAllText(_sett.FileName);
                //_sett = JsonSerializer.Deserialize<ClassSettings>(jsonString);
                //_sett.InitializeJsonIgnore(_path);
                var settings = JsonSerializer.Deserialize<ClassSettings>(jsonString);
                settings.Icon_Close = _sett.Icon_Close;
                settings.Icon_Data = _sett.Icon_Data;
                settings.Icon_Open = _sett.Icon_Open;
                settings.FileName = _sett.FileName;
                settings.Path = _sett.Path;
                settings.ArrayFixedColumns = _sett.ArrayFixedColumns;
                _sett = settings;

                this.StartPosition = FormStartPosition.Manual;
                this.DesktopLocation = new Point(_sett.Wnd_Left, _sett.Wnd_Top);
                this.ClientSize = new Size(_sett.Wnd_Width, _sett.Wnd_Height);
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception ex)
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show(this,
                        "Error loading settings file.\n\n" + ex.Message + "\n\n" + "Default values will be used instead.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Saves data from class instance _sett into _sett.FileName
        /// </summary>
        private void SaveProgramSettingsJSON()
        {
            _sett.Wnd_Left = DesktopLocation.X;
            _sett.Wnd_Top = DesktopLocation.Y;
            _sett.Wnd_Width = ClientSize.Width;
            _sett.Wnd_Height = ClientSize.Height;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(_sett, options);
            File.WriteAllText(_sett.FileName, jsonString);
        }

        #endregion Program settings

    }
}
