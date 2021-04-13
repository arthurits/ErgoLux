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

        public string StrCommand
        {
            get => _strCommand;
            set => _strCommand = value;
        }

        public ClassT10()
        {

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
            string strBCC;
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

    }
}
