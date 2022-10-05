using Microsoft.VisualBasic.ApplicationServices;
using ScottPlot.Styles;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ErgoLux;

public static class StringResources
{
    /// <summary>
    /// Represents a resource manager that provides convinient access to culture-specific resources at run time
    /// </summary>
    public static System.Resources.ResourceManager StringRM { get; set; } = new("ErgoLux.localization.strings", typeof(FrmMain).Assembly);

    /// <summary>
    /// Specific culture from which the string resources will be retrieved
    /// </summary>
    public static System.Globalization.CultureInfo Culture { get; set; } = System.Globalization.CultureInfo.CurrentCulture;

    public static string GetString(string StringName, System.Globalization.CultureInfo Culture) => StringRM.GetString(StringName, Culture) ?? string.Empty;

    public static string FileHeader01 => StringRM.GetString("strFileHeader01", Culture) ?? "ErgoLux data";
    public static string FileHeader02 => StringRM.GetString("strFileHeader02", Culture) ?? "Start time";
    public static string FileHeader03 => StringRM.GetString("strFileHeader03", Culture) ?? "End time";
    public static string FileHeader04 => StringRM.GetString("strFileHeader04", Culture) ?? "Total measuring time";
    public static string FileHeader05 => StringRM.GetString("strFileHeader05", Culture) ?? "Number of sensors";
    public static string FileHeader06 => StringRM.GetString("strFileHeader06", Culture) ?? "Number of data points";
    public static string FileHeader07 => StringRM.GetString("strFileHeader07", Culture) ?? "Sampling frequency";
    public static string FileHeader08 => StringRM.GetString("strFileHeader08", Culture) ?? "Sensor #";
    public static string FileHeader09 => StringRM.GetString("strFileHeader09", Culture) ?? "Maximum";
    public static string FileHeader10 => StringRM.GetString("strFileHeader10", Culture) ?? "Average";
    public static string FileHeader11 => StringRM.GetString("strFileHeader11", Culture) ?? "Minimum";
    public static string FileHeader12 => StringRM.GetString("strFileHeader12", Culture) ?? "Min/Average";
    public static string FileHeader13 => StringRM.GetString("strFileHeader13", Culture) ?? "Min/Max";
    public static string FileHeader14 => StringRM.GetString("strFileHeader14", Culture) ?? "Average/Max";
    public static string FileHeader15 => StringRM.GetString("strFileHeader15", Culture) ?? "Average";
    public static string FileHeader16 => StringRM.GetString("strFileHeader16", Culture) ?? "Illuminance";
    public static string FileHeader17 => StringRM.GetString("strFileHeader17", Culture) ?? "Missing an empty line.";
    public static string FileHeader18 => StringRM.GetString("strFileHeader18", Culture) ?? "Missing column headers (series names).";
    public static string FileHeader19 => StringRM.GetString("strFileHeader19", Culture) ?? "days";
    public static string FileHeader20 => StringRM.GetString("strFileHeader20", Culture) ?? "hours";
    public static string FileHeader21 => StringRM.GetString("strFileHeader21", Culture) ?? "minutes";
    public static string FileHeader22 => StringRM.GetString("strFileHeader22", Culture) ?? "seconds";
    public static string FileHeader23 => StringRM.GetString("strFileHeader23", Culture) ?? "and";
    public static string FileHeader24 => StringRM.GetString("strFileHeader24", Culture) ?? "milliseconds";
    public static string FileHeaderSection => StringRM.GetString("strFileHeaderSection", Culture) ?? "Section '{0}' is mis-formatted.";


    public static string FormTitle => StringRM.GetString("strFormTitle", Culture) ?? "ErgoLux";
    public static string FormLanguage => StringRM.GetString("strFormLanguage", Culture) ?? "Select culture";
    public static string FormSettings => StringRM.GetString("strFormSettings", Culture) ?? "Settings";
    public static string FormTitleUnion => StringRM.GetString("strFormTitleUnion", Culture) ?? " - ";

