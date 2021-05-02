
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
            this.statusStripLabelID = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.BtnStop = new System.Windows.Forms.Button();
            this.NumSensors = new System.Windows.Forms.NumericUpDown();
            this.BtnSettings = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mnuMainFrm.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumSensors)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspTop
            // 
            this.tspTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tspTop.Location = new System.Drawing.Point(0, 0);
            this.tspTop.Name = "tspTop";
            this.tspTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspTop.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspTop.Size = new System.Drawing.Size(800, 0);
            // 
            // tspBottom
            // 
            this.tspBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspBottom.Location = new System.Drawing.Point(0, 483);
            this.tspBottom.Name = "tspBottom";
            this.tspBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspBottom.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspBottom.Size = new System.Drawing.Size(800, 0);
            // 
            // mnuMainFrm
            // 
            this.mnuMainFrm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mnuMainFrm.Location = new System.Drawing.Point(0, 0);
            this.mnuMainFrm.Name = "mnuMainFrm";
            this.mnuMainFrm.Size = new System.Drawing.Size(1291, 24);
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
            this.statusStripLabelID,
            this.statusStripLabelStatus,
            this.statusStripLabelXtras,
            this.statusStripLabelVideo,
            this.statusStripLabelData,
            this.statusStripLabelMirror,
            this.statusStripLabelPlots,
            this.statusStripLabelSkeleton,
            this.statusStripLabelJoint,
            this.statusStripLabelAngle});
            this.statusStrip.Location = new System.Drawing.Point(0, 455);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(800, 28);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusStripLabelID
            // 
            this.statusStripLabelID.AutoSize = false;
            this.statusStripLabelID.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelID.Name = "statusStripLabelID";
            this.statusStripLabelID.Size = new System.Drawing.Size(150, 23);
            this.statusStripLabelID.Text = "ID: 0x000000000000h";
            this.statusStripLabelID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripLabelID.ToolTipText = "Kinect ID connection";
            // 
            // statusStripLabelStatus
            // 
            this.statusStripLabelStatus.AutoSize = false;
            this.statusStripLabelStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusStripLabelStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelStatus.Name = "statusStripLabelStatus";
            this.statusStripLabelStatus.Size = new System.Drawing.Size(100, 23);
            this.statusStripLabelStatus.Text = "Disconnected";
            this.statusStripLabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusStripLabelStatus.ToolTipText = "Kinect status";
            // 
            // statusStripLabelXtras
            // 
            this.statusStripLabelXtras.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelXtras.Name = "statusStripLabelXtras";
            this.statusStripLabelXtras.Size = new System.Drawing.Size(355, 23);
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
            this.statusStripLabelAngle.Enabled = false;
            this.statusStripLabelAngle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusStripLabelAngle.Name = "statusStripLabelAngle";
            this.statusStripLabelAngle.Size = new System.Drawing.Size(26, 23);
            this.statusStripLabelAngle.Text = "A";
            this.statusStripLabelAngle.ToolTipText = "Drawing angles";
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
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMain.Size = new System.Drawing.Size(800, 55);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "Main toolbar";
            // 
            // toolStripMain_Exit
            // 
            this.toolStripMain_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Exit.Name = "toolStripMain_Exit";
            this.toolStripMain_Exit.Size = new System.Drawing.Size(32, 52);
            this.toolStripMain_Exit.Text = "Exit";
            this.toolStripMain_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Exit.ToolTipText = "Exit application";
            this.toolStripMain_Exit.Click += new System.EventHandler(this.toolStripMain_Exit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripMain_Connect
            // 
            this.toolStripMain_Connect.CheckOnClick = true;
            this.toolStripMain_Connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Connect.Name = "toolStripMain_Connect";
            this.toolStripMain_Connect.Size = new System.Drawing.Size(59, 52);
            this.toolStripMain_Connect.Text = "Connect";
            this.toolStripMain_Connect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Connect.ToolTipText = "Connect Kinnect";
            this.toolStripMain_Connect.CheckedChanged += new System.EventHandler(this.toolStripMain_Connect_CheckedChanged);
            // 
            // toolStripMain_Disconnect
            // 
            this.toolStripMain_Disconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Disconnect.Name = "toolStripMain_Disconnect";
            this.toolStripMain_Disconnect.Size = new System.Drawing.Size(75, 52);
            this.toolStripMain_Disconnect.Text = "Disconnect";
            this.toolStripMain_Disconnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Disconnect.ToolTipText = "Disconnect Kinnect";
            this.toolStripMain_Disconnect.Click += new System.EventHandler(this.toolStripMain_Disconnect_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripMain_Settings
            // 
            this.toolStripMain_Settings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripMain_Settings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Settings.Image")));
            this.toolStripMain_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Settings.Name = "toolStripMain_Settings";
            this.toolStripMain_Settings.Size = new System.Drawing.Size(52, 52);
            this.toolStripMain_Settings.Text = "Settings";
            this.toolStripMain_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Settings.ToolTipText = "T-10A settings";
            this.toolStripMain_Settings.Click += new System.EventHandler(this.toolStripMain_Settings_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripMain_About
            // 
            this.toolStripMain_About.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripMain_About.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_About.Image")));
            this.toolStripMain_About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_About.Name = "toolStripMain_About";
            this.toolStripMain_About.Size = new System.Drawing.Size(52, 52);
            this.toolStripMain_About.Text = "About";
            this.toolStripMain_About.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_About.ToolTipText = "About this software";
            this.toolStripMain_About.Click += new System.EventHandler(this.toolStripMain_About_Click);
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(4, 3);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(298, 198);
            this.formsPlot1.TabIndex = 0;
            // 
            // formsPlot2
            // 
            this.formsPlot2.Location = new System.Drawing.Point(390, 3);
            this.formsPlot2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(298, 198);
            this.formsPlot2.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(13, 380);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(774, 33);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(575, 421);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(108, 31);
            this.BtnConnect.TabIndex = 3;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(703, 421);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(83, 30);
            this.BtnStop.TabIndex = 4;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // NumSensors
            // 
            this.NumSensors.Location = new System.Drawing.Point(261, 424);
            this.NumSensors.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.NumSensors.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumSensors.Name = "NumSensors";
            this.NumSensors.Size = new System.Drawing.Size(47, 23);
            this.NumSensors.TabIndex = 5;
            this.NumSensors.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // BtnSettings
            // 
            this.BtnSettings.Location = new System.Drawing.Point(422, 421);
            this.BtnSettings.Name = "BtnSettings";
            this.BtnSettings.Size = new System.Drawing.Size(104, 31);
            this.BtnSettings.TabIndex = 6;
            this.BtnSettings.Text = "Settings";
            this.BtnSettings.UseVisualStyleBackColor = true;
            this.BtnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.formsPlot1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot2, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 95);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(773, 279);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 483);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.BtnSettings);
            this.Controls.Add(this.NumSensors);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
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
            ((System.ComponentModel.ISupportInitialize)(this.NumSensors)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelStatus;
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
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.NumericUpDown NumSensors;
        private System.Windows.Forms.Button BtnSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripAbout;
        private System.Windows.Forms.ToolStripButton toolStripMain_About;
    }
}

