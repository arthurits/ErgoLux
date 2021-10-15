
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabDevice = new System.Windows.Forms.TabPage();
            this.lblHz = new System.Windows.Forms.Label();
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
            this.txtPlotWindow = new System.Windows.Forms.TextBox();
            this.txtArrayPoints = new System.Windows.Forms.TextBox();
            this.lblPlotWindow = new System.Windows.Forms.Label();
            this.lblArrayPoints = new System.Windows.Forms.Label();
            this.chkShowRatio = new System.Windows.Forms.CheckBox();
            this.chkShowAverage = new System.Windows.Forms.CheckBox();
            this.chkShowRadar = new System.Windows.Forms.CheckBox();
            this.chkShowRaw = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.updSensors)).BeginInit();
            this.tabSettings.SuspendLayout();
            this.tabDevice.SuspendLayout();
            this.tabPlots.SuspendLayout();
            this.SuspendLayout();
            // 
            // updSensors
            // 
            this.updSensors.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updSensors.Location = new System.Drawing.Point(151, 262);
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
            this.updSensors.Size = new System.Drawing.Size(49, 25);
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
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOK.Location = new System.Drawing.Point(302, 354);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 30);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "&Accept";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(402, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabDevice);
            this.tabSettings.Controls.Add(this.tabPlots);
            this.tabSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabSettings.Location = new System.Drawing.Point(12, 12);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(480, 333);
            this.tabSettings.TabIndex = 22;
            // 
            // tabDevice
            // 
            this.tabDevice.Controls.Add(this.lblHz);
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
            // lblHz
            // 
            this.lblHz.AutoSize = true;
            this.lblHz.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHz.Location = new System.Drawing.Point(243, 156);
            this.lblHz.Name = "lblHz";
            this.lblHz.Size = new System.Drawing.Size(67, 19);
            this.lblHz.TabIndex = 37;
            this.lblHz.Text = "Freq. (Hz)";
            // 
            // txtHz
            // 
            this.txtHz.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtHz.Location = new System.Drawing.Point(328, 153);
            this.txtHz.Name = "txtHz";
            this.txtHz.Size = new System.Drawing.Size(32, 25);
            this.txtHz.TabIndex = 36;
            // 
            // lblOff
            // 
            this.lblOff.AutoSize = true;
            this.lblOff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOff.Location = new System.Drawing.Point(380, 264);
            this.lblOff.Name = "lblOff";
            this.lblOff.Size = new System.Drawing.Size(28, 19);
            this.lblOff.TabIndex = 35;
            this.lblOff.Text = "Off";
            // 
            // lblOn
            // 
            this.lblOn.AutoSize = true;
            this.lblOn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOn.Location = new System.Drawing.Point(302, 264);
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
            this.lblFlowControl.Location = new System.Drawing.Point(243, 228);
            this.lblFlowControl.Name = "lblFlowControl";
            this.lblFlowControl.Size = new System.Drawing.Size(84, 19);
            this.lblFlowControl.TabIndex = 30;
            this.lblFlowControl.Text = "Flow control";
            // 
            // cboFlowControl
            // 
            this.cboFlowControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboFlowControl.FormattingEnabled = true;
            this.cboFlowControl.Location = new System.Drawing.Point(328, 225);
            this.cboFlowControl.Name = "cboFlowControl";
            this.cboFlowControl.Size = new System.Drawing.Size(125, 25);
            this.cboFlowControl.TabIndex = 29;
            this.cboFlowControl.SelectedIndexChanged += new System.EventHandler(this.cboFlowControl_SelectedIndexChanged);
            // 
            // cboParity
            // 
            this.cboParity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Location = new System.Drawing.Point(95, 225);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(125, 25);
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
            this.lblStopBits.Location = new System.Drawing.Point(243, 192);
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
            this.txtBaudRate.Location = new System.Drawing.Point(95, 153);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.ShortcutsEnabled = false;
            this.txtBaudRate.Size = new System.Drawing.Size(125, 25);
            this.txtBaudRate.TabIndex = 24;
            // 
            // cboStopBits
            // 
            this.cboStopBits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboStopBits.FormattingEnabled = true;
            this.cboStopBits.Location = new System.Drawing.Point(328, 189);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(125, 25);
            this.cboStopBits.TabIndex = 23;
            // 
            // cboDataBits
            // 
            this.cboDataBits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboDataBits.FormattingEnabled = true;
            this.cboDataBits.Location = new System.Drawing.Point(95, 189);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(125, 25);
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
            this.viewDevices.HideSelection = false;
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
            this.tabPlots.Controls.Add(this.txtPlotWindow);
            this.tabPlots.Controls.Add(this.txtArrayPoints);
            this.tabPlots.Controls.Add(this.lblPlotWindow);
            this.tabPlots.Controls.Add(this.lblArrayPoints);
            this.tabPlots.Controls.Add(this.chkShowRatio);
            this.tabPlots.Controls.Add(this.chkShowAverage);
            this.tabPlots.Controls.Add(this.chkShowRadar);
            this.tabPlots.Controls.Add(this.chkShowRaw);
            this.tabPlots.Location = new System.Drawing.Point(4, 26);
            this.tabPlots.Name = "tabPlots";
            this.tabPlots.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlots.Size = new System.Drawing.Size(472, 303);
            this.tabPlots.TabIndex = 1;
            this.tabPlots.Text = "Plots";
            this.tabPlots.UseVisualStyleBackColor = true;
            // 
            // txtPlotWindow
            // 
            this.txtPlotWindow.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPlotWindow.Location = new System.Drawing.Point(183, 126);
            this.txtPlotWindow.Name = "txtPlotWindow";
            this.txtPlotWindow.Size = new System.Drawing.Size(84, 25);
            this.txtPlotWindow.TabIndex = 7;
            // 
            // txtArrayPoints
            // 
            this.txtArrayPoints.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtArrayPoints.Location = new System.Drawing.Point(183, 95);
            this.txtArrayPoints.Name = "txtArrayPoints";
            this.txtArrayPoints.Size = new System.Drawing.Size(85, 25);
            this.txtArrayPoints.TabIndex = 6;
            // 
            // lblPlotWindow
            // 
            this.lblPlotWindow.AutoSize = true;
            this.lblPlotWindow.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPlotWindow.Location = new System.Drawing.Point(31, 129);
            this.lblPlotWindow.Name = "lblPlotWindow";
            this.lblPlotWindow.Size = new System.Drawing.Size(145, 19);
            this.lblPlotWindow.TabIndex = 5;
            this.lblPlotWindow.Text = "Plot window (seconds)";
            // 
            // lblArrayPoints
            // 
            this.lblArrayPoints.AutoSize = true;
            this.lblArrayPoints.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblArrayPoints.Location = new System.Drawing.Point(31, 98);
            this.lblArrayPoints.Name = "lblArrayPoints";
            this.lblArrayPoints.Size = new System.Drawing.Size(146, 19);
            this.lblArrayPoints.TabIndex = 4;
            this.lblArrayPoints.Text = "Array minimum points";
            // 
            // chkShowRatio
            // 
            this.chkShowRatio.AutoSize = true;
            this.chkShowRatio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowRatio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowRatio.Location = new System.Drawing.Point(211, 54);
            this.chkShowRatio.Name = "chkShowRatio";
            this.chkShowRatio.Size = new System.Drawing.Size(90, 23);
            this.chkShowRatio.TabIndex = 3;
            this.chkShowRatio.Text = "Plot ratios";
            this.chkShowRatio.UseVisualStyleBackColor = true;
            // 
            // chkShowAverage
            // 
            this.chkShowAverage.AutoSize = true;
            this.chkShowAverage.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowAverage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowAverage.Location = new System.Drawing.Point(27, 54);
            this.chkShowAverage.Name = "chkShowAverage";
            this.chkShowAverage.Size = new System.Drawing.Size(104, 23);
            this.chkShowAverage.TabIndex = 2;
            this.chkShowAverage.Text = "Plot average";
            this.chkShowAverage.UseVisualStyleBackColor = true;
            // 
            // chkShowRadar
            // 
            this.chkShowRadar.AutoSize = true;
            this.chkShowRadar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowRadar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowRadar.Location = new System.Drawing.Point(211, 25);
            this.chkShowRadar.Name = "chkShowRadar";
            this.chkShowRadar.Size = new System.Drawing.Size(126, 23);
            this.chkShowRadar.TabIndex = 1;
            this.chkShowRadar.Text = "Plot distribution";
            this.chkShowRadar.UseVisualStyleBackColor = true;
            // 
            // chkShowRaw
            // 
            this.chkShowRaw.AutoSize = true;
            this.chkShowRaw.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowRaw.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowRaw.Location = new System.Drawing.Point(27, 25);
            this.chkShowRaw.Name = "chkShowRaw";
            this.chkShowRaw.Size = new System.Drawing.Size(109, 23);
            this.chkShowRaw.TabIndex = 0;
            this.chkShowRaw.Text = "Plot raw data";
            this.chkShowRaw.UseVisualStyleBackColor = true;
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
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(504, 396);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown updSensors;
        private System.Windows.Forms.Label lblSensors;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabDevice;
        private System.Windows.Forms.Label lblHz;
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
        private System.Windows.Forms.CheckBox chkShowRadar;
        private System.Windows.Forms.CheckBox chkShowRaw;
        private System.Windows.Forms.TextBox txtPlotWindow;
        private System.Windows.Forms.TextBox txtArrayPoints;
        private System.Windows.Forms.Button btnReset;
    }
}