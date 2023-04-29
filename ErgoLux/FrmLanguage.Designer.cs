namespace ErgoLux
{
    partial class FrmLanguage
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
            cboAllCultures = new ComboBox();
            radCurrentCulture = new RadioButton();
            radInvariantCulture = new RadioButton();
            radUserCulture = new RadioButton();
            btnCancel = new Button();
            btnAccept = new Button();
            lblCurrentCulture = new Label();
            lblInvariantCulture = new Label();
            lblUserCulture = new Label();
            SuspendLayout();
            // 
            // cboAllCultures
            // 
            cboAllCultures.FormattingEnabled = true;
            cboAllCultures.Location = new Point(45, 141);
            cboAllCultures.Name = "cboAllCultures";
            cboAllCultures.Size = new Size(190, 25);
            cboAllCultures.TabIndex = 3;
            cboAllCultures.SelectionChangeCommitted += AllCultures_SelectionChangeCommitted;
            // 
            // radCurrentCulture
            // 
            radCurrentCulture.AutoSize = true;
            radCurrentCulture.Location = new Point(23, 32);
            radCurrentCulture.MaximumSize = new Size(275, 0);
            radCurrentCulture.Name = "radCurrentCulture";
            radCurrentCulture.Size = new Size(14, 13);
            radCurrentCulture.TabIndex = 0;
            radCurrentCulture.TabStop = true;
            radCurrentCulture.UseVisualStyleBackColor = true;
            radCurrentCulture.CheckedChanged += CurrentCulture_CheckedChanged;
            // 
            // radInvariantCulture
            // 
            radInvariantCulture.AutoSize = true;
            radInvariantCulture.Location = new Point(23, 73);
            radInvariantCulture.MaximumSize = new Size(275, 0);
            radInvariantCulture.Name = "radInvariantCulture";
            radInvariantCulture.Size = new Size(14, 13);
            radInvariantCulture.TabIndex = 1;
            radInvariantCulture.TabStop = true;
            radInvariantCulture.UseVisualStyleBackColor = true;
            radInvariantCulture.CheckedChanged += InvariantCulture_CheckedChanged;
            // 
            // radUserCulture
            // 
            radUserCulture.AutoSize = true;
            radUserCulture.Location = new Point(23, 114);
            radUserCulture.MaximumSize = new Size(275, 0);
            radUserCulture.Name = "radUserCulture";
            radUserCulture.Size = new Size(14, 13);
            radUserCulture.TabIndex = 2;
            radUserCulture.TabStop = true;
            radUserCulture.UseVisualStyleBackColor = true;
            radUserCulture.CheckedChanged += UserCulture_CheckedChanged;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(117, 190);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += Cancel_Click;
            // 
            // btnAccept
            // 
            btnAccept.DialogResult = DialogResult.OK;
            btnAccept.Location = new Point(213, 190);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(90, 30);
            btnAccept.TabIndex = 5;
            btnAccept.Text = "&Accept";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += Accept_Click;
            // 
            // lblCurrentCulture
            // 
            lblCurrentCulture.AutoSize = true;
            lblCurrentCulture.BackColor = Color.Transparent;
            lblCurrentCulture.Location = new Point(39, 29);
            lblCurrentCulture.MaximumSize = new Size(275, 0);
            lblCurrentCulture.Name = "lblCurrentCulture";
            lblCurrentCulture.Size = new Size(102, 19);
            lblCurrentCulture.TabIndex = 4;
            lblCurrentCulture.Text = "Current culture";
            lblCurrentCulture.Click += LabelCulture_Click;
            // 
            // lblInvariantCulture
            // 
            lblInvariantCulture.AutoSize = true;
            lblInvariantCulture.Location = new Point(39, 70);
            lblInvariantCulture.MaximumSize = new Size(275, 0);
            lblInvariantCulture.Name = "lblInvariantCulture";
            lblInvariantCulture.Size = new Size(109, 19);
            lblInvariantCulture.TabIndex = 5;
            lblInvariantCulture.Text = "Invariant culture";
            lblInvariantCulture.Click += LabelCulture_Click;
            // 
            // lblUserCulture
            // 
            lblUserCulture.AutoSize = true;
            lblUserCulture.Location = new Point(39, 111);
            lblUserCulture.MaximumSize = new Size(275, 0);
            lblUserCulture.Name = "lblUserCulture";
            lblUserCulture.Size = new Size(90, 19);
            lblUserCulture.TabIndex = 6;
            lblUserCulture.Text = "Select culture";
            lblUserCulture.Click += LabelCulture_Click;
            // 
            // FrmLanguage
            // 
            AcceptButton = btnAccept;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(315, 232);
            Controls.Add(lblUserCulture);
            Controls.Add(lblInvariantCulture);
            Controls.Add(lblCurrentCulture);
            Controls.Add(btnAccept);
            Controls.Add(btnCancel);
            Controls.Add(radUserCulture);
            Controls.Add(radInvariantCulture);
            Controls.Add(radCurrentCulture);
            Controls.Add(cboAllCultures);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLanguage";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select language";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cboAllCultures;
        private RadioButton radCurrentCulture;
        private RadioButton radInvariantCulture;
        private RadioButton radUserCulture;
        private Button btnCancel;
        private Button btnAccept;
        private Label lblCurrentCulture;
        private Label lblInvariantCulture;
        private Label lblUserCulture;
    }
}