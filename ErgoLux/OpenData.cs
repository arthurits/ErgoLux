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
        string? strLine;

        try
        {
            // https://stackoverflow.com/questions/897796/how-do-i-open-an-already-opened-file-with-a-net-streamreader
            using var fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs, System.Text.Encoding.UTF8);

            strLine = sr.ReadLine();    // ErgoLux data
            if (strLine is null)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader01));
            System.Globalization.CultureInfo fileCulture = new(strLine[(strLine.IndexOf("(") + 1)..^1]);
            if (!strLine.Contains($"{StringResources.GetString("strFileHeader01", fileCulture) ?? "ErgoLux data"} (", StringComparison.Ordinal))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader01));

            strLine = sr.ReadLine();    // Start time
            if (strLine is null)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader02));
            if (!strLine.Contains($"{StringResources.GetString("strFileHeader02", fileCulture) ?? "Start time"}: ", StringComparison.Ordinal))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader02));
            string fullPattern = fileCulture.DateTimeFormat.FullDateTimePattern;
            fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", _settings.GetMillisecondsFormat(fileCulture));
            if (!DateTime.TryParseExact(strLine[(strLine.IndexOf(":") + 2)..], fullPattern, fileCulture, System.Globalization.DateTimeStyles.None, out _timeStart))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader02));

            strLine = sr.ReadLine();    // End time
            if (strLine is null)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader03));
            if (!strLine.Contains($"{StringResources.GetString("strFileHeader03", fileCulture) ?? "End time"}: ", StringComparison.Ordinal))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader03));
            if (!DateTime.TryParseExact(strLine[(strLine.IndexOf(":") + 2)..], fullPattern, fileCulture, System.Globalization.DateTimeStyles.None, out _timeEnd))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader03));

            strLine = sr.ReadLine();    // Total measuring time
            if (strLine is null)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringsRM.GetString("strFileHeader04", _settings.AppCulture) ?? "Total measuring time"));
            if (!strLine.Contains($"{StringResources.GetString("strFileHeader04", fileCulture) ?? "Total measuring time"}: ", StringComparison.InvariantCulture))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringsRM.GetString("strFileHeader04", _settings.AppCulture) ?? "Total measuring time"));

            strLine = sr.ReadLine();    // Number of sensors
            if (strLine is null)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader05));
            if (!strLine.Contains($"{StringResources.GetString("strFileHeader05", fileCulture) ?? "Number of sensors"}: ", StringComparison.Ordinal))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader05));
            if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nSensors))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader05));
            if (nSensors == 0)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader05));
            _settings.T10_NumberOfSensors = nSensors;

            strLine = sr.ReadLine();    // Number of data points
            if (strLine is null)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader06));
            if (!strLine.Contains($"{StringResources.GetString("strFileHeader06", fileCulture) ?? "Number of data points"}: ", StringComparison.Ordinal))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader06));
            if (!int.TryParse(strLine[(strLine.IndexOf(":") + 1)..], out nPoints))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader06));
            if (nPoints == 0)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader06));
            _settings.Plot_ArrayPoints = nPoints;

            strLine = sr.ReadLine();    // Sampling frequency
            if (strLine is null)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader07));
            if (!strLine.Contains($"{StringResources.GetString("strFileHeader07", fileCulture) ?? "Sampling frequency"}: ", StringComparison.Ordinal))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader07));
            if (!double.TryParse(strLine[(strLine.IndexOf(":") + 1)..], System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands, fileCulture, out nFreq))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader07));
            if (nFreq <= 0)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader07));
            _settings.T10_Frequency = nFreq;

            strLine = sr.ReadLine();    // Empty line
            if (strLine is null)
                throw new FormatException(StringResources.FileHeader17);
            if (strLine != string.Empty)
                throw new FormatException(StringResources.FileHeader17);

            strLine = sr.ReadLine();    // Column header names
            if (strLine is null)
                throw new FormatException(StringResources.FileHeader18);
            _seriesLabels = strLine.Split('\t');
            if (_seriesLabels == Array.Empty<string>())
                throw new FormatException(StringResources.FileHeader18);

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
                MessageBox.Show(string.Format(StringResources.ReadDataErrorCulture, ex.Message),
                    StringResources.ReadDataErrorCultureTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        catch (FormatException ex)
        {
            result = false;
            using (new CenterWinDialog(this))
                MessageBox.Show(string.Format(StringResources.ReadDataError, ex.Message),
                    StringResources.ReadDataErrorTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        catch (ArithmeticException ex)
        {
            result = false;
            using (new CenterWinDialog(this))
                MessageBox.Show(string.Format(StringResources.ReadDataErrorNumber, ex.Message),
                    StringResources.ReadDataErrorNumberTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            result = false;
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(string.Format(StringResources.MsgBoxErrorOpenData, ex.Message),
                    StringResources.MsgBoxErrorOpenDataTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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

        try
        {
            using var fs = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var br = new BinaryReader(fs, System.Text.Encoding.UTF8);

            string strLine = br.ReadString();   // ErgoLux data
            if (strLine is null)
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader01));
            if (!strLine.Contains($"{StringResources.FileHeader01} (", StringComparison.Ordinal))
                throw new FormatException(string.Format(StringResources.FileHeaderSection, StringResources.FileHeader01));

            _timeStart = br.ReadDateTime();
            _timeEnd = br.ReadDateTime();
            int dummy = br.ReadInt32();     // days
            dummy = br.ReadInt32();         // hours
            dummy = br.ReadInt32();         // minutes
            dummy = br.ReadInt32();         // seconds
            dummy = br.ReadInt32();         // milliseconds
            _settings.T10_NumberOfSensors = br.ReadInt32();
            _settings.Plot_ArrayPoints = br.ReadInt32();
            _nPoints = _settings.Plot_ArrayPoints;
            _settings.T10_Frequency = br.ReadDouble();
            strLine = br.ReadString();      // column header names
            if (strLine is null)
                throw new FormatException(StringResources.FileHeader18);
            _seriesLabels = strLine.Split('\t');
            if (_seriesLabels == Array.Empty<string>())
                throw new FormatException(StringResources.FileHeader18);

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
                MessageBox.Show(string.Format(StringResources.ReadDataErrorCulture, ex.Message),
                    StringResources.ReadDataErrorCultureTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            result = false;
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(string.Format(StringResources.MsgBoxErrorOpenData, ex.Message),
                    StringResources.MsgBoxErrorOpenDataTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        return result;
    }

}
