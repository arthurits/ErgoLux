using System.Text.RegularExpressions;

namespace ErgoLux;

// Custom struct with our desired values
public struct SerialPort
{
    public string name;
    public string vid;
    public string pid;
    public string description;
}

public class SerialPorts
{
    private const string vidPattern = @"VID_([0-9A-F]{4})";
    private const string pidPattern = @"PID_([0-9A-F]{4})";

    private static List<ComPort> GetSerialPorts()
    {
        using var searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort");
        var ports = searcher.Get().Cast<System.Management.ManagementBaseObject>().ToList();
        return ports.Select(p =>
        {
            ComPort c = new()
            {
                name = p.GetPropertyValue("DeviceID").ToString() ?? string.Empty,
                vid = p.GetPropertyValue("PNPDeviceID").ToString() ?? string.Empty,
                description = p.GetPropertyValue("Caption").ToString() ?? string.Empty
            };

            Match mVID = Regex.Match(c.vid, vidPattern, RegexOptions.IgnoreCase);
            Match mPID = Regex.Match(c.vid, pidPattern, RegexOptions.IgnoreCase);

            if (mVID.Success)
                c.vid = mVID.Groups[1].Value;
            if (mPID.Success)
                c.pid = mPID.Groups[1].Value;

            return c;

        }).ToList();
    }
}

//using System.IO.Ports.SerialPort;

//Alternative and easier connection method using System.IO.Ports.SerialPort (.NET Platform Extensions 5) instead of FTD2.dll and ClassFTDI
//The only disadvantage is that we need to know the port name the deviced is connected into.

//private void BtnConnect_Click(object sender, EventArgs e)
//{
//    // Sets the serial port parameters and opens it
//    _serialPort = new SerialPort("COM5", 9600, Parity.Even, 7, StopBits.One);
//    _serialPort.Handshake = Handshake.None;
//    _serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
//    //_serialPort.ReadTimeout = 500;
//    //_serialPort.NewLine = "\n";
//    _serialPort.Open();

//    if (_serialPort.IsOpen)
//    {
//        // Initialize the arrays containing the data
//        InitializeArrays();

//        // First, clear all data (if any) in the plots
//        Plots_Clear();

//        // Bind the arrays to the plots
//        Plots_DataBinding();

//        // Show the legends in the picture boxes
//        Plots_ShowLegends();

//        _serialPort.Write(ClassT10.Command_54);

//        m_timer.Start();
//    }
//}

//private void BtnStop_Click(object sender, EventArgs e)
//{
//    m_timer.Stop();
//    _serialPort.DataReceived -= sp_DataReceived;

//    // Shows plots's full data
//    Plots_ShowFull();
//}

//private void OnTimedEvent(object sender, EventArgs e)
//{
//    _serialPort.Write(ClassT10.ReceptorsSingle[0]);
//}

//private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
//{
//    (int Sensor, double Iluminance, double Increment, double Percent) result;
//    string data = _serialPort.ReadLine() + _serialPort.NewLine;     //ReadLine deletes the string defined in _serialPort.NewLine
//    if (data.Length == ClassT10.LongBytesLength)
//    {
//        result = ClassT10.DecodeCommand(data);
//        if (result.Sensor < _sett.T10_NumberOfSensors - 1)
//            _serialPort.Write(ClassT10.ReceptorsSingle[result.Sensor + 1]);
//        Plots_Update(result.Sensor, result.Iluminance);
//    }
//}