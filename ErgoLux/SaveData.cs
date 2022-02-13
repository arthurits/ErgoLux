namespace ErgoLux;

partial class FrmMain
{
    /// <summary>
    /// Saves data into an elux-formatted file.
    /// </summary>
    /// <param name="FileName">Path (including name) of the elux file</param>
    private void SaveELuxData(string FileName)
    {
        var cursor = Cursor.Current;
        Cursor.Current = Cursors.WaitCursor;

        try
        {
            using var fs = File.Open(FileName, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
            using var sw = new StreamWriter(fs, System.Text.Encoding.UTF8, leaveOpen: false);

            // Append millisecond pattern to current culture's full date time pattern
            //string fullPattern = System.Globalization.DateTimeFormatInfo.CurrentInfo.FullDateTimePattern;
            string fullPattern = _sett.AppCulture.DateTimeFormat.FullDateTimePattern;
            fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", _sett.MillisecondsFormat);
            TimeSpan nTime = _timeEnd - _timeStart;

            // Save the header text into the file
            sw.WriteLine($"{(StringsRM.GetString("strHeader01", _sett.AppCulture) ?? "ErgoLux data")} ({_sett.AppCultureName})");
            sw.WriteLine($"{(StringsRM.GetString("strHeader02", _sett.AppCulture) ?? "Start time")}: {_timeStart.ToString(fullPattern, _sett.AppCulture)}");
            sw.WriteLine($"{(StringsRM.GetString("strHeader03", _sett.AppCulture) ?? "End time")}: {_timeEnd.ToString(fullPattern, _sett.AppCulture)}");
            sw.WriteLine($"{(StringsRM.GetString("strHeader04", _sett.AppCulture) ?? "Total measuring time")}: {nTime.Days} days, {nTime.Hours} hours, {nTime.Minutes} minutes, {nTime.Seconds} seconds, and {nTime.Milliseconds} millisecons");
            sw.WriteLine($"{(StringsRM.GetString("strHeader05", _sett.AppCulture) ?? "Number of sensors")}: {_sett.T10_NumberOfSensors.ToString()}");
            sw.WriteLine($"{(StringsRM.GetString("strHeader06", _sett.AppCulture) ?? "Number of data points")}: {_nPoints.ToString()}");
            sw.WriteLine($"{(StringsRM.GetString("strHeader07", _sett.AppCulture) ?? "Sampling frequency")}: {_sett.T10_Frequency.ToString(_sett.AppCulture)}");
            sw.WriteLine();
            string content = string.Empty;
            for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                content += $"{(StringsRM.GetString("strHeader08", _sett.AppCulture) ?? "Sensor #")}{i.ToString("00")}\t";
            content += $"{(StringsRM.GetString("strHeader09", _sett.AppCulture) ?? "Maximum")}\t" +
                    $"{(StringsRM.GetString("strHeader10", _sett.AppCulture) ?? "Average")}\t" +
                    $"{(StringsRM.GetString("strHeader11", _sett.AppCulture) ?? "Minimum")}\t" +
                    $"{(StringsRM.GetString("strHeader12", _sett.AppCulture) ?? "Min/Average")}\t" +
                    $"{(StringsRM.GetString("strHeader13", _sett.AppCulture) ?? "Min/Max")}\t" +
                    $"{(StringsRM.GetString("strHeader14", _sett.AppCulture) ?? "Average/Max")}\t";
            sw.WriteLine(content);

            // Save the numerical values
            for (int j = 0; j < _nPoints; j++)
            {
                content = string.Empty;
                for (int i = 0; i < _plotData.Length; i++)
                {
                    content += _plotData[i][j].ToString(_sett.DataFormat,_sett.AppCulture) + "\t";
                }
                //trying to write data to csv
                sw.WriteLine(content.TrimEnd('\t'));
            }

            // Show OK save data
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(StringsRM.GetString("strMsgBoxSaveData", _sett.AppCulture) ?? "Data has been successfully saved to disk.",
                    StringsRM.GetString("strMsgBoxSaveDataTitle", _sett.AppCulture) ?? "Data saving",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            // Show error message
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(String.Format(StringsRM.GetString("strMsgBoxErrorSaveData", _sett.AppCulture) ?? "An unexpected error happened while saving data to disk.\nPlease try again later or contact the software engineer." + Environment.NewLine + "{0}", ex.Message),
                    StringsRM.GetString("strMsgBoxErrorSaveDataTitle", _sett.AppCulture) ?? "Error saving data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        finally
        {
            Cursor.Current = cursor;
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
            string fullPattern = System.Globalization.DateTimeFormatInfo.CurrentInfo.FullDateTimePattern;
            fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", _sett.MillisecondsFormat);
            TimeSpan nTime = _timeEnd - _timeStart;

            // Save the header text into the file
            bw.Write($"{(StringsRM.GetString("strHeader01", _sett.AppCulture) ?? "ErgoLux data")} ({_sett.AppCultureName})");
            bw.Write(_timeStart);
            bw.Write(_timeEnd);
            bw.Write(nTime.Days);
            bw.Write(nTime.Hours);
            bw.Write(nTime.Minutes);
            bw.Write(nTime.Seconds);
            bw.Write(nTime.Milliseconds);
            bw.Write(_sett.T10_NumberOfSensors);
            bw.Write(_nPoints);
            bw.Write(_sett.T10_Frequency);
            string strLine = string.Empty;
            for (int i = 0; i < _sett.T10_NumberOfSensors; i++)
                strLine += "Sensor #" + i.ToString("00") + "\t";
            strLine += "Maximum" + "\t" + "Average" + "\t" + "Minimum" + "\t" + "Min/Average" + "\t" + "Min/Max" + "\t" + "Average/Max";
            bw.Write(strLine);

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
                MessageBox.Show(StringsRM.GetString("strMsgBoxSaveData", _sett.AppCulture) ?? "Data has been successfully saved to disk.",
                    StringsRM.GetString("strMsgBoxSaveDataTitle", _sett.AppCulture) ?? "Data saving",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            // Show error message
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(String.Format(StringsRM.GetString("strMsgBoxErrorSaveData", _sett.AppCulture) ?? "An unexpected error happened while saving data to disk.\nPlease try again later or contact the software engineer." + Environment.NewLine + "{0}", ex.Message),
                    StringsRM.GetString("strMsgBoxErrorSaveDataTitle", _sett.AppCulture) ?? "Error saving data",
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

