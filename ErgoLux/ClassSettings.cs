using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErgoLux
{
    /// <summary>
    /// Keeps the settings for the T-10 device, plottting options and app window properties
    /// </summary>
    public class ClassSettings
    {
        [JsonPropertyName("Window top")]
        public int Wnd_Top { get; set; }
        [JsonPropertyName("Window left")]
        public int Wnd_Left { get; set; }
        [JsonPropertyName("Window width")]
        public int Wnd_Width { get; set; }
        [JsonPropertyName("Window height")]
        public int Wnd_Height { get; set; }

        [JsonPropertyName("Location id")]
        public int T10_LocationID { get; set; }
        [JsonPropertyName("Number of sensors")]
        public int T10_NumberOfSensors { get; set; }
        [JsonPropertyName("Baud rate")]
        public int T10_BaudRate { get; set; }
        [JsonPropertyName("Data bits")]
        public int T10_DataBits { get; set; }
        [JsonPropertyName("Stop bits")]
        public int T10_StopBits { get; set; }
        [JsonPropertyName("Parity")]
        public int T10_Parity { get; set; }
        [JsonPropertyName("Flow control")]
        public int T10_FlowControl { get; set; }
        [JsonPropertyName("Character on")]
        public int T10_CharOn { get; set; }
        [JsonPropertyName("Chareacter off")]
        public int T10_CharOff { get; set; }
        [JsonPropertyName("Sample frequency")]
        public int T10_Frequency { get; set; }

        /// <summary>
        /// Number of points the array can store
        /// </summary>
        [JsonPropertyName("Array points")]
        public int Plot_ArrayPoints { get; set; }
        
        /// <summary>
        /// Number of points to be shown in the plots
        /// </summary>
        [JsonPropertyName("Plots moving window points")]
        public int Plot_WindowPoints { get; set; }
        [JsonPropertyName("Show illuminance plot")]
        public bool Plot_ShowRawData { get; set; }
        [JsonPropertyName("Show radar plot")]
        public bool Plot_ShowRadar { get; set; }
        [JsonPropertyName("Show average plot")]
        public bool Plot_ShowAverage { get; set; }
        [JsonPropertyName("Show ratios plot")]
        public bool Plot_ShowRatios { get; set; }

        /// <summary>
        /// Icon indicating the T-10 is opened and ready to be sent commands
        /// </summary>
        [JsonIgnore]
        public System.Drawing.Bitmap Icon_Open { get; set; }
        /// <summary>
        /// Icon indicating the T-10 is closed
        /// </summary>
        [JsonIgnore]
        public System.Drawing.Bitmap Icon_Close { get; set; }
        /// <summary>
        /// Icon indicating the T-10 receiving and sending data
        /// </summary>
        [JsonIgnore]
        public System.Drawing.Bitmap Icon_Data { get; set; }
        
        /// <summary>
        /// Stores the settings file name
        /// </summary>
        [JsonIgnore]
        public string FileName { get; set; }
        /// <summary>
        /// Absolute path of the executable
        /// </summary>
        [JsonIgnore]
        public string Path { get; set; }
        /// <summary>
        /// Number of columns reserved for computed (not measured) values
        /// </summary>
        [JsonIgnore]
        public int ArrayFixedColumns { get; set; }

        public ClassSettings()
        {
            Wnd_Top = 0;
            Wnd_Left = 0;
            Wnd_Width = 950;
            Wnd_Height = 650;

            T10_BaudRate = 9600;
            T10_CharOff = 13;
            T10_CharOn = 11;
            T10_DataBits = 7;
            T10_FlowControl = 0;
            T10_Frequency = 2;
            T10_LocationID = 0;
            T10_NumberOfSensors = 1;
            T10_Parity = 2; ;
            T10_StopBits = 0;

            Plot_ArrayPoints = 7200;
            Plot_WindowPoints = 20;
            Plot_ShowRawData = true;
            Plot_ShowRadar = true;
            Plot_ShowAverage = true;
            Plot_ShowRatios = true;

            FileName = "configuration.json";
            Path = string.Empty;
            ArrayFixedColumns = 6;
        }

        public ClassSettings(string path)
            :this()
        {
            Path = path;
            if (System.IO.File.Exists(path + @"\images\close.ico")) Icon_Close = new System.Drawing.Icon(path + @"\images\close.ico", 16, 16).ToBitmap();
            if (System.IO.File.Exists(path + @"\images\open.ico")) Icon_Open = new System.Drawing.Icon(path + @"\images\open.ico", 16, 16).ToBitmap();
            if (System.IO.File.Exists(path + @"\images\exchange.ico")) Icon_Data = new System.Drawing.Icon(path + @"\images\exchange.ico", 16, 16).ToBitmap();
        }

        ~ClassSettings()
        {
            if (Icon_Open != null) Icon_Open.Dispose();
            if (Icon_Close != null) Icon_Close.Dispose();
            if (Icon_Data != null) Icon_Data.Dispose();
        }

        /// <summary>
        /// Initializes all the fields that are JsonIgnored
        /// </summary>
        public void InitializeJsonIgnore(string path = null)
        {
            FileName = "configuration.json";
            Path = path ?? string.Empty;
            ArrayFixedColumns = 6;

            if(Path != string.Empty)
            {
                if (System.IO.File.Exists(Path + @"\images\close.ico")) Icon_Close = new System.Drawing.Icon(Path + @"\images\close.ico", 16, 16).ToBitmap();
                if (System.IO.File.Exists(Path + @"\images\open.ico")) Icon_Open = new System.Drawing.Icon(Path + @"\images\open.ico", 16, 16).ToBitmap();
                if (System.IO.File.Exists(Path + @"\images\exchange.ico")) Icon_Data = new System.Drawing.Icon(Path + @"\images\exchange.ico", 16, 16).ToBitmap();
            }
        }
    }
}
