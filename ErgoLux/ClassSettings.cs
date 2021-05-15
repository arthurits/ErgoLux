using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErgoLux
{
    class ClassSettings
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

        [JsonPropertyName("Array points")]
        public int Plot_ArrayPoints { get; set; }
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

        [JsonIgnore]
        public System.Drawing.Bitmap Icon_Open { get; set; }
        [JsonIgnore]
        public System.Drawing.Bitmap Icon_Close { get; set; }
        [JsonIgnore]
        public System.Drawing.Bitmap Icon_Data { get; set; }

        [JsonIgnore]
        public string FileName { get; set; }

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
        }

        ~ClassSettings()
        {
            if (Icon_Open != null) Icon_Open.Dispose();
            if (Icon_Close != null) Icon_Close.Dispose();
            if (Icon_Data != null) Icon_Data.Dispose();
        }
    }
}
