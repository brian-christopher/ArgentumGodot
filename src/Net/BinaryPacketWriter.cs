using System.IO;
using System.Text;

namespace ArgentumOnline.Net;

public class BinaryPacketWriter
{
    private readonly MemoryStream _stream;
    private readonly BinaryWriter _writer;
    
    private const byte TrueValue = 1;
    private const byte FalseValue = 0;

    public BinaryPacketWriter()
    {
        _stream = new();
        _writer = new(_stream);
    }
    
    public BinaryPacketWriter WriteByte(byte value)
    {
        _writer.Write(value);
        return this;
    }

    public BinaryPacketWriter WriteBoolean(bool value)
    {
        WriteByte(value ? TrueValue : FalseValue);
        return this;
    }
    
    public BinaryPacketWriter WriteInteger(short value)
    {
        _writer.Write(value);
        return this;
    }
    
    public BinaryPacketWriter WriteLong(long value)
    {
        _writer.Write(value);
        return this;
    }

    public BinaryPacketWriter WriteSingle(float value)
    {
        _writer.Write(value);
        return this;
    }

    public BinaryPacketWriter WriteDouble(double value)
    {
        _writer.Write(value);
        return this;
    }

    public BinaryPacketWriter WriteString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            _writer.Write((short)0);
            return this;
        }
        
        byte[] bytes = Encoding.Latin1.GetBytes(value);
        
        _writer.Write((short)bytes.Length);
        _writer.Write(bytes);
        
        return this;
    }

    public byte[] Build()
    {
        if (_stream.Length == 0)
        {
            return [];
        }
        
        byte[] data = _stream.ToArray();
        _stream.SetLength(0);
        
        return data;
    }
}