using System;
using System.IO;
using System.Text;

namespace ArgentumOnline.Net;

public sealed class Reader
{
    private readonly MemoryStream _stream;
    private readonly BinaryReader _reader;
    
    public Reader(byte[] data)
    {
        ArgumentNullException.ThrowIfNull(data);
        
        _stream = new(data);
        _reader = new(_stream);
    }

    public long Position => _stream.Position;
    public long Length => _stream.Length;

    public byte ReadByte()
    {
        return _reader.ReadByte();
    }

    public bool ReadBoolean()
    {
        return ReadByte() == 1 ? true : false;
    }

    public short ReadInteger()
    {
        return _reader.ReadInt16();
    }

    public int ReadLong()
    {
        return _reader.ReadInt32();
    }

    public float ReadSingle()
    {
        return _reader.ReadSingle();
    }

    public double ReadDouble()
    {
        return _reader.ReadDouble();
    }
    
    public string ReadString()
    {
        int length = _reader.ReadInt16();
        
        if (length == 0)
            return string.Empty;

        byte[] bytes = _reader.ReadBytes(length);
        return Encoding.Latin1.GetString(bytes);
    }
}