using System.Text.Json.Serialization;


namespace ErgoLux;

/// <summary>
/// Keeps the settings for the T-10 device, plottting options and app window properties
/// </summary>
public class ClassSettings
{
    /// <summary>
    /// Stores the settings file name
    /// </summary>
    [JsonIgnore]
    public string SettingsFileName { get; set; } = "configuration.json";

    /// <summary>
    /// Remember window position on start up
    /// </summary>
    [JsonPropertyName("Window position")]
    public bool WindowPosition { get; set; } = true;

    [JsonPropertyName("Window top")]
    public int WindowTop { get; set; } = 0;
    [JsonPropertyName("Window left")]
    public int WindowLeft { get; set; } = 0;
    [JsonPropertyName("Window width")]
    public int WindowWidth { get; set; } = 950;
    [JsonPropertyName("Window height")]
    public int WindowHeight { get; set; } = 650;

    [JsonPropertyName("Location id")]
    public int T10_LocationID { get; set; } = 0;
    [JsonPropertyName("Number of sensors")]
    public int T10_NumberOfSensors { get; set; } = 1;
    [JsonPropertyName("Baud rate")]
    public int T10_BaudRate { get; set; } = 9600;
    [JsonPropertyName("Data bits")]
    public int T10_DataBits { get; set; } = 7;
    [JsonPropertyName("Stop bits")]
    public int T10_StopBits { get; set; } = 0;
    [JsonPropertyName("Parity")]
    public int T10_Parity { get; set; } = 2;
    [JsonPropertyName("Flow control")]
    public int T10_FlowControl { get; set; } = 0;
    [JsonPropertyName("Character on")]
    public int T10_CharOn { get; set; } = 11;
    [JsonPropertyName("Chareacter off")]
    public int T10_CharOff { get; set; } = 13;
    [JsonPropertyName("Sample frequency")]
    public double T10_Frequency { get; set; } = 2;


    /// <value>Number of points the array can store</value>
    [JsonPropertyName("Array points")]
    public int Plot_ArrayPoints { get; set; } = 7200;

    /// <value>Seconds to show in the abscissa axis</value>
    [JsonPropertyName("Plots moving window points")]
    public int Plot_WindowPoints { get; set; } = 20;

    /// <value><see langword="True" /> if the plot gets updated, <see langword="false" /> otherwise</value>
    [JsonPropertyName("Show illuminance plot")]
    public bool Plot_ShowRawData { get; set; } = true;
    
    [JsonPropertyName("Show distribution plot")]
    public bool Plot_ShowDistribution { get; set; } = true;

    [JsonPropertyName("Show average plot")]
    public bool Plot_ShowAverage { get; set; } = true;

    [JsonPropertyName("Show ratios plot")]
    public bool Plot_ShowRatios { get; set; } = true;

    [JsonPropertyName("Distribution is radar")]
    public bool Plot_DistIsRadar { get; set; } = true;
    /// <summary>
    /// The number of pixels between two legends
    /// </summary>
    [JsonPropertyName("Pixels between legends")]
    public int PxBetweenLegends { get; set; } = 10;

    /// <summary>
    /// Icon indicating the T-10 is opened and ready to be sent commands
    /// </summary>
    [JsonIgnore]
    public System.Drawing.Bitmap Icon_Open { get; set; } = new System.Drawing.Icon(GraphicsResources.IconOpenConnection, 16, 16).ToBitmap();
    /// <summary>
    /// Icon indicating the T-10 is closed
    /// </summary>
    [JsonIgnore]
    public System.Drawing.Bitmap Icon_Close { get; set; } = new System.Drawing.Icon(GraphicsResources.IconCloseConnection, 16, 16).ToBitmap();
    /// <summary>
    /// Icon indicating the T-10 receiving and sending data
    /// </summary>
    [JsonIgnore]
    public System.Drawing.Bitmap Icon_Data { get; set; } = new System.Drawing.Icon(GraphicsResources.IconExchangeConnection, 16, 16).ToBitmap();

    /// <summary>
    /// Absolute path of the executable
    /// </summary>
    [JsonIgnore]
    public string AppPath { get; set; } = System.IO.Path.GetDirectoryName(System.Environment.ProcessPath) ?? String.Empty;
    /// <summary>
    /// Number of columns reserved for computed (not measured) values
    /// </summary>
    [JsonIgnore]
    public int ArrayFixedColumns { get; set; } = 6;

    /// <summary>
    /// Culture used throughout the app
    /// </summary>
    [JsonIgnore]
    public System.Globalization.CultureInfo AppCulture { get; set; } = System.Globalization.CultureInfo.CurrentCulture;

    /// <summary>
    /// Define the culture used throughout the app by asigning a culture string name
    /// </summary>
    [JsonPropertyName("Culture name")]
    public string AppCultureName
    {
        get { return AppCulture.Name; }
        set { AppCulture = new System.Globalization.CultureInfo(value); }
    }

    /// <summary>
    /// Milliseconds format
    /// </summary>
    [JsonIgnore]
    public string MillisecondsFormat
    {
        //get { return $"$1{AppCulture.NumberFormat.NumberDecimalSeparator}fff"; }
        get { return GetMillisecondsFormat(AppCulture); }
    }

    /// <summary>
    /// Numeric data formatting string
    /// </summary>
    [JsonIgnore]
    public string DataFormat { get; set; } = "#0.0##";

    /// <summary>
    /// True if open/save dialogs should remember the user's previous path
    /// </summary>
    [JsonPropertyName("Remember path in FileDlg?")]
    public bool RememberFileDialogPath { get; set; } = true;
    /// <summary>
    /// Default path for saving files to disk
    /// </summary>
    [JsonPropertyName("Default save path")]
    public string DefaultSavePath { get; set; } = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
    /// <summary>
    /// User-defined path for saving files to disk
    /// </summary>
    [JsonPropertyName("User save path")]
    public string UserSavePath { get; set; } = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
    /// <summary>
    /// Default path for reading files from disk
    /// </summary>
    [JsonPropertyName("Default open path")]
    public string DefaultOpenPath { get; set; } = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\examples";
    /// <summary>
    /// User-defined path for reading files from disk
    /// </summary>
    [JsonPropertyName("User open path")]
    public string UserOpenPath { get; set; } = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\examples";

    public ClassSettings()
    {

    }

    ~ClassSettings()
    {
        if (Icon_Open is not null) Icon_Open.Dispose();
        if (Icon_Close is not null) Icon_Close.Dispose();
        if (Icon_Data is not null) Icon_Data.Dispose();
    }

    public string GetMillisecondsFormat(System.Globalization.CultureInfo culture)
    {
        return $"$1{culture.NumberFormat.NumberDecimalSeparator}fff";
    }
}