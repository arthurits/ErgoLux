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

        //public string[] Commands { get => _commands; }
        //public string StrCommand
        //{
        //    get => _strCommand;
        //    set => _strCommand = value;
        //}

        //public static string[] ReceptorsSingle { get => _receptors; }
        /// <summary>
        /// Get 11-code list for measurements
        /// </summary>
        public static string[] ReceptorsSingle
        {
            get { return _receptors; }
        }

        //public static string[] ReceptorsIntegrated { get => _integrated; }
        /// <summary>
        /// Get 11-code list for the integration measurements
        /// </summary>
        public static string[] ReceptorsIntegrated
        {
            get { return _integrated; }
        }

        
        public string Value { get; set; }

        //public static ClassT10 Command11 { get { return new ClassT10(strSTX + "00541   " + strETX + "13" + strDelimiter); } }
        /// <summary>
        /// Clear integrated data
        /// </summary>
        public static ClassT10 Command28 { get { return new ClassT10(strSTX + "0028    " + strETX + "13" + strDelimiter); } }
        /// <summary>
        /// Set PC connection mode
        /// </summary>
        public static ClassT10 Command54 { get { return new ClassT10(strSTX + "00541   " + strETX + "13" + strDelimiter); } }
        /// <summary>
        /// Start integration mode
        /// </summary>
        public static ClassT10 Command550 { get { return new ClassT10(strSTX + "99550  0" + strETX + "13" + strDelimiter); } }
        /// <summary>
        /// End integration mode
        /// </summary>
        public static ClassT10 Command551 { get { return new ClassT10(strSTX + "99551  0" + strETX + "13" + strDelimiter); } }
        


        // This sould be set private
        private ClassT10(string value) { Value = value; }

        public ClassT10()
        {
            //string strSTX = Char.ConvertFromUtf32(2);
            //string strETX = Char.ConvertFromUtf32(3);
            //string strDelimiter = Char.ConvertFromUtf32(13) + Char.ConvertFromUtf32(10);

            //_commands = new string[2];
            //_commands[0] = strSTX + "00541   " + strETX + "13" + strDelimiter;
            //_commands[1] = strSTX + "00541   " + strETX + "13" + strDelimiter;

            //_receptors = new string[31];
            //_receptors[0] = strSTX + "00100200" + strETX + "00" + strDelimiter;
            //_receptors[1] = strSTX + "01100200" + strETX + "01" + strDelimiter;
            //_receptors[2] = strSTX + "02100200" + strETX + "02" + strDelimiter;
            //_receptors[3] = strSTX + "03100200" + strETX + "03" + strDelimiter;
            //_receptors[4] = strSTX + "04100200" + strETX + "04" + strDelimiter;
            //_receptors[5] = strSTX + "05100200" + strETX + "05" + strDelimiter;
            //_receptors[6] = strSTX + "06100200" + strETX + "06" + strDelimiter;
            //_receptors[7] = strSTX + "07100200" + strETX + "07" + strDelimiter;
            //_receptors[8] = strSTX + "08100200" + strETX + "08" + strDelimiter;
            //_receptors[9] = strSTX + "09100200" + strETX + "09" + strDelimiter;
            //_receptors[10] = strSTX + "10100200" + strETX + "01" + strDelimiter;
            //_receptors[11] = strSTX + "11100200" + strETX + "00" + strDelimiter;
            //_receptors[12] = strSTX + "12100200" + strETX + "03" + strDelimiter;
            //_receptors[13] = strSTX + "13100200" + strETX + "02" + strDelimiter;
            //_receptors[14] = strSTX + "14100200" + strETX + "05" + strDelimiter;
            //_receptors[15] = strSTX + "15100200" + strETX + "04" + strDelimiter;
            //_receptors[16] = strSTX + "16100200" + strETX + "07" + strDelimiter;
            //_receptors[17] = strSTX + "17100200" + strETX + "06" + strDelimiter;
            //_receptors[18] = strSTX + "18100200" + strETX + "09" + strDelimiter;
            //_receptors[19] = strSTX + "19100200" + strETX + "08" + strDelimiter;
            //_receptors[20] = strSTX + "20100200" + strETX + "02" + strDelimiter;
            //_receptors[21] = strSTX + "21100200" + strETX + "03" + strDelimiter;
            //_receptors[22] = strSTX + "22100200" + strETX + "00" + strDelimiter;
            //_receptors[23] = strSTX + "23100200" + strETX + "01" + strDelimiter;
            //_receptors[24] = strSTX + "24100200" + strETX + "06" + strDelimiter;
            //_receptors[25] = strSTX + "25100200" + strETX + "07" + strDelimiter;
            //_receptors[26] = strSTX + "26100200" + strETX + "04" + strDelimiter;
            //_receptors[27] = strSTX + "27100200" + strETX + "05" + strDelimiter;
            //_receptors[28] = strSTX + "28100200" + strETX + "0a" + strDelimiter;
            //_receptors[29] = strSTX + "29100200" + strETX + "0b" + strDelimiter;
            //_receptors[30] = strSTX + "30100200" + strETX + "03" + strDelimiter;

            //_integrated = new string[31];
            //_integrated[0] = strSTX + "00110200" + strETX + "01" + strDelimiter;
            //_integrated[1] = strSTX + "01110200" + strETX + "00" + strDelimiter;
            //_integrated[2] = strSTX + "02110200" + strETX + "03" + strDelimiter;
            //_integrated[3] = strSTX + "03110200" + strETX + "02" + strDelimiter;
            //_integrated[4] = strSTX + "04110200" + strETX + "05" + strDelimiter;
            //_integrated[5] = strSTX + "05110200" + strETX + "04" + strDelimiter;
            //_integrated[6] = strSTX + "06110200" + strETX + "07" + strDelimiter;
            //_integrated[7] = strSTX + "07110200" + strETX + "06" + strDelimiter;
            //_integrated[8] = strSTX + "08110200" + strETX + "09" + strDelimiter;
            //_integrated[9] = strSTX + "09110200" + strETX + "08" + strDelimiter;
            //_integrated[10] = strSTX + "10110200" + strETX + "00" + strDelimiter;
            //_integrated[11] = strSTX + "11110200" + strETX + "01" + strDelimiter;
            //_integrated[12] = strSTX + "12110200" + strETX + "02" + strDelimiter;
            //_integrated[13] = strSTX + "13110200" + strETX + "03" + strDelimiter;
            //_integrated[14] = strSTX + "14110200" + strETX + "04" + strDelimiter;
            //_integrated[15] = strSTX + "15110200" + strETX + "05" + strDelimiter;
            //_integrated[16] = strSTX + "16110200" + strETX + "06" + strDelimiter;
            //_integrated[17] = strSTX + "17110200" + strETX + "07" + strDelimiter;
            //_integrated[18] = strSTX + "18110200" + strETX + "08" + strDelimiter;
            //_integrated[19] = strSTX + "19110200" + strETX + "09" + strDelimiter;
            //_integrated[20] = strSTX + "20110200" + strETX + "03" + strDelimiter;
            //_integrated[21] = strSTX + "21110200" + strETX + "02" + strDelimiter;
            //_integrated[22] = strSTX + "22110200" + strETX + "01" + strDelimiter;
            //_integrated[23] = strSTX + "23110200" + strETX + "00" + strDelimiter;
            //_integrated[24] = strSTX + "24110200" + strETX + "07" + strDelimiter;
            //_integrated[25] = strSTX + "25110200" + strETX + "06" + strDelimiter;
            //_integrated[26] = strSTX + "26110200" + strETX + "05" + strDelimiter;
            //_integrated[27] = strSTX + "27110200" + strETX + "04" + strDelimiter;
            //_integrated[28] = strSTX + "28110200" + strETX + "0b" + strDelimiter;
            //_integrated[29] = strSTX + "29110200" + strETX + "0a" + strDelimiter;
            //_integrated[30] = strSTX + "30110200" + strETX + "02" + strDelimiter;
        }

        //public ClassT10(string str)
        //    :this()
        //{
        //    _strCommand = str;
        //}

        public static string EncodeCommand(string strCommand)
        {
            string strSTX = Char.ConvertFromUtf32(2);
            string strETX = Char.ConvertFromUtf32(3);
            string strBCC = BCC(strCommand, strETX);
            string strDelimiter = Char.ConvertFromUtf32(13) + Char.ConvertFromUtf32(10);   // \r\n
            string temp = strCommand + strETX;

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

            return strSTX + strCommand + strETX + strBCC + strDelimiter;
        }

        public static (int Sensor, double nIluminance, int nIncrement, int nPercent) DecodeCommand(string strCommand = null)
        {
            if (strCommand.Length != 32) return (0, 0, 0, 0);

            //if (strCommand.Length != 14) return (0, 0, 0, 0);

            string strTemp = strCommand.Substring(strCommand.Length - (18 + 5), 18);
            string strTemp1 = strTemp.Substring(0, 6);
            string strTemp2 = strTemp.Substring(6, 6);
            string strTemp3 = strTemp.Substring(12, 6);

            var Sensor = Convert.ToInt32(strCommand.Substring(1, 2));
            var nIluminance = Convert.ToDouble(strTemp1.Substring(1, 4)) * Math.Pow(10.0, Convert.ToInt32(strTemp1.Substring(5, 1)) - 4);
            //var nIncrement = Convert.ToInt32(strTemp2.Substring(1, 4)) * (int)Math.Pow(10.0, Convert.ToInt32(strTemp2.Substring(5, 1)) - 4);
            //var nPercent = Convert.ToInt32(strTemp3.Substring(1, 4)) * (int)Math.Pow(10.0, Convert.ToInt32(strTemp3.Substring(5, 1)) - 4);

            return (Sensor, nIluminance, 0, 0);
        }


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
    }
}
