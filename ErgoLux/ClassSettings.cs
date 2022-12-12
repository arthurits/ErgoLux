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
    public System.Drawing.Bitmap? Icon_Open { get; set; }
    /// <summary>
    /// Icon indicating the T-10 is closed
    /// </summary>
    [JsonIgnore]
    public System.Drawing.Bitmap? Icon_Close { get; set; }
    /// <summary>
    /// Icon indicating the T-10 receiving and sending data
    /// </summary>
    [JsonIgnore]
    public System.Drawing.Bitmap? Icon_Data { get; set; }

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
    public string DefaultOpenPath { get; set; } = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Environment.ProcessPath) ?? String.Empty, "examples");
    /// <summary>
    /// User-defined path for reading files from disk
    /// </summary>
    [JsonPropertyName("User open path")]
    public string UserOpenPath { get; set; } = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Environment.ProcessPath) ?? String.Empty, "examples");

    /// <summary>
    /// Default class constructor. This is needed in <see cref="FrmMain.LoadProgramSettingsJSON"/> in order to avoid <see cref="System.Text.Json.JsonSerializer.Deserialize"/> throwing exception <see cref="System.InvalidOperationException"/>
    /// </summary>
    public ClassSettings()
    {
    }

    public ClassSettings(ClassSettings? oldSettings = null)
    {
        if (oldSettings is not null)
        {
            AppCulture = oldSettings.AppCulture;
            //AppCultureName = oldSettings.AppCultureName;
            AppPath = oldSettings.AppPath;
            ArrayFixedColumns = oldSettings.ArrayFixedColumns;
            DataFormat = oldSettings.DataFormat;
            DefaultSavePath = oldSettings.DefaultSavePath;
            DefaultOpenPath = oldSettings.DefaultOpenPath;
            Icon_Close = oldSettings.Icon_Close;
            Icon_Data = oldSettings.Icon_Data;
            Icon_Open = oldSettings.Icon_Open;
            //MillisecondsFormat = oldSettings.MillisecondsFormat;
            Plot_ArrayPoints = oldSettings.Plot_ArrayPoints;
            Plot_DistIsRadar = oldSettings.Plot_DistIsRadar;
            Plot_ShowAverage = oldSettings.Plot_ShowAverage;
            Plot_ShowDistribution = oldSettings.Plot_ShowDistribution;
            Plot_ShowRatios = oldSettings.Plot_ShowRatios;
            Plot_ShowRawData = oldSettings.Plot_ShowRawData;
            Plot_WindowPoints = oldSettings.Plot_WindowPoints;
            PxBetweenLegends = oldSettings.PxBetweenLegends;
            RememberFileDialogPath = oldSettings.RememberFileDialogPath;
            SettingsFileName = oldSettings.SettingsFileName;
            T10_BaudRate = oldSettings.T10_BaudRate;
            T10_CharOff = oldSettings.T10_CharOff;
            T10_CharOn = oldSettings.T10_CharOn;
            T10_DataBits = oldSettings.T10_DataBits;
            T10_DeviceType = oldSettings.T10_DeviceType;
            T10_DevideID = oldSettings.T10_DevideID;
            T10_FlowControl = oldSettings.T10_FlowControl;
            T10_Frequency = oldSettings.T10_Frequency;
            T10_LocationID = oldSettings.T10_LocationID;
            T10_NumberOfSensors = oldSettings.T10_NumberOfSensors;
            T10_Parity = oldSettings.T10_Parity;
            T10_StopBits = oldSettings.T10_StopBits;
            UserOpenPath = oldSettings.UserOpenPath;
            UserSavePath = oldSettings.UserSavePath;
            WindowHeight = oldSettings.WindowHeight;
            WindowLeft = oldSettings.WindowLeft;
            WindowPosition = oldSettings.WindowPosition;
            WindowTop = oldSettings.WindowTop;
            WindowWidth = oldSettings.WindowWidth;
        }
    }

    ~ClassSettings()
    {
        if (Icon_Open is not null) Icon_Open.Dispose();
        if (Icon_Close is not null) Icon_Close.Dispose();
        if (Icon_Data is not null) Icon_Data.Dispose();
    }

    /// <summary>
    /// To be used only if we want to load specific GraphicsResources
    /// </summary>
    public void LoadGraphicResources()
    {
        Icon_Open = GraphicsResources.LoadIcon(GraphicsResources.IconOpenConnection, 16);
        Icon_Close = GraphicsResources.LoadIcon(GraphicsResources.IconCloseConnection, 16);
        Icon_Data = GraphicsResources.LoadIcon(GraphicsResources.IconExchangeConnection, 16);
    }

    public string GetMillisecondsFormat(System.Globalization.CultureInfo culture)
    {
        return $"$1{culture.NumberFormat.NumberDecimalSeparator}fff";
    }
}