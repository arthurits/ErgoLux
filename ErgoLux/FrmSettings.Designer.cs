
namespace ErgoLux
{
    partial class FrmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.updSensors = new System.Windows.Forms.NumericUpDown();
            this.lblSensors = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabDevice = new System.Windows.Forms.TabPage();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.txtHz = new System.Windows.Forms.TextBox();
            this.lblOff = new System.Windows.Forms.Label();
            this.lblOn = new System.Windows.Forms.Label();
            this.txtOff = new System.Windows.Forms.TextBox();
            this.txtOn = new System.Windows.Forms.TextBox();
            this.lblParity = new System.Windows.Forms.Label();
            this.lblFlowControl = new System.Windows.Forms.Label();
            this.cboFlowControl = new System.Windows.Forms.ComboBox();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.txtBaudRate = new System.Windows.Forms.TextBox();
            this.cboStopBits = new System.Windows.Forms.ComboBox();
            this.cboDataBits = new System.Windows.Forms.ComboBox();
            this.lblDevices = new System.Windows.Forms.Label();
            this.viewDevices = new System.Windows.Forms.ListView();
            this.tabPlots = new System.Windows.Forms.TabPage();
            this.chkShowDistribution = new System.Windows.Forms.CheckBox();
            this.grpPlot = new System.Windows.Forms.GroupBox();
            this.radRadar = new System.Windows.Forms.RadioButton();
            this.radRadial = new System.Windows.Forms.RadioButton();
            this.txtPlotWindow = new System.Windows.Forms.TextBox();
            this.txtArrayPoints = new System.Windows.Forms.TextBox();
            this.lblPlotWindow = new System.Windows.Forms.Label();
            this.lblArrayPoints = new System.Windows.Forms.Label();
            this.chkShowRatio = new System.Windows.Forms.CheckBox();
            this.chkShowAverage = new System.Windows.Forms.CheckBox();
            this.chkShowRaw = new System.Windows.Forms.CheckBox();
            this.tabGUI = new System.Windows.Forms.TabPage();
            this.lblDlgPath = new System.Windows.Forms.Label();
            this.txtDataFormat = new System.Windows.Forms.TextBox();
            this.lblDataFormat = new System.Windows.Forms.Label();
            this.chkDlgPath = new System.Windows.Forms.CheckBox();
            this.grpCulture = new System.Windows.Forms.GroupBox();
            this.cboAllCultures = new System.Windows.Forms.ComboBox();
            this.radUserCulture = new System.Windows.Forms.RadioButton();
            this.radInvariantCulture = new System.Windows.Forms.RadioButton();
            this.radCurrentCulture = new System.Windows.Forms.RadioButton();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.updSensors)).BeginInit();
            this.tabSettings.SuspendLayout();
            this.tabDevice.SuspendLayout();
            this.tabPlots.SuspendLayout();
            this.grpPlot.SuspendLayout();
            this.tabGUI.SuspendLayout();
            this.grpCulture.SuspendLayout();
            this.SuspendLayout();
            // 
            // updSensors
            // 
            this.updSensors.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updSensors.Location = new System.Drawing.Point(161, 262);
            this.updSensors.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.updSensors.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updSensors.Name = "updSensors";
            this.updSensors.Size = new System.Drawing.Size(52, 25);
            this.updSensors.TabIndex = 4;
            this.updSensors.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSensors
            // 
            this.lblSensors.AutoSize = true;
            this.lblSensors.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSensors.Location = new System.Drawing.Point(23, 264);
            this.lblSensors.Name = "lblSensors";
            this.lblSensors.Size = new System.Drawing.Size(125, 19);
            this.lblSensors.TabIndex = 8;
            this.lblSensors.Text = "Number of sensors";
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAccept.Location = new System.Drawing.Point(402, 354);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(90, 30);
            this.btnAccept.TabIndex = 17;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(302, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabDevice);
            this.tabSettings.Controls.Add(this.tabPlots);
            this.tabSettings.Controls.Add(this.tabGUI);
            this.tabSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabSettings.Location = new System.Drawing.Point(12, 12);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(480, 333);
            this.tabSettings.TabIndex = 22;
            // 
            // tabDevice
            // 
            this.tabDevice.Controls.Add(this.lblFrequency);
            this.tabDevice.Controls.Add(this.txtHz);
            this.tabDevice.Controls.Add(this.lblSensors);
            this.tabDevice.Controls.Add(this.updSensors);
            this.tabDevice.Controls.Add(this.lblOff);
            this.tabDevice.Controls.Add(this.lblOn);
            this.tabDevice.Controls.Add(this.txtOff);
            this.tabDevice.Controls.Add(this.txtOn);
            this.tabDevice.Controls.Add(this.lblParity);
            this.tabDevice.Controls.Add(this.lblFlowControl);
            this.tabDevice.Controls.Add(this.cboFlowControl);
            this.tabDevice.Controls.Add(this.cboParity);
            this.tabDevice.Controls.Add(this.lblBaudRate);
            this.tabDevice.Controls.Add(this.lblStopBits);
            this.tabDevice.Controls.Add(this.lblDataBits);
            this.tabDevice.Controls.Add(this.txtBaudRate);
            this.tabDevice.Controls.Add(this.cboStopBits);
            this.tabDevice.Controls.Add(this.cboDataBits);
            this.tabDevice.Controls.Add(this.lblDevices);
            this.tabDevice.Controls.Add(this.viewDevices);
            this.tabDevice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabDevice.Location = new System.Drawing.Point(4, 26);
            this.tabDevice.Name = "tabDevice";
            this.tabDevice.Padding = new System.Windows.Forms.Padding(3);
            this.tabDevice.Size = new System.Drawing.Size(472, 303);
            this.tabDevice.TabIndex = 0;
            this.tabDevice.Text = "T-10A";
            this.tabDevice.UseVisualStyleBackColor = true;
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFrequency.Location = new System.Drawing.Point(258, 156);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(67, 19);
            this.lblFrequency.TabIndex = 37;
            this.lblFrequency.Text = "Freq. (Hz)";
            // 
            // txtHz
            // 
            this.txtHz.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtHz.Location = new System.Drawing.Point(369, 153);
            this.txtHz.Name = "txtHz";
            this.txtHz.Size = new System.Drawing.Size(32, 25);
            this.txtHz.TabIndex = 36;
            // 
            // lblOff
            // 
            this.lblOff.AutoSize = true;
            this.lblOff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOff.Location = new System.Drawing.Point(378, 264);
            this.lblOff.Name = "lblOff";
            this.lblOff.Size = new System.Drawing.Size(28, 19);
            this.lblOff.TabIndex = 35;
            this.lblOff.Text = "Off";
            // 
            // lblOn
            // 
            this.lblOn.AutoSize = true;
            this.lblOn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOn.Location = new System.Drawing.Point(295, 264);
            this.lblOn.Name = "lblOn";
            this.lblOn.Size = new System.Drawing.Size(28, 19);
            this.lblOn.TabIndex = 34;
            this.lblOn.Text = "On";
            // 
            // txtOff
            // 
            this.txtOff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOff.Location = new System.Drawing.Point(409, 261);
            this.txtOff.Name = "txtOff";
            this.txtOff.Size = new System.Drawing.Size(40, 25);
            this.txtOff.TabIndex = 33;
            // 
            // txtOn
            // 
            this.txtOn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOn.Location = new System.Drawing.Point(330, 261);
            this.txtOn.Name = "txtOn";
            this.txtOn.Size = new System.Drawing.Size(40, 25);
            this.txtOn.TabIndex = 32;
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblParity.Location = new System.Drawing.Point(23, 228);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(44, 19);
            this.lblParity.TabIndex = 31;
            this.lblParity.Text = "Parity";
            // 
            // lblFlowControl
            // 
            this.lblFlowControl.AutoSize = true;
            this.lblFlowControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFlowControl.Location = new System.Drawing.Point(258, 228);
            this.lblFlowControl.Name = "lblFlowControl";
            this.lblFlowControl.Size = new System.Drawing.Size(84, 19);
            this.lblFlowControl.TabIndex = 30;
            this.lblFlowControl.Text = "Flow control";
            // 
            // cboFlowControl
            // 
            this.cboFlowControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboFlowControl.FormattingEnabled = true;
            this.cboFlowControl.Location = new System.Drawing.Point(369, 225);
            this.cboFlowControl.Name = "cboFlowControl";
            this.cboFlowControl.Size = new System.Drawing.Size(80, 25);
            this.cboFlowControl.TabIndex = 29;
            this.cboFlowControl.SelectedIndexChanged += new System.EventHandler(this.cboFlowControl_SelectedIndexChanged);
            // 
            // cboParity
            // 
            this.cboParity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Location = new System.Drawing.Point(133, 225);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(80, 25);
            this.cboParity.TabIndex = 28;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBaudRate.Location = new System.Drawing.Point(23, 156);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(68, 19);
            this.lblBaudRate.TabIndex = 27;
            this.lblBaudRate.Text = "Baud rate";
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStopBits.Location = new System.Drawing.Point(258, 192);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(63, 19);
            this.lblStopBits.TabIndex = 26;
            this.lblStopBits.Text = "Stop bits";
            // 
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDataBits.Location = new System.Drawing.Point(23, 192);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(64, 19);
            this.lblDataBits.TabIndex = 25;
            this.lblDataBits.Text = "Data bits";
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtBaudRate.Location = new System.Drawing.Point(133, 153);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.ShortcutsEnabled = false;
            this.txtBaudRate.Size = new System.Drawing.Size(80, 25);
            this.txtBaudRate.TabIndex = 24;
            // 
            // cboStopBits
            // 
            this.cboStopBits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboStopBits.FormattingEnabled = true;
            this.cboStopBits.Location = new System.Drawing.Point(369, 189);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(80, 25);
            this.cboStopBits.TabIndex = 23;
            // 
            // cboDataBits
            // 
            this.cboDataBits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboDataBits.FormattingEnabled = true;
            this.cboDataBits.Location = new System.Drawing.Point(133, 189);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(80, 25);
            this.cboDataBits.TabIndex = 22;
            // 
            // lblDevices
            // 
            this.lblDevices.AutoSize = true;
            this.lblDevices.BackColor = System.Drawing.Color.Transparent;
            this.lblDevices.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDevices.Location = new System.Drawing.Point(10, 10);
            this.lblDevices.Name = "lblDevices";
            this.lblDevices.Size = new System.Drawing.Size(100, 19);
            this.lblDevices.TabIndex = 21;
            this.lblDevices.Text = "FTDI device list";
            // 
            // viewDevices
            // 
            this.viewDevices.FullRowSelect = true;
            this.viewDevices.GridLines = true;
            this.viewDevices.Location = new System.Drawing.Point(10, 29);
            this.viewDevices.MultiSelect = false;
            this.viewDevices.Name = "viewDevices";
            this.viewDevices.ShowGroups = false;
            this.viewDevices.Size = new System.Drawing.Size(449, 107);
            this.viewDevices.TabIndex = 20;
            this.viewDevices.UseCompatibleStateImageBehavior = false;
            this.viewDevices.View = System.Windows.Forms.View.Details;
            // 
            // tabPlots
            // 
            this.tabPlots.Controls.Add(this.chkShowDistribution);
            this.tabPlots.Controls.Add(this.grpPlot);
            this.tabPlots.Controls.Add(this.txtPlotWindow);
            this.tabPlots.Controls.Add(this.txtArrayPoints);
            this.tabPlots.Controls.Add(this.lblPlotWindow);
            this.tabPlots.Controls.Add(this.lblArrayPoints);
            this.tabPlots.Controls.Add(this.chkShowRatio);
            this.tabPlots.Controls.Add(this.chkShowAverage);
            this.tabPlots.Controls.Add(this.chkShowRaw);
            this.tabPlots.Location = new System.Drawing.Point(4, 26);
            this.tabPlots.Name = "tabPlots";
            this.tabPlots.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlots.Size = new System.Drawing.Size(472, 303);
            this.tabPlots.TabIndex = 1;
            this.tabPlots.Text = "Plots";
            this.tabPlots.UseVisualStyleBackColor = true;
            // 
            // chkShowDistribution
            // 
            this.chkShowDistribution.AutoSize = true;
            this.chkShowDistribution.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowDistribution.Location = new System.Drawing.Point(215, 29);
            this.chkShowDistribution.Name = "chkShowDistribution";
            this.chkShowDistribution.Size = new System.Drawing.Size(126, 23);
            this.chkShowDistribution.TabIndex = 2;
            this.chkShowDistribution.Text = "Plot distribution";
            this.chkShowDistribution.UseVisualStyleBackColor = true;
            this.chkShowDistribution.CheckedChanged += new System.EventHandler(this.chkShowDistribution_CheckedChanged);
            // 
            // grpPlot
            // 
            this.grpPlot.Controls.Add(this.radRadar);
            this.grpPlot.Controls.Add(this.radRadial);
            this.grpPlot.Location = new System.Drawing.Point(81, 104);
            this.grpPlot.Name = "grpPlot";
            this.grpPlot.Size = new System.Drawing.Size(187, 98);
            this.grpPlot.TabIndex = 9;
            this.grpPlot.TabStop = false;
            this.grpPlot.Text = "Plot distribution";
            // 
            // radRadar
            // 
            this.radRadar.AutoSize = true;
            this.radRadar.Location = new System.Drawing.Point(14, 24);
            this.radRadar.Name = "radRadar";
            this.radRadar.Size = new System.Drawing.Size(62, 23);
            this.radRadar.TabIndex = 10;
            this.radRadar.TabStop = true;
            this.radRadar.Text = "Radar";
            this.radRadar.UseVisualStyleBackColor = true;
            // 
            // radRadial
            // 
            this.radRadial.AutoSize = true;
            this.radRadial.Location = new System.Drawing.Point(14, 53);
            this.radRadial.Name = "radRadial";
            this.radRadial.Size = new System.Drawing.Size(105, 23);
            this.radRadial.TabIndex = 11;
            this.radRadial.TabStop = true;
            this.radRadial.Text = "Radial gauge";
            this.radRadial.UseVisualStyleBackColor = true;
            // 
            // txtPlotWindow
            // 
            this.txtPlotWindow.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPlotWindow.Location = new System.Drawing.Point(229, 251);
            this.txtPlotWindow.Name = "txtPlotWindow";
            this.txtPlotWindow.Size = new System.Drawing.Size(85, 25);
            this.txtPlotWindow.TabIndex = 7;
            // 
            // txtArrayPoints
            // 
            this.txtArrayPoints.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtArrayPoints.Location = new System.Drawing.Point(229, 220);
            this.txtArrayPoints.Name = "txtArrayPoints";
            this.txtArrayPoints.Size = new System.Drawing.Size(85, 25);
            this.txtArrayPoints.TabIndex = 6;
            // 
            // lblPlotWindow
            // 
            this.lblPlotWindow.AutoSize = true;
            this.lblPlotWindow.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPlotWindow.Location = new System.Drawing.Point(31, 254);
            this.lblPlotWindow.Name = "lblPlotWindow";
            this.lblPlotWindow.Size = new System.Drawing.Size(145, 19);
            this.lblPlotWindow.TabIndex = 5;
            this.lblPlotWindow.Text = "Plot window (seconds)";
            // 
            // lblArrayPoints
            // 
            this.lblArrayPoints.AutoSize = true;
            this.lblArrayPoints.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblArrayPoints.Location = new System.Drawing.Point(31, 223);
            this.lblArrayPoints.Name = "lblArrayPoints";
            this.lblArrayPoints.Size = new System.Drawing.Size(146, 19);
            this.lblArrayPoints.TabIndex = 4;
            this.lblArrayPoints.Text = "Array minimum points";
            // 
            // chkShowRatio
            // 
            this.chkShowRatio.AutoSize = true;
            this.chkShowRatio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowRatio.Location = new System.Drawing.Point(215, 66);
            this.chkShowRatio.Name = "chkShowRatio";
            this.chkShowRatio.Size = new System.Drawing.Size(90, 23);
            this.chkShowRatio.TabIndex = 3;
            this.chkShowRatio.Text = "Plot ratios";
            this.chkShowRatio.UseVisualStyleBackColor = true;
            // 
            // chkShowAverage
            // 
            this.chkShowAverage.AutoSize = true;
            this.chkShowAverage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowAverage.Location = new System.Drawing.Point(27, 66);
            this.chkShowAverage.Name = "chkShowAverage";
            this.chkShowAverage.Size = new System.Drawing.Size(104, 23);
            this.chkShowAverage.TabIndex = 2;
            this.chkShowAverage.Text = "Plot average";
            this.chkShowAverage.UseVisualStyleBackColor = true;
            // 
            // chkShowRaw
            // 
            this.chkShowRaw.AutoSize = true;
            this.chkShowRaw.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowRaw.Location = new System.Drawing.Point(27, 29);
            this.chkShowRaw.Name = "chkShowRaw";
            this.chkShowRaw.Size = new System.Drawing.Size(109, 23);
            this.chkShowRaw.TabIndex = 0;
            this.chkShowRaw.Text = "Plot raw data";
            this.chkShowRaw.UseVisualStyleBackColor = true;
            // 
            // tabGUI
            // 
            this.tabGUI.Controls.Add(this.lblDlgPath);
            this.tabGUI.Controls.Add(this.txtDataFormat);
            this.tabGUI.Controls.Add(this.lblDataFormat);
            this.tabGUI.Controls.Add(this.chkDlgPath);
            this.tabGUI.Controls.Add(this.grpCulture);
            this.tabGUI.Location = new System.Drawing.Point(4, 26);
            this.tabGUI.Name = "tabGUI";
            this.tabGUI.Padding = new System.Windows.Forms.Padding(3);
            this.tabGUI.Size = new System.Drawing.Size(472, 303);
            this.tabGUI.TabIndex = 2;
            this.tabGUI.Text = "Interface";
            this.tabGUI.UseVisualStyleBackColor = true;
            // 
            // lblDlgPath
            // 
            this.lblDlgPath.AutoSize = true;
            this.lblDlgPath.Location = new System.Drawing.Point(45, 210);
            this.lblDlgPath.MaximumSize = new System.Drawing.Size(350, 0);
            this.lblDlgPath.Name = "lblDlgPath";
            this.lblDlgPath.Size = new System.Drawing.Size(292, 19);
            this.lblDlgPath.TabIndex = 4;
            this.lblDlgPath.Text = "Remember previous path in open/save dialogs";
            // 
            // txtDataFormat
            // 
            this.txtDataFormat.Location = new System.Drawing.Point(259, 254);
            this.txtDataFormat.Name = "txtDataFormat";
            this.txtDataFormat.Size = new System.Drawing.Size(100, 25);
            this.txtDataFormat.TabIndex = 3;
            // 
            // lblDataFormat
            // 
            this.lblDataFormat.AutoSize = true;
            this.lblDataFormat.Location = new System.Drawing.Point(26, 257);
            this.lblDataFormat.MaximumSize = new System.Drawing.Size(230, 0);
            this.lblDataFormat.Name = "lblDataFormat";
            this.lblDataFormat.Size = new System.Drawing.Size(177, 19);
            this.lblDataFormat.TabIndex = 2;
            this.lblDataFormat.Text = "Numeric data-format string";
            // 
            // chkDlgPath
            // 
            this.chkDlgPath.AutoSize = true;
            this.chkDlgPath.Location = new System.Drawing.Point(29, 213);
            this.chkDlgPath.Name = "chkDlgPath";
            this.chkDlgPath.Size = new System.Drawing.Size(15, 14);
            this.chkDlgPath.TabIndex = 1;
            this.chkDlgPath.UseVisualStyleBackColor = true;
            // 
            // grpCulture
            // 
            this.grpCulture.Controls.Add(this.cboAllCultures);
            this.grpCulture.Controls.Add(this.radUserCulture);
            this.grpCulture.Controls.Add(this.radInvariantCulture);
            this.grpCulture.Controls.Add(this.radCurrentCulture);
            this.grpCulture.Location = new System.Drawing.Point(29, 24);
            this.grpCulture.Name = "grpCulture";
            this.grpCulture.Size = new System.Drawing.Size(364, 171);
            this.grpCulture.TabIndex = 0;
            this.grpCulture.TabStop = false;
            this.grpCulture.Text = "Culture settings";
            // 
            // cboAllCultures
            // 
            this.cboAllCultures.FormattingEnabled = true;
            this.cboAllCultures.Location = new System.Drawing.Point(71, 135);
            this.cboAllCultures.Name = "cboAllCultures";
            this.cboAllCultures.Size = new System.Drawing.Size(190, 25);
            this.cboAllCultures.TabIndex = 3;
            this.cboAllCultures.SelectedValueChanged += new System.EventHandler(this.AllCultures_SelectedValueChanged);
            // 
            // radUserCulture
            // 
            this.radUserCulture.AutoSize = true;
            this.radUserCulture.Location = new System.Drawing.Point(27, 102);
            this.radUserCulture.Name = "radUserCulture";
            this.radUserCulture.Size = new System.Drawing.Size(101, 23);
            this.radUserCulture.TabIndex = 2;
            this.radUserCulture.TabStop = true;
            this.radUserCulture.Text = "User culture";
            this.radUserCulture.UseVisualStyleBackColor = true;
            this.radUserCulture.CheckedChanged += new System.EventHandler(this.UserCulture_CheckedChanged);
            // 
            // radInvariantCulture
            // 
            this.radInvariantCulture.AutoSize = true;
            this.radInvariantCulture.Location = new System.Drawing.Point(27, 66);
            this.radInvariantCulture.Name = "radInvariantCulture";
            this.radInvariantCulture.Size = new System.Drawing.Size(127, 23);
            this.radInvariantCulture.TabIndex = 1;
            this.radInvariantCulture.TabStop = true;
            this.radInvariantCulture.Text = "Invariant culture";
            this.radInvariantCulture.UseVisualStyleBackColor = true;
            this.radInvariantCulture.CheckedChanged += new System.EventHandler(this.InvariantCulture_CheckedChanged);
            // 
            // radCurrentCulture
            // 
            this.radCurrentCulture.AutoSize = true;
            this.radCurrentCulture.Location = new System.Drawing.Point(27, 30);
            this.radCurrentCulture.Name = "radCurrentCulture";
            this.radCurrentCulture.Size = new System.Drawing.Size(120, 23);
            this.radCurrentCulture.TabIndex = 0;
            this.radCurrentCulture.TabStop = true;
            this.radCurrentCulture.Text = "Current culture";
            this.radCurrentCulture.UseVisualStyleBackColor = true;
            this.radCurrentCulture.CheckedChanged += new System.EventHandler(this.CurrentCulture_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReset.Location = new System.Drawing.Point(12, 355);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(90, 30);
            this.btnReset.TabIndex = 23;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(504, 396);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.tabSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "T-10 illuminance meter settings";
            ((System.ComponentModel.ISupportInitialize)(this.updSensors)).EndInit();
            this.tabSettings.ResumeLayout(false);
            this.tabDevice.ResumeLayout(false);
            this.tabDevice.PerformLayout();
            this.tabPlots.ResumeLayout(false);
            this.tabPlots.PerformLayout();
            this.grpPlot.ResumeLayout(false);
            this.grpPlot.PerformLayout();
            this.tabGUI.ResumeLayout(false);
            this.tabGUI.PerformLayout();
            this.grpCulture.ResumeLayout(false);
            this.grpCulture.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown updSensors;
        private System.Windows.Forms.Label lblSensors;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabDevice;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.TextBox txtHz;
        private System.Windows.Forms.Label lblOff;
        private System.Windows.Forms.Label lblOn;
        private System.Windows.Forms.TextBox txtOff;
        private System.Windows.Forms.TextBox txtOn;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.Label lblFlowControl;
        private System.Windows.Forms.ComboBox cboFlowControl;
        private System.Windows.Forms.ComboBox cboParity;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.TextBox txtBaudRate;
        private System.Windows.Forms.ComboBox cboStopBits;
        private System.Windows.Forms.ComboBox cboDataBits;
        private System.Windows.Forms.Label lblDevices;
        private System.Windows.Forms.ListView viewDevices;
        private System.Windows.Forms.TabPage tabPlots;
        private System.Windows.Forms.Label lblPlotWindow;
        private System.Windows.Forms.Label lblArrayPoints;
        private System.Windows.Forms.CheckBox chkShowRatio;
        private System.Windows.Forms.CheckBox chkShowAverage;
        private System.Windows.Forms.CheckBox chkShowRaw;
        private System.Windows.Forms.TextBox txtPlotWindow;
        private System.Windows.Forms.TextBox txtArrayPoints;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkShowDistribution;
        private System.Windows.Forms.GroupBox grpPlot;
        private System.Windows.Forms.RadioButton radRadar;
        private System.Windows.Forms.RadioButton radRadial;
        private TabPage tabGUI;
        private CheckBox chkDlgPath;
        private GroupBox grpCulture;
        private RadioButton radInvariantCulture;
        private RadioButton radCurrentCulture;
        private TextBox txtDataFormat;
        private Label lblDataFormat;
        private ComboBox cboAllCultures;
        private RadioButton radUserCulture;
        private Label lblDlgPath;
    }
}