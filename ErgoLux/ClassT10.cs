using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgoLux
{
    public class ClassT10
    {
        private string _strCommand;
        private string[] _commands;
        private string[] _receptors;
        private string[] _integrated;

        public string[] Commands { get => _commands; }
        public string[] ReceptorsSingle { get => _receptors; }
        public string[] ReceptorsIntegrated { get => _integrated; }

        public string StrCommand
        {
            get => _strCommand;
            set => _strCommand = value;
        }

        public ClassT10()
        {
            string strSTX = Char.ConvertFromUtf32(2);
            string strETX = Char.ConvertFromUtf32(3);
            string strDelimiter = Char.ConvertFromUtf32(13) + Char.ConvertFromUtf32(10);

            _commands = new string[2];
            _commands[0] = strSTX + "00541   " + strETX + "13" + strDelimiter;
            _commands[1] = strSTX + "00541   " + strETX + "13" + strDelimiter;

            _receptors = new string[25];
            _receptors[0] = strSTX + "00100200" + strETX + "00" + strDelimiter;
            _receptors[1] = strSTX + "01100200" + strETX + "01" + strDelimiter;
            _receptors[2] = strSTX + "02100200" + strETX + "02" + strDelimiter;
            _receptors[3] = strSTX + "03100200" + strETX + "03" + strDelimiter;
            _receptors[4] = strSTX + "04100200" + strETX + "04" + strDelimiter;
            _receptors[5] = strSTX + "05100200" + strETX + "05" + strDelimiter;
            _receptors[6] = strSTX + "06100200" + strETX + "06" + strDelimiter;
            _receptors[7] = strSTX + "07100200" + strETX + "07" + strDelimiter;
            _receptors[8] = strSTX + "08100200" + strETX + "08" + strDelimiter;
            _receptors[9] = strSTX + "09100200" + strETX + "09" + strDelimiter;
            _receptors[10] = strSTX + "10100200" + strETX + "01" + strDelimiter;
            _receptors[11] = strSTX + "11100200" + strETX + "00" + strDelimiter;
            _receptors[12] = strSTX + "12100200" + strETX + "03" + strDelimiter;
            _receptors[13] = strSTX + "13100200" + strETX + "02" + strDelimiter;
            _receptors[14] = strSTX + "14100200" + strETX + "05" + strDelimiter;
            _receptors[15] = strSTX + "15100200" + strETX + "04" + strDelimiter;
            _receptors[16] = strSTX + "16100200" + strETX + "07" + strDelimiter;
            _receptors[17] = strSTX + "17100200" + strETX + "06" + strDelimiter;
            _receptors[18] = strSTX + "18100200" + strETX + "09" + strDelimiter;
            _receptors[19] = strSTX + "19100200" + strETX + "08" + strDelimiter;
            _receptors[20] = strSTX + "20100200" + strETX + "02" + strDelimiter;
            _receptors[21] = strSTX + "21100200" + strETX + "03" + strDelimiter;
            _receptors[22] = strSTX + "22100200" + strETX + "00" + strDelimiter;
            _receptors[23] = strSTX + "23100200" + strETX + "01" + strDelimiter;
            _receptors[24] = strSTX + "24100200" + strETX + "06" + strDelimiter;

            _integrated = new string[25];
            _integrated[0] = strSTX + "00110200" + strETX + "01" + strDelimiter;
            _integrated[1] = strSTX + "01110200" + strETX + "00" + strDelimiter;
            _integrated[2] = strSTX + "02110200" + strETX + "03" + strDelimiter;
            _integrated[3] = strSTX + "03110200" + strETX + "02" + strDelimiter;
            _integrated[4] = strSTX + "04110200" + strETX + "05" + strDelimiter;
            _integrated[5] = strSTX + "05110200" + strETX + "04" + strDelimiter;
            _integrated[6] = strSTX + "06110200" + strETX + "07" + strDelimiter;
            _integrated[7] = strSTX + "07110200" + strETX + "06" + strDelimiter;
            _integrated[8] = strSTX + "08110200" + strETX + "09" + strDelimiter;
            _integrated[9] = strSTX + "09110200" + strETX + "08" + strDelimiter;
            _integrated[10] = strSTX + "10110200" + strETX + "00" + strDelimiter;
            _integrated[11] = strSTX + "11110200" + strETX + "01" + strDelimiter;
            _integrated[12] = strSTX + "12110200" + strETX + "02" + strDelimiter;
            _integrated[13] = strSTX + "13110200" + strETX + "03" + strDelimiter;
            _integrated[14] = strSTX + "14110200" + strETX + "04" + strDelimiter;
            _integrated[15] = strSTX + "15110200" + strETX + "05" + strDelimiter;
            _integrated[16] = strSTX + "16110200" + strETX + "06" + strDelimiter;
            _integrated[17] = strSTX + "17110200" + strETX + "07" + strDelimiter;
            _integrated[18] = strSTX + "18110200" + strETX + "08" + strDelimiter;
            _integrated[19] = strSTX + "19110200" + strETX + "09" + strDelimiter;
            _integrated[20] = strSTX + "20110200" + strETX + "03" + strDelimiter;
            _integrated[21] = strSTX + "21110200" + strETX + "02" + strDelimiter;
            _integrated[22] = strSTX + "22110200" + strETX + "01" + strDelimiter;
            _integrated[23] = strSTX + "23110200" + strETX + "00" + strDelimiter;
            _integrated[24] = strSTX + "24110200" + strETX + "07" + strDelimiter;
        }

        public ClassT10(string str)
            :this()
        {
            _strCommand = str;
        }

        public string EncodeCommand(string strCommand = null)
        {
            string strSTX = Char.ConvertFromUtf32(2);
            string strETX = Char.ConvertFromUtf32(3);
            string strBCC = BCC(strCommand ?? _strCommand) + strETX;
            string strDelimiter = Char.ConvertFromUtf32(13) + Char.ConvertFromUtf32(10);   // \r\n
            string temp = (strCommand ?? _strCommand) + strETX;

            // Xor the digits of strCommand + strETX
            int xor = temp.Substring(0, 1)[0];
            for (int i = 1; i < temp.Length; i++)
            {
                xor ^= temp.Substring(i, 1)[0];
            }

            // Convert to string
            strBCC = Convert.ToString(xor, 16);
            
            // Add a 0 digit if necessary
            if (strBCC.Length == 1)
                strBCC = 0 + strBCC;

            return strSTX + (strCommand ?? _strCommand) + strETX + strBCC + strDelimiter;
        }

        public (int nIluminance, int nIncrement, int nPercent) DecodeCommand(string strCommand = null)
        {
            string strTemp = strCommand.Substring(strCommand.Length - 18, 18);
            string strTemp1 = strTemp.Substring(0, 6);
            string strTemp2 = strTemp.Substring(5, 6);
            string strTemp3 = strTemp.Substring(11, 6);

            var nIluminance = Convert.ToInt32(strTemp.Substring(0, 5)) * 10 ^ (Convert.ToInt32(strTemp.Substring(4, 1)) - 4);
            var nIncrement = Convert.ToInt32(strTemp.Substring(0, 5)) * 10 ^ (Convert.ToInt32(strTemp.Substring(4, 1)) - 4);
            var nPercent = Convert.ToInt32(strTemp.Substring(0, 5)) * 10 ^ (Convert.ToInt32(strTemp.Substring(4, 1)) - 4);

            return (nIluminance, nIncrement, nPercent);
        }


        private string BCC(string strCommand, string strETX = null)
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
