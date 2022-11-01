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

    [JsonIgnore]
    public string T10_DeviceType { get; set; } = string.Empty;
    [JsonIgnore]
    public int T10_DevideID { get; set; } = 0;

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
    public System.Drawing.Bitmap? Icon_Open { get; set; } = GraphicsResources.LoadIcon(GraphicsResources.IconOpenConnection, 16);
    /// <summary>
    /// Icon indicating the T-10 is closed
    /// </summary>
    [JsonIgnore]
    public System.Drawing.Bitmap? Icon_Close { get; set; } = GraphicsResources.LoadIcon(GraphicsResources.IconCloseConnection, 16);
    /// <summary>
    /// Icon indicating the T-10 receiving and sending data
    /// </summary>
    [JsonIgnore]
    public System.Drawing.Bitmap? Icon_Data { get; set; } = GraphicsResources.LoadIcon(GraphicsResources.IconExchangeConnection, 16);

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
        get { return this.AppCulture.Name; }
        set { this.AppCulture = new System.Globalization.CultureInfo(value); }
    }

    /// <summary>
    /// Milliseconds format
    /// </summary>
    [JsonIgnore]
    public string MillisecondsFormat
    {
        //get { return $"$1{AppCulture.NumberFormat.NumberDecimalSeparator}fff"; }
        get { return this.GetMillisecondsFormat(this.AppCulture); }
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
    public string DefaultOpenPath { get; set; } = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Environment.ProcessPath) ?? String.Empty, "examples");
    /// <summary>
    /// User-defined path for reading files from disk
    /// </summary>
    [JsonPropertyName("User open path")]
    public string UserOpenPath { get; set; } = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Environment.ProcessPath) ?? String.Empty, "examples");

    public ClassSettings()
    {
    }

    ~ClassSettings()
    {
        if (this.Icon_Open is not null) this.Icon_Open.Dispose();
        if (this.Icon_Close is not null) this.Icon_Close.Dispose();
        if (this.Icon_Data is not null) this.Icon_Data.Dispose();
    }

    /// <summary>
    /// To be used only if we want to load specific GraphicsResources
    /// </summary>
    public void LoadGraphicResources()
    {
        this.Icon_Open = GraphicsResources.LoadIcon(GraphicsResources.IconOpenConnection, 16);
        this.Icon_Close = GraphicsResources.LoadIcon(GraphicsResources.IconCloseConnection, 16);
        this.Icon_Data = GraphicsResources.LoadIcon(GraphicsResources.IconExchangeConnection, 16);
    }

    public string GetMillisecondsFormat(System.Globalization.CultureInfo culture)
    {
        return $"$1{culture.NumberFormat.NumberDecimalSeparator}fff";
    }
}