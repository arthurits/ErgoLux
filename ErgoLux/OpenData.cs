namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Opens an elux-formatted illuminance data file.
    /// </summary>
    /// <param name="FileName">Path (including name) of the elux file</param>
    /// <returns><see langword="True"/> if successful, <see langword="false"/> otherwise</returns>
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
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader01", _sett.AppCulture) ?? "ErgoLux data"));
            if (!strLine.Contains($"{StringsRM.GetString("strFileHeader01", _sett.AppCulture) ?? "ErgoLux data"} (", StringComparison.Ordinal))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader01", _sett.AppCulture) ?? "ErgoLux data"));
            System.Globalization.CultureInfo fileCulture = new (strLine[(strLine.IndexOf("(") + 1)..^1]);

            strLine = sr.ReadLine();    // Start time
            if (strLine is null)
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader02", _sett.AppCulture) ?? "Start time"));
            if (!strLine.Contains($"{StringsRM.GetString("strFileHeader02", _sett.AppCulture) ?? "Start time"}: ", StringComparison.Ordinal))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader02", _sett.AppCulture) ?? "Start time"));
            string fullPattern = fileCulture.DateTimeFormat.FullDateTimePattern;
            fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", _sett.GetMillisecondsFormat(fileCulture));
            if (!DateTime.TryParseExact(strLine[(strLine.IndexOf(":") + 2)..], fullPattern, fileCulture, System.Globalization.DateTimeStyles.None, out _timeStart))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader02", _sett.AppCulture) ?? "Start time"));

            strLine = sr.ReadLine();    // End time
            if (strLine is null)
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader03", _sett.AppCulture) ?? "End time"));
            if (!strLine.Contains($"{StringsRM.GetString("strFileHeader03", _sett.AppCulture) ?? "End time"}: ", StringComparison.Ordinal))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader03", _sett.AppCulture) ?? "End time"));
            if (!DateTime.TryParseExact(strLine[(strLine.IndexOf(":") + 2)..], fullPattern, fileCulture, System.Globalization.DateTimeStyles.None, out _timeEnd))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader03", _sett.AppCulture) ?? "End time"));

            strLine = sr.ReadLine();    // Total measuring time
            if (strLine is null)
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader04", _sett.AppCulture) ?? "Total measuring time"));
            if (!strLine.Contains($"{StringsRM.GetString("strFileHeader04", _sett.AppCulture) ?? "Total measuring time"}: ", StringComparison.InvariantCulture))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader04", _sett.AppCulture) ?? "Total measuring time"));

            strLine = sr.ReadLine();    // Number of sensors
            if (strLine is null)
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader05", _sett.AppCulture) ?? "Number of sensors"));
            if (!strLine.Contains($"{StringsRM.GetString("strFileHeader05", _sett.AppCulture) ?? "Number of sensors"}: ", StringComparison.Ordinal))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader05", _sett.AppCulture) ?? "Number of sensors"));
            if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nSensors))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader05", _sett.AppCulture) ?? "Number of sensors"));
            if (nSensors == 0)
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader05", _sett.AppCulture) ?? "Number of sensors"));
            _sett.T10_NumberOfSensors = nSensors;

            strLine = sr.ReadLine();    // Number of data points
            if (strLine is null)
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader06", _sett.AppCulture) ?? "Number of data points"));
            if (!strLine.Contains($"{StringsRM.GetString("strFileHeader06", _sett.AppCulture) ?? "Number of data points"}: ", StringComparison.Ordinal))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader06", _sett.AppCulture) ?? "Number of data points"));
            if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nPoints))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader06", _sett.AppCulture) ?? "Number of data points"));
            if (nPoints == 0)
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader06", _sett.AppCulture) ?? "Number of data points"));
            _sett.Plot_ArrayPoints = nPoints;

            strLine = sr.ReadLine();    // Sampling frequency
            if (strLine is null)
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader07", _sett.AppCulture) ?? "Sampling frequency"));
            if (!strLine.Contains($"{StringsRM.GetString("strFileHeader07", _sett.AppCulture) ?? "Sampling frequency"}: ", StringComparison.Ordinal))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader07", _sett.AppCulture) ?? "Sampling frequency"));
            if (!double.TryParse(strLine[(strLine.IndexOf(":") + 1)..], System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands, fileCulture, out nFreq))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader07", _sett.AppCulture) ?? "Sampling frequency"));
            if (nFreq <= 0)
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader07", _sett.AppCulture) ?? "Sampling frequency"));
            _sett.T10_Frequency = nFreq;

            strLine = sr.ReadLine();    // Empty line
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strFileHeader17", _sett.AppCulture) ?? "Missing an empty line.");
            if (strLine != string.Empty)
                throw new FormatException(StringsRM.GetString("strFileHeader17", _sett.AppCulture) ?? "Missing an empty line.");

            strLine = sr.ReadLine();    // Column header names
            if (strLine is null)
                throw new FormatException(StringsRM.GetString("strFileHeader18", _sett.AppCulture) ?? "Missing column headers (series names).");
            _seriesLabels = strLine.Split('\t');
            if (_seriesLabels == Array.Empty<string>())
                throw new FormatException(StringsRM.GetString("strFileHeader18", _sett.AppCulture) ?? "Missing column headers (series names).");

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
                    if (!double.TryParse(data[row], System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands, fileCulture, out _plotData[row][col]))
                        throw new ArithmeticException(data[row].ToString(fileCulture));
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
                MessageBox.Show(String.Format(StringsRM.GetString("strMsgBoxErrorOpenData", _sett.AppCulture) ?? "An unexpected error happened while opening data file.\nPlease try again later or contact the software engineer." + Environment.NewLine + "{0}", ex.Message),
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
    /// <returns><see langword="True"/> if successful, <see langword="false"/> otherwise</returns>
    private bool OpenTextData(string FileName)
    {
        return OpenELuxData(FileName);
    }

    /// <summary>
    /// Opens a binary-formatted illuminance data file
    /// </summary>
    /// <param name="FileName">Path (including name) of the elux file</param>
    /// <returns><see langword="True"/> if successful, <see langword="false"/> otherwise</returns>
    private bool OpenBinaryData(string FileName)
    {
        bool result = true;
        var cursor = Cursor.Current;
        Cursor.Current = Cursors.WaitCursor;

        try
        {
            using var fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var br = new BinaryReader(fs, System.Text.Encoding.UTF8);

            string strLine = br.ReadString();   // ErgoLux data
            if (strLine is null)
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader01", _sett.AppCulture) ?? "ErgoLux data"));
            if (!strLine.Contains($"{StringsRM.GetString("strFileHeader01", _sett.AppCulture) ?? "ErgoLux data"} (", StringComparison.Ordinal))
                throw new FormatException(String.Format(StringsRM.GetString("strFileHeaderSection", _sett.AppCulture) ?? "Section '{0}' is mis-formatted.", StringsRM.GetString("strFileHeader01", _sett.AppCulture) ?? "ErgoLux data"));

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
                throw new FormatException(StringsRM.GetString("strFileHeader18", _sett.AppCulture) ?? "Missing column headers (series names).");
            _seriesLabels = strLine.Split('\t');
            if (_seriesLabels == Array.Empty<string>())
                throw new FormatException(StringsRM.GetString("strFileHeader18", _sett.AppCulture) ?? "Missing column headers (series names).");

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
                MessageBox.Show(String.Format(StringsRM.GetString("strMsgBoxErrorOpenData", _sett.AppCulture) ?? "An unexpected error happened while opening data file.\nPlease try again later or contact the software engineer." + Environment.NewLine + "{0}", ex.Message),
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

}

