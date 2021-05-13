
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.tspTop = new System.Windows.Forms.ToolStripPanel();
            this.tspBottom = new System.Windows.Forms.ToolStripPanel();
            this.mnuMainFrm = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectKinectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconectKinectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStripLabelType = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelID = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripIconOpen = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripIconExchange = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelXtras = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelVideo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelData = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelMirror = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelPlots = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelSkeleton = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelJoint = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelAngle = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripMain_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_Connect = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_Disconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_Settings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_About = new System.Windows.Forms.ToolStripButton();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.BtnStop = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.formsPlot3 = new ScottPlot.FormsPlot();
            this.formsPlot4 = new ScottPlot.FormsPlot();
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
            this.tspTop.Size = new System.Drawing.Size(933, 0);
            // 
            // tspBottom
            // 
            this.tspBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspBottom.Location = new System.Drawing.Point(0, 603);
            this.tspBottom.Name = "tspBottom";
            this.tspBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspBottom.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspBottom.Size = new System.Drawing.Size(933, 0);
            // 
            // mnuMainFrm
            // 
            this.mnuMainFrm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mnuMainFrm.Location = new System.Drawing.Point(0, 0);
            this.mnuMainFrm.Name = "mnuMainFrm";
            this.mnuMainFrm.Size = new System.Drawing.Size(933, 24);
            this.mnuMainFrm.TabIndex = 5;
            this.mnuMainFrm.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectKinectToolStripMenuItem,
            this.disconectKinectToolStripMenuItem,
            this.toolStripMenuSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // connectKinectToolStripMenuItem
            // 
            this.connectKinectToolStripMenuItem.Name = "connectKinectToolStripMenuItem";
            this.connectKinectToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.connectKinectToolStripMenuItem.Text = "&Connect Kinect";
            // 
            // disconectKinectToolStripMenuItem
            // 
            this.disconectKinectToolStripMenuItem.Name = "disconectKinectToolStripMenuItem";
            this.disconectKinectToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.disconectKinectToolStripMenuItem.Text = "&Disconect Kinect";
            // 
            // toolStripMenuSeparator1
            // 
            this.toolStripMenuSeparator1.Name = "toolStripMenuSeparator1";
            this.toolStripMenuSeparator1.Size = new System.Drawing.Size(159, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabelType,
            this.statusStripLabelID,
            this.statusStripIconOpen,
            this.statusStripIconExchange,
            this.statusStripLabelXtras,
            this.statusStripLabelVideo,
            this.statusStripLabelData,
            this.statusStripLabelMirror,
            this.statusStripLabelPlots,
            this.statusStripLabelSkeleton,
            this.statusStripLabelJoint,
            this.statusStripLabelAngle});
            this.statusStrip.Location = new System.Drawing.Point(0, 575);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(933, 28);
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
            this.statusStripLabelID.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusStripLabelID.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelID.Name = "statusStripLabelID";
            this.statusStripLabelID.Size = new System.Drawing.Size(150, 23);
            this.statusStripLabelID.Text = "Location ID";
            this.statusStripLabelID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripLabelID.ToolTipText = "T-10A location ID";
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
            this.statusStripLabelXtras.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelXtras.Name = "statusStripLabelXtras";
            this.statusStripLabelXtras.Size = new System.Drawing.Size(378, 23);
            this.statusStripLabelXtras.Spring = true;
            this.statusStripLabelXtras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStripLabelVideo
            // 
            this.statusStripLabelVideo.AutoSize = false;
            this.statusStripLabelVideo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusStripLabelVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelVideo.Enabled = false;
            this.statusStripLabelVideo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelVideo.Name = "statusStripLabelVideo";
            this.statusStripLabelVideo.Size = new System.Drawing.Size(22, 23);
            this.statusStripLabelVideo.Text = "V";
            this.statusStripLabelVideo.ToolTipText = "Video stream";
            // 
            // statusStripLabelData
            // 
            this.statusStripLabelData.AutoSize = false;
            this.statusStripLabelData.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusStripLabelData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelData.Enabled = false;
            this.statusStripLabelData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusStripLabelData.Name = "statusStripLabelData";
            this.statusStripLabelData.Size = new System.Drawing.Size(23, 23);
            this.statusStripLabelData.Text = "D";
            this.statusStripLabelData.ToolTipText = "Saving data";
            // 
            // statusStripLabelMirror
            // 
            this.statusStripLabelMirror.AutoSize = false;
            this.statusStripLabelMirror.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusStripLabelMirror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelMirror.Enabled = false;
            this.statusStripLabelMirror.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelMirror.Name = "statusStripLabelMirror";
            this.statusStripLabelMirror.Size = new System.Drawing.Size(26, 23);
            this.statusStripLabelMirror.Text = "M";
            this.statusStripLabelMirror.ToolTipText = "Mirror Kinect video";
            // 
            // statusStripLabelPlots
            // 
            this.statusStripLabelPlots.AutoSize = false;
            this.statusStripLabelPlots.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusStripLabelPlots.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelPlots.Enabled = false;
            this.statusStripLabelPlots.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelPlots.Name = "statusStripLabelPlots";
            this.statusStripLabelPlots.Size = new System.Drawing.Size(26, 23);
            this.statusStripLabelPlots.Text = "P";
            this.statusStripLabelPlots.ToolTipText = "Plotting data";
            // 
            // statusStripLabelSkeleton
            // 
            this.statusStripLabelSkeleton.AutoSize = false;
            this.statusStripLabelSkeleton.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusStripLabelSkeleton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelSkeleton.Enabled = false;
            this.statusStripLabelSkeleton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelSkeleton.Name = "statusStripLabelSkeleton";
            this.statusStripLabelSkeleton.Size = new System.Drawing.Size(26, 23);
            this.statusStripLabelSkeleton.Text = "S";
            this.statusStripLabelSkeleton.ToolTipText = "Drawing skeleton";
            // 
            // statusStripLabelJoint
            // 
            this.statusStripLabelJoint.AutoSize = false;
            this.statusStripLabelJoint.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusStripLabelJoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelJoint.Enabled = false;
            this.statusStripLabelJoint.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelJoint.Name = "statusStripLabelJoint";
            this.statusStripLabelJoint.Size = new System.Drawing.Size(26, 23);
            this.statusStripLabelJoint.Text = "J";
            this.statusStripLabelJoint.ToolTipText = "Drawing joints";
            // 
            // statusStripLabelAngle
            // 
            this.statusStripLabelAngle.AutoSize = false;
            this.statusStripLabelAngle.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusStripLabelAngle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelAngle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelAngle.Name = "statusStripLabelAngle";
            this.statusStripLabelAngle.Size = new System.Drawing.Size(26, 23);
            this.statusStripLabelAngle.Text = "A";
            this.statusStripLabelAngle.ToolTipText = "Drawing angles";
            this.statusStripLabelAngle.Click += new System.EventHandler(this.statusStripLabelAngle_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMain_Exit,
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
            this.toolStripMain.Size = new System.Drawing.Size(933, 72);
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
            this.formsPlot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot1.Location = new System.Drawing.Point(0, 0);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(0);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(376, 214);
            this.formsPlot1.TabIndex = 0;
            // 
            // formsPlot2
            // 
            this.formsPlot2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot2.Location = new System.Drawing.Point(526, 0);
            this.formsPlot2.Margin = new System.Windows.Forms.Padding(0);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(376, 214);
            this.formsPlot2.TabIndex = 1;
            // 
            // BtnConnect
            // 
            this.BtnConnect.Enabled = false;
            this.BtnConnect.Location = new System.Drawing.Point(710, 532);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(108, 31);
            this.BtnConnect.TabIndex = 3;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Enabled = false;
            this.BtnStop.Location = new System.Drawing.Point(838, 532);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(83, 30);
            this.BtnStop.TabIndex = 4;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.formsPlot1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot4, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 96);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(912, 429);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(376, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 214);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(902, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(10, 214);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // formsPlot3
            // 
            this.formsPlot3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot3.Location = new System.Drawing.Point(4, 217);
            this.formsPlot3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot3.Name = "formsPlot3";
            this.formsPlot3.Size = new System.Drawing.Size(368, 209);
            this.formsPlot3.TabIndex = 4;
            // 
            // formsPlot4
            // 
            this.formsPlot4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot4.Location = new System.Drawing.Point(530, 217);
            this.formsPlot4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot4.Name = "formsPlot4";
            this.formsPlot4.Size = new System.Drawing.Size(368, 209);
            this.formsPlot4.TabIndex = 5;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(933, 603);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.mnuMainFrm);
            this.Controls.Add(this.tspBottom);
            this.Controls.Add(this.tspTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectKinectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconectKinectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripMain_Exit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripMain_Connect;
        private System.Windows.Forms.ToolStripButton toolStripMain_Disconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripMain_Settings;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelID;
        private System.Windows.Forms.ToolStripStatusLabel statusStripIconOpen;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelData;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelMirror;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelPlots;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelSkeleton;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelJoint;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelAngle;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelXtras;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelVideo;
        private ScottPlot.FormsPlot formsPlot1;
        private ScottPlot.FormsPlot formsPlot2;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripAbout;
        private System.Windows.Forms.ToolStripButton toolStripMain_About;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelType;
        private System.Windows.Forms.ToolStripStatusLabel statusStripIconExchange;
        private ScottPlot.FormsPlot formsPlot3;
        private ScottPlot.FormsPlot formsPlot4;
    }
}

