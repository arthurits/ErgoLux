namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Opens an elux-formatted illuminance data file.
    /// </summary>
    /// <param name="FileName">Path (including name) of the elux file</param>
    /// <returns><see langword="True"/> if successfull, <see langword="false"/> otherwise</returns>
    private bool OpenELuxData(string FileName)
    {
        bool result = true;
        int nSensors = 0, nPoints = 0;
        double nFreq = 0.0;
        string strLine;

        var cursor = Cursor.Current;
        Cursor.Current = Cursors.WaitCursor;

        try
        {
            // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader
            using var fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs, System.Text.Encoding.UTF8);

            strLine = sr.ReadLine();    // ErgoLux data
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader01", _sett.AppCulture));
            if (!strLine.Contains("ErgoLux data (", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader01", _sett.AppCulture));
            System.Globalization.CultureInfo fileCulture = new (strLine[(strLine.IndexOf("(") + 1)..^1]);

            strLine = sr.ReadLine();    // Start time
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader02", _sett.AppCulture));
            if (!strLine.Contains("Start time: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader02", _sett.AppCulture));
            string fullPattern = _sett.AppCulture.DateTimeFormat.FullDateTimePattern;
            fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", _sett.GetMillisecondsFormat(fileCulture));
            if (!DateTime.TryParseExact(strLine[(strLine.IndexOf(":") + 2)..], fullPattern, fileCulture, System.Globalization.DateTimeStyles.None, out _timeStart))
                throw new FormatException(StringsRM.GetString("strELuxHeader02", _sett.AppCulture));

            strLine = sr.ReadLine();    // End time
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader03", _sett.AppCulture));
            if (!strLine.Contains("End time: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader03", _sett.AppCulture));
            if (!DateTime.TryParseExact(strLine[(strLine.IndexOf(":") + 2)..], fullPattern, fileCulture, System.Globalization.DateTimeStyles.None, out _timeEnd))
                throw new FormatException(StringsRM.GetString("strELuxHeader03", _sett.AppCulture));

            strLine = sr.ReadLine();    // Total measuring time
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader04", _sett.AppCulture));
            if (!strLine.Contains("Total measuring time: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader04", _sett.AppCulture));

            strLine = sr.ReadLine();    // Number of sensors
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader05", _sett.AppCulture));
            if (!strLine.Contains("Number of sensors: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader05", _sett.AppCulture));
            if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nSensors))
                throw new FormatException(StringsRM.GetString("strELuxHeader05", _sett.AppCulture));
            if (nSensors == 0)
                throw new FormatException(StringsRM.GetString("strELuxHeader05", _sett.AppCulture));
            _sett.T10_NumberOfSensors = nSensors;

            strLine = sr.ReadLine();    // Number of data points
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader06", _sett.AppCulture));
            if (!strLine.Contains("Number of data points: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader06", _sett.AppCulture));
            if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nPoints))
                throw new FormatException(StringsRM.GetString("strELuxHeader06", _sett.AppCulture));
            if (nPoints == 0)
                throw new FormatException(StringsRM.GetString("strELuxHeader06", _sett.AppCulture));
            _sett.Plot_ArrayPoints = nPoints;

            strLine = sr.ReadLine();    // Sampling frequency
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader07", _sett.AppCulture));
            if (!strLine.Contains("Sampling frequency: ", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader07", _sett.AppCulture));
            if (!double.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nFreq))
                throw new FormatException(StringsRM.GetString("strELuxHeader07", _sett.AppCulture));
            if (nFreq <= 0)
                throw new FormatException(StringsRM.GetString("strELuxHeader07", _sett.AppCulture));
            _sett.T10_Frequency = nFreq;

            strLine = sr.ReadLine();    // Empty line
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader08", _sett.AppCulture));
            if (strLine != string.Empty)
                throw new FormatException(StringsRM.GetString("strELuxHeader08", _sett.AppCulture));

            strLine = sr.ReadLine();    // Column header names
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader09", _sett.AppCulture));
            string[] seriesLabels = strLine.Split('\t');
            if (seriesLabels == Array.Empty<string>())
                throw new FormatException(StringsRM.GetString("strELuxHeader09", _sett.AppCulture));

            // Initialize data arrays
            InitializeArrays();

            // Read data into _plotData
            string[] data;
            int row = 0, col = 0;
            while ((strLine = sr.ReadLine()) is not null)
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
        catch (System.Globalization.CultureNotFoundException ex)
        {
            result = false;
            using (new CenterWinDialog(this))
                MessageBox.Show(String.Format(StringsRM.GetString("strReadDataErrorCulture", _sett.AppCulture) ?? "The culture identifier string name is not valid.\n{0}", ex.Message),
                    StringsRM.GetString("strReadDataErrorCultureTitle" ?? "Culture name error", _sett.AppCulture),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        catch (FormatException ex)
        {
            result = false;
            using (new CenterWinDialog(this))
                MessageBox.Show(String.Format(StringsRM.GetString("strReadDataError", _sett.AppCulture) ?? "Unable to read data from file.\n{0}", ex.Message),
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
                MessageBox.Show(String.Format(StringsRM.GetString("strMsgBoxErrorOpenData", _sett.AppCulture) ?? "An unexpected error happened while saving data to disk.\nPlease try again later or contact the software engineer." + Environment.NewLine + "{0}", ex.Message),
                    StringsRM.GetString("strMsgBoxErrorOpenDataTitle", _sett.AppCulture) ?? "Error opening data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        finally
        {
            Cursor.Current = cursor;
        }

        return result;
    }

    /// <summary>
    /// Opens a text-formatted illuminance data file
    /// </summary>
    /// <param name="FileName">Path (including name) of the elux file</param>
    /// <returns><see langword="True"/> if successfull, <see langword="false"/> otherwise</returns>
    private bool OpenTextData(string FileName)
    {
        return OpenELuxData(FileName);
    }

    /// <summary>
    /// Opens a binary-formatted illuminance data file
    /// </summary>
    /// <param name="FileName">Path (including name) of the elux file</param>
    /// <returns><see langword="True"/> if successfull, <see langword="false"/> otherwise</returns>
    private bool OpenBinaryData(string FileName)
    {
        bool result = true;
        try
        {
            using var fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var br = new BinaryReader(fs, System.Text.Encoding.UTF8);

            string strLine = br.ReadString();   // ErgoLux data
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader01", _sett.AppCulture));
            if (!strLine.Contains("ErgoLux data (", StringComparison.Ordinal))
                throw new FormatException(StringsRM.GetString("strELuxHeader01", _sett.AppCulture));

            _timeStart = br.ReadDateTime();
            _timeEnd = br.ReadDateTime();
            int dummy = br.ReadInt32();     // days
            dummy = br.ReadInt32();         // hours
            dummy = br.ReadInt32();         // minutes
            dummy = br.ReadInt32();         // seconds
            dummy = br.ReadInt32();         // milliseconds
            _sett.T10_NumberOfSensors = br.ReadInt32();
            _sett.Plot_ArrayPoints = br.ReadInt32();
            _nPoints = _sett.Plot_ArrayPoints;
            _sett.T10_Frequency = br.ReadDouble();
            strLine = br.ReadString();      // column header names
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strELuxHeader09", _sett.AppCulture));
            string[] seriesLabels = strLine.Split('\t');
            if (seriesLabels == Array.Empty<string>())
                throw new FormatException(StringsRM.GetString("strELuxHeader09", _sett.AppCulture));

            // Initialize data arrays
            InitializeArrays();

            // Read data into _plotData https://stackoverflow.com/questions/6952923/conversion-double-array-to-byte-array
            byte[] bytesLine;
            for (int i = 0; i < _plotData.Length; i++)
            {
                bytesLine = br.ReadBytes(_plotData[i].Length * sizeof(double));
                Buffer.BlockCopy(bytesLine, 0, _plotData[i], 0, bytesLine.Length);
            }

        }
        catch (System.Globalization.CultureNotFoundException ex)
        {
            result = false;
            using (new CenterWinDialog(this))
                MessageBox.Show(String.Format(StringsRM.GetString("strReadDataErrorCulture", _sett.AppCulture) ?? "The culture identifier string name is not valid.\n{0}", ex.Message),
                    StringsRM.GetString("strReadDataErrorCultureTitle" ?? "Culture name error", _sett.AppCulture),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            result = false;
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(String.Format(StringsRM.GetString("strMsgBoxErrorOpenData", _sett.AppCulture) ?? "An unexpected error happened while saving data to disk.\nPlease try again later or contact the software engineer." + Environment.NewLine + "{0}", ex.Message),
                    StringsRM.GetString("strMsgBoxErrorOpenDataTitle", _sett.AppCulture) ?? "Error opening data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        return result;
    }

}

