using System;

namespace WadUnpacker.Core
{
    public class IdxLine
    {
        public string FileName { get; }
        public int Offset { get; }
        public int Length { get; }
        public int UncompressedLength { get; }
        public Conversion ConversionType { get; }

        public enum Conversion
        {
            UType = 'u',
            WType = 'w',
            BType = 'b'
        }

        public IdxLine(string line)
        {
            var lines = line.Split(',');
            FileName = lines[0];
            Offset = int.Parse(lines[1]);
            Length = int.Parse(lines[2]);
            UncompressedLength = int.Parse(lines[3]);
            ConversionType = (Conversion) Enum.Parse(typeof(Conversion), lines[4]);
        }
    }
}

