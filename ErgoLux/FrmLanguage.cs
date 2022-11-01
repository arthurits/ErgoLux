using System.Globalization;
using ScottPlot;

namespace ErgoLux;

public partial class FrmLanguage : Form
{
    private CultureInfo _culture = CultureInfo.CurrentCulture;
    private readonly ClassSettings? Settings;

    public FrmLanguage()
    {
        this.InitializeComponent();
        this.FillDefinedCultures("ErgoLux.localization.strings", typeof(FrmLanguage).Assembly);
    }

    public FrmLanguage(ClassSettings settings)
    : this()
    {
        this.Settings = settings;
        this._culture = settings.AppCulture;
        this.UpdateControls(settings.AppCultureName);
    }

    private void Accept_Click(object sender, EventArgs e)
    {
        if (this.Settings is not null) this.Settings.AppCulture = this._culture;
        this.DialogResult = DialogResult.OK;
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
    }

    private void LabelCulture_Click(object sender, EventArgs e)
    {
        switch ((sender as Label)?.Name)
        {
            case "lblCurrentCulture":
                this.radCurrentCulture.Checked = true;
                break;
            case "lblInvariantCulture":
                this.radInvariantCulture.Checked = true;
                break;
            case "lblUserCulture":
            default:
                this.radUserCulture.Checked = true;
                break;
        }
    }

    private void CurrentCulture_CheckedChanged(object sender, EventArgs e)
    {
        if (this.radCurrentCulture.Checked)
        {
            this._culture = System.Globalization.CultureInfo.CurrentCulture;
            this.UpdateUI_Language();
        }
    }

    private void InvariantCulture_CheckedChanged(object sender, EventArgs e)
    {
        if (this.radInvariantCulture.Checked)
        {
            this._culture = System.Globalization.CultureInfo.InvariantCulture;
            this.UpdateUI_Language();
        }
    }

    private void UserCulture_CheckedChanged(object sender, EventArgs e)
    {
        this.cboAllCultures.Enabled = this.radUserCulture.Checked;
        if (this.cboAllCultures.Enabled)
        {
            this._culture = new((string)this.cboAllCultures.SelectedValue ?? String.Empty);
            this.UpdateUI_Language();
        }
    }

    private void AllCultures_SelectedValueChanged(object sender, EventArgs e)
    {
        var cbo = sender as ComboBox;
        if (cbo is not null && cbo.Items.Count > 0 && cbo.SelectedValue is not null)
        {
            this._culture = new((string)cbo.SelectedValue);
            this.UpdateUI_Language();
        }
    }

    /// <summary>
    /// Updates the form's controls with values from the settings class
    /// </summary>
    private void UpdateControls(string cultureName = "")
    {
        this.cboAllCultures.Enabled = false;
        if (cultureName == string.Empty)
        {
            this.radInvariantCulture.Checked = true;
        }
        else if (cultureName == System.Globalization.CultureInfo.CurrentCulture.Name)
        {
            this.radCurrentCulture.Checked = true;
        }
        else
        {
            this.cboAllCultures.SelectedValue = cultureName;
            this.radUserCulture.Checked = true;
        }
    }

    /// <summary>
    /// Databind only the cultures found in .resources files for a given type
    /// </summary>
    /// <param name="type">A type from which the resource manager derives all information for finding .resources files</param>
    private void FillDefinedCultures(string baseName, System.Reflection.Assembly assembly)
    {
        string cultureName = this._culture.Name;
        var cultures = System.Globalization.GlobalizationUtilities.GetAvailableCultures(baseName, assembly);
        this.cboAllCultures.DisplayMember = "DisplayName";
        this.cboAllCultures.ValueMember = "Name";
        this.cboAllCultures.DataSource = cultures.ToArray();
        this.cboAllCultures.SelectedValue = cultureName;
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language()
    {
        this.UpdateUI_Language(this._culture);
    }

    /// <summary>
    /// Update the form's interface language
    /// </summary>
    /// <param name="culture">Culture used to display the UI</param>
    private void UpdateUI_Language(System.Globalization.CultureInfo culture)
    {
        StringResources.Culture = culture;

        this.Text = StringResources.FrmLanguage;
        this.lblCurrentCulture.Text = StringResources.RadCurrentCulture + $" ({System.Globalization.CultureInfo.CurrentCulture.Name})";
        this.lblInvariantCulture.Text = StringResources.RadInvariantCulture;
        this.lblUserCulture.Text = StringResources.RadUserCulture;
        this.btnCancel.Text = StringResources.BtnCancel;
        this.btnAccept.Text = StringResources.BtnAccept;

        // Reposition controls to compensate for the culture text length in labels
        this.lblCurrentCulture.Top = this.radCurrentCulture.Top + (this.radCurrentCulture.Height - this.lblCurrentCulture.Height) / 2;
        this.lblInvariantCulture.Top = this.radInvariantCulture.Top + (this.radInvariantCulture.Height - this.lblInvariantCulture.Height) / 2;
        this.lblUserCulture.Top = this.radUserCulture.Top + (this.radUserCulture.Height - this.lblUserCulture.Height) / 2;
    }

}