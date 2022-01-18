using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Opens an elux-formatted illuminance data file.
    /// </summary>
    /// <param name="FileName">Path (including name) of the elux file</param>
    private void OpenELuxData(string FileName)
    {
        var cursor = Cursor.Current;
        Cursor.Current = Cursors.WaitCursor;

        try
        {
            // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader
            using var fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            int nSensors = 0, nPoints = 0;
            double nFreq = 0.0;

            string strLine = sr.ReadLine();
            if (strLine != "ErgoLux data")
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            // Better implement a try parse block. Each read line should throw an exception instead of "return"
            strLine = sr.ReadLine();
            if (!strLine.Contains("Start time: ", StringComparison.Ordinal))
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            strLine = sr.ReadLine();
            if (!strLine.Contains("End time: ", StringComparison.Ordinal))
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            strLine = sr.ReadLine();
            if (!strLine.Contains("Total measuring time: ", StringComparison.Ordinal))
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            strLine = sr.ReadLine();
            if (!strLine.Contains("Number of sensors: ", StringComparison.Ordinal))
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nSensors)) return;
            if (nSensors == 0) return;
            _sett.T10_NumberOfSensors = nSensors;

            strLine = sr.ReadLine();
            if (!strLine.Contains("Number of data points: ", StringComparison.Ordinal))
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nPoints)) return;
            if (nPoints == 0) return;
            _sett.Plot_ArrayPoints = nPoints;

            strLine = sr.ReadLine();
            if (!strLine.Contains("Sampling frequency: ", StringComparison.Ordinal))
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("Unable to read data from file:\nwrong file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            if (!double.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nFreq)) return;
            _sett.T10_Frequency = nFreq;

            strLine = sr.ReadLine();    // It should be an empty line
            if (strLine != string.Empty) return;
            strLine = sr.ReadLine();    // Column header lines

            // Initialize data arrays
            InitializeArrays();

            // Read data into _plotData
            for (int i = 0; i < _plotData.Length; i++)
            {
                _plotData[i] = new double[_sett.Plot_ArrayPoints];
            }
            string[] data;
            int row = 0, col = 0;
            while ((strLine = sr.ReadLine()) != null)
            {
                data = strLine.Split("\t");
                for (row = 0; row < data.Length; row++)
                {
                    double.TryParse(data[row], out _plotData[row][col]);
                }
                col++;
            }

        }
        catch
        {
            // Show error message
            using (new CenterWinDialog(this))
            {
                MessageBox.Show("An unexpected error happened while opening data file.\nPlease try again later or contact the software engineer.", "Error opening data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        finally
        {
            Cursor.Current = cursor;
        }
    }

    private void OpenTextData(string FileName)
    {

    }

    private void OpenBinaryData(string FileName)
    {

    }
}