    public static string ErrorDeserialize => StringRM.GetString("strErrorDeserialize", Culture) ?? "Error loading settings file." +
        Environment.NewLine + Environment.NewLine + "{0}" +
        Environment.NewLine + Environment.NewLine + "Default values will be used instead.";
    public static string ErrorDeserializeTitle => StringRM.GetString("strErrorDeserializeTitle", Culture) ?? "Error";


    public static string BtnAccept => StringRM.GetString("strBtnAccept", Culture) ?? "&Accept";
    public static string BtnCancel => StringRM.GetString("strBtnCancel", Culture) ?? "&Cancel";
    public static string BtnData => StringRM.GetString("strBtnData", Culture) ?? "Select data";
    public static string BtnExport => StringRM.GetString("strBtnExport", Culture) ?? "&Export";
    public static string BtnReset => StringRM.GetString("strBtnReset", Culture) ?? "&Reset";
    public static string BtnSettings => StringRM.GetString("strBtnSettings", Culture) ?? "&Settings";
    public static string ChkCrossHair => StringRM.GetString("strChkCrossHair", Culture) ?? "Show plots' crosshair";
    public static string ChkCumulative => StringRM.GetString("strChkCumulative", Culture) ?? "Cumulative fractal dimension";
    public static string ChkDlgPath => StringRM.GetString("strChkDlgPath", Culture) ?? "Remember open/save dialog previous path";
    public static string ChkEntropy => StringRM.GetString("strChkEntropy", Culture) ?? "Entropy (approximate && sample)";
    public static string ChkPower => StringRM.GetString("strChkPower", Culture) ?? "Power (dB)";
    public static string DlgReset => StringRM.GetString("strDlgReset", Culture) ?? "Do you want to reset all fields" + Environment.NewLine + "to their default values?";
    public static string DlgResetTitle => StringRM.GetString("strDlgResetTitle", Culture) ?? "Reset settings?";


    public static string MsgBoxErrorOpenData => StringRM.GetString("strMsgBoxErrorOpenData", Culture) ?? "An unexpected error happened while opening file data." + Environment.NewLine + "Please try again later or contact the software engineer." + Environment.NewLine + "{0}";
    public static string MsgBoxErrorOpenDataTitle => StringRM.GetString("strMsgBoxErrorOpenDataTitle", Culture) ?? "Error opening data";
    public static string MsgBoxErrorOpenDevice => StringRM.GetString("strMsgBoxErrorOpenDevice", Culture) ?? "Could not open the device";
    public static string MsgBoxErrorOpenDeviceTitle => StringRM.GetString("strMsgBoxErrorOpenDeviceTitle", Culture) ?? "Error";
    public static string MsgBoxErrorSaveData => StringRM.GetString("strMsgBoxErrorSaveData", Culture) ?? "An unexpected error happened while saving data to disk." + Environment.NewLine + "Please try again later or contact the software engineer." + Environment.NewLine + "{0}";
    public static string MsgBoxErrorSaveDataTitle => StringRM.GetString("strMsgBoxErrorSaveDataTitle", Culture) ?? "Error saving data";
    public static string MsgBoxErrorSettings => StringRM.GetString("strMsgBoxErrorSettings", Culture) ?? "Error loading settings file." +
        Environment.NewLine +
        Environment.NewLine + "{0}" +
        Environment.NewLine +
        Environment.NewLine +
        "Default values will be used instead.";
    public static string MsgBoxErrorSettingsTitle => StringRM.GetString("strMsgBoxErrorSettingsTitle", Culture) ?? "Error";
    public static string MsgBoxExitTitle => StringRM.GetString("strMsgBoxExitTitle", Culture) ?? "Exit?";
    public static string MsgBoxExit => StringRM.GetString("strMsgBoxExit", Culture) ?? "Are you sure you want to exit" + Environment.NewLine + "the application?";


