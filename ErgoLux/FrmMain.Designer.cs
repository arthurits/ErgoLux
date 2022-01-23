
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
            this.tspTop = new System.Windows.Forms.ToolStripPanel();
            this.tspBottom = new System.Windows.Forms.ToolStripPanel();
            this.mnuMainFrm = new System.Windows.Forms.MenuStrip();
            this.mnuMainFrm_File = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMainFrm_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_View = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_View_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_View_Toolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMainFrm_View_Raw = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_View_Distribution = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_View_Average = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_View_Ratio = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_Tools_Connect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_Tools_Disconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMainFrm_Tools_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFrm_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStripLabelType = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelID = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripIconOpen = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripIconExchange = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelXtras = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelRaw = new System.Windows.Forms.ToolStripStatusLabelEx();
            this.statusStripLabelRadar = new System.Windows.Forms.ToolStripStatusLabelEx();
            this.statusStripLabelMax = new System.Windows.Forms.ToolStripStatusLabelEx();
            this.statusStripLabelRatio = new System.Windows.Forms.ToolStripStatusLabelEx();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripMain_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_Open = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_Connect = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_Disconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_Settings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_About = new System.Windows.Forms.ToolStripButton();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.formsPlot3 = new ScottPlot.FormsPlot();
            this.formsPlot4 = new ScottPlot.FormsPlot();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.mnuMainFrm.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tspTop
            // 
            this.tspTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tspTop.Location = new System.Drawing.Point(0, 0);
            this.tspTop.Name = "tspTop";
            this.tspTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspTop.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspTop.Size = new System.Drawing.Size(934, 0);
            // 
            // tspBottom
            // 
            this.tspBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspBottom.Location = new System.Drawing.Point(0, 611);
            this.tspBottom.Name = "tspBottom";
            this.tspBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspBottom.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspBottom.Size = new System.Drawing.Size(934, 0);
            // 
            // mnuMainFrm
            // 
            this.mnuMainFrm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFrm_File,
            this.mnuMainFrm_View,
            this.mnuMainFrm_Tools,
            this.mnuMainFrm_Help});
            this.mnuMainFrm.Location = new System.Drawing.Point(0, 0);
            this.mnuMainFrm.Name = "mnuMainFrm";
            this.mnuMainFrm.Size = new System.Drawing.Size(934, 24);
            this.mnuMainFrm.TabIndex = 5;
            this.mnuMainFrm.Text = "menuStrip1";
            // 
            // mnuMainFrm_File
            // 
            this.mnuMainFrm_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFrm_File_Open,
            this.mnuMainFrm_File_Save,
            this.toolStripMenuSeparator1,
            this.mnuMainFrm_File_Exit});
            this.mnuMainFrm_File.Name = "mnuMainFrm_File";
            this.mnuMainFrm_File.Size = new System.Drawing.Size(37, 20);
            this.mnuMainFrm_File.Text = "&File";
            // 
            // mnuMainFrm_File_Open
            // 
            this.mnuMainFrm_File_Open.Name = "mnuMainFrm_File_Open";
            this.mnuMainFrm_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuMainFrm_File_Open.Size = new System.Drawing.Size(180, 22);
            this.mnuMainFrm_File_Open.Text = "&Open...";
            this.mnuMainFrm_File_Open.Click += new System.EventHandler(this.mnuMainFrm_File_Open_Click);
            // 
            // mnuMainFrm_File_Save
            // 
            this.mnuMainFrm_File_Save.Name = "mnuMainFrm_File_Save";
            this.mnuMainFrm_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuMainFrm_File_Save.Size = new System.Drawing.Size(180, 22);
            this.mnuMainFrm_File_Save.Text = "&Save...";
            this.mnuMainFrm_File_Save.Click += new System.EventHandler(this.mnuMainFrm_File_Save_Click);
            // 
            // toolStripMenuSeparator1
            // 
            this.toolStripMenuSeparator1.Name = "toolStripMenuSeparator1";
            this.toolStripMenuSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuMainFrm_File_Exit
            // 
            this.mnuMainFrm_File_Exit.Name = "mnuMainFrm_File_Exit";
            this.mnuMainFrm_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuMainFrm_File_Exit.Size = new System.Drawing.Size(180, 22);
            this.mnuMainFrm_File_Exit.Text = "&Exit...";
            this.mnuMainFrm_File_Exit.Click += new System.EventHandler(this.mnuMainFrm_File_Exit_Click);
            // 
            // mnuMainFrm_View
            // 
            this.mnuMainFrm_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFrm_View_Menu,
            this.mnuMainFrm_View_Toolbar,
            this.toolStripSeparator4,
            this.mnuMainFrm_View_Raw,
            this.mnuMainFrm_View_Distribution,
            this.mnuMainFrm_View_Average,
            this.mnuMainFrm_View_Ratio});
            this.mnuMainFrm_View.Name = "mnuMainFrm_View";
            this.mnuMainFrm_View.Size = new System.Drawing.Size(44, 20);
            this.mnuMainFrm_View.Text = "&View";
            // 
            // mnuMainFrm_View_Menu
            // 
            this.mnuMainFrm_View_Menu.Name = "mnuMainFrm_View_Menu";
            this.mnuMainFrm_View_Menu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mnuMainFrm_View_Menu.Size = new System.Drawing.Size(184, 22);
            this.mnuMainFrm_View_Menu.Text = "Show menu";
            this.mnuMainFrm_View_Menu.Click += new System.EventHandler(this.mnuMainFrm_View_Menu_Click);
            // 
            // mnuMainFrm_View_Toolbar
            // 
            this.mnuMainFrm_View_Toolbar.Name = "mnuMainFrm_View_Toolbar";
            this.mnuMainFrm_View_Toolbar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mnuMainFrm_View_Toolbar.Size = new System.Drawing.Size(184, 22);
            this.mnuMainFrm_View_Toolbar.Text = "Show toolbar";
            this.mnuMainFrm_View_Toolbar.Click += new System.EventHandler(this.mnuMainFrm_View_Toolbar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(181, 6);
            // 
            // mnuMainFrm_View_Raw
            // 
            this.mnuMainFrm_View_Raw.Name = "mnuMainFrm_View_Raw";
            this.mnuMainFrm_View_Raw.Size = new System.Drawing.Size(184, 22);
            this.mnuMainFrm_View_Raw.Text = "Raw data";
            this.mnuMainFrm_View_Raw.Click += new System.EventHandler(this.mnuMainFrm_View_Raw_Click);
            // 
            // mnuMainFrm_View_Distribution
            // 
            this.mnuMainFrm_View_Distribution.Name = "mnuMainFrm_View_Distribution";
            this.mnuMainFrm_View_Distribution.Size = new System.Drawing.Size(184, 22);
            this.mnuMainFrm_View_Distribution.Text = "Radial distribution";
            this.mnuMainFrm_View_Distribution.Click += new System.EventHandler(this.mnuMainFrm_View_Radial_Click);
            // 
            // mnuMainFrm_View_Average
            // 
            this.mnuMainFrm_View_Average.Name = "mnuMainFrm_View_Average";
            this.mnuMainFrm_View_Average.Size = new System.Drawing.Size(184, 22);
            this.mnuMainFrm_View_Average.Text = "Averages";
            this.mnuMainFrm_View_Average.Click += new System.EventHandler(this.mnuMainFrm_View_Average_Click);
            // 
            // mnuMainFrm_View_Ratio
            // 
            this.mnuMainFrm_View_Ratio.Name = "mnuMainFrm_View_Ratio";
            this.mnuMainFrm_View_Ratio.Size = new System.Drawing.Size(184, 22);
            this.mnuMainFrm_View_Ratio.Text = "Ratios";
            this.mnuMainFrm_View_Ratio.Click += new System.EventHandler(this.mnuMainFrm_View_Ratio_Click);
            // 
            // mnuMainFrm_Tools
            // 
            this.mnuMainFrm_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFrm_Tools_Connect,
            this.mnuMainFrm_Tools_Disconnect,
            this.toolStripSeparator5,
            this.mnuMainFrm_Tools_Settings});
            this.mnuMainFrm_Tools.Name = "mnuMainFrm_Tools";
            this.mnuMainFrm_Tools.Size = new System.Drawing.Size(46, 20);
            this.mnuMainFrm_Tools.Text = "&Tools";
            // 
            // mnuMainFrm_Tools_Connect
            // 
            this.mnuMainFrm_Tools_Connect.Name = "mnuMainFrm_Tools_Connect";
            this.mnuMainFrm_Tools_Connect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuMainFrm_Tools_Connect.Size = new System.Drawing.Size(175, 22);
            this.mnuMainFrm_Tools_Connect.Text = "&Connect";
            this.mnuMainFrm_Tools_Connect.Click += new System.EventHandler(this.mnuMainFrm_Tools_Connect_Click);
            // 
            // mnuMainFrm_Tools_Disconnect
            // 
            this.mnuMainFrm_Tools_Disconnect.Name = "mnuMainFrm_Tools_Disconnect";
            this.mnuMainFrm_Tools_Disconnect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mnuMainFrm_Tools_Disconnect.Size = new System.Drawing.Size(175, 22);
            this.mnuMainFrm_Tools_Disconnect.Text = "&Disconnect";
            this.mnuMainFrm_Tools_Disconnect.Click += new System.EventHandler(this.mnuMainFrm_Tools_Disconnect_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(172, 6);
            // 
            // mnuMainFrm_Tools_Settings
            // 
            this.mnuMainFrm_Tools_Settings.Name = "mnuMainFrm_Tools_Settings";
            this.mnuMainFrm_Tools_Settings.Size = new System.Drawing.Size(180, 22);
            this.mnuMainFrm_Tools_Settings.Text = "&Settings...";
            this.mnuMainFrm_Tools_Settings.Click += new System.EventHandler(this.mnuMainFrm_Tools_Settings_Click);
            // 
            // mnuMainFrm_Help
            // 
            this.mnuMainFrm_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFrm_Help_About});
            this.mnuMainFrm_Help.Name = "mnuMainFrm_Help";
            this.mnuMainFrm_Help.Size = new System.Drawing.Size(44, 20);
            this.mnuMainFrm_Help.Text = "&Help";
            // 
            // mnuMainFrm_Help_About
            // 
            this.mnuMainFrm_Help_About.Name = "mnuMainFrm_Help_About";
            this.mnuMainFrm_Help_About.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuMainFrm_Help_About.Size = new System.Drawing.Size(158, 22);
            this.mnuMainFrm_Help_About.Text = "&About...";
            this.mnuMainFrm_Help_About.Click += new System.EventHandler(this.mnuMainFrm_Help_About_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabelType,
            this.statusStripLabelID,
            this.statusStripLabelLocation,
            this.statusStripIconOpen,
            this.statusStripIconExchange,
            this.statusStripLabelXtras,
            this.statusStripLabelRaw,
            this.statusStripLabelRadar,
            this.statusStripLabelMax,
            this.statusStripLabelRatio});
            this.statusStrip.Location = new System.Drawing.Point(0, 583);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(934, 28);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusStripLabelType
            // 
            this.statusStripLabelType.AutoSize = false;
            this.statusStripLabelType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelType.Name = "statusStripLabelType";
            this.statusStripLabelType.Size = new System.Drawing.Size(200, 23);
            this.statusStripLabelType.Text = "Device type";
            this.statusStripLabelType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripLabelType.ToolTipText = "Device type";
            // 
            // statusStripLabelID
            // 
            this.statusStripLabelID.AutoSize = false;
            this.statusStripLabelID.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelID.Name = "statusStripLabelID";
            this.statusStripLabelID.Size = new System.Drawing.Size(150, 23);
            this.statusStripLabelID.Text = "Device ID";
            this.statusStripLabelID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripLabelID.ToolTipText = "Device ID";
            // 
            // statusStripLabelLocation
            // 
            this.statusStripLabelLocation.AutoSize = false;
            this.statusStripLabelLocation.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusStripLabelLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelLocation.Name = "statusStripLabelLocation";
            this.statusStripLabelLocation.Size = new System.Drawing.Size(150, 23);
            this.statusStripLabelLocation.Text = "Location ID";
            this.statusStripLabelLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripLabelLocation.ToolTipText = "T-10A location ID";
            // 
            // statusStripIconOpen
            // 
            this.statusStripIconOpen.AutoSize = false;
            this.statusStripIconOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statusStripIconOpen.Name = "statusStripIconOpen";
            this.statusStripIconOpen.Size = new System.Drawing.Size(30, 23);
            this.statusStripIconOpen.Text = "Disconnected";
            this.statusStripIconOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripIconOpen.ToolTipText = "Connection status";
            // 
            // statusStripIconExchange
            // 
            this.statusStripIconExchange.AutoSize = false;
            this.statusStripIconExchange.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusStripIconExchange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statusStripIconExchange.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.statusStripIconExchange.Name = "statusStripIconExchange";
            this.statusStripIconExchange.Size = new System.Drawing.Size(30, 23);
            this.statusStripIconExchange.Text = "Exchanging data";
            this.statusStripIconExchange.ToolTipText = "Receiving data";
            // 
            // statusStripLabelXtras
            // 
            this.statusStripLabelXtras.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusStripLabelXtras.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelXtras.Name = "statusStripLabelXtras";
            this.statusStripLabelXtras.Size = new System.Drawing.Size(234, 23);
            this.statusStripLabelXtras.Spring = true;
            this.statusStripLabelXtras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStripLabelRaw
            // 
            this.statusStripLabelRaw.AutoSize = false;
            this.statusStripLabelRaw.BackColor = System.Drawing.Color.Transparent;
            this.statusStripLabelRaw.Checked = false;
            this.statusStripLabelRaw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelRaw.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelRaw.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusStripLabelRaw.Margin = new System.Windows.Forms.Padding(2, 3, 0, 2);
            this.statusStripLabelRaw.Name = "statusStripLabelRaw";
            this.statusStripLabelRaw.Size = new System.Drawing.Size(28, 23);
            this.statusStripLabelRaw.Text = "W";
            this.statusStripLabelRaw.ToolTipText = "Plot raw data";
            this.statusStripLabelRaw.Click += new System.EventHandler(this.statusStripLabelPlots_Click);
            // 
            // statusStripLabelRadar
            // 
            this.statusStripLabelRadar.AutoSize = false;
            this.statusStripLabelRadar.BackColor = System.Drawing.Color.Transparent;
            this.statusStripLabelRadar.Checked = false;
            this.statusStripLabelRadar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelRadar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelRadar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusStripLabelRadar.Margin = new System.Windows.Forms.Padding(2, 3, 0, 2);
            this.statusStripLabelRadar.Name = "statusStripLabelRadar";
            this.statusStripLabelRadar.Size = new System.Drawing.Size(28, 23);
            this.statusStripLabelRadar.Text = "D";
            this.statusStripLabelRadar.ToolTipText = "Plot distribution";
            this.statusStripLabelRadar.Click += new System.EventHandler(this.statusStripLabelPlots_Click);
            // 
            // statusStripLabelMax
            // 
            this.statusStripLabelMax.AutoSize = false;
            this.statusStripLabelMax.BackColor = System.Drawing.Color.Transparent;
            this.statusStripLabelMax.Checked = false;
            this.statusStripLabelMax.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelMax.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelMax.Margin = new System.Windows.Forms.Padding(2, 3, 0, 2);
            this.statusStripLabelMax.Name = "statusStripLabelMax";
            this.statusStripLabelMax.Size = new System.Drawing.Size(28, 23);
            this.statusStripLabelMax.Text = "A";
            this.statusStripLabelMax.ToolTipText = "Plot max, average and min";
            this.statusStripLabelMax.Click += new System.EventHandler(this.statusStripLabelPlots_Click);
            // 
            // statusStripLabelRatio
            // 
            this.statusStripLabelRatio.AutoSize = false;
            this.statusStripLabelRatio.BackColor = System.Drawing.Color.Transparent;
            this.statusStripLabelRatio.Checked = false;
            this.statusStripLabelRatio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelRatio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelRatio.Margin = new System.Windows.Forms.Padding(2, 3, 0, 2);
            this.statusStripLabelRatio.Name = "statusStripLabelRatio";
            this.statusStripLabelRatio.Size = new System.Drawing.Size(28, 23);
            this.statusStripLabelRatio.Text = "R";
            this.statusStripLabelRatio.ToolTipText = "Plot ratios";
            this.statusStripLabelRatio.Click += new System.EventHandler(this.statusStripLabelPlots_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMain_Exit,
            this.toolStripMain_Save,
            this.toolStripMain_Open,
            this.toolStripSeparator1,
            this.toolStripMain_Connect,
            this.toolStripMain_Disconnect,
            this.toolStripSeparator2,
            this.toolStripMain_Settings,
            this.toolStripSeparator3,
            this.toolStripMain_About});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMain.Size = new System.Drawing.Size(934, 72);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "Main toolbar";
            // 
            // toolStripMain_Exit
            // 
            this.toolStripMain_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Exit.Name = "toolStripMain_Exit";
            this.toolStripMain_Exit.Size = new System.Drawing.Size(32, 69);
            this.toolStripMain_Exit.Text = "Exit";
            this.toolStripMain_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Exit.ToolTipText = "Exit application";
            this.toolStripMain_Exit.Click += new System.EventHandler(this.toolStripMain_Exit_Click);
            // 
            // toolStripMain_Save
            // 
            this.toolStripMain_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Save.Name = "toolStripMain_Save";
            this.toolStripMain_Save.Size = new System.Drawing.Size(39, 69);
            this.toolStripMain_Save.Text = "Save";
            this.toolStripMain_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Save.ToolTipText = "Save data";
            this.toolStripMain_Save.Click += new System.EventHandler(this.toolStripMain_Save_Click);
            // 
            // toolStripMain_Open
            // 
            this.toolStripMain_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Open.Name = "toolStripMain_Open";
            this.toolStripMain_Open.Size = new System.Drawing.Size(44, 69);
            this.toolStripMain_Open.Text = "Open";
            this.toolStripMain_Open.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Open.Click += new System.EventHandler(this.toolStripMain_Open_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 72);
            // 
            // toolStripMain_Connect
            // 
            this.toolStripMain_Connect.CheckOnClick = true;
            this.toolStripMain_Connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Connect.Name = "toolStripMain_Connect";
            this.toolStripMain_Connect.Size = new System.Drawing.Size(59, 69);
            this.toolStripMain_Connect.Text = "Connect";
            this.toolStripMain_Connect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Connect.ToolTipText = "Connect Kinnect";
            this.toolStripMain_Connect.CheckedChanged += new System.EventHandler(this.toolStripMain_Connect_CheckedChanged);
            // 
            // toolStripMain_Disconnect
            // 
            this.toolStripMain_Disconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Disconnect.Name = "toolStripMain_Disconnect";
            this.toolStripMain_Disconnect.Size = new System.Drawing.Size(75, 69);
            this.toolStripMain_Disconnect.Text = "Disconnect";
            this.toolStripMain_Disconnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Disconnect.ToolTipText = "Disconnect Kinnect";
            this.toolStripMain_Disconnect.Click += new System.EventHandler(this.toolStripMain_Disconnect_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 72);
            // 
            // toolStripMain_Settings
            // 
            this.toolStripMain_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Settings.Name = "toolStripMain_Settings";
            this.toolStripMain_Settings.Size = new System.Drawing.Size(58, 69);
            this.toolStripMain_Settings.Text = "Settings";
            this.toolStripMain_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Settings.ToolTipText = "T-10A settings";
            this.toolStripMain_Settings.Click += new System.EventHandler(this.toolStripMain_Settings_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 72);
            // 
            // toolStripMain_About
            // 
            this.toolStripMain_About.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_About.Image")));
            this.toolStripMain_About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_About.Name = "toolStripMain_About";
            this.toolStripMain_About.Size = new System.Drawing.Size(52, 69);
            this.toolStripMain_About.Text = "About";
            this.toolStripMain_About.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_About.ToolTipText = "About this software";
            this.toolStripMain_About.Click += new System.EventHandler(this.toolStripMain_About_Click);
            // 
            // formsPlot1
            // 
            this.formsPlot1.BackColor = System.Drawing.Color.Transparent;
            this.formsPlot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot1.Location = new System.Drawing.Point(0, 0);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(0);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(381, 243);
            this.formsPlot1.TabIndex = 0;
            this.formsPlot1.PlottableDragged += new System.EventHandler(this.formsPlot_PlottableDragged);
            // 
            // formsPlot2
            // 
            this.formsPlot2.BackColor = System.Drawing.Color.Transparent;
            this.formsPlot2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot2.Location = new System.Drawing.Point(531, 0);
            this.formsPlot2.Margin = new System.Windows.Forms.Padding(0);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(382, 243);
            this.formsPlot2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.formsPlot1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 96);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(913, 487);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(381, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 243);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // formsPlot3
            // 
            this.formsPlot3.BackColor = System.Drawing.Color.Transparent;
            this.formsPlot3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot3.Location = new System.Drawing.Point(4, 246);
            this.formsPlot3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot3.Name = "formsPlot3";
            this.formsPlot3.Size = new System.Drawing.Size(373, 238);
            this.formsPlot3.TabIndex = 4;
            // 
            // formsPlot4
            // 
            this.formsPlot4.BackColor = System.Drawing.Color.Transparent;
            this.formsPlot4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot4.Location = new System.Drawing.Point(535, 246);
            this.formsPlot4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot4.Name = "formsPlot4";
            this.formsPlot4.Size = new System.Drawing.Size(374, 238);
            this.formsPlot4.TabIndex = 5;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(381, 243);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(150, 244);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(934, 611);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.mnuMainFrm);
            this.Controls.Add(this.tspBottom);
            this.Controls.Add(this.tspTop);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ErgoLux";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.mnuMainFrm.ResumeLayout(false);
            this.mnuMainFrm.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ToolStripStatusLabelEx statusStripLabelRatio;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelXtras;
        private System.Windows.Forms.ToolStripStatusLabelEx statusStripLabelRaw;
        private ScottPlot.FormsPlot formsPlot1;
        private ScottPlot.FormsPlot formsPlot2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripMain_About;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelType;
        private System.Windows.Forms.ToolStripStatusLabel statusStripIconExchange;
        private ScottPlot.FormsPlot formsPlot3;
        private ScottPlot.FormsPlot formsPlot4;
        private System.Windows.Forms.ToolStripButton toolStripMain_Save;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelID;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripStatusLabelEx statusStripLabelMax;
        private System.Windows.Forms.ToolStripStatusLabelEx statusStripLabelRadar;
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
    }
}

