
namespace ErgoLux
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            tspTop = new ToolStripPanel();
            tspBottom = new ToolStripPanel();
            mnuMainFrm = new MenuStrip();
            mnuMainFrm_File = new ToolStripMenuItem();
            mnuMainFrm_File_Open = new ToolStripMenuItem();
            mnuMainFrm_File_Save = new ToolStripMenuItem();
            toolStripMenuSeparator1 = new ToolStripSeparator();
            mnuMainFrm_File_Exit = new ToolStripMenuItem();
            mnuMainFrm_View = new ToolStripMenuItem();
            mnuMainFrm_View_Menu = new ToolStripMenuItem();
            mnuMainFrm_View_Toolbar = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            mnuMainFrm_View_Raw = new ToolStripMenuItem();
            mnuMainFrm_View_Distribution = new ToolStripMenuItem();
            mnuMainFrm_View_Average = new ToolStripMenuItem();
            mnuMainFrm_View_Ratio = new ToolStripMenuItem();
            mnuMainFrm_Tools = new ToolStripMenuItem();
            mnuMainFrm_Tools_Connect = new ToolStripMenuItem();
            mnuMainFrm_Tools_Disconnect = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            mnuMainFrm_Tools_Settings = new ToolStripMenuItem();
            mnuMainFrm_Help = new ToolStripMenuItem();
            mnuMainFrm_Help_About = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            statusStripLabelType = new ToolStripStatusLabel();
            statusStripLabelID = new ToolStripStatusLabel();
            statusStripLabelLocation = new ToolStripStatusLabel();
            statusStripIconOpen = new ToolStripStatusLabel();
            statusStripIconExchange = new ToolStripStatusLabel();
            statusStripLabelXtras = new ToolStripStatusLabel();
            statusStripLabelUILanguage = new ToolStripStatusLabel();
            statusStripLabelRaw = new ToolStripStatusLabelEx();
            statusStripLabelRadar = new ToolStripStatusLabelEx();
            statusStripLabelMax = new ToolStripStatusLabelEx();
            statusStripLabelRatio = new ToolStripStatusLabelEx();
            statusStripLabelCross = new ToolStripStatusLabelEx();
            toolStripMain = new ToolStrip();
            toolStripMain_Exit = new ToolStripButton();
            toolStripMain_Save = new ToolStripButton();
            toolStripMain_Open = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripMain_Connect = new ToolStripButton();
            toolStripMain_Disconnect = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripMain_Settings = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripMain_About = new ToolStripButton();
            plotData = new ScottPlot.FormsPlotCrossHair();
            plotDistribution = new ScottPlot.FormsPlotCulture();
            layoutGlobal = new TableLayoutPanel();
            pictureBox1 = new PictureBox();
            plotStats = new ScottPlot.FormsPlotCrossHair();
            plotRatio = new ScottPlot.FormsPlotCrossHair();
            pictureBox2 = new PictureBox();
            mnuMainFrm.SuspendLayout();
            statusStrip.SuspendLayout();
            toolStripMain.SuspendLayout();
            layoutGlobal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // tspTop
            // 
            tspTop.Dock = DockStyle.Top;
            tspTop.Location = new Point(0, 0);
            tspTop.Name = "tspTop";
            tspTop.Orientation = Orientation.Horizontal;
            tspTop.RowMargin = new Padding(3, 0, 0, 0);
            tspTop.Size = new Size(934, 0);
            // 
            // tspBottom
            // 
            tspBottom.Dock = DockStyle.Bottom;
            tspBottom.Location = new Point(0, 611);
            tspBottom.Name = "tspBottom";
            tspBottom.Orientation = Orientation.Horizontal;
            tspBottom.RowMargin = new Padding(3, 0, 0, 0);
            tspBottom.Size = new Size(934, 0);
            // 
            // mnuMainFrm
            // 
            mnuMainFrm.Items.AddRange(new ToolStripItem[] { mnuMainFrm_File, mnuMainFrm_View, mnuMainFrm_Tools, mnuMainFrm_Help });
            mnuMainFrm.Location = new Point(0, 0);
            mnuMainFrm.Name = "mnuMainFrm";
            mnuMainFrm.Size = new Size(934, 24);
            mnuMainFrm.TabIndex = 5;
            mnuMainFrm.Text = "menuStrip1";
            // 
            // mnuMainFrm_File
            // 
            mnuMainFrm_File.DropDownItems.AddRange(new ToolStripItem[] { mnuMainFrm_File_Open, mnuMainFrm_File_Save, toolStripMenuSeparator1, mnuMainFrm_File_Exit });
            mnuMainFrm_File.Name = "mnuMainFrm_File";
            mnuMainFrm_File.Size = new Size(37, 20);
            mnuMainFrm_File.Text = "&File";
            // 
            // mnuMainFrm_File_Open
            // 
            mnuMainFrm_File_Open.Name = "mnuMainFrm_File_Open";
            mnuMainFrm_File_Open.ShortcutKeys = Keys.Control | Keys.O;
            mnuMainFrm_File_Open.Size = new Size(155, 22);
            mnuMainFrm_File_Open.Text = "&Open...";
            mnuMainFrm_File_Open.Click += Open_Click;
            // 
            // mnuMainFrm_File_Save
            // 
            mnuMainFrm_File_Save.Name = "mnuMainFrm_File_Save";
            mnuMainFrm_File_Save.ShortcutKeys = Keys.Control | Keys.S;
            mnuMainFrm_File_Save.Size = new Size(155, 22);
            mnuMainFrm_File_Save.Text = "&Save...";
            mnuMainFrm_File_Save.Click += Save_Click;
            // 
            // toolStripMenuSeparator1
            // 
            toolStripMenuSeparator1.Name = "toolStripMenuSeparator1";
            toolStripMenuSeparator1.Size = new Size(152, 6);
            // 
            // mnuMainFrm_File_Exit
            // 
            mnuMainFrm_File_Exit.Name = "mnuMainFrm_File_Exit";
            mnuMainFrm_File_Exit.ShortcutKeys = Keys.Control | Keys.E;
            mnuMainFrm_File_Exit.Size = new Size(155, 22);
            mnuMainFrm_File_Exit.Text = "&Exit...";
            mnuMainFrm_File_Exit.Click += Exit_Click;
            // 
            // mnuMainFrm_View
            // 
            mnuMainFrm_View.DropDownItems.AddRange(new ToolStripItem[] { mnuMainFrm_View_Menu, mnuMainFrm_View_Toolbar, toolStripSeparator4, mnuMainFrm_View_Raw, mnuMainFrm_View_Distribution, mnuMainFrm_View_Average, mnuMainFrm_View_Ratio });
            mnuMainFrm_View.Name = "mnuMainFrm_View";
            mnuMainFrm_View.Size = new Size(44, 20);
            mnuMainFrm_View.Text = "&View";
            // 
            // mnuMainFrm_View_Menu
            // 
            mnuMainFrm_View_Menu.Name = "mnuMainFrm_View_Menu";
            mnuMainFrm_View_Menu.ShortcutKeys = Keys.Control | Keys.M;
            mnuMainFrm_View_Menu.Size = new Size(184, 22);
            mnuMainFrm_View_Menu.Text = "Show menu";
            mnuMainFrm_View_Menu.Click += mnuMainFrm_View_Menu_Click;
            // 
            // mnuMainFrm_View_Toolbar
            // 
            mnuMainFrm_View_Toolbar.Name = "mnuMainFrm_View_Toolbar";
            mnuMainFrm_View_Toolbar.ShortcutKeys = Keys.Control | Keys.T;
            mnuMainFrm_View_Toolbar.Size = new Size(184, 22);
            mnuMainFrm_View_Toolbar.Text = "Show toolbar";
            mnuMainFrm_View_Toolbar.Click += mnuMainFrm_View_Toolbar_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(181, 6);
            // 
            // mnuMainFrm_View_Raw
            // 
            mnuMainFrm_View_Raw.Name = "mnuMainFrm_View_Raw";
            mnuMainFrm_View_Raw.Size = new Size(184, 22);
            mnuMainFrm_View_Raw.Text = "Raw data";
            mnuMainFrm_View_Raw.Click += mnuMainFrm_View_Raw_Click;
            // 
            // mnuMainFrm_View_Distribution
            // 
            mnuMainFrm_View_Distribution.Name = "mnuMainFrm_View_Distribution";
            mnuMainFrm_View_Distribution.Size = new Size(184, 22);
            mnuMainFrm_View_Distribution.Text = "Radial distribution";
            mnuMainFrm_View_Distribution.Click += mnuMainFrm_View_Radial_Click;
            // 
            // mnuMainFrm_View_Average
            // 
            mnuMainFrm_View_Average.Name = "mnuMainFrm_View_Average";
            mnuMainFrm_View_Average.Size = new Size(184, 22);
            mnuMainFrm_View_Average.Text = "Averages";
            mnuMainFrm_View_Average.Click += mnuMainFrm_View_Average_Click;
            // 
            // mnuMainFrm_View_Ratio
            // 
            mnuMainFrm_View_Ratio.Name = "mnuMainFrm_View_Ratio";
            mnuMainFrm_View_Ratio.Size = new Size(184, 22);
            mnuMainFrm_View_Ratio.Text = "Ratios";
            mnuMainFrm_View_Ratio.Click += mnuMainFrm_View_Ratio_Click;
            // 
            // mnuMainFrm_Tools
            // 
            mnuMainFrm_Tools.DropDownItems.AddRange(new ToolStripItem[] { mnuMainFrm_Tools_Connect, mnuMainFrm_Tools_Disconnect, toolStripSeparator5, mnuMainFrm_Tools_Settings });
            mnuMainFrm_Tools.Name = "mnuMainFrm_Tools";
            mnuMainFrm_Tools.Size = new Size(46, 20);
            mnuMainFrm_Tools.Text = "&Tools";
            // 
            // mnuMainFrm_Tools_Connect
            // 
            mnuMainFrm_Tools_Connect.Name = "mnuMainFrm_Tools_Connect";
            mnuMainFrm_Tools_Connect.ShortcutKeys = Keys.Control | Keys.C;
            mnuMainFrm_Tools_Connect.Size = new Size(175, 22);
            mnuMainFrm_Tools_Connect.Text = "&Connect";
            mnuMainFrm_Tools_Connect.Click += mnuMainFrm_Tools_Connect_Click;
            // 
            // mnuMainFrm_Tools_Disconnect
            // 
            mnuMainFrm_Tools_Disconnect.Name = "mnuMainFrm_Tools_Disconnect";
            mnuMainFrm_Tools_Disconnect.ShortcutKeys = Keys.Control | Keys.D;
            mnuMainFrm_Tools_Disconnect.Size = new Size(175, 22);
            mnuMainFrm_Tools_Disconnect.Text = "&Disconnect";
            mnuMainFrm_Tools_Disconnect.Click += Disconnect_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(172, 6);
            // 
            // mnuMainFrm_Tools_Settings
            // 
            mnuMainFrm_Tools_Settings.Name = "mnuMainFrm_Tools_Settings";
            mnuMainFrm_Tools_Settings.Size = new Size(175, 22);
            mnuMainFrm_Tools_Settings.Text = "&Settings...";
            mnuMainFrm_Tools_Settings.Click += Settings_Click;
            // 
            // mnuMainFrm_Help
            // 
            mnuMainFrm_Help.DropDownItems.AddRange(new ToolStripItem[] { mnuMainFrm_Help_About });
            mnuMainFrm_Help.Name = "mnuMainFrm_Help";
            mnuMainFrm_Help.Size = new Size(44, 20);
            mnuMainFrm_Help.Text = "&Help";
            // 
            // mnuMainFrm_Help_About
            // 
            mnuMainFrm_Help_About.Name = "mnuMainFrm_Help_About";
            mnuMainFrm_Help_About.ShortcutKeys = Keys.Control | Keys.A;
            mnuMainFrm_Help_About.Size = new Size(158, 22);
            mnuMainFrm_Help_About.Text = "&About...";
            mnuMainFrm_Help_About.Click += About_Click;
            // 
            // statusStrip
            // 
            statusStrip.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusStripLabelType, statusStripLabelID, statusStripLabelLocation, statusStripIconOpen, statusStripIconExchange, statusStripLabelXtras, statusStripLabelUILanguage, statusStripLabelRaw, statusStripLabelRadar, statusStripLabelMax, statusStripLabelRatio, statusStripLabelCross });
            statusStrip.Location = new Point(0, 583);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 19, 0);
            statusStrip.RenderMode = ToolStripRenderMode.Professional;
            statusStrip.ShowItemToolTips = true;
            statusStrip.Size = new Size(934, 28);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // statusStripLabelType
            // 
            statusStripLabelType.AutoSize = false;
            statusStripLabelType.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusStripLabelType.Name = "statusStripLabelType";
            statusStripLabelType.Size = new Size(200, 23);
            statusStripLabelType.Text = "Device type";
            statusStripLabelType.TextAlign = ContentAlignment.MiddleLeft;
            statusStripLabelType.ToolTipText = "Device type";
            // 
            // statusStripLabelID
            // 
            statusStripLabelID.AutoSize = false;
            statusStripLabelID.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusStripLabelID.Name = "statusStripLabelID";
            statusStripLabelID.Size = new Size(150, 23);
            statusStripLabelID.Text = "Device ID";
            statusStripLabelID.TextAlign = ContentAlignment.MiddleLeft;
            statusStripLabelID.ToolTipText = "Device ID";
            // 
            // statusStripLabelLocation
            // 
            statusStripLabelLocation.AutoSize = false;
            statusStripLabelLocation.BorderSides = ToolStripStatusLabelBorderSides.Right;
            statusStripLabelLocation.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusStripLabelLocation.Name = "statusStripLabelLocation";
            statusStripLabelLocation.Size = new Size(150, 23);
            statusStripLabelLocation.Text = "Location ID";
            statusStripLabelLocation.TextAlign = ContentAlignment.MiddleLeft;
            statusStripLabelLocation.ToolTipText = "T-10A location ID";
            // 
            // statusStripIconOpen
            // 
            statusStripIconOpen.AutoSize = false;
            statusStripIconOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            statusStripIconOpen.ImageScaling = ToolStripItemImageScaling.None;
            statusStripIconOpen.ImageTransparentColor = Color.Transparent;
            statusStripIconOpen.Name = "statusStripIconOpen";
            statusStripIconOpen.Size = new Size(30, 23);
            statusStripIconOpen.Text = "Disconnected";
            statusStripIconOpen.ToolTipText = "Connection status";
            // 
            // statusStripIconExchange
            // 
            statusStripIconExchange.AutoSize = false;
            statusStripIconExchange.BorderSides = ToolStripStatusLabelBorderSides.Right;
            statusStripIconExchange.DisplayStyle = ToolStripItemDisplayStyle.Image;
            statusStripIconExchange.ImageScaling = ToolStripItemImageScaling.None;
            statusStripIconExchange.ImageTransparentColor = Color.Transparent;
            statusStripIconExchange.Name = "statusStripIconExchange";
            statusStripIconExchange.Size = new Size(30, 23);
            statusStripIconExchange.Text = "Receiving data";
            statusStripIconExchange.ToolTipText = "Receiving data";
            // 
            // statusStripLabelXtras
            // 
            statusStripLabelXtras.BorderSides = ToolStripStatusLabelBorderSides.Right;
            statusStripLabelXtras.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusStripLabelXtras.Name = "statusStripLabelXtras";
            statusStripLabelXtras.Size = new Size(134, 23);
            statusStripLabelXtras.Spring = true;
            statusStripLabelXtras.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusStripLabelUILanguage
            // 
            statusStripLabelUILanguage.AutoSize = false;
            statusStripLabelUILanguage.BorderSides = ToolStripStatusLabelBorderSides.Right;
            statusStripLabelUILanguage.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusStripLabelUILanguage.Name = "statusStripLabelUILanguage";
            statusStripLabelUILanguage.Size = new Size(70, 23);
            statusStripLabelUILanguage.Text = "Invariant";
            statusStripLabelUILanguage.ToolTipText = "User interface language";
            statusStripLabelUILanguage.Click += Language_Click;
            // 
            // statusStripLabelRaw
            // 
            statusStripLabelRaw.AutoSize = false;
            statusStripLabelRaw.BackColor = Color.Transparent;
            statusStripLabelRaw.Checked = false;
            statusStripLabelRaw.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusStripLabelRaw.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            statusStripLabelRaw.ForeColor = Color.LightGray;
            statusStripLabelRaw.ForeColorChecked = Color.Black;
            statusStripLabelRaw.ForeColorUnchecked = Color.LightGray;
            statusStripLabelRaw.Margin = new Padding(2, 3, 0, 2);
            statusStripLabelRaw.Name = "statusStripLabelRaw";
            statusStripLabelRaw.Size = new Size(28, 23);
            statusStripLabelRaw.Text = "W";
            statusStripLabelRaw.ToolTipText = "Plot raw data";
            statusStripLabelRaw.Click += LabelExPlots_Click;
            // 
            // statusStripLabelRadar
            // 
            statusStripLabelRadar.AutoSize = false;
            statusStripLabelRadar.BackColor = Color.Transparent;
            statusStripLabelRadar.Checked = false;
            statusStripLabelRadar.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusStripLabelRadar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            statusStripLabelRadar.ForeColor = Color.LightGray;
            statusStripLabelRadar.ForeColorChecked = Color.Black;
            statusStripLabelRadar.ForeColorUnchecked = Color.LightGray;
            statusStripLabelRadar.Margin = new Padding(2, 3, 0, 2);
            statusStripLabelRadar.Name = "statusStripLabelRadar";
            statusStripLabelRadar.Size = new Size(28, 23);
            statusStripLabelRadar.Text = "D";
            statusStripLabelRadar.ToolTipText = "Plot distribution";
            statusStripLabelRadar.Click += LabelExPlots_Click;
            // 
            // statusStripLabelMax
            // 
            statusStripLabelMax.AutoSize = false;
            statusStripLabelMax.BackColor = Color.Transparent;
            statusStripLabelMax.Checked = false;
            statusStripLabelMax.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusStripLabelMax.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            statusStripLabelMax.ForeColor = Color.LightGray;
            statusStripLabelMax.ForeColorChecked = Color.Black;
            statusStripLabelMax.ForeColorUnchecked = Color.LightGray;
            statusStripLabelMax.Margin = new Padding(2, 3, 0, 2);
            statusStripLabelMax.Name = "statusStripLabelMax";
            statusStripLabelMax.Size = new Size(28, 23);
            statusStripLabelMax.Text = "A";
            statusStripLabelMax.ToolTipText = "Plot max, average and min";
            statusStripLabelMax.Click += LabelExPlots_Click;
            // 
            // statusStripLabelRatio
            // 
            statusStripLabelRatio.AutoSize = false;
            statusStripLabelRatio.BackColor = Color.Transparent;
            statusStripLabelRatio.Checked = false;
            statusStripLabelRatio.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusStripLabelRatio.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            statusStripLabelRatio.ForeColor = Color.LightGray;
            statusStripLabelRatio.ForeColorChecked = Color.Black;
            statusStripLabelRatio.ForeColorUnchecked = Color.LightGray;
            statusStripLabelRatio.Margin = new Padding(2, 3, 0, 2);
            statusStripLabelRatio.Name = "statusStripLabelRatio";
            statusStripLabelRatio.Size = new Size(28, 23);
            statusStripLabelRatio.Text = "R";
            statusStripLabelRatio.ToolTipText = "Plot ratios";
            statusStripLabelRatio.Click += LabelExPlots_Click;
            // 
            // statusStripLabelCross
            // 
            statusStripLabelCross.AutoSize = false;
            statusStripLabelCross.BackColor = Color.Transparent;
            statusStripLabelCross.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusStripLabelCross.Checked = false;
            statusStripLabelCross.DisplayStyle = ToolStripItemDisplayStyle.Text;
            statusStripLabelCross.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            statusStripLabelCross.ForeColor = Color.LightGray;
            statusStripLabelCross.ForeColorChecked = Color.Black;
            statusStripLabelCross.ForeColorUnchecked = Color.LightGray;
            statusStripLabelCross.Margin = new Padding(2, 3, 0, 2);
            statusStripLabelCross.Name = "statusStripLabelCross";
            statusStripLabelCross.Size = new Size(28, 23);
            statusStripLabelCross.Text = "C";
            statusStripLabelCross.ToolTipText = "Show plot's crosshairs";
            statusStripLabelCross.Click += LabelExCross_Click;
            // 
            // toolStripMain
            // 
            toolStripMain.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripMain.ImageScalingSize = new Size(48, 48);
            toolStripMain.Items.AddRange(new ToolStripItem[] { toolStripMain_Exit, toolStripMain_Save, toolStripMain_Open, toolStripSeparator1, toolStripMain_Connect, toolStripMain_Disconnect, toolStripSeparator2, toolStripMain_Settings, toolStripSeparator3, toolStripMain_About });
            toolStripMain.Location = new Point(0, 24);
            toolStripMain.Name = "toolStripMain";
            toolStripMain.RenderMode = ToolStripRenderMode.Professional;
            toolStripMain.Size = new Size(934, 72);
            toolStripMain.TabIndex = 2;
            toolStripMain.Text = "Main toolbar";
            // 
            // toolStripMain_Exit
            // 
            toolStripMain_Exit.ImageTransparentColor = Color.Magenta;
            toolStripMain_Exit.Name = "toolStripMain_Exit";
            toolStripMain_Exit.Size = new Size(32, 69);
            toolStripMain_Exit.Text = "Exit";
            toolStripMain_Exit.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStripMain_Exit.ToolTipText = "Exit application";
            toolStripMain_Exit.Click += Exit_Click;
            // 
            // toolStripMain_Save
            // 
            toolStripMain_Save.ImageTransparentColor = Color.Magenta;
            toolStripMain_Save.Name = "toolStripMain_Save";
            toolStripMain_Save.Size = new Size(39, 69);
            toolStripMain_Save.Text = "Save";
            toolStripMain_Save.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStripMain_Save.ToolTipText = "Save data";
            toolStripMain_Save.CheckedChanged += Save_CheckedChanged;
            toolStripMain_Save.Click += Save_Click;
            // 
            // toolStripMain_Open
            // 
            toolStripMain_Open.ImageTransparentColor = Color.Magenta;
            toolStripMain_Open.Name = "toolStripMain_Open";
            toolStripMain_Open.Size = new Size(44, 69);
            toolStripMain_Open.Text = "Open";
            toolStripMain_Open.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStripMain_Open.Click += Open_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 72);
            // 
            // toolStripMain_Connect
            // 
            toolStripMain_Connect.CheckOnClick = true;
            toolStripMain_Connect.ImageTransparentColor = Color.Magenta;
            toolStripMain_Connect.Name = "toolStripMain_Connect";
            toolStripMain_Connect.Size = new Size(59, 69);
            toolStripMain_Connect.Text = "Connect";
            toolStripMain_Connect.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStripMain_Connect.ToolTipText = "Connect Kinnect";
            toolStripMain_Connect.CheckedChanged += Connect_CheckedChanged;
            // 
            // toolStripMain_Disconnect
            // 
            toolStripMain_Disconnect.ImageTransparentColor = Color.Magenta;
            toolStripMain_Disconnect.Name = "toolStripMain_Disconnect";
            toolStripMain_Disconnect.Size = new Size(75, 69);
            toolStripMain_Disconnect.Text = "Disconnect";
            toolStripMain_Disconnect.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStripMain_Disconnect.ToolTipText = "Disconnect Kinnect";
            toolStripMain_Disconnect.Click += Disconnect_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 72);
            // 
            // toolStripMain_Settings
            // 
            toolStripMain_Settings.ImageTransparentColor = Color.Magenta;
            toolStripMain_Settings.Name = "toolStripMain_Settings";
            toolStripMain_Settings.Size = new Size(58, 69);
            toolStripMain_Settings.Text = "Settings";
            toolStripMain_Settings.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStripMain_Settings.ToolTipText = "T-10A settings";
            toolStripMain_Settings.Click += Settings_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 72);
            // 
            // toolStripMain_About
            // 
            toolStripMain_About.Image = (Image)resources.GetObject("toolStripMain_About.Image");
            toolStripMain_About.ImageTransparentColor = Color.Magenta;
            toolStripMain_About.Name = "toolStripMain_About";
            toolStripMain_About.Size = new Size(52, 69);
            toolStripMain_About.Text = "About";
            toolStripMain_About.TextImageRelation = TextImageRelation.ImageAboveText;
            toolStripMain_About.ToolTipText = "About this software";
            toolStripMain_About.Click += About_Click;
            // 
            // plotData
            // 
            plotData.BackColor = Color.Transparent;
            plotData.CrossHairColor = Color.Red;
            plotData.CultureUI = new System.Globalization.CultureInfo("en-US");
            plotData.Dock = DockStyle.Fill;
            plotData.Location = new Point(0, 0);
            plotData.Margin = new Padding(0);
            plotData.Name = "plotData";
            plotData.ShowCrossHair = false;
            plotData.ShowCrossHairHorizontal = false;
            plotData.ShowCrossHairVertical = false;
            plotData.Size = new Size(387, 243);
            plotData.SnapToPoint = false;
            plotData.TabIndex = 0;
            // 
            // plotDistribution
            // 
            plotDistribution.BackColor = Color.Transparent;
            plotDistribution.CultureUI = new System.Globalization.CultureInfo("es-ES");
            plotDistribution.Dock = DockStyle.Fill;
            plotDistribution.Location = new Point(537, 0);
            plotDistribution.Margin = new Padding(0);
            plotDistribution.Name = "plotDistribution";
            plotDistribution.Size = new Size(388, 243);
            plotDistribution.TabIndex = 1;
            // 
            // layoutGlobal
            // 
            layoutGlobal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            layoutGlobal.BackColor = SystemColors.Control;
            layoutGlobal.ColumnCount = 3;
            layoutGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            layoutGlobal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutGlobal.Controls.Add(plotData, 0, 0);
            layoutGlobal.Controls.Add(plotDistribution, 2, 0);
            layoutGlobal.Controls.Add(pictureBox1, 1, 0);
            layoutGlobal.Controls.Add(plotStats, 0, 1);
            layoutGlobal.Controls.Add(plotRatio, 2, 1);
            layoutGlobal.Controls.Add(pictureBox2, 1, 1);
            layoutGlobal.Location = new Point(9, 96);
            layoutGlobal.Margin = new Padding(0);
            layoutGlobal.Name = "layoutGlobal";
            layoutGlobal.RowCount = 2;
            layoutGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutGlobal.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutGlobal.Size = new Size(913, 487);
            layoutGlobal.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(387, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(150, 243);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // plotStats
            // 
            plotStats.BackColor = Color.Transparent;
            plotStats.CrossHairColor = Color.Red;
            plotStats.CultureUI = new System.Globalization.CultureInfo("en-US");
            plotStats.Dock = DockStyle.Fill;
            plotStats.Location = new Point(4, 246);
            plotStats.Margin = new Padding(4, 3, 4, 3);
            plotStats.Name = "plotStats";
            plotStats.ShowCrossHair = false;
            plotStats.ShowCrossHairHorizontal = false;
            plotStats.ShowCrossHairVertical = false;
            plotStats.Size = new Size(379, 238);
            plotStats.SnapToPoint = false;
            plotStats.TabIndex = 4;
            // 
            // plotRatio
            // 
            plotRatio.BackColor = Color.Transparent;
            plotRatio.CrossHairColor = Color.Red;
            plotRatio.CultureUI = new System.Globalization.CultureInfo("en-US");
            plotRatio.Dock = DockStyle.Fill;
            plotRatio.Location = new Point(541, 246);
            plotRatio.Margin = new Padding(4, 3, 4, 3);
            plotRatio.Name = "plotRatio";
            plotRatio.ShowCrossHair = false;
            plotRatio.ShowCrossHairHorizontal = false;
            plotRatio.ShowCrossHairVertical = false;
            plotRatio.Size = new Size(380, 238);
            plotRatio.SnapToPoint = false;
            plotRatio.TabIndex = 5;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Location = new Point(387, 243);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(150, 244);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(934, 611);
            Controls.Add(layoutGlobal);
            Controls.Add(statusStrip);
            Controls.Add(toolStripMain);
            Controls.Add(mnuMainFrm);
            Controls.Add(tspBottom);
            Controls.Add(tspTop);
            MinimumSize = new Size(950, 650);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ErgoLux";
            FormClosing += FrmMain_FormClosing;
            Shown += FrmMain_Shown;
            mnuMainFrm.ResumeLayout(false);
            mnuMainFrm.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            toolStripMain.ResumeLayout(false);
            toolStripMain.PerformLayout();
            layoutGlobal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStripPanel tspTop;
        private System.Windows.Forms.ToolStripPanel tspBottom;
        private System.Windows.Forms.MenuStrip mnuMainFrm;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_File;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_File_Open;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_File_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_File_Exit;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripMain_Exit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripMain_Connect;
        private System.Windows.Forms.ToolStripButton toolStripMain_Disconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripMain_Settings;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelLocation;
        private System.Windows.Forms.ToolStripStatusLabel statusStripIconOpen;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelXtras;
        private ScottPlot.FormsPlotCrossHair plotData;
        private ScottPlot.FormsPlotCulture plotDistribution;
        private ScottPlot.FormsPlotCrossHair plotStats;
        private ScottPlot.FormsPlotCrossHair plotRatio;
        private System.Windows.Forms.TableLayoutPanel layoutGlobal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripMain_About;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelType;
        private System.Windows.Forms.ToolStripStatusLabel statusStripIconExchange;
        private System.Windows.Forms.ToolStripButton toolStripMain_Save;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelID;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelUILanguage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripButton toolStripMain_Open;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_View;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_View_Menu;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_View_Toolbar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_View_Raw;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_View_Distribution;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_View_Average;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_View_Ratio;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_Help;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_Help_About;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_Tools;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_Tools_Connect;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_Tools_Disconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFrm_Tools_Settings;
        private System.Windows.Forms.ToolStripStatusLabelEx statusStripLabelRatio;
        private System.Windows.Forms.ToolStripStatusLabelEx statusStripLabelRaw;
        private System.Windows.Forms.ToolStripStatusLabelEx statusStripLabelMax;
        private System.Windows.Forms.ToolStripStatusLabelEx statusStripLabelRadar;
        private System.Windows.Forms.ToolStripStatusLabelEx statusStripLabelCross;
    }
}

