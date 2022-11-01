using System.Text.Json;

namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Loads all settings from file _settings.FileName into class instance _settings
    /// Shows MessageBox error if unsuccessful
    /// </summary>
    /// <returns><see langword="True"/> if successful, <see langword="false"/> otherwise</returns>
    private bool LoadProgramSettingsJSON()
    {
        bool result = false;
        try
        {
            var jsonString = File.ReadAllText(this._settings.SettingsFileName);
            this._settings = JsonSerializer.Deserialize<ClassSettings>(jsonString) ?? this._settings;
            result = true;
        }
        catch (FileNotFoundException)
        {
        }
        catch (Exception ex)
        {
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(this,
                    StringResources.MsgBoxErrorSettings,
                    StringResources.MsgBoxErrorSettingsTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        return result;
    }

    /// <summary>
    /// Saves data from class instance _sett into _sett.FileName
    /// </summary>
    private void SaveProgramSettingsJSON()
    {
        this._settings.WindowLeft = this.DesktopLocation.X;
        this._settings.WindowTop = this.DesktopLocation.Y;
        this._settings.WindowWidth = this.ClientSize.Width;
        this._settings.WindowHeight = this.ClientSize.Height;

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(this._settings, options);
        File.WriteAllText(this._settings.SettingsFileName, jsonString);
    }

    /// <summary>
    /// Update UI with settings
    /// </summary>
    /// <param name="WindowSettings"><see langword="True"/> if the window position and size should be applied. <see langword="False"/> if omitted</param>
    private void ApplySettingsJSON(bool WindowPosition = false)
    {
        if (WindowPosition)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.DesktopLocation = new Point(this._settings.WindowLeft, this._settings.WindowTop);
            this.ClientSize = new Size(this._settings.WindowWidth, this._settings.WindowHeight);
        }
    }
}