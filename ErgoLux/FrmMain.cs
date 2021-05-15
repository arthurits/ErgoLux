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
        private System.Timers.Timer m_timer;
        private string _path;
        private ClassSettings _sett;
        private FTDISample myFtdiDevice;
        private double[][] _plotData;
        private double[,] _plotRadar;
        private double[][] _plotAverage;
        private double[][] _plotRatio;
        private int[] _settings = new int[]
        {
            0,                                  // Location ID
            1,                                  // Number of sensors
            9600,                               // Baud rate
            FTDI.FT_DATA_BITS.FT_BITS_7,        // Data bits
            FTDI.FT_STOP_BITS.FT_STOP_BITS_1,   // Stop bits
            FTDI.FT_PARITY.FT_PARITY_EVEN,      // Parity
            FTDI.FT_FLOW_CONTROL.FT_FLOW_NONE,  // Flow
            11,                                 // On char
            13,                                 // Off char
            2                                   // Frequency (Herz)
        };
        private int _nPoints = 0;
        Random rand = new Random(0);

        private const int ArraySize = 7200;
        private const int PlotRangeX = 20;

        public FrmMain()
        {
            _path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            
            InitializeComponent();

            // Custom initialization routines
            InitializeToolStripPanel();
            InitializeToolStrip();
            InitializeStatusStrip();
            InitializeMenuStrip();
            
            m_timer = new System.Timers.Timer() { Interval = 500, AutoReset = true };
            m_timer.Elapsed += OnTimedEvent;
            m_timer.Enabled = false;

            // plot the data array only once and we can updates its value later
            _plotData = new double[2][];
            _plotData[0] = new double[ArraySize];
            _plotData[1] = new double[ArraySize];
            //sigPlot = formsPlot1.plt.PlotSignal(_plotData[0], sampleRate: 2);
            //formsPlot1.plt.PlotSignal(_plotData[1], sampleRate: 2);
            //sigPlot = formsPlot1.plt.PlotSignal(_plotData, 2);
            formsPlot1.plt.AxisAutoX(margin: 0);
            formsPlot1.plt.Axis(x1: 0, x2: PlotRangeX, y1: 0);

            // customize styling
            formsPlot1.plt.Title("Illuminance");
            formsPlot1.plt.YLabel("Lux");
            formsPlot1.plt.XLabel("Time (seconds)");
            formsPlot1.plt.Grid(false);

            // Customize the Radar plot
            formsPlot2.plt.Grid(false);
            formsPlot2.plt.Frame(false);
            formsPlot2.plt.Ticks(false, false);

            // Customize the Average plot
            formsPlot3.plt.AxisAutoX(margin: 0);
            formsPlot3.plt.Axis(x1: 0, x2: PlotRangeX, y1: 0);

            formsPlot3.plt.Title("Average, max, min");
            formsPlot3.plt.YLabel("Lux");
            formsPlot3.plt.XLabel("Time (seconds)");
            formsPlot3.plt.Grid(false);

            // Customize the Ratio plot
            formsPlot4.plt.AxisAutoX(margin: 0);
            formsPlot4.plt.Axis(x1: 0, x2: PlotRangeX, y1: 0);

            formsPlot4.plt.Title("Illuminance ratios");
            formsPlot4.plt.YLabel("Ratio");
            formsPlot4.plt.XLabel("Time (seconds)");
            formsPlot4.plt.Grid(false);

            // Load settings
            _sett = new ClassSettings();
            LoadProgramSettingsJSON();

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

            toolStripMain.Renderer = new customRenderer(Brushes.SteelBlue, Brushes.LightSkyBlue);

            //var path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(_path + @"\images\exit.ico")) this.toolStripMain_Exit.Image = new Icon(_path + @"\images\exit.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\connect.ico")) this.toolStripMain_Connect.Image = new Icon(_path + @"\images\connect.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\disconnect.ico")) this.toolStripMain_Disconnect.Image = new Icon(_path + @"\images\disconnect.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\cinema.ico")) this.toolStripMain_Video.Image = new Icon(path + @"\images\cinema.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\save.ico")) this.toolStripMain_Data.Image = new Icon(path + @"\images\save.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\picture.ico")) this.toolStripMain_Picture.Image = new Icon(path + @"\images\picture.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\reflect-horizontal.ico")) this.toolStripMain_Mirror.Image = new Icon(path + @"\images\reflect-horizontal.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\plot.ico")) this.toolStripMain_Plots.Image = new Icon(path + @"\images\plot.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\settings.ico")) this.toolStripMain_Settings.Image = new Icon(_path + @"\images\settings.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\about.ico")) this.toolStripMain_About.Image = new Icon(_path + @"\images\about.ico", 48, 48).ToBitmap();

            /*
            using (Graphics g = Graphics.FromImage(this.toolStripMain_Skeleton.Image))
            {
                g.Clear(Color.PowderBlue);
            }
            */

            //this.toolStripMain_Disconnect.Enabled = false;
            //this.toolStripMain_SkeletonWidth.NumericUpDownControl.Maximum = 20;
            //this.toolStripMain_SkeletonWidth.NumericUpDownControl.Minimum = 1;

            // Exit the method
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
            if (File.Exists(_path + @"\images\close.ico")) this.statusStripIconOpen.Image = new Icon(_path + @"\images\close.ico", 16, 16).ToBitmap();
            return;
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
                if (result.Sensor < _settings[1] - 1)
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

        private void toolStripMain_Connect_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripMain_Connect.Checked == true)
            {
                toolStripMain_Disconnect.Enabled = true;
                myFtdiDevice.DataReceived += OnDataReceived;
                if (myFtdiDevice.Write(ClassT10.Command_54))
                {
                    System.Threading.Thread.Sleep(500);
                    myFtdiDevice.ClearBuffer();
                    m_timer.Start();
                }
            }
            else
            {
                toolStripMain_Disconnect.Enabled = false;
                //toolStripMain_Data.Checked = false;
                //DisconnectKinect();
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
            formsPlot4.plt.Axis(x1: 0, y1: 0);
            formsPlot4.Render();
        }
        private void toolStripMain_Settings_Click(object sender, EventArgs e)
        {
            bool result = true;
            
            var frm = new FrmSettings(_settings);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.toolStripMain_Connect.Enabled = true;
                _settings = frm.GetData;

                if (myFtdiDevice != null && myFtdiDevice.IsOpen)
                    myFtdiDevice.Close();

                myFtdiDevice = new FTDISample();
                result = myFtdiDevice.OpenDevice(location: (uint)_settings[0],
                    baud: _settings[2],
                    dataBits: _settings[3],
                    stopBits: _settings[4],
                    parity: _settings[5],
                    flowControl: _settings[6],
                    xOn: _settings[7],
                    xOff: _settings[8],
                    readTimeOut: 0,
                    writeTimeOut: 0);
                
                if (result == true)
                {
                    this.statusStripLabelID.Text = "Location ID: " + String.Format("{0:X}", _settings[0]);
                    this.statusStripLabelType.Text = "Device type: " + frm.GetDeviceType;
                    if (File.Exists(_path + @"\images\open.ico")) this.statusStripIconOpen.Image = new Icon(_path + @"\images\open.ico", 16, 16).ToBitmap();

                    _plotData = new double[_settings[1]][];
                    for (int i = 0; i < _settings[1]; i++)
                    {
                        _plotData[i] = new double[ArraySize];
                        formsPlot1.plt.PlotSignal(_plotData[i], sampleRate: _settings[9], label: "Sensor #" + i.ToString("#0"));
                    }

                    formsPlot1.plt.AxisAuto(horizontalMargin: 0);
                    formsPlot1.plt.Axis(x1: 0, x2: PlotRangeX, y1: 0);

                    pictureBox1.Image = formsPlot1.plt.GetLegendBitmap();

                    // Radar plot
                    _plotRadar = new double[2, _settings[1]];
                    string[] labels = new string[_settings[1]];
                    for (int i = 0; i < _settings[1]; i++)
                    {
                        labels[i] = "Sensor #" + i.ToString("#0");
                    }
                    formsPlot2.plt.PlotRadar(_plotRadar, categoryNames: labels, groupNames: new string[] { "Illuminance", "Average" });

                    // Average plot
                    _plotAverage = new double[3][];
                    for (int i = 0; i < 3; i++)
                    {
                        _plotAverage[i] = new double[ArraySize];
                        formsPlot3.plt.PlotSignal(_plotAverage[i], sampleRate: _settings[9], label: (i==0 ? "Max" : (i==1 ? "Average" : "Min")));
                    }

                    formsPlot3.plt.AxisAuto(horizontalMargin: 0);
                    formsPlot3.plt.Axis(x1: 0, x2: PlotRangeX, y1: 0);
                    formsPlot3.plt.Colorset(ScottPlot.Drawing.Colorset.Nord);

                    // Ratios plot
                    _plotRatio = new double[3][];
                    for (int i = 0; i < 2; i++)
                    {
                        _plotRatio[i] = new double[ArraySize];
                        formsPlot4.plt.PlotSignal(_plotRatio[i], sampleRate: _settings[9], label: (i == 0 ? "Max/Min" : (i == 1 ? "Max/Average" : "Min/Average")));
                    }

                    formsPlot4.plt.AxisAuto(horizontalMargin: 0);
                    formsPlot4.plt.Axis(x1: 0, x2: PlotRangeX, y1: 0);
                    formsPlot3.plt.Colorset(ScottPlot.Drawing.Colorset.Aurora);
                }
                else
                {
                    if (File.Exists(_path + @"\images\close.ico")) this.statusStripIconOpen.Image = new Icon(_path + @"\images\close.ico", 16, 16).ToBitmap();
                    MessageBox.Show("Could not open the device", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        
        private void statusStripLabelAngle_Click(object sender, EventArgs e)
        {
            if (statusStripLabelAngle.ForeColor == Color.Green)
                statusStripLabelAngle.ForeColor = Color.LightGray;
            else
                statusStripLabelAngle.ForeColor = Color.Green;
            
        }

        #endregion statusSrip events

        private void Plots_Update(int sensor, double value)
        {
            // Resize arrays if necessary
            int factor = ArraySize * (_nPoints + 10) / ArraySize;
            if (factor > _plotData[0].Length)
            {
                for (int i = 0; i < _plotData.Length; i++)
                    Array.Resize<double>(ref _plotData[i], factor * ArraySize);
                //Array.Copy(_plotData[0], 1, _plotData[0], 0, _plotData[0].Length - 1);
            }

            _plotData[sensor][_nPoints] = value;
            _plotRadar[0, sensor] = value;


            // Only render when the last sensor value is received
            if (sensor == _settings[1] - 1)
            {
                foreach (var plot in formsPlot1.plt.GetPlottables())
                {
                    ((ScottPlot.PlottableSignal)plot).maxRenderIndex = _nPoints;
                    //((ScottPlot.PlottableSignal)plot).minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                    //sigPlot.maxRenderIndex = _dataN;
                    //sigPlot.minRenderIndex = _dataN > 40 ? _dataN - 40 : 0;
                }

                foreach (var plot in formsPlot3.plt.GetPlottables())
                {
                    ((ScottPlot.PlottableSignal)plot).maxRenderIndex = _nPoints;
                }

                foreach (var plot in formsPlot4.plt.GetPlottables())
                {
                    ((ScottPlot.PlottableSignal)plot).maxRenderIndex = _nPoints;
                }

                //formsPlot1.plt.AxisAuto(horizontalMargin: 0, tightenLayout: true);
                //formsPlot1.plt.AxisAutoX(margin: 0);
                formsPlot1.plt.AxisAutoY();
                formsPlot3.plt.AxisAutoY();
                formsPlot4.plt.AxisAutoY();
                formsPlot1.plt.Axis(y1: 0);
                formsPlot3.plt.Axis(y1: 0);
                formsPlot4.plt.Axis(y1: 0);
                if (_nPoints / _settings[9] >= PlotRangeX)
                {
                    formsPlot1.plt.Axis(x1: (_nPoints - PlotRangeX) / 2, x2: (_nPoints + PlotRangeX) / 2, y1: 0);
                    formsPlot3.plt.Axis(x1: (_nPoints - PlotRangeX) / 2, x2: (_nPoints + PlotRangeX) / 2, y1: 0);
                    formsPlot4.plt.Axis(x1: (_nPoints - PlotRangeX) / 2, x2: (_nPoints + PlotRangeX) / 2, y1: 0);
                }



                formsPlot1.Render(skipIfCurrentlyRendering: true);

                var average = _plotData[0][_nPoints];
                var min = _plotData[0][_nPoints];
                var max = _plotData[0][_nPoints];

                for (int i = 1; i < _settings[1]; i++)
                {
                    min = _plotData[i][_nPoints] < min ? _plotData[i][_nPoints] : min;
                    max = _plotData[i][_nPoints] > max ? _plotData[i][_nPoints] : max;
                    average += _plotData[i][_nPoints];
                }
                average /= _settings[1];
                for (int i = 0; i < _settings[1]; i++)
                {
                    _plotRadar[1, i] = average;
                }
                formsPlot2.plt.Clear();
                formsPlot2.plt.PlotRadar(_plotRadar);
                formsPlot2.Render(skipIfCurrentlyRendering: true);

                _plotAverage[0][_nPoints] = max;
                _plotAverage[1][_nPoints] = average;
                _plotAverage[2][_nPoints] = min;
                formsPlot3.Render(skipIfCurrentlyRendering: true);

                _plotRatio[0][_nPoints] = max / min;
                _plotRatio[1][_nPoints] = max / average;
                _plotRatio[2][_nPoints] = min / average;
                formsPlot4.Render(skipIfCurrentlyRendering: true);

                ++_nPoints;
            }

            //// Plot data
            //_plotData[result.Sensor][_dataN] = result.Iluminance;

            //sigPlot.maxRenderIndex = _dataN;
            //////sigPlot.minRenderIndex = _dataN > 20 ? _dataN - 20 : 0;
            //if (result.Sensor == 0) ++_dataN;
            //formsPlot1.plt.AxisAuto();
            //formsPlot1.Render(skipIfCurrentlyRendering: true);

            // https://github.com/ScottPlot/ScottPlot/blob/096062f5dfde8fd5f1e2eb2e15e0e7ce9b17a54b/src/ScottPlot.Demo.WinForms/WinFormsDemos/LiveDataUpdate.cs#L14-L91
        }

        #region Program settings

        private void LoadProgramSettingsJSON()
        {
            try
            {
                var jsonString = File.ReadAllText(_sett.FileName);
                _sett = JsonSerializer.Deserialize<ClassSettings>(jsonString);

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
                        "Error loading settings file\n\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

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
            var jsonString = JsonSerializer.Serialize(_settings, options);
            File.WriteAllText(_sett.FileName, jsonString);
        }

        #endregion Program settings
    }
}
