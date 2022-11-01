using System;

namespace ErgoLux
{
    // Optional approach to to handle a class as an Enum https://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp

    /// <summary>
    /// Low-level parameters and commands for T-10A,as well as functions to encode and decode information to and from T-10A
    /// </summary>
    public class ClassT10
    {
        /// <summary>
        /// Start of text constant (1 byte)
        /// </summary>
        private static readonly string strSTX;

        /// <summary>
        /// End of text constant (1 byte)
        /// </summary>
        private static readonly string strETX;

        /// <summary>
        /// Delimier constant (2 bytes)
        /// </summary>
        private static readonly string strDelimiter;

        /// <summary>
        /// Gets the response length for commands 28, 54 and 55
        /// </summary>
        public static int ShortBytesLength { get; private set; }

        /// <summary>
        /// Gets the response length for commands 10 and 11
        /// </summary>
        public static int LongBytesLength { get; private set; }

        /// <summary>
        /// Gets the maximum numbers of multipoint T-10A sensors
        /// </summary>
        public static int MaximumSensors { get; private set; }

        /// <summary>
        /// Gets the single-measurements code list (Command 10)
        /// </summary>
        public static string[] ReceptorsSingle { get; private set; }

        /// <summary>
        /// Gets the integrated-measurements code list (Command 11)
        /// </summary>
        public static string[] ReceptorsIntegrated { get; private set; }

        /// <summary>
        /// Command to clear integrated data
        /// </summary>
        public static string[] Command_28 { get; private set; }

        /// <summary>
        /// Command to set the PC connection mode
        /// </summary>
        public static string Command_54 { get; private set; }

        /// <summary>
        /// Command to start integration mode (set Hold status)
        /// </summary>
        public static string Command_55_Set { get; private set; }

        /// <summary>
        /// Command to end integration mode (release Hold status)
        /// </summary>
        public static string Command_55_Release { get; private set; }


        public ClassT10()
        {
        }

        static ClassT10()
        {
            LongBytesLength = 32;
            ShortBytesLength = 14;
            MaximumSensors = 30;

            strSTX = Char.ConvertFromUtf32(2);
            strETX = Char.ConvertFromUtf32(3);
            strDelimiter = Char.ConvertFromUtf32(13) + Char.ConvertFromUtf32(10);

            // Command 10 (single measurements) and command 11 (integrated measurements) initialization
            ReceptorsSingle = new string[MaximumSensors];
            ReceptorsIntegrated = new string[MaximumSensors];
            Command_28 = new string[MaximumSensors];
            for (int i = 0; i < MaximumSensors; i++)
            {
                ReceptorsSingle[i] = EncodeCommand(i.ToString("00") + "100200");
                ReceptorsIntegrated[i] = EncodeCommand(i.ToString("00") + "110200");
                Command_28[i] = EncodeCommand(i.ToString("00") + "28    ");
            }

            // Commands initialization
            Command_54 = EncodeCommand("00541   ");
            Command_55_Release = EncodeCommand("99550  0");
            Command_55_Set = EncodeCommand("99551  0");
        }

        /// <summary>
        /// Encodes a string command into the T-10A format
        /// </summary>
        /// <param name="strCommand">String command to encode</param>
        /// <returns>The full command ready to send to the T-10A</returns>
        public static string EncodeCommand(string strCommand)
        {
            return strSTX + strCommand + strETX + BCC(strCommand, strETX) + strDelimiter;
        }

        /// <summary>
        /// Calculates the Block Check Character code (2 bytes) for a given string
        /// </summary>
        /// <param name="strCommand">String whose BBC code need to be computed</param>
        /// <param name="strETX">Option string suffix to be appended to the first parameter string</param>
        /// <returns>The 2-bytes BCC code as a 2-character string</returns>
        private static string BCC(string strCommand, string strETX = null)
        {
            string strResult;
            string strBBC = strCommand + (strETX ?? string.Empty);

            // Xor the digits of strCommand + strETX
            int nBBC = strBBC.Substring(0, 1)[0];   // Better? int nBBC = strBBC[0];
            for (int i = 1; i < strBBC.Length; i++)
            {
                nBBC ^= strBBC.Substring(i, 1)[0];
            }

            // Convert to string
            strResult = Convert.ToString(nBBC, 16);

            // Add a 0 digit if necessary
            if (strResult.Length == 1)
                strResult = "0" + strResult;

            return strResult;
        }

        /// <summary>
        /// Decodes the value returned by the T-10A illuminancemeter
        /// </summary>
        /// <param name="strCommand">String returned by the T-10A device</param>
        /// <returns>The sensor index (0-based), illuminance, increment, and percent</returns>
        public static (int Sensor, double nIluminance, double nIncrement, double nPercent) DecodeCommand(string strCommand = null)
        {
            if (strCommand.Length != 32) return (0, 0, 0, 0);

            //if (strCommand.Length != 14) return (0, 0, 0, 0);

            string strTemp = strCommand.Substring(strCommand.Length - (18 + 5), 18);
            string strTemp1 = strTemp.Substring(0, 6);
            string strTemp2 = strTemp.Substring(6, 6);
            string strTemp3 = strTemp.Substring(12, 6);

            var Sensor = Convert.ToInt32(strCommand.Substring(1, 2));

            var nIluminance = Convert.ToDouble(Convert.ToInt32(strTemp1.Substring(1, 4))) * Math.Pow(10.0, Convert.ToInt32(strTemp1.Substring(5, 1)) - 4);

            var nIncrement = 0.0;
            if (strTemp2.Trim().Length != 0)
            {
                nIncrement = Convert.ToDouble(Convert.ToInt32(strTemp2.Substring(1, 4))) * (int)Math.Pow(10.0, Convert.ToInt32(strTemp2.Substring(5, 1)) - 4);
                if (strTemp2.Substring(0, 1) == "-") nIncrement = -nIncrement;
            }

            var nPercent = 0.0;
            if (strTemp3.Trim().Length != 0)
                nPercent = Convert.ToDouble(Convert.ToInt32(strTemp3.Substring(1, 4))) * (int)Math.Pow(10.0, Convert.ToInt32(strTemp3.Substring(5, 1)) - 4);

            return (Sensor, nIluminance, nIncrement, nPercent);
        }

    }
}