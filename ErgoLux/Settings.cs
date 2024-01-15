using System.Text.Json;

namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Loads all settings from file _settings.FileName into instance <see cref="AppSettings">_settings</see>.
    /// Shows MessageBox error if unsuccessful
    /// </summary>
    private void LoadProgramSettingsJSON()
    {
        try
        {
            var jsonString = File.ReadAllText(_settings.SettingsFileName);
            _settings = JsonSerializer.Deserialize<ClassSettings>(jsonString) ?? _settings;
            //SetWindowPos(_settings.WindowPosition);
        }
        catch (FileNotFoundException)
        {
            _settingsFileExist = false;
        }
        catch (Exception ex)
        {
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(this,
                    StringResources.MsgBoxErrorSettings + Environment.NewLine + Environment.NewLine + ex.Message,
                    StringResources.MsgBoxErrorSettingsTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Saves the current program settings from <see cref="AppSettings">_settings</see> into _settings.FileName.
    /// </summary>
    private void SaveProgramSettingsJSON()
    {
        _settings.WindowLeft = DesktopLocation.X;
        _settings.WindowTop = DesktopLocation.Y;
        _settings.WindowWidth = ClientSize.Width;
        _settings.WindowHeight = ClientSize.Height;

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(_settings, options);
        File.WriteAllText(_settings.SettingsFileName, jsonString);
    }

    /// <summary>
    /// Modifies window size and position to the values in <see cref="AppSettings">_settings</see>
    /// </summary>
    private void SetWindowPos()
    {
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.DesktopLocation = new Point(_settings.WindowLeft, _settings.WindowTop);
        this.ClientSize = new Size(_settings.WindowWidth, _settings.WindowHeight);
    }
}
