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
    private bool OpenELuxData(string FileName)
    {
        // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader
        using var fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs, Encoding.UTF8);

        var cursor = Cursor.Current;
        Cursor.Current = Cursors.WaitCursor;

        bool result = ReadData(sr);

        Cursor.Current = cursor;

        return result;
    }

    private void OpenTextData(string FileName)
    {

    }

    private bool OpenBinaryData(string FileName)
    {
        using var fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var br = new BinaryReader(fs, Encoding.UTF8);

        bool result = ReadData(br);

        return result;
    }

    /// <summary>
    /// Reads the numeric data section pointed at.
    /// </summary>
    /// <param name="sr">This reader should be pointing to the beginning of the numeric data section</param>
    private bool ReadData(object genericReader)
    {
        bool result = true;
        int nSensors = 0, nPoints = 0;
        double nFreq = 0.0;
        string? strLine;
        
        var readerType = genericReader.GetType();
        System.Reflection.MethodInfo? reader = null;

        if (readerType == typeof(StreamReader))
            reader = readerType.GetMethod("ReadLine");
        else if (readerType == typeof(BinaryReader))
            reader = typeof(BinaryIOExtensions).GetMethod("ReadLine");

        if (reader is null) return false;

        try
        {
            //using var fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //using var br = new BinaryReader(fs, Encoding.UTF8);

            strLine = (string)reader.Invoke(genericReader, null);    // ErgoLux data
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader01", _sett.AppCulture));
            if (!strLine.Contains("ErgoLux data", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader01", _sett.AppCulture));

            strLine = (string)reader.Invoke(genericReader, null);    // Start time
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader02", _sett.AppCulture));
            if (!strLine.Contains("Start time: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader02", _sett.AppCulture));
            string fullPattern = _sett.AppCulture.DateTimeFormat.FullDateTimePattern;
            fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", _sett.MillisecondsFormat);
            if (!DateTime.TryParseExact(strLine[(strLine.IndexOf(":") + 2)..], fullPattern, _sett.AppCulture, System.Globalization.DateTimeStyles.None, out _timeStart))
                throw new FormatException(StringsRM.GetString("strELuxHeader02", _sett.AppCulture));

            strLine = (string)reader.Invoke(genericReader, null);    // End time
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader03", _sett.AppCulture));
            if (!strLine.Contains("End time: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader03", _sett.AppCulture));
            if (!DateTime.TryParseExact(strLine[(strLine.IndexOf(":") + 2)..], fullPattern, _sett.AppCulture, System.Globalization.DateTimeStyles.None, out _timeEnd))
                throw new FormatException(StringsRM.GetString("strELuxHeader03", _sett.AppCulture));

            strLine = (string)reader.Invoke(genericReader, null);    // Total measuring time
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader04", _sett.AppCulture));
            if (!strLine.Contains("Total measuring time: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader04", _sett.AppCulture));

            strLine = (string)reader.Invoke(genericReader, null);    // Number of sensors
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader05", _sett.AppCulture));
            if (!strLine.Contains("Number of sensors: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader05", _sett.AppCulture));
            if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nSensors))
                throw new FormatException(StringsRM.GetString("strELuxHeader05", _sett.AppCulture));
            if (nSensors == 0)
                throw new FormatException(StringsRM.GetString("strELuxHeader05", _sett.AppCulture));
            _sett.T10_NumberOfSensors = nSensors;

            strLine = (string)reader.Invoke(genericReader, null);    // Number of data points
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader06", _sett.AppCulture));
            if (!strLine.Contains("Number of data points: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader06", _sett.AppCulture));
            if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nPoints))
                throw new FormatException(StringsRM.GetString("strELuxHeader06", _sett.AppCulture));
            if (nPoints == 0)
                throw new FormatException(StringsRM.GetString("strELuxHeader06", _sett.AppCulture));
            _sett.Plot_ArrayPoints = nPoints;

            strLine = (string)reader.Invoke(genericReader, null);    // Sampling frequency
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader07", _sett.AppCulture));
            if (!strLine.Contains("Sampling frequency: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader07", _sett.AppCulture));
            if (!double.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nFreq))
                throw new FormatException(StringsRM.GetString("strELuxHeader07", _sett.AppCulture));
            if (nFreq <= 0)
                throw new FormatException(StringsRM.GetString("strELuxHeader07", _sett.AppCulture));
            _sett.T10_Frequency = nFreq;

            strLine = (string)reader.Invoke(genericReader, null);    // Empty line
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader08", _sett.AppCulture));
            if (strLine != string.Empty)
                throw new FormatException(StringsRM.GetString("strELuxHeader08", _sett.AppCulture));

            strLine = (string)reader.Invoke(genericReader, null);    // Column header lines
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader09", _sett.AppCulture));
            //seriesLabels = strLine.Split('\t');
            //if (seriesLabels == Array.Empty<string>())
            //    throw new FormatException(StringsRM.GetString("strELuxHeader09", _sett.AppCulture));

            // Initialize data arrays
            InitializeArrays();

            // Read data into _plotData
            for (int i = 0; i < _plotData.Length; i++)
            {
                _plotData[i] = new double[_sett.Plot_ArrayPoints];
            }
            string[] data;
            int row = 0, col = 0;
            while ((strLine = (string)reader.Invoke(genericReader, null)) is not null)
            {
                data = strLine.Split("\t");
                for (row = 0; row < data.Length; row++)
                {
                    if (!double.TryParse(data[row], out _plotData[row][col]))
                        throw new ArithmeticException(data[row].ToString());
                }
                col++;
            }
        }
        catch (FormatException ex)
        {
            result = false;
            using (new CenterWinDialog(this))
                MessageBox.Show(String.Format(StringsRM.GetString("strReadDataError", _sett.AppCulture) ?? "Unable to read data from file." + Environment.NewLine + "{0}", ex.Message),
                    StringsRM.GetString("strReadDataErrorTitle" ?? "Error opening data", _sett.AppCulture),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        catch (ArithmeticException ex)
        {
            result = false;
            using (new CenterWinDialog(this))
                MessageBox.Show(String.Format(StringsRM.GetString("strReadDataErrorNumber", _sett.AppCulture) ?? "Invalid numeric value: {0}", ex.Message),
                    StringsRM.GetString("strReadDataErrorNumberTitle", _sett.AppCulture) ?? "Error parsing data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            result = false;
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(String.Format(StringsRM.GetString("strMsgBoxReadArray", _sett.AppCulture) ?? "Unexpected error in 'ReadData' function." + Environment.NewLine + "{0}", ex.Message),
                    StringsRM.GetString("strMsgBoxReadArrayTitle", _sett.AppCulture) ?? "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        return result;

    }
}

