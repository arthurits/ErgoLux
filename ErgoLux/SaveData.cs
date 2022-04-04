namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Saves data into an elux-formatted file.
    /// </summary>
    /// <param name="FileName">Path (including name) of the elux file</param>
    private void SaveELuxData(string FileName)
    {
        try
        {
            using var fs = File.Open(FileName, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
            using var sw = new StreamWriter(fs, System.Text.Encoding.UTF8, leaveOpen: false);

            // Append millisecond pattern to current culture's full date time pattern
            //string fullPattern = System.Globalization.DateTimeFormatInfo.CurrentInfo.FullDateTimePattern;
            string fullPattern = _settings.AppCulture.DateTimeFormat.FullDateTimePattern;
            fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", _settings.MillisecondsFormat);
            TimeSpan nTime = _timeEnd - _timeStart;

            // Save the header text into the file
            sw.WriteLine($"{(StringsRM.GetString("strFileHeader01", _settings.AppCulture) ?? "ErgoLux data")} ({_settings.AppCultureName})");
            sw.WriteLine($"{(StringsRM.GetString("strFileHeader02", _settings.AppCulture) ?? "Start time")}: {_timeStart.ToString(fullPattern, _settings.AppCulture)}");
            sw.WriteLine($"{(StringsRM.GetString("strFileHeader03", _settings.AppCulture) ?? "End time")}: {_timeEnd.ToString(fullPattern, _settings.AppCulture)}");
            //sw.WriteLine($"{(StringsRM.GetString("strFileHeader04", _sett.AppCulture) ?? "Total measuring time")}: {nTime.Days} days, {nTime.Hours} hours, {nTime.Minutes} minutes, {nTime.Seconds} seconds, and {nTime.Milliseconds} millisecons");
            sw.WriteLine($"{(StringsRM.GetString("strFileHeader04", _settings.AppCulture) ?? "Total measuring time")}: " +
                $"{nTime.Days} {(StringsRM.GetString("strFileHeader19", _settings.AppCulture) ?? "days")}, " +
                $"{nTime.Hours} {(StringsRM.GetString("strFileHeader20", _settings.AppCulture) ?? "hours")}, " +
                $"{nTime.Minutes} {(StringsRM.GetString("strFileHeader21", _settings.AppCulture) ?? "minutes")}, " +
                $"{nTime.Seconds} {(StringsRM.GetString("strFileHeader22", _settings.AppCulture) ?? "seconds")} " +
                $"{(StringsRM.GetString("strFileHeader23", _settings.AppCulture) ?? "and")} " +
                $"{nTime.Milliseconds} {(StringsRM.GetString("strFileHeader24", _settings.AppCulture) ?? "milliseconds")}");
            sw.WriteLine($"{(StringsRM.GetString("strFileHeader05", _settings.AppCulture) ?? "Number of sensors")}: {_settings.T10_NumberOfSensors}");
            sw.WriteLine($"{(StringsRM.GetString("strFileHeader06", _settings.AppCulture) ?? "Number of data points")}: {_nPoints}");
            sw.WriteLine($"{(StringsRM.GetString("strFileHeader07", _settings.AppCulture) ?? "Sampling frequency")}: {_settings.T10_Frequency.ToString(_settings.AppCulture)}");
            sw.WriteLine();
            string content = string.Empty;
            for (int i = 0; i < _settings.T10_NumberOfSensors; i++)
                content += $"{(StringsRM.GetString("strFileHeader08", _settings.AppCulture) ?? "Sensor #")}{i:00}\t";
            content += $"{(StringsRM.GetString("strFileHeader09", _settings.AppCulture) ?? "Maximum")}\t" +
                    $"{(StringsRM.GetString("strFileHeader10", _settings.AppCulture) ?? "Average")}\t" +
                    $"{(StringsRM.GetString("strFileHeader11", _settings.AppCulture) ?? "Minimum")}\t" +
                    $"{(StringsRM.GetString("strFileHeader12", _settings.AppCulture) ?? "Min/Average")}\t" +
                    $"{(StringsRM.GetString("strFileHeader13", _settings.AppCulture) ?? "Min/Max")}\t" +
                    $"{(StringsRM.GetString("strFileHeader14", _settings.AppCulture) ?? "Average/Max")}";
            sw.WriteLine(content);

            // Save the numerical values
            for (int j = 0; j < _nPoints; j++)
            {
                content = string.Empty;
                for (int i = 0; i < _plotData.Length; i++)
                {
                    content += _plotData[i][j].ToString(_settings.DataFormat, _settings.AppCulture) + "\t";
                }
                //trying to write data to csv
                sw.WriteLine(content.TrimEnd('\t'));
            }

            // Show OK save data
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(StringsRM.GetString("strMsgBoxSaveData", _settings.AppCulture) ?? "Data has been successfully saved to disk.",
                    StringsRM.GetString("strMsgBoxSaveDataTitle", _settings.AppCulture) ?? "Data saving",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            // Show error message
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(String.Format(StringsRM.GetString("strMsgBoxErrorSaveData", _settings.AppCulture) ?? "An unexpected error happened while saving data to disk.\nPlease try again later or contact the software engineer." + Environment.NewLine + "{0}", ex.Message),
                    StringsRM.GetString("strMsgBoxErrorSaveDataTitle", _settings.AppCulture) ?? "Error saving data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Saves data into a text-formatted file.
    /// </summary>
    /// <param name="FileName">Path (including name) of the text file</param>
    private void SaveTextData (string FileName)
    {
        SaveELuxData(FileName);
    }

    /// <summary>
    /// Saves data into a binary-formatted file.
    /// </summary>
    /// <param name="FileName">Path (including name) of the binary file</param>
    private void SaveBinaryData (string FileName)
    {
        try
        {
            using var fs = File.Open(FileName, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
            using var bw = new BinaryWriter(fs, System.Text.Encoding.UTF8, false);

            // Append millisecond pattern to current culture's full date time pattern
            //string fullPattern = System.Globalization.DateTimeFormatInfo.CurrentInfo.FullDateTimePattern;
            //fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", _sett.MillisecondsFormat);
            TimeSpan nTime = _timeEnd - _timeStart;

            // Save the header text into the file
            bw.Write($"{(StringsRM.GetString("strFileHeader01", _settings.AppCulture) ?? "ErgoLux data")} ({_settings.AppCultureName})");
            bw.Write(_timeStart);
            bw.Write(_timeEnd);
            bw.Write(nTime.Days);
            bw.Write(nTime.Hours);
            bw.Write(nTime.Minutes);
            bw.Write(nTime.Seconds);
            bw.Write(nTime.Milliseconds);
            bw.Write(_settings.T10_NumberOfSensors);
            bw.Write(_nPoints);
            bw.Write(_settings.T10_Frequency);
            string content = string.Empty;
            for (int i = 0; i < _settings.T10_NumberOfSensors; i++)
                content += $"{(StringsRM.GetString("strFileHeader08", _settings.AppCulture) ?? "Sensor #")}{i:00}\t";
            content += $"{(StringsRM.GetString("strFileHeader09", _settings.AppCulture) ?? "Maximum")}\t" +
                    $"{(StringsRM.GetString("strFileHeader10", _settings.AppCulture) ?? "Average")}\t" +
                    $"{(StringsRM.GetString("strFileHeader11", _settings.AppCulture) ?? "Minimum")}\t" +
                    $"{(StringsRM.GetString("strFileHeader12", _settings.AppCulture) ?? "Min/Average")}\t" +
                    $"{(StringsRM.GetString("strFileHeader13", _settings.AppCulture) ?? "Min/Max")}\t" +
                    $"{(StringsRM.GetString("strFileHeader14", _settings.AppCulture) ?? "Average/Max")}\t";
            bw.WriteLine(content);

            // https://stackoverflow.com/questions/6952923/conversion-double-array-to-byte-array
            byte[] bytesLine;
            for (int i = 0; i < _plotData.Length; i++)
            {
                // bw.Write(_plotData[i].SelectMany(value => BitConverter.GetBytes(value)).ToArray()); // Requires LINQ
                bytesLine = new byte[_plotData[i].Length * sizeof(double)];
                Buffer.BlockCopy(_plotData[i], 0, bytesLine, 0, bytesLine.Length);
                bw.Write(bytesLine);
            }

            // Show OK save data
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(StringsRM.GetString("strMsgBoxSaveData", _settings.AppCulture) ?? "Data has been successfully saved to disk.",
                    StringsRM.GetString("strMsgBoxSaveDataTitle", _settings.AppCulture) ?? "Data saving",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            // Show error message
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(String.Format(StringsRM.GetString("strMsgBoxErrorSaveData", _settings.AppCulture) ?? "An unexpected error happened while saving data to disk.\nPlease try again later or contact the software engineer." + Environment.NewLine + "{0}", ex.Message),
                    StringsRM.GetString("strMsgBoxErrorSaveDataTitle", _settings.AppCulture) ?? "Error saving data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Saves data. Default behaviour
    /// </summary>
    /// <param name="FileName">Path (including name) of the elux file</param>
    private void SaveDefaultData(string FileName)
    {
        SaveELuxData(FileName);
    }

}

