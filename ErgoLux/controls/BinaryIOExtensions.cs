
namespace System.IO;

public static class BinaryIOExtensions
{
    /// <summary>
    /// Reads a line of characters from the current binary stream and returns the data as a string.
    /// A line is defined as a sequence of characters followed by a line feed ("\n"), a carriage return ("\r"), or a carriage return immediately followed by a line feed ("\r\n").
    /// The string that is returned does not contain the terminating carriage return or line feed.
    /// The returned value is null if the end of the input stream is reached.
    /// </summary>
    /// <param name="reader">The binary stream to be read from.</param>
    /// <returns>The next line from the input stream, or null if the end of the input stream is reached.</returns>
    /// <seealso cref="https://www.codeproject.com/Articles/996254/BinaryReader-ReadLine-extension"/>
    public static string? ReadLine(this BinaryReader reader)
    {
        var result = new System.Text.StringBuilder();
        bool foundEndOfLine = false;
        char ch;
        while (!foundEndOfLine)
        {
            try
            {
                ch = reader.ReadChar();
            }
            catch (EndOfStreamException)
            {
                if (result.Length == 0) return null;
                else break;
            }

            switch (ch)
            {
                case '\r':
                    if (reader.PeekChar() == '\n') reader.ReadChar();
                    foundEndOfLine = true;
                    break;
                case '\n':
                    foundEndOfLine = true;
                    break;
                default:
                    result.Append(ch);
                    break;
            }
        }
        return result.ToString();
    }

    /// <summary>
    /// Writes a string to the stream, followed by a line terminator.
    /// </summary>
    /// <param name="reader">The binary stream to be read</param>
    /// <param name="line">The string to be written.</param>
    public static void WriteLine(this BinaryWriter reader, string line = null)
    {
        reader.Write(line + Environment.NewLine);
    }
}