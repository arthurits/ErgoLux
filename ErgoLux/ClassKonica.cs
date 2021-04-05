using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcancesMesa
{
    class Konica
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


        private void StartMeasurement()
        {
            Int32 rng;
            intErrflg = 0;

            // Step 2 PC MODE
            strSndCommand = "00541 ";
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
            strSTX_Command = strReceiveStr.Left(strReceiveStr, (InStr(1, strReceiveStr, Char.ConvertFromUtf32(3)) - 1));

        }

        private void BCC_Append(string Command)
        {
            Int64 intBCC;
            string strBCC;

            strCommand_ETX = Command + Char.ConvertFromUtf32(3);

            intBCC = 0;
            for (int i = 1; i <= strCommand_ETX.Length; i++)
            {
                //intBCC = intBCC Xor Asc(Mid(strCommand_ETX, i, 1));
            }

            return;
        }

        private void ErrCheck()
        {
            return;
        }

        
    }
}


public static class Utils
{
    public static string Left(this string str, int length)
    {
        return str.Substring(0, Math.Min(length, str.Length));
    }
}
