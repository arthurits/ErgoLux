
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
            updSensors = new NumericUpDown();
            lblSensors = new Label();
            btnAccept = new Button();
            btnCancel = new Button();
            tabSettings = new TabControl();
            tabDevice = new TabPage();
            lblFrequency = new Label();
            txtHz = new TextBox();
            lblOff = new Label();
            lblOn = new Label();
            txtOff = new TextBox();
            txtOn = new TextBox();
            lblParity = new Label();
            lblFlowControl = new Label();
            cboFlowControl = new ComboBox();
            cboParity = new ComboBox();
            lblBaudRate = new Label();
            lblStopBits = new Label();
            lblDataBits = new Label();
            txtBaudRate = new TextBox();
            cboStopBits = new ComboBox();
            cboDataBits = new ComboBox();
            lblDevices = new Label();
            viewDevices = new ListView();
            tabPlots = new TabPage();
            lblChkShowRatio = new Label();
            lblChkShowDistribution = new Label();
            lblChkShowAverage = new Label();
            lblChkShowRaw = new Label();
            chkShowDistribution = new CheckBox();
            grpPlot = new GroupBox();
            radRadar = new RadioButton();
            radRadial = new RadioButton();
            txtPlotWindow = new TextBox();
            txtArrayPoints = new TextBox();
            lblPlotWindow = new Label();
            lblArrayPoints = new Label();
            chkShowRatio = new CheckBox();
            chkShowAverage = new CheckBox();
            chkShowRaw = new CheckBox();
            tabGUI = new TabPage();
            lblDlgPath = new Label();
            txtDataFormat = new TextBox();
            lblDataFormat = new Label();
            chkDlgPath = new CheckBox();
            grpCulture = new GroupBox();
            cboAllCultures = new ComboBox();
            radUserCulture = new RadioButton();
            radInvariantCulture = new RadioButton();
            radCurrentCulture = new RadioButton();
            btnReset = new Button();
            ((System.ComponentModel.ISupportInitialize)updSensors).BeginInit();
            tabSettings.SuspendLayout();
            tabDevice.SuspendLayout();
            tabPlots.SuspendLayout();
            grpPlot.SuspendLayout();
            tabGUI.SuspendLayout();
            grpCulture.SuspendLayout();
            SuspendLayout();
            // 
            // updSensors
            // 
            updSensors.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            updSensors.Location = new Point(163, 262);
            updSensors.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            updSensors.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            updSensors.Name = "updSensors";
            updSensors.Size = new Size(52, 25);
            updSensors.TabIndex = 4;
            updSensors.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblSensors
            // 
            lblSensors.AutoSize = true;
            lblSensors.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblSensors.Location = new Point(23, 264);
            lblSensors.Name = "lblSensors";
            lblSensors.Size = new Size(125, 19);
            lblSensors.TabIndex = 8;
            lblSensors.Text = "Number of sensors";
            // 
            // btnAccept
            // 
            btnAccept.DialogResult = DialogResult.OK;
            btnAccept.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnAccept.Location = new Point(402, 354);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(90, 30);
            btnAccept.TabIndex = 3;
            btnAccept.Text = "&Accept";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += Accept_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnCancel.Location = new Point(302, 354);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += Cancel_Click;
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(tabDevice);
            tabSettings.Controls.Add(tabPlots);
            tabSettings.Controls.Add(tabGUI);
            tabSettings.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            tabSettings.Location = new Point(12, 12);
            tabSettings.Name = "tabSettings";
            tabSettings.SelectedIndex = 0;
            tabSettings.Size = new Size(480, 333);
            tabSettings.TabIndex = 0;
            // 
            // tabDevice
            // 
            tabDevice.Controls.Add(lblFrequency);
            tabDevice.Controls.Add(txtHz);
            tabDevice.Controls.Add(lblSensors);
            tabDevice.Controls.Add(updSensors);
            tabDevice.Controls.Add(lblOff);
            tabDevice.Controls.Add(lblOn);
            tabDevice.Controls.Add(txtOff);
            tabDevice.Controls.Add(txtOn);
            tabDevice.Controls.Add(lblParity);
            tabDevice.Controls.Add(lblFlowControl);
            tabDevice.Controls.Add(cboFlowControl);
            tabDevice.Controls.Add(cboParity);
            tabDevice.Controls.Add(lblBaudRate);
            tabDevice.Controls.Add(lblStopBits);
            tabDevice.Controls.Add(lblDataBits);
            tabDevice.Controls.Add(txtBaudRate);
            tabDevice.Controls.Add(cboStopBits);
            tabDevice.Controls.Add(cboDataBits);
            tabDevice.Controls.Add(lblDevices);
            tabDevice.Controls.Add(viewDevices);
            tabDevice.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            tabDevice.Location = new Point(4, 26);
            tabDevice.Name = "tabDevice";
            tabDevice.Padding = new Padding(3);
            tabDevice.Size = new Size(472, 303);
            tabDevice.TabIndex = 0;
            tabDevice.Text = "T-10A";
            tabDevice.UseVisualStyleBackColor = true;
            // 
            // lblFrequency
            // 
            lblFrequency.AutoSize = true;
            lblFrequency.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblFrequency.Location = new Point(258, 156);
            lblFrequency.Name = "lblFrequency";
            lblFrequency.Size = new Size(67, 19);
            lblFrequency.TabIndex = 10;
            lblFrequency.Text = "Freq. (Hz)";
            // 
            // txtHz
            // 
            txtHz.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtHz.Location = new Point(369, 153);
            txtHz.Name = "txtHz";
            txtHz.Size = new Size(32, 25);
            txtHz.TabIndex = 5;
            // 
            // lblOff
            // 
            lblOff.AutoSize = true;
            lblOff.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblOff.Location = new Point(378, 264);
            lblOff.Name = "lblOff";
            lblOff.Size = new Size(28, 19);
            lblOff.TabIndex = 18;
            lblOff.Text = "Off";
            // 
            // lblOn
            // 
            lblOn.AutoSize = true;
            lblOn.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblOn.Location = new Point(295, 264);
            lblOn.Name = "lblOn";
            lblOn.Size = new Size(28, 19);
            lblOn.TabIndex = 16;
            lblOn.Text = "On";
            // 
            // txtOff
            // 
            txtOff.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtOff.Location = new Point(409, 261);
            txtOff.Name = "txtOff";
            txtOff.Size = new Size(40, 25);
            txtOff.TabIndex = 9;
            // 
            // txtOn
            // 
            txtOn.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtOn.Location = new Point(330, 261);
            txtOn.Name = "txtOn";
            txtOn.Size = new Size(40, 25);
            txtOn.TabIndex = 8;
            // 
            // lblParity
            // 
            lblParity.AutoSize = true;
            lblParity.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblParity.Location = new Point(23, 228);
            lblParity.Name = "lblParity";
            lblParity.Size = new Size(44, 19);
            lblParity.TabIndex = 6;
            lblParity.Text = "Parity";
            // 
            // lblFlowControl
            // 
            lblFlowControl.AutoSize = true;
            lblFlowControl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblFlowControl.Location = new Point(258, 228);
            lblFlowControl.Name = "lblFlowControl";
            lblFlowControl.Size = new Size(84, 19);
            lblFlowControl.TabIndex = 14;
            lblFlowControl.Text = "Flow control";
            // 
            // cboFlowControl
            // 
            cboFlowControl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cboFlowControl.FormattingEnabled = true;
            cboFlowControl.Location = new Point(369, 225);
            cboFlowControl.Name = "cboFlowControl";
            cboFlowControl.Size = new Size(80, 25);
            cboFlowControl.TabIndex = 7;
            cboFlowControl.SelectedIndexChanged += cboFlowControl_SelectedIndexChanged;
            // 
            // cboParity
            // 
            cboParity.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cboParity.FormattingEnabled = true;
            cboParity.Location = new Point(150, 225);
            cboParity.Name = "cboParity";
            cboParity.Size = new Size(65, 25);
            cboParity.TabIndex = 3;
            // 
            // lblBaudRate
            // 
            lblBaudRate.AutoSize = true;
            lblBaudRate.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblBaudRate.Location = new Point(23, 156);
            lblBaudRate.MaximumSize = new Size(140, 0);
            lblBaudRate.Name = "lblBaudRate";
            lblBaudRate.Size = new Size(68, 19);
            lblBaudRate.TabIndex = 2;
            lblBaudRate.Text = "Baud rate";
            // 
            // lblStopBits
            // 
            lblStopBits.AutoSize = true;
            lblStopBits.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblStopBits.Location = new Point(258, 192);
            lblStopBits.Name = "lblStopBits";
            lblStopBits.Size = new Size(63, 19);
            lblStopBits.TabIndex = 12;
            lblStopBits.Text = "Stop bits";
            // 
            // lblDataBits
            // 
            lblDataBits.AutoSize = true;
            lblDataBits.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblDataBits.Location = new Point(23, 192);
            lblDataBits.Name = "lblDataBits";
            lblDataBits.Size = new Size(64, 19);
            lblDataBits.TabIndex = 4;
            lblDataBits.Text = "Data bits";
            // 
            // txtBaudRate
            // 
            txtBaudRate.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtBaudRate.Location = new Point(150, 153);
            txtBaudRate.Name = "txtBaudRate";
            txtBaudRate.ShortcutsEnabled = false;
            txtBaudRate.Size = new Size(65, 25);
            txtBaudRate.TabIndex = 1;
            // 
            // cboStopBits
            // 
            cboStopBits.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cboStopBits.FormattingEnabled = true;
            cboStopBits.Location = new Point(369, 189);
            cboStopBits.Name = "cboStopBits";
            cboStopBits.Size = new Size(80, 25);
            cboStopBits.TabIndex = 6;
            // 
            // cboDataBits
            // 
            cboDataBits.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cboDataBits.FormattingEnabled = true;
            cboDataBits.Location = new Point(150, 189);
            cboDataBits.Name = "cboDataBits";
            cboDataBits.Size = new Size(65, 25);
            cboDataBits.TabIndex = 2;
            // 
            // lblDevices
            // 
            lblDevices.AutoSize = true;
            lblDevices.BackColor = Color.Transparent;
            lblDevices.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblDevices.Location = new Point(10, 10);
            lblDevices.Name = "lblDevices";
            lblDevices.Size = new Size(100, 19);
            lblDevices.TabIndex = 0;
            lblDevices.Text = "FTDI device list";
            // 
            // viewDevices
            // 
            viewDevices.FullRowSelect = true;
            viewDevices.GridLines = true;
            viewDevices.Location = new Point(10, 29);
            viewDevices.MultiSelect = false;
            viewDevices.Name = "viewDevices";
            viewDevices.ShowGroups = false;
            viewDevices.Size = new Size(449, 107);
            viewDevices.TabIndex = 0;
            viewDevices.UseCompatibleStateImageBehavior = false;
            viewDevices.View = View.Details;
            // 
            // tabPlots
            // 
            tabPlots.Controls.Add(lblChkShowRatio);
            tabPlots.Controls.Add(lblChkShowDistribution);
            tabPlots.Controls.Add(lblChkShowAverage);
            tabPlots.Controls.Add(lblChkShowRaw);
            tabPlots.Controls.Add(chkShowDistribution);
            tabPlots.Controls.Add(grpPlot);
            tabPlots.Controls.Add(txtPlotWindow);
            tabPlots.Controls.Add(txtArrayPoints);
            tabPlots.Controls.Add(lblPlotWindow);
            tabPlots.Controls.Add(lblArrayPoints);
            tabPlots.Controls.Add(chkShowRatio);
            tabPlots.Controls.Add(chkShowAverage);
            tabPlots.Controls.Add(chkShowRaw);
            tabPlots.Location = new Point(4, 26);
            tabPlots.Name = "tabPlots";
            tabPlots.Padding = new Padding(3);
            tabPlots.Size = new Size(472, 303);
            tabPlots.TabIndex = 1;
            tabPlots.Text = "Plots";
            tabPlots.UseVisualStyleBackColor = true;
            // 
            // lblChkShowRatio
            // 
            lblChkShowRatio.AutoSize = true;
            lblChkShowRatio.Location = new Point(236, 66);
            lblChkShowRatio.MaximumSize = new Size(180, 0);
            lblChkShowRatio.Name = "lblChkShowRatio";
            lblChkShowRatio.Size = new Size(71, 19);
            lblChkShowRatio.TabIndex = 3;
            lblChkShowRatio.Text = "Plot ratios";
            lblChkShowRatio.Click += Label_Click;
            // 
            // lblChkShowDistribution
            // 
            lblChkShowDistribution.AutoSize = true;
            lblChkShowDistribution.Location = new Point(236, 26);
            lblChkShowDistribution.MaximumSize = new Size(180, 0);
            lblChkShowDistribution.Name = "lblChkShowDistribution";
            lblChkShowDistribution.Size = new Size(107, 19);
            lblChkShowDistribution.TabIndex = 1;
            lblChkShowDistribution.Text = "Plot distribution";
            lblChkShowDistribution.Click += Label_Click;
            // 
            // lblChkShowAverage
            // 
            lblChkShowAverage.AutoSize = true;
            lblChkShowAverage.Location = new Point(43, 66);
            lblChkShowAverage.MaximumSize = new Size(155, 0);
            lblChkShowAverage.Name = "lblChkShowAverage";
            lblChkShowAverage.Size = new Size(85, 19);
            lblChkShowAverage.TabIndex = 2;
            lblChkShowAverage.Text = "Plot average";
            lblChkShowAverage.Click += Label_Click;
            // 
            // lblChkShowRaw
            // 
            lblChkShowRaw.AutoSize = true;
            lblChkShowRaw.Location = new Point(43, 26);
            lblChkShowRaw.MaximumSize = new Size(155, 0);
            lblChkShowRaw.Name = "lblChkShowRaw";
            lblChkShowRaw.Size = new Size(90, 19);
            lblChkShowRaw.TabIndex = 0;
            lblChkShowRaw.Text = "Plot raw data";
            lblChkShowRaw.Click += Label_Click;
            // 
            // chkShowDistribution
            // 
            chkShowDistribution.AutoSize = true;
            chkShowDistribution.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chkShowDistribution.Location = new Point(220, 30);
            chkShowDistribution.Name = "chkShowDistribution";
            chkShowDistribution.Size = new Size(15, 14);
            chkShowDistribution.TabIndex = 1;
            chkShowDistribution.UseVisualStyleBackColor = true;
            chkShowDistribution.CheckedChanged += chkShowDistribution_CheckedChanged;
            // 
            // grpPlot
            // 
            grpPlot.Controls.Add(radRadar);
            grpPlot.Controls.Add(radRadial);
            grpPlot.Location = new Point(81, 104);
            grpPlot.Name = "grpPlot";
            grpPlot.Size = new Size(187, 98);
            grpPlot.TabIndex = 4;
            grpPlot.TabStop = false;
            grpPlot.Text = "Plot distribution";
            // 
            // radRadar
            // 
            radRadar.AutoSize = true;
            radRadar.Location = new Point(14, 28);
            radRadar.Name = "radRadar";
            radRadar.Size = new Size(62, 23);
            radRadar.TabIndex = 0;
            radRadar.TabStop = true;
            radRadar.Text = "Radar";
            radRadar.UseVisualStyleBackColor = true;
            // 
            // radRadial
            // 
            radRadial.AutoSize = true;
            radRadial.Location = new Point(14, 60);
            radRadial.Name = "radRadial";
            radRadial.Size = new Size(105, 23);
            radRadial.TabIndex = 1;
            radRadial.TabStop = true;
            radRadial.Text = "Radial gauge";
            radRadial.UseVisualStyleBackColor = true;
            // 
            // txtPlotWindow
            // 
            txtPlotWindow.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtPlotWindow.Location = new Point(229, 251);
            txtPlotWindow.Name = "txtPlotWindow";
            txtPlotWindow.Size = new Size(85, 25);
            txtPlotWindow.TabIndex = 6;
            // 
            // txtArrayPoints
            // 
            txtArrayPoints.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtArrayPoints.Location = new Point(229, 220);
            txtArrayPoints.Name = "txtArrayPoints";
            txtArrayPoints.Size = new Size(85, 25);
            txtArrayPoints.TabIndex = 5;
            // 
            // lblPlotWindow
            // 
            lblPlotWindow.AutoSize = true;
            lblPlotWindow.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblPlotWindow.Location = new Point(31, 254);
            lblPlotWindow.Name = "lblPlotWindow";
            lblPlotWindow.Size = new Size(145, 19);
            lblPlotWindow.TabIndex = 11;
            lblPlotWindow.Text = "Plot window (seconds)";
            // 
            // lblArrayPoints
            // 
            lblArrayPoints.AutoSize = true;
            lblArrayPoints.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblArrayPoints.Location = new Point(31, 223);
            lblArrayPoints.Name = "lblArrayPoints";
            lblArrayPoints.Size = new Size(146, 19);
            lblArrayPoints.TabIndex = 9;
            lblArrayPoints.Text = "Array minimum points";
            // 
            // chkShowRatio
            // 
            chkShowRatio.AutoSize = true;
            chkShowRatio.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chkShowRatio.Location = new Point(220, 70);
            chkShowRatio.Name = "chkShowRatio";
            chkShowRatio.Size = new Size(15, 14);
            chkShowRatio.TabIndex = 3;
            chkShowRatio.UseVisualStyleBackColor = true;
            // 
            // chkShowAverage
            // 
            chkShowAverage.AutoSize = true;
            chkShowAverage.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chkShowAverage.Location = new Point(27, 70);
            chkShowAverage.Name = "chkShowAverage";
            chkShowAverage.Size = new Size(15, 14);
            chkShowAverage.TabIndex = 2;
            chkShowAverage.UseVisualStyleBackColor = true;
            // 
            // chkShowRaw
            // 
            chkShowRaw.AutoSize = true;
            chkShowRaw.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chkShowRaw.Location = new Point(27, 30);
            chkShowRaw.Name = "chkShowRaw";
            chkShowRaw.Size = new Size(15, 14);
            chkShowRaw.TabIndex = 0;
            chkShowRaw.UseVisualStyleBackColor = true;
            // 
            // tabGUI
            // 
            tabGUI.Controls.Add(lblDlgPath);
            tabGUI.Controls.Add(txtDataFormat);
            tabGUI.Controls.Add(lblDataFormat);
            tabGUI.Controls.Add(chkDlgPath);
            tabGUI.Controls.Add(grpCulture);
            tabGUI.Location = new Point(4, 26);
            tabGUI.Name = "tabGUI";
            tabGUI.Padding = new Padding(3);
            tabGUI.Size = new Size(472, 303);
            tabGUI.TabIndex = 2;
            tabGUI.Text = "Interface";
            tabGUI.UseVisualStyleBackColor = true;
            // 
            // lblDlgPath
            // 
            lblDlgPath.AutoSize = true;
            lblDlgPath.Location = new Point(45, 210);
            lblDlgPath.MaximumSize = new Size(395, 0);
            lblDlgPath.Name = "lblDlgPath";
            lblDlgPath.Size = new Size(292, 19);
            lblDlgPath.TabIndex = 2;
            lblDlgPath.Text = "Remember previous path in open/save dialogs";
            // 
            // txtDataFormat
            // 
            txtDataFormat.Location = new Point(259, 254);
            txtDataFormat.Name = "txtDataFormat";
            txtDataFormat.Size = new Size(95, 25);
            txtDataFormat.TabIndex = 3;
            // 
            // lblDataFormat
            // 
            lblDataFormat.AutoSize = true;
            lblDataFormat.Location = new Point(26, 257);
            lblDataFormat.MaximumSize = new Size(310, 0);
            lblDataFormat.Name = "lblDataFormat";
            lblDataFormat.Size = new Size(177, 19);
            lblDataFormat.TabIndex = 2;
            lblDataFormat.Text = "Numeric data-format string";
            // 
            // chkDlgPath
            // 
            chkDlgPath.AutoSize = true;
            chkDlgPath.Location = new Point(29, 213);
            chkDlgPath.Name = "chkDlgPath";
            chkDlgPath.Size = new Size(15, 14);
            chkDlgPath.TabIndex = 1;
            chkDlgPath.UseVisualStyleBackColor = true;
            // 
            // grpCulture
            // 
            grpCulture.Controls.Add(cboAllCultures);
            grpCulture.Controls.Add(radUserCulture);
            grpCulture.Controls.Add(radInvariantCulture);
            grpCulture.Controls.Add(radCurrentCulture);
            grpCulture.Location = new Point(29, 24);
            grpCulture.Name = "grpCulture";
            grpCulture.Size = new Size(410, 171);
            grpCulture.TabIndex = 0;
            grpCulture.TabStop = false;
            grpCulture.Text = "Culture settings";
            // 
            // cboAllCultures
            // 
            cboAllCultures.FormattingEnabled = true;
            cboAllCultures.Location = new Point(71, 135);
            cboAllCultures.Name = "cboAllCultures";
            cboAllCultures.Size = new Size(190, 25);
            cboAllCultures.TabIndex = 3;
            cboAllCultures.SelectionChangeCommitted += AllCultures_SelectionChangeCommitted;
            // 
            // radUserCulture
            // 
            radUserCulture.AutoSize = true;
            radUserCulture.Location = new Point(27, 102);
            radUserCulture.Name = "radUserCulture";
            radUserCulture.Size = new Size(101, 23);
            radUserCulture.TabIndex = 2;
            radUserCulture.TabStop = true;
            radUserCulture.Text = "User culture";
            radUserCulture.UseVisualStyleBackColor = true;
            radUserCulture.CheckedChanged += UserCulture_CheckedChanged;
            // 
            // radInvariantCulture
            // 
            radInvariantCulture.AutoSize = true;
            radInvariantCulture.Location = new Point(27, 66);
            radInvariantCulture.Name = "radInvariantCulture";
            radInvariantCulture.Size = new Size(127, 23);
            radInvariantCulture.TabIndex = 1;
            radInvariantCulture.TabStop = true;
            radInvariantCulture.Text = "Invariant culture";
            radInvariantCulture.UseVisualStyleBackColor = true;
            radInvariantCulture.CheckedChanged += InvariantCulture_CheckedChanged;
            // 
            // radCurrentCulture
            // 
            radCurrentCulture.AutoSize = true;
            radCurrentCulture.Location = new Point(27, 30);
            radCurrentCulture.Name = "radCurrentCulture";
            radCurrentCulture.Size = new Size(120, 23);
            radCurrentCulture.TabIndex = 0;
            radCurrentCulture.TabStop = true;
            radCurrentCulture.Text = "Current culture";
            radCurrentCulture.UseVisualStyleBackColor = true;
            radCurrentCulture.CheckedChanged += CurrentCulture_CheckedChanged;
            // 
            // btnReset
            // 
            btnReset.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnReset.Location = new Point(12, 355);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(90, 30);
            btnReset.TabIndex = 1;
            btnReset.Text = "&Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += Reset_Click;
            // 
            // FrmSettings
            // 
            AcceptButton = btnAccept;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(504, 396);
            Controls.Add(btnReset);
            Controls.Add(btnCancel);
            Controls.Add(btnAccept);
            Controls.Add(tabSettings);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSettings";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "T-10 illuminance meter settings";
            ((System.ComponentModel.ISupportInitialize)updSensors).EndInit();
            tabSettings.ResumeLayout(false);
            tabDevice.ResumeLayout(false);
            tabDevice.PerformLayout();
            tabPlots.ResumeLayout(false);
            tabPlots.PerformLayout();
            grpPlot.ResumeLayout(false);
            grpPlot.PerformLayout();
            tabGUI.ResumeLayout(false);
            tabGUI.PerformLayout();
            grpCulture.ResumeLayout(false);
            grpCulture.PerformLayout();
            ResumeLayout(false);
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
        private Label lblChkShowRatio;
        private Label lblChkShowDistribution;
        private Label lblChkShowAverage;
        private Label lblChkShowRaw;
    }
}