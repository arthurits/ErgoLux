using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgoLux
{
    public class Konica
    {
        string strSndCommand;   //command
        string strRcvCommand;
        string strSendStr;   // character
        string strReceiveStr;
        string strSTX_Command;   //STX & command
        string strCommand_ETX;   //command & ETX
        string strCommand_ETX_BCC;   //command & ETX & BCC
        Int32 intErrNO;  //Error No
        Int32 intErrflg;
        Int32 intStopFlg;
        string strData; //measurement data Block
        string strData1;
        string strData2;
        string strData3;
        byte sngData1;    //measurement data Ev
        byte sngData2;   //measurement data x
        float sngData3;  //measurement data y
        Int32 i;     //for LOOP


        public void StartMeasurement()
        {
            Int32 rng;
            intErrflg = 0;

            // Step 2 PC MODE
            strSndCommand = "00541   ";
            this.CmdSend(1);
            this.ErrCheck();
            if (intErrflg == 1)
                return;

            // Insert code to wait 500ms here
            System.Windows.Forms.Application.DoEvents();

            // Step 3 SET CONDITION
            strSndCommand = "00100200";
            this.CmdSend(1);
            this.ErrCheck();
            if (intErrflg == 1)
                return;

            System.Windows.Forms.Application.DoEvents();
            // Insert code to wait 3s here
            intStopFlg = 0;

            // Step 4 READ MEASUREMENT DATA
            rng = 0;
            while (intStopFlg == 0)
            {
                strSndCommand = "00100200";
                this.CmdSend(1);
                this.ErrCheck();
                if (intErrflg == 1)
                    return;

                System.Windows.Forms.Application.DoEvents();
            }

            return;
        }

        public string CmdSend(string strCommand)
        {
            strSndCommand = strCommand;
            CmdSend(1);
            return strSendStr;
        }

        private void CmdSend(int FlgTimeoutCheck)
        {
            float sngStartTime;
            float sngFinishTime;
            string varBuf;

            intErrNO = 0;
            strRcvCommand = "";
            strReceiveStr = "";

            // Transmission
            this.BCC_Append(strSndCommand);
            strSendStr = Char.ConvertFromUtf32(2) + strCommand_ETX_BCC + "\r\n";
            // Insert code for sending data here

            // Reception & TimeOut Check
            //Insert code to handle data receiving within timeout limit here

            // BCC Check
            //strSTX_Command = strReceiveStr.Left(strReceiveStr, (InStr(1, strReceiveStr, Char.ConvertFromUtf32(3)) - 1));
            if (strReceiveStr != string.Empty)
            {
                strSTX_Command = strReceiveStr.Substring(0, strReceiveStr.IndexOf(Char.ConvertFromUtf32(3)) - 1);

                strRcvCommand = strSTX_Command.Substring(0, 2);
                BCC_Append(strRcvCommand);

                if (strReceiveStr != Char.ConvertFromUtf32(2) + strCommand_ETX_BCC + "\r\n")
                    intErrNO = 9;
                else
                    intErrNO = 0;
            }
        }

        private void BCC_Append(string Command)
        {
            Int64 intBCC = 0;
            string strBCC;

            strCommand_ETX = Command + Char.ConvertFromUtf32(3);

            for (int i = 0; i < strCommand_ETX.Length; i++)
            {
                //intBCC = intBCC Xor Asc(Mid(strCommand_ETX, i, 1));
                //intBCC ^= strCommand_ETX.ToCharArray(i, 1)[0];
                intBCC = intBCC ^ strCommand_ETX.Substring(i, 1)[0];
            }

            strBCC = Convert.ToString(intBCC, 16);
            if (strBCC.Length == 1)
                strBCC = 0 + strBCC;

            strCommand_ETX_BCC = strCommand_ETX + strBCC;

            return;
        }

        private void ErrCheck()
        {
            if (strRcvCommand.Substring(8, 1) == "1")
            {
                intErrNO = 11;
                return;
            }
            
            if (intErrNO ==0)
            {
                if (strRcvCommand.Substring(6, 1) == " ")
                    intErrNO = 0;
                else
                    intErrNO = (int)strRcvCommand.Substring(6, 1)[0];
            }

            string strMsgBox = string.Empty;
            switch (intErrNO)
            {
                case 0:
                    return;
                    
                case 1:
                    strMsgBox = "Power of sensor is off";
                    break;
                case 2:
                    strMsgBox = "EE-Prom error";
                    break;
                case 3:
                    strMsgBox = "EE-Prom error";
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    strMsgBox = "Time out";
                    break;
                case 9:
                    strMsgBox = "BBC error";
                    break;
                case 10:
                    break;
                case 11:
                    strMsgBox = "Battery out";
                    break;
            }

            if (strMsgBox != string.Empty)
                System.Windows.Forms.MessageBox.Show(strMsgBox, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);


            return;
        }

        
    }
}


public static class Utils
{
    /// <summary>
    /// Retrieves the left part of the string
    /// </summary>
    /// <param name="str">Original string</param>
    /// <param name="length">Number of characters to retrieve starting from the left</param>
    /// <returns>The left substring</returns>
    public static string Left(this string str, int length)
    {
        return str.Substring(0, Math.Min(length, str.Length));
    }
}
