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
            if (File.Exists(_path + @"\images\save.ico")) this.toolStripMain_Save.Image = new Icon(_path + @"\images\save.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\openfolder.ico")) this.toolStripMain_Open.Image = new Icon(_path + @"\images\openfolder.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\settings.ico")) this.toolStripMain_Settings.Image = new Icon(_path + @"\images\settings.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\about.ico")) this.toolStripMain_About.Image = new Icon(_path + @"\images\about.ico", 48, 48).ToBitmap();

            this.toolStripMain_Disconnect.Enabled = false;
            this.toolStripMain_Connect.Enabled = false;
            this.toolStripMain_Open.Enabled = true; // maybe set as default in the WinForms designer
            this.toolStripMain_Save.Enabled = false;

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
            if (File.Exists(_path + @"\images\openfolder.ico")) this.mnuMainFrm_File_Open.Image = new Icon(_path + @"\images\openfolder.ico", 16, 16).ToBitmap();
            if (File.Exists(_path + @"\images\save.ico")) this.mnuMainFrm_File_Save.Image = new Icon(_path + @"\images\save.ico", 16, 16).ToBitmap();
            if (File.Exists(_path + @"\images\exit.ico")) this.mnuMainFrm_File_Exit.Image = new Icon(_path + @"\images\exit.ico", 16, 16).ToBitmap();

            if (File.Exists(_path + @"\images\connect.ico")) this.mnuMainFrm_Tools_Connect.Image = new Icon(_path + @"\images\connect.ico", 16, 16).ToBitmap();
            if (File.Exists(_path + @"\images\disconnect.ico")) this.mnuMainFrm_Tools_Disconnect.Image = new Icon(_path + @"\images\disconnect.ico", 16, 16).ToBitmap();
            if (File.Exists(_path + @"\images\settings.ico")) this.mnuMainFrm_Tools_Settings.Image = new Icon(_path + @"\images\settings.ico", 16, 16).ToBitmap();

            if (File.Exists(_path + @"\images\about.ico")) this.mnuMainFrm_Help_About.Image = new Icon(_path + @"\images\about.ico", 16, 16).ToBitmap();

            // Initialize the menu checked items
            this.mnuMainFrm_View_Menu.Checked = true;
            this.mnuMainFrm_View_Toolbar.Checked = true;
            this.mnuMainFrm_View_Raw.Checked = _sett.Plot_ShowRawData;
            this.mnuMainFrm_View_Radial.Checked = _sett.Plot_ShowRadar;
            this.mnuMainFrm_View_Average.Checked = _sett.Plot_ShowAverage;
            this.mnuMainFrm_View_Ratio.Checked = _sett.Plot_ShowRatios;

            // Initialize enable status
            this.mnuMainFrm_Tools_Connect.Enabled = false;
            this.mnuMainFrm_Tools_Disconnect.Enabled = false;
            this.toolStripMain_Disconnect.Enabled = false;
            this.toolStripMain_Connect.Enabled = false;

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

            //statusStrip.Renderer = new customRenderer<ToolStripLabel>(Brushes.SteelBlue, Brushes.LightSkyBlue);
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

        /// <summary>
        /// Initialize plots: titles, labels, grids, colors, and other visual stuff.
        /// It does not modify axes. That's done elsewhere (when updating the charts with values).
        /// </summary>
        private void InitializePlots()
        {
            // Starting from v 4.1.18 Refresh() must be called manually at least once
            formsPlot1.Refresh();
            formsPlot2.Refresh();
            formsPlot3.Refresh();
            formsPlot4.Refresh();

            //formsPlot1.plt.AxisAutoX(margin: 0);
            formsPlot1.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0, yMax: 1000);

            // customize styling
            formsPlot1.Plot.Palette = ScottPlot.Drawing.Palette.Category10;
            formsPlot1.Plot.Title("Illuminance");
            formsPlot1.Plot.YLabel("Lux");
            formsPlot1.Plot.XLabel("Time (seconds)");
            formsPlot1.Plot.Grid(enable: false);

            formsPlot1.MouseDown += new System.Windows.Forms.MouseEventHandler(formsPlot_Click);
            formsPlot1.MouseClick += new System.Windows.Forms.MouseEventHandler(formsPlot_Click);
            formsPlot1.Click += new EventHandler(formsPlot_Click);

            // Customize the Radar plot
            formsPlot2.Plot.Palette = ScottPlot.Drawing.Palette.Microcharts;
            formsPlot2.Plot.Grid(enable: false);
            formsPlot2.Plot.Title("Illuminance distribution");
            formsPlot2.Plot.XAxis.Ticks(false);
            formsPlot2.Plot.YAxis.Ticks(false);

            // Customize the Average plot
            formsPlot3.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0, yMax: 1000);

            formsPlot3.Plot.Palette = ScottPlot.Drawing.Palette.Nord;
            formsPlot3.Plot.Title("Average, max, min");
            formsPlot3.Plot.YLabel("Lux");
            formsPlot3.Plot.XLabel("Time (seconds)");
            formsPlot3.Plot.Grid(enable: false);

            formsPlot3.MouseDown += new System.Windows.Forms.MouseEventHandler(formsPlot_Click);

            // Customize the Ratio plot
            //formsPlot4.plt.AxisAuto(horizontalMargin: 0);
            formsPlot4.Plot.SetAxisLimits(xMin: 0, xMax: _sett.Plot_WindowPoints, yMin: 0, yMax: 1);

            formsPlot4.Plot.Palette = ScottPlot.Drawing.Palette.OneHalf;
            formsPlot4.Plot.Title("Illuminance ratios");
            formsPlot4.Plot.YLabel("Ratio");
            formsPlot4.Plot.XLabel("Time (seconds)");
            formsPlot4.Plot.Grid(enable: false);

            formsPlot4.MouseDown += new System.Windows.Forms.MouseEventHandler(formsPlot_Click);

            //formsPlot4.plt.AxisAuto(horizontalMargin: 0);
        }

        private void InitializeRadialGauge()
        {
            formsPlot2.Plot.XAxis2.Hide(false);
            formsPlot2.Plot.XAxis2.Ticks(false);
            formsPlot2.Plot.XAxis.Hide(false);
            formsPlot2.Plot.XAxis.Ticks(false);
            formsPlot2.Plot.YAxis2.Hide(false);
            formsPlot2.Plot.YAxis2.Ticks(false);
            formsPlot2.Plot.YAxis.Hide(false);
            formsPlot2.Plot.YAxis.Ticks(false);
        }

        #endregion Initialization routines 

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
            _sett.Plot_ShowRadar = status;
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
            SaveFileDialog SaveDlg = new()
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

                //switch (Path.GetExtension(SaveDlg.FileName).ToLower())
                //{
                //    case ".elux":
                //        writer = new BinaryWriter(SaveDlg.OpenFile());
                //        break;
                //    case ".txt":
                //    default:
                //        writer = new StreamWriter(SaveDlg.OpenFile());
                //        break;
                //}

                // Append millisecond pattern to current culture's full date time pattern
                string fullPattern = System.Globalization.DateTimeFormatInfo.CurrentInfo.FullDateTimePattern;
                fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", "$1.fff");
                TimeSpan nTime = _timeEnd - _timeStart;

                // Convert array data into CSV format.
                using var outfile = new StreamWriter(SaveDlg.OpenFile());
                
                // Save the header text into the file
                string content = string.Empty;
                outfile.WriteLine("ErgoLux data");
                outfile.WriteLine("Start time: {0}", _timeStart.ToString(fullPattern));
                outfile.WriteLine("End time: {0}", _timeEnd.ToString(fullPattern));
                //outfile.WriteLine("Total measuring time: {0} days, {1} hours, {2} minutes, {3} seconds, and {4} milliseconds ({5})", nTime.Days, nTime.Hours, nTime.Minutes, nTime.Seconds, nTime.Milliseconds, nTime.ToString(@"dd\-hh\:mm\:ss.fff"));
                outfile.WriteLine("Total measuring time: {0} days, {1} hours, {2} minutes, {3} seconds, and {4} milliseconds", nTime.Days, nTime.Hours, nTime.Minutes, nTime.Seconds, nTime.Milliseconds);
                outfile.WriteLine("Number of sensors: {0}", _sett.T10_NumberOfSensors.ToString());
                outfile.WriteLine("Number of data points: {0}", _nPoints.ToString());
                outfile.WriteLine("Sampling frequency: {0}", _sett.T10_Frequency.ToString());
                outfile.WriteLine();
                for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                {
                    content += "Sensor #" + i.ToString("00") + "\t";
                }
                content += "Maximum" + "\t" + "Average" + "\t" + "Minimum" + "\t" + "Min/Average" + "\t" + "Min/Max" + "\t" + "Average/Max";
                outfile.WriteLine(content);

                // Save the numerical values
                for (int j = 0; j < _nPoints; j++)
                {
                    content = string.Empty;
                    for (int i = 0; i < _plotData.Length; i++)
                    {
                        content += _plotData[i][j].ToString(i < _sett.T10_NumberOfSensors + 3 ? "#0.0" : "0.000") + "\t";
                    }
                    //trying to write data to csv
                    outfile.WriteLine(content.TrimEnd('\t'));
                }
            }

        }

        private void toolStripMain_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new()
            {
                DefaultExt = "*.elux",
                Filter = "ELUX file (*.elux)|*.elux|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Open ErgoLux file",
                InitialDirectory = _path + @"\Examples"
            };

            DialogResult result;
            using (new CenterWinDialog(this))
            {
                result = openDlg.ShowDialog(this);
            }

            // If the file name is not an empty string open it for saving.  
            if (result == DialogResult.OK && openDlg.FileName != "")
            {
                // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader
                using var fs = File.Open(openDlg.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var sr = new StreamReader(fs, Encoding.UTF8);

                int nSensors = 0, nPoints = 0;
                double nFreq = 0.0;

                string strLine = sr.ReadLine();
                if (strLine != "ErgoLux data")
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                // Better implement a try parse block. Each read line should throw an exception instead of "return"
                strLine = sr.ReadLine();
                if (!strLine.Contains("Start time: ", StringComparison.Ordinal))
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                strLine = sr.ReadLine();
                if (!strLine.Contains("End time: ", StringComparison.Ordinal))
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                strLine = sr.ReadLine();
                if (!strLine.Contains("Total measuring time: ", StringComparison.Ordinal))
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                strLine = sr.ReadLine();
                if (!strLine.Contains("Number of sensors: ", StringComparison.Ordinal))
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
                if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nSensors)) return;
                if (nSensors == 0) return;
                _sett.T10_NumberOfSensors = nSensors;

                strLine = sr.ReadLine();
                if (!strLine.Contains("Number of data points: ", StringComparison.Ordinal))
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
                if(!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nPoints)) return;
                if (nPoints == 0) return;
                _sett.Plot_ArrayPoints = nPoints;

                strLine = sr.ReadLine();
                if (!strLine.Contains("Sampling frequency: ", StringComparison.Ordinal))
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
                if (!double.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nFreq)) return;
                _sett.T10_Frequency = nFreq;

                strLine = sr.ReadLine();    // Empty line
                strLine = sr.ReadLine();    // Column header lines

                // Initialize data arrays
                InitializeArrays();

                // Read data into _plotData
                for (int i = 0; i < _plotData.Length; i++)
                {
                    _plotData[i] = new double[_sett.Plot_ArrayPoints];
                }
                string[] data;
                int row = 0, col = 0;
                while ((strLine = sr.ReadLine()) != null)
                {
                    data = strLine.Split("\t");
                    for (row = 0; row < data.Length; row++)
                    {
                        double.TryParse(data[row], out _plotData[row][col]);
                    }
                    col++;
                }

                // Show data into plots
                Plots_Clear();  // this also sets _nPoints = 0, so we need to reset it next
                _nPoints = _sett.Plot_ArrayPoints;
                Plots_DataBinding();
                Plots_ShowLegends();
                Plots_ShowFull();
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

                    // Initialize the arrays containing the data
                    InitializeArrays();

                    // First, clear all data (if any) in the plots
                    Plots_Clear();

                    // Bind the arrays to the plots
                    Plots_DataBinding();
                    
                    // Show the legends in the picture boxes
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
                        _sett.Plot_ShowRadar = label.Checked;
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
        /// Binds the data arrays to the plots. Both _plotData and _plotRadar should be initilized, otherwise this function will throw an error.
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
            
            // Binding for Plot Radar
            // Control wether 2 or more sensors, otherwise don't
            string[] labels = new string[_sett.T10_NumberOfSensors];
            for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                labels[i] = "#" + i.ToString("#0");

            //var plt = formsPlot2.Plot.AddRadar(_plotRadar, disableFrameAndGrid: false);
            //plt.FillColors[0] = Color.FromArgb(100, plt.LineColors[0]);
            //plt.FillColors[1] = Color.FromArgb(150, plt.LineColors[1]);
            ////formsPlot2.Plot.Grid(enable: false);

            //plt.AxisType = ScottPlot.RadarAxis.Polygon;
            //plt.ShowAxisValues = false;
            //plt.CategoryLabels = labels;
            //plt.GroupLabels = new string[] { "Average", "Illuminance" };

            var plt = formsPlot2.Plot.AddRadialGauge(_plotRadialGauge);
            var strLabels = new string[_sett.T10_NumberOfSensors];
            for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                strLabels[i] = "Sensor #" + i.ToString("#0");
            plt.Labels = strLabels;
            plt.StartingAngle = 180;
            InitializeRadialGauge();

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
            int nVertDist = 10;
            
            // Combine legends from Plot1 and Plot2 and draw a black border around each legend
            var legendA = formsPlot1.Plot.RenderLegend();
            var legendB = _sett.T10_NumberOfSensors > 0 ? formsPlot2.Plot.RenderLegend() : new Bitmap(1, 1);
            var bitmap = new Bitmap(Math.Max(legendA.Width, legendB.Width) + 2, legendA.Height + legendB.Height + nVertDist + 4);
            using Graphics GraphicsA = Graphics.FromImage(bitmap);
            GraphicsA.DrawRectangle(new Pen(Color.Black),
                                    (bitmap.Width - legendA.Width - 2) / 2,
                                    0,
                                    legendA.Width + 1,
                                    legendA.Height + 1);
            GraphicsA.DrawImage(legendA, (bitmap.Width - legendA.Width - 2) / 2 + 1, 1);
            GraphicsA.DrawRectangle(new Pen(Color.Black),
                                    (bitmap.Width - legendB.Width - 2) / 2,
                                    legendA.Height + nVertDist + 1,
                                    legendB.Width + 1,
                                    legendB.Height + 2);
            GraphicsA.DrawImage(legendB, (bitmap.Width - legendB.Width - 2) / 2 + 1, legendA.Height + nVertDist + 2);
            pictureBox1.Image = bitmap;

            // Combine legends from Plot3 and Plot4 and draw a black border around each legend
            legendA = formsPlot3.Plot.RenderLegend();
            legendB = formsPlot4.Plot.RenderLegend();
            bitmap = new Bitmap(Math.Max(legendA.Width, legendB.Width) + 2, legendA.Height + legendB.Height + nVertDist + 4);
            using Graphics GraphicsB = Graphics.FromImage(bitmap);
            GraphicsB.DrawRectangle(new Pen(Color.Black),
                                    (bitmap.Width - legendA.Width - 2) / 2,
                                    0,
                                    legendA.Width + 1,
                                    legendA.Height + 1);
            GraphicsB.DrawImage(legendA, (bitmap.Width - legendA.Width - 2) / 2 + 1, 1);
            GraphicsB.DrawRectangle(new Pen(Color.Black),
                                    (bitmap.Width - legendB.Width - 2) / 2,
                                    legendA.Height + nVertDist + 1,
                                    legendB.Width + 1,
                                    legendB.Height + 2);
            GraphicsB.DrawImage(legendB, (bitmap.Width - legendB.Width - 2) / 2 + 1, legendA.Height + nVertDist + 2);
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
                if (_sett.Plot_ShowRadar)
                {
                    for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                    {
                        _plotRadar[0, i] = _average;
                    }

                    //((ScottPlot.Plottable.RadarPlot)formsPlot2.Plot.GetPlottables()[0]).Update(_plotRadar, false);
                    var plot = (ScottPlot.Plottable.RadialGaugePlot)formsPlot2.Plot.GetPlottables()[0];
                    plot.Update(_plotRadialGauge);
                    plot.MaximumAngle = 180 * _plotRadialGauge.Max() / _average;
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

        /// <summary>
        /// Initializes the data arrays using <see cref="ClassSettings"/> properties T10_NumberOfPoints and ArrayFixedColumns
        /// </summary>
        private void InitializeArrays()
        {
            _plotRadialGauge = new double[_sett.T10_NumberOfSensors];
            _plotRadar = new double[2, _sett.T10_NumberOfSensors];
            _plotData = new double[_sett.T10_NumberOfSensors + _sett.ArrayFixedColumns][];
            for (int i = 0; i < _sett.T10_NumberOfSensors + _sett.ArrayFixedColumns; i++)
                _plotData[i] = new double[_sett.Plot_ArrayPoints];
        }

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
                _sett = JsonSerializer.Deserialize<ClassSettings>(jsonString);
                _sett.InitializeJsonIgnore(_path);
                //var settings = JsonSerializer.Deserialize<ClassSettings>(jsonString);
                //settings.InitializeJsonIgnore(_path);
                //_sett = settings;

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

        private void formsPlot_Click(object sender, EventArgs e)
        {
            Text = sender.ToString();
            (double mouseCoordX, double mouseCoordY) = formsPlot1.GetMouseCoordinates();
            double xyRatio = formsPlot1.Plot.XAxis.Dims.PxPerUnit / formsPlot1.Plot.YAxis.Dims.PxPerUnit;
            //(double pointX, double pointY, int pointIndex) = MyScatterPlot.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);
            //Text = $"Point index {pointIndex} at ({pointX}, {pointY})";
        }

        private void formsPlot_Click(object sender, MouseEventArgs e)
        {
            
            // If we are reading from the sensor, then exit
            if (_reading) return;

            if (e.Button != MouseButtons.Left) return;

            var MyPlot = ((ScottPlot.FormsPlot)sender);
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

            //((ScottPlot.Plottable.RadarPlot)formsPlot2.Plot.GetPlottables()[0]).Update(_plotRadar, false);
            var plot = ((ScottPlot.Plottable.RadialGaugePlot)formsPlot2.Plot.GetPlottables()[0]);
            plot.Update(_plotRadialGauge);
            plot.MaximumAngle = 180 * _plotRadialGauge.Max() / _plotRadialGauge.Average();
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