    public static string MsgBoxNoData => StringRM.GetString("strMsgBoxNoData", Culture) ?? "There is no data available to be saved.	";
    public static string MsgBoxNoDataTitle => StringRM.GetString("strMsgBoxNoDataTitle", Culture) ?? "No data";
    public static string MsgBoxReset => StringRM.GetString("strMsgBoxReset", Culture) ?? "Do you want to reset all fields" + Environment.NewLine + "to their default values?";
    public static string MsgBoxResetTitle => StringRM.GetString("strMsgBoxResetTitle", Culture) ?? "Reset?";
    public static string MsgBoxSaveData => StringRM.GetString("strMsgBoxSaveData", Culture) ?? "Data has been successfully saved to disk.";
    public static string MsgBoxSaveDataTitle => StringRM.GetString("strMsgBoxSaveDataTitle", Culture) ?? "Data saving";
    //public static string MsgBoxTaskCancel => StringRM.GetString("strMsgBoxTaskCancel", Culture) ?? $"Computation of the Hausdorff-Besicovitch fractal{Environment.NewLine}dimension has been stopped.";
    //public static string MsgBoxTaskCancelTitle => StringRM.GetString("strMsgBoxTaskCancelTitle", Culture) ?? "Stop";

    public static string OpenDlgFilter => StringRM.GetString("strOpenDlgFilter", Culture) ?? "ErgoLux file (*.elux)|*.elux|Text file (*.txt)|*.txt|Binary file (*.bin)|*.bin|All files (*.*)|*.*";
    public static string OpenDlgTitle => StringRM.GetString("strOpenDlgTitle", Culture) ?? "Open illuminance data";
    public static string SaveDlgFilter => StringRM.GetString("strSaveDlgFilter", Culture) ?? "ErgoLux file(*.elux)|*.elux|Text file(*.txt)|*.txt|Binary file(*.bin)|*.bin|All files(*.*)|*.*";
    public static string SaveDlgTitle => StringRM.GetString("strSaveDlgTitle", Culture) ?? "Save illuminance data";

    public static string ReadDataError => StringRM.GetString("strReadDataError", Culture) ?? "Unable to read data from file." + Environment.NewLine + "{0}";
    public static string ReadDataErrorCulture => StringRM.GetString("strReadDataErrorCulture", Culture) ?? "The culture identifier string name is not valid." + Environment.NewLine + "{0}";
    public static string ReadDataErrorCultureTitle => StringRM.GetString("strReadDataErrorCultureTitle", Culture) ?? "Culture name error";
    public static string ReadDataErrorNumber => StringRM.GetString("strReadDataErrorNumber", Culture) ?? "Invalid numeric value: {0}";
    public static string ReadDataErrorNumberTitle => StringRM.GetString("strReadDataErrorNumberTitle", Culture) ?? "Error parsing data";
    public static string ReadDataErrorTitle => StringRM.GetString("strReadDataErrorTitle", Culture) ?? "Error opening data";

    // FrmSettings
    public static string GrpCulture => StringRM.GetString("strGrpCulture", Culture) ?? "UI and data format";
    public static string LblDataFormat => StringRM.GetString("strLblDataFormat", Culture) ?? "Numeric data-formatting string";
    public static string RadCurrentCulture => StringRM.GetString("strRadCurrentCulture", Culture) ?? "Current culture formatting";
    public static string RadInvariantCulture => StringRM.GetString("strRadInvariantCulture", Culture) ?? "Invariant culture formatting";
    public static string RadUserCulture => StringRM.GetString("strRadUserCulture", Culture) ?? "Select culture";
    public static string TabDevice => StringRM.GetString("strTabDevice", Culture) ?? "T-10A sensor";
    public static string TabGUI => StringRM.GetString("strTabGUI", Culture) ?? "User interface";
    public static string TabPlots => StringRM.GetString("strTabPlots", Culture) ?? "Plotting";

}
