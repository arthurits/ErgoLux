
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
            this.viewDevices = new System.Windows.Forms.ListView();
            this.cboDataBits = new System.Windows.Forms.ComboBox();
            this.cboStopBits = new System.Windows.Forms.ComboBox();
            this.txtBaudRate = new System.Windows.Forms.TextBox();
            this.updSensors = new System.Windows.Forms.NumericUpDown();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.lblSensors = new System.Windows.Forms.Label();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.cboFlowControl = new System.Windows.Forms.ComboBox();
            this.lblFlowControl = new System.Windows.Forms.Label();
            this.lblParity = new System.Windows.Forms.Label();
            this.txtOn = new System.Windows.Forms.TextBox();
            this.txtOff = new System.Windows.Forms.TextBox();
            this.lblOn = new System.Windows.Forms.Label();
            this.lblOff = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDevices = new System.Windows.Forms.Label();
            this.txtHz = new System.Windows.Forms.TextBox();
            this.lblHz = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.updSensors)).BeginInit();
            this.SuspendLayout();
            // 
            // viewDevices
            // 
            this.viewDevices.FullRowSelect = true;
            this.viewDevices.GridLines = true;
            this.viewDevices.HideSelection = false;
            this.viewDevices.Location = new System.Drawing.Point(26, 32);
            this.viewDevices.MultiSelect = false;
            this.viewDevices.Name = "viewDevices";
            this.viewDevices.ShowGroups = false;
            this.viewDevices.Size = new System.Drawing.Size(449, 143);
            this.viewDevices.TabIndex = 0;
            this.viewDevices.UseCompatibleStateImageBehavior = false;
            this.viewDevices.View = System.Windows.Forms.View.Details;
            // 
            // cboDataBits
            // 
            this.cboDataBits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboDataBits.FormattingEnabled = true;
            this.cboDataBits.Location = new System.Drawing.Point(115, 228);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(137, 25);
            this.cboDataBits.TabIndex = 1;
            // 
            // cboStopBits
            // 
            this.cboStopBits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboStopBits.FormattingEnabled = true;
            this.cboStopBits.Location = new System.Drawing.Point(115, 264);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(137, 25);
            this.cboStopBits.TabIndex = 2;
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtBaudRate.Location = new System.Drawing.Point(115, 192);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.ShortcutsEnabled = false;
            this.txtBaudRate.Size = new System.Drawing.Size(137, 25);
            this.txtBaudRate.TabIndex = 3;
            // 
            // updSensors
            // 
            this.updSensors.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updSensors.Location = new System.Drawing.Point(154, 414);
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
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDataBits.Location = new System.Drawing.Point(30, 231);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(60, 19);
            this.lblDataBits.TabIndex = 5;
            this.lblDataBits.Text = "Databits";
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStopBits.Location = new System.Drawing.Point(30, 267);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(63, 19);
            this.lblStopBits.TabIndex = 6;
            this.lblStopBits.Text = "Stop bits";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBaudRate.Location = new System.Drawing.Point(30, 195);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(68, 19);
            this.lblBaudRate.TabIndex = 7;
            this.lblBaudRate.Text = "Baud rate";
            // 
            // lblSensors
            // 
            this.lblSensors.AutoSize = true;
            this.lblSensors.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSensors.Location = new System.Drawing.Point(26, 416);
            this.lblSensors.Name = "lblSensors";
            this.lblSensors.Size = new System.Drawing.Size(125, 19);
            this.lblSensors.TabIndex = 8;
            this.lblSensors.Text = "Number of sensors";
            // 
            // cboParity
            // 
            this.cboParity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Location = new System.Drawing.Point(115, 300);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(137, 25);
            this.cboParity.TabIndex = 9;
            // 
            // cboFlowControl
            // 
            this.cboFlowControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboFlowControl.FormattingEnabled = true;
            this.cboFlowControl.Location = new System.Drawing.Point(115, 336);
            this.cboFlowControl.Name = "cboFlowControl";
            this.cboFlowControl.Size = new System.Drawing.Size(137, 25);
            this.cboFlowControl.TabIndex = 10;
            this.cboFlowControl.SelectedIndexChanged += new System.EventHandler(this.cboFlowControl_SelectedIndexChanged);
            // 
            // lblFlowControl
            // 
            this.lblFlowControl.AutoSize = true;
            this.lblFlowControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFlowControl.Location = new System.Drawing.Point(30, 339);
            this.lblFlowControl.Name = "lblFlowControl";
            this.lblFlowControl.Size = new System.Drawing.Size(84, 19);
            this.lblFlowControl.TabIndex = 11;
            this.lblFlowControl.Text = "Flow control";
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblParity.Location = new System.Drawing.Point(30, 303);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(44, 19);
            this.lblParity.TabIndex = 12;
            this.lblParity.Text = "Parity";
            // 
            // txtOn
            // 
            this.txtOn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOn.Location = new System.Drawing.Point(323, 336);
            this.txtOn.Name = "txtOn";
            this.txtOn.Size = new System.Drawing.Size(40, 25);
            this.txtOn.TabIndex = 13;
            // 
            // txtOff
            // 
            this.txtOff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOff.Location = new System.Drawing.Point(402, 336);
            this.txtOff.Name = "txtOff";
            this.txtOff.Size = new System.Drawing.Size(40, 25);
            this.txtOff.TabIndex = 14;
            // 
            // lblOn
            // 
            this.lblOn.AutoSize = true;
            this.lblOn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOn.Location = new System.Drawing.Point(295, 339);
            this.lblOn.Name = "lblOn";
            this.lblOn.Size = new System.Drawing.Size(28, 19);
            this.lblOn.TabIndex = 15;
            this.lblOn.Text = "On";
            // 
            // lblOff
            // 
            this.lblOff.AutoSize = true;
            this.lblOff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOff.Location = new System.Drawing.Point(373, 339);
            this.lblOff.Name = "lblOff";
            this.lblOff.Size = new System.Drawing.Size(28, 19);
            this.lblOff.TabIndex = 16;
            this.lblOff.Text = "Off";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOK.Location = new System.Drawing.Point(285, 405);
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
            this.btnCancel.Location = new System.Drawing.Point(385, 405);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDevices
            // 
            this.lblDevices.AutoSize = true;
            this.lblDevices.BackColor = System.Drawing.Color.Transparent;
            this.lblDevices.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDevices.Location = new System.Drawing.Point(26, 13);
            this.lblDevices.Name = "lblDevices";
            this.lblDevices.Size = new System.Drawing.Size(100, 19);
            this.lblDevices.TabIndex = 19;
            this.lblDevices.Text = "FTDI device list";
            // 
            // txtHz
            // 
            this.txtHz.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtHz.Location = new System.Drawing.Point(115, 372);
            this.txtHz.Name = "txtHz";
            this.txtHz.Size = new System.Drawing.Size(32, 25);
            this.txtHz.TabIndex = 20;
            // 
            // lblHz
            // 
            this.lblHz.AutoSize = true;
            this.lblHz.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHz.Location = new System.Drawing.Point(30, 375);
            this.lblHz.Name = "lblHz";
            this.lblHz.Size = new System.Drawing.Size(67, 19);
            this.lblHz.TabIndex = 21;
            this.lblHz.Text = "Freq. (Hz)";
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(487, 450);
            this.Controls.Add(this.lblHz);
            this.Controls.Add(this.txtHz);
            this.Controls.Add(this.lblDevices);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblOff);
            this.Controls.Add(this.lblOn);
            this.Controls.Add(this.txtOff);
            this.Controls.Add(this.txtOn);
            this.Controls.Add(this.lblParity);
            this.Controls.Add(this.lblFlowControl);
            this.Controls.Add(this.cboFlowControl);
            this.Controls.Add(this.cboParity);
            this.Controls.Add(this.lblSensors);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.lblStopBits);
            this.Controls.Add(this.lblDataBits);
            this.Controls.Add(this.updSensors);
            this.Controls.Add(this.txtBaudRate);
            this.Controls.Add(this.cboStopBits);
            this.Controls.Add(this.cboDataBits);
            this.Controls.Add(this.viewDevices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "T-10 illuminance meter settings";
            ((System.ComponentModel.ISupportInitialize)(this.updSensors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView viewDevices;
        private System.Windows.Forms.ComboBox cboDataBits;
        private System.Windows.Forms.ComboBox cboStopBits;
        private System.Windows.Forms.TextBox txtBaudRate;
        private System.Windows.Forms.NumericUpDown updSensors;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Label lblSensors;
        private System.Windows.Forms.ComboBox cboParity;
        private System.Windows.Forms.ComboBox cboFlowControl;
        private System.Windows.Forms.Label lblFlowControl;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.TextBox txtOn;
        private System.Windows.Forms.TextBox txtOff;
        private System.Windows.Forms.Label lblOn;
        private System.Windows.Forms.Label lblOff;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDevices;
        private System.Windows.Forms.TextBox txtHz;
        private System.Windows.Forms.Label lblHz;
    }
}