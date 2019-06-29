using System.Linq;

namespace WadUnpacker.Core
{
    public class IdxLine
    {
        public string FileName { get; }
        public int Offset { get; }
        public int Length { get; }
        public int UncompressedLength { get; }
        public char ConversionType { get; }
        
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
            ConversionType = lines[4].FirstOrDefault();
        }
    }
}