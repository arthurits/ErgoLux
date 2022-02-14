using System.Drawing;
using System.Text.Json;

namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Loads all settings from file _sett.FileName into class instance _sett
    /// Shows MessageBox error if unsuccessful
    /// </summary>
    private void LoadProgramSettingsJSON()
    {
        try
        {
            var jsonString = File.ReadAllText(_sett.SettingsFileName);
            _sett = JsonSerializer.Deserialize<ClassSettings>(jsonString);
            _sett.InitializeJsonIgnore(_sett.AppPath);

            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.DesktopLocation = new Point(_sett.Wnd_Left, _sett.Wnd_Top);
            this.ClientSize = new Size(_sett.Wnd_Width, _sett.Wnd_Height);
        }
        catch (FileNotFoundException)
        {
        }
        catch (Exception ex)
        {
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(this,
                    StringsRM.GetString("strMsgBoxErrorSettings", _sett.AppCulture) ?? $"Error loading settings file.\n\n{ex.Message}\n\nDefault values will be used instead.",
                    StringsRM.GetString("strMsgBoxErrorSettingsTitle", _sett.AppCulture) ?? "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Saves data from class instance _sett into _sett.FileName
    /// </summary>
    private void SaveProgramSettingsJSON()
    {
        _sett.Wnd_Left = DesktopLocation.X;
        _sett.Wnd_Top = DesktopLocation.Y;
        _sett.Wnd_Width = ClientSize.Width;
        _sett.Wnd_Height = ClientSize.Height;

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(_sett, options);
        File.WriteAllText(_sett.SettingsFileName, jsonString);
    }
}
