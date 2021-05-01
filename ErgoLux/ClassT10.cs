using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgoLux
{
    //https://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp
    public class ClassT10
    {
        private static readonly string strSTX = Char.ConvertFromUtf32(2);
        private static readonly string strETX = Char.ConvertFromUtf32(3);
        private static readonly string strDelimiter = Char.ConvertFromUtf32(13) + Char.ConvertFromUtf32(10);
        //private string _strCommand;
        //private string[] _commands;
        private static readonly string[] _receptors = new string[]
                {
                    strSTX + "00100200" + strETX + "00" + strDelimiter,
                    strSTX + "01100200" + strETX + "01" + strDelimiter,
                    strSTX + "02100200" + strETX + "02" + strDelimiter,
                    strSTX + "03100200" + strETX + "03" + strDelimiter,
                    strSTX + "04100200" + strETX + "04" + strDelimiter,
                    strSTX + "05100200" + strETX + "05" + strDelimiter,
                    strSTX + "06100200" + strETX + "06" + strDelimiter,
                    strSTX + "07100200" + strETX + "07" + strDelimiter,
                    strSTX + "08100200" + strETX + "08" + strDelimiter,
                    strSTX + "09100200" + strETX + "09" + strDelimiter,
                    strSTX + "10100200" + strETX + "01" + strDelimiter,
                    strSTX + "11100200" + strETX + "00" + strDelimiter,
                    strSTX + "12100200" + strETX + "03" + strDelimiter,
                    strSTX + "13100200" + strETX + "02" + strDelimiter,
                    strSTX + "14100200" + strETX + "05" + strDelimiter,
                    strSTX + "15100200" + strETX + "04" + strDelimiter,
                    strSTX + "16100200" + strETX + "07" + strDelimiter,
                    strSTX + "17100200" + strETX + "06" + strDelimiter,
                    strSTX + "18100200" + strETX + "09" + strDelimiter,
                    strSTX + "19100200" + strETX + "08" + strDelimiter,
                    strSTX + "20100200" + strETX + "02" + strDelimiter,
                    strSTX + "21100200" + strETX + "03" + strDelimiter,
                    strSTX + "22100200" + strETX + "00" + strDelimiter,
                    strSTX + "23100200" + strETX + "01" + strDelimiter,
                    strSTX + "24100200" + strETX + "06" + strDelimiter,
                    strSTX + "25100200" + strETX + "07" + strDelimiter,
                    strSTX + "26100200" + strETX + "04" + strDelimiter,
                    strSTX + "27100200" + strETX + "05" + strDelimiter,
                    strSTX + "28100200" + strETX + "0a" + strDelimiter,
                    strSTX + "29100200" + strETX + "0b" + strDelimiter,
                    strSTX + "30100200" + strETX + "03" + strDelimiter
                };
        private static readonly string[] _integrated = new string[]
                {
                    strSTX + "00110200" + strETX + "01" + strDelimiter,
                    strSTX + "01110200" + strETX + "00" + strDelimiter,
                    strSTX + "02110200" + strETX + "03" + strDelimiter,
                    strSTX + "03110200" + strETX + "02" + strDelimiter,
                    strSTX + "04110200" + strETX + "05" + strDelimiter,
                    strSTX + "05110200" + strETX + "04" + strDelimiter,
                    strSTX + "06110200" + strETX + "07" + strDelimiter,
                    strSTX + "07110200" + strETX + "06" + strDelimiter,
                    strSTX + "08110200" + strETX + "09" + strDelimiter,
                    strSTX + "09110200" + strETX + "08" + strDelimiter,
                    strSTX + "10110200" + strETX + "00" + strDelimiter,
                    strSTX + "11110200" + strETX + "01" + strDelimiter,
                    strSTX + "12110200" + strETX + "02" + strDelimiter,
                    strSTX + "13110200" + strETX + "03" + strDelimiter,
                    strSTX + "14110200" + strETX + "04" + strDelimiter,
                    strSTX + "15110200" + strETX + "05" + strDelimiter,
                    strSTX + "16110200" + strETX + "06" + strDelimiter,
                    strSTX + "17110200" + strETX + "07" + strDelimiter,
                    strSTX + "18110200" + strETX + "08" + strDelimiter,
                    strSTX + "19110200" + strETX + "09" + strDelimiter,
                    strSTX + "20110200" + strETX + "03" + strDelimiter,
                    strSTX + "21110200" + strETX + "02" + strDelimiter,
                    strSTX + "22110200" + strETX + "01" + strDelimiter,
                    strSTX + "23110200" + strETX + "00" + strDelimiter,
                    strSTX + "24110200" + strETX + "07" + strDelimiter,
                    strSTX + "25110200" + strETX + "06" + strDelimiter,
                    strSTX + "26110200" + strETX + "05" + strDelimiter,
                    strSTX + "27110200" + strETX + "04" + strDelimiter,
                    strSTX + "28110200" + strETX + "0b" + strDelimiter,
                    strSTX + "29110200" + strETX + "0a" + strDelimiter,
                    strSTX + "30110200" + strETX + "02" + strDelimiter
                };


        /// <summary>
        /// Get 11-code list for measurements
        /// </summary>
        public static string[] ReceptorsSingle { get => _receptors; }

        /// <summary>
        /// Get 11-code list for the integration measurements
        /// </summary>
        public static string[] ReceptorsIntegrated { get => _integrated; }

        
        public string Value { get; set; }

        //public static ClassT10 Command11 { get { return new ClassT10(strSTX + "00541   " + strETX + "13" + strDelimiter); } }
        /// <summary>
        /// Clear integrated data
        /// </summary>
        public static string Command28 { get { return (new ClassT10(strSTX + "0028    " + strETX + "13" + strDelimiter)).Value; } }
        /// <summary>
        /// Set PC connection mode
        /// </summary>
        public static string Command54 { get { return (new ClassT10(strSTX + "00541   " + strETX + "13" + strDelimiter)).Value; } }
        /// <summary>
        /// Start integration mode
        /// </summary>
        public static ClassT10 Command550 { get { return new ClassT10(strSTX + "99550  0" + strETX + "13" + strDelimiter); } }
        /// <summary>
        /// End integration mode
        /// </summary>
        public static ClassT10 Command551 { get { return new ClassT10(strSTX + "99551  0" + strETX + "13" + strDelimiter); } }
        

        private ClassT10(string value) { Value = value; }

        public ClassT10()
        {
        }

        /// <summary>
        /// Encodes a Command into the T-10A format
        /// </summary>
        /// <param name="strCommand">String to encode</param>
        /// <returns>The full command ready to send to the T-10A</returns>
        public static string EncodeCommand(string strCommand)
        {
            //string strSTX = Char.ConvertFromUtf32(2);
            //string strETX = Char.ConvertFromUtf32(3);
            //string strBCC = BCC(strCommand, strETX);
            //string strDelimiter = Char.ConvertFromUtf32(13) + Char.ConvertFromUtf32(10);   // \r\n
            //string temp = strCommand + strETX;

            //// Xor the digits of strCommand + strETX
            //int xor = temp.Substring(0, 1)[0];
            //for (int i = 1; i < temp.Length; i++)
            //{
            //    xor ^= temp.Substring(i, 1)[0];
            //}

            //// Convert to string
            //strBCC = Convert.ToString(xor, 16);
            
            //// Add a 0 digit if necessary
            //if (strBCC.Length == 1)
            //    strBCC = 0 + strBCC;

            //var algo = strSTX + strCommand + strETX + strBCC + strDelimiter;

            return strSTX + strCommand + strETX + BCC(strCommand, strETX) + strDelimiter;
        }

        /// <summary>
        /// Calculates the BCC code for a string
        /// </summary>
        /// <param name="strCommand"></param>
        /// <param name="strETX"></param>
        /// <returns>The BCC code</returns>
        private static string BCC(string strCommand, string strETX = null)
        {
            string strResult;
            string strBBC = strCommand + (strETX ?? string.Empty);

            // Xor the digits of strCommand + strETX
            int nBBC = strBBC.Substring(0, 1)[0];
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
        /// Decodes the value returned by the T-10A
        /// </summary>
        /// <param name="strCommand">String returned by the T-10A</param>
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
            var nIncrement = Convert.ToDouble(Convert.ToInt32(strTemp2.Substring(1, 4))) * (int)Math.Pow(10.0, Convert.ToInt32(strTemp2.Substring(5, 1)) - 4);
            if (strTemp2.Substring(0, 1) == "-") nIncrement = -nIncrement;
            var nPercent = Convert.ToDouble(Convert.ToInt32(strTemp3.Substring(1, 4))) * (int)Math.Pow(10.0, Convert.ToInt32(strTemp3.Substring(5, 1)) - 4);

            return (Sensor, nIluminance, nIncrement, nPercent);
        }

    }
}
