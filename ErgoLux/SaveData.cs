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
            string fullPattern = this._settings.AppCulture.DateTimeFormat.FullDateTimePattern;
            fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", this._settings.MillisecondsFormat);
            TimeSpan nTime = this._timeEnd - this._timeStart;

            // Save the header text into the file
            sw.WriteLine($"{StringResources.FileHeader01} ({this._settings.AppCultureName})");
            sw.WriteLine($"{StringResources.FileHeader02}: {this._timeStart.ToString(fullPattern, this._settings.AppCulture)}");
            sw.WriteLine($"{StringResources.FileHeader03}: {this._timeEnd.ToString(fullPattern, this._settings.AppCulture)}");
            //sw.WriteLine($"{(StringsRM.GetString("strFileHeader04", _sett.AppCulture) ?? "Total measuring time")}: {nTime.Days} days, {nTime.Hours} hours, {nTime.Minutes} minutes, {nTime.Seconds} seconds, and {nTime.Milliseconds} millisecons");
            sw.WriteLine($"{StringResources.FileHeader04}: " +
                $"{nTime.Days} {StringResources.FileHeader19}, " +
                $"{nTime.Hours} {StringResources.FileHeader20}, " +
                $"{nTime.Minutes} {StringResources.FileHeader21}, " +
                $"{nTime.Seconds} {StringResources.FileHeader22} " +
                $"{StringResources.FileHeader23} " +
                $"{nTime.Milliseconds} {StringResources.FileHeader24}");
            sw.WriteLine($"{StringResources.FileHeader05}: {this._settings.T10_NumberOfSensors}");
            sw.WriteLine($"{StringResources.FileHeader06}: {this._nPoints}");
            sw.WriteLine($"{StringResources.FileHeader07}: {this._settings.T10_Frequency.ToString(this._settings.AppCulture)}");
            sw.WriteLine();
            string content = string.Empty;
            for (int i = 0; i < this._settings.T10_NumberOfSensors; i++)
                content += $"{StringResources.FileHeader08}{i:00}\t";
            content += $"{StringResources.FileHeader09}\t" +
                    $"{StringResources.FileHeader10}\t" +
                    $"{StringResources.FileHeader11}\t" +
                    $"{StringResources.FileHeader12}\t" +
                    $"{StringResources.FileHeader13}\t" +
                    $"{StringResources.FileHeader14}";
            sw.WriteLine(content);

            // Save the numerical values
            for (int j = 0; j < this._nPoints; j++)
            {
                content = string.Empty;
                for (int i = 0; i < this._plotData.Length; i++)
                {
                    content += this._plotData[i][j].ToString(this._settings.DataFormat, this._settings.AppCulture) + "\t";
                }
                //trying to write data to csv
                sw.WriteLine(content.TrimEnd('\t'));
            }

            // Show OK save data
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(StringResources.MsgBoxSaveData,
                    StringResources.MsgBoxSaveDataTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            // Show error message
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(String.Format(StringResources.MsgBoxErrorSaveData, ex.Message),
                    StringResources.MsgBoxErrorSaveDataTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Saves data into a text-formatted file.
    /// </summary>
    /// <param name="FileName">Path (including name) of the text file</param>
    private void SaveTextData(string FileName)
    {
        this.SaveELuxData(FileName);
    }

    /// <summary>
    /// Saves data into a binary-formatted file.
    /// </summary>
    /// <param name="FileName">Path (including name) of the binary file</param>
    private void SaveBinaryData(string FileName)
    {
        try
        {
            using var fs = File.Open(FileName, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
            using var bw = new BinaryWriter(fs, System.Text.Encoding.UTF8, false);

            // Append millisecond pattern to current culture's full date time pattern
            //string fullPattern = System.Globalization.DateTimeFormatInfo.CurrentInfo.FullDateTimePattern;
            //fullPattern = System.Text.RegularExpressions.Regex.Replace(fullPattern, "(:ss|:s)", _sett.MillisecondsFormat);
            TimeSpan nTime = this._timeEnd - this._timeStart;

            // Save the header text into the file
            bw.Write($"{StringResources.FileHeader01} ({this._settings.AppCultureName})");
            bw.Write(this._timeStart);
            bw.Write(this._timeEnd);
            bw.Write(nTime.Days);
            bw.Write(nTime.Hours);
            bw.Write(nTime.Minutes);
            bw.Write(nTime.Seconds);
            bw.Write(nTime.Milliseconds);
            bw.Write(this._settings.T10_NumberOfSensors);
            bw.Write(this._nPoints);
            bw.Write(this._settings.T10_Frequency);
            string content = string.Empty;
            for (int i = 0; i < this._settings.T10_NumberOfSensors; i++)
                content += $"{StringResources.FileHeader08}{i:00}\t";
            content += $"{StringResources.FileHeader09}\t" +
                    $"{StringResources.FileHeader10}\t" +
                    $"{StringResources.FileHeader11}\t" +
                    $"{StringResources.FileHeader12}\t" +
                    $"{StringResources.FileHeader13}\t" +
                    $"{StringResources.FileHeader14}\t";
            bw.WriteLine(content);

            // https://stackoverflow.com/questions/6952923/conversion-double-array-to-byte-array
            byte[] bytesLine;
            for (int i = 0; i < this._plotData.Length; i++)
            {
                // bw.Write(_plotData[i].SelectMany(value => BitConverter.GetBytes(value)).ToArray()); // Requires LINQ
                bytesLine = new byte[this._plotData[i].Length * sizeof(double)];
                Buffer.BlockCopy(this._plotData[i], 0, bytesLine, 0, bytesLine.Length);
                bw.Write(bytesLine);
            }

            // Show OK save data
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(StringResources.MsgBoxSaveData,
                    StringResources.MsgBoxSaveDataTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            // Show error message
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(String.Format(StringResources.MsgBoxErrorSaveData, ex.Message),
                    StringResources.MsgBoxErrorSaveDataTitle,
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
        this.SaveELuxData(FileName);
    }

}