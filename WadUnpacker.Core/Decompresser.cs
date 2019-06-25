using System;
using System.Collections.Generic;
using System.IO;

namespace WadUnpacker.Core
{
    public static class Compression
    {
        //parser turns lines from .idx file into IdxLine class objects
        public static IEnumerable<IdxLine> LoadIdx(string idxLocation)
        {
            var idxLines = File.ReadAllLines(idxLocation);
            var idxList = new List<IdxLine>();
            foreach (var line in idxLines)
            {
                idxList.Add(new IdxLine(line));
            }

            return idxList;
        }
        
        public static BinaryReader LoadWad(string wadLocation)
        {
            var reader = new BinaryReader(File.Open(wadLocation, FileMode.Open));
            return reader;
        }

        public static void Decompress(BinaryReader reader, IEnumerable<IdxLine> idx, string path)
        {
            const int wOffset = 2;
            const int bOffset = 3;

            foreach (var file in idx)
            {
                var maxPosition = file.Length + file.Offset;
                
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                using (var writer = new BinaryWriter(File.Open(path + "\\" + file.FileName, FileMode.Create)))
                {
                    switch (file.ConversionType)
                    {
                        //already uncompressed
                        case 'u':
                        {
                            writer.Write(reader.ReadBytes(maxPosition));
                            break;
                        }

                        case 'w':
                        {
                            while (reader.BaseStream.Position < maxPosition)
                            {
                                var k = reader.ReadSByte(); //read int8 from input
                                if (k < 0) //if k<0, read -k int16 and write them to output
                                {
                                    for (var i = 0; i < -k; i++)
                                    {
                                        writer.Write(reader.ReadInt16());
                                    }
                                }
                                else //read one int16 from input and write it k+2 times to output
                                {
                                    var int16 = reader.ReadInt16();
                                    for (var i = 0; i < k + wOffset; i++)
                                    {
                                        writer.Write(int16);
                                    }
                                }
                            }
                            break;
                        }

                        case 'b':
                        {
                            while (reader.BaseStream.Position < maxPosition)
                            {
                                var k = reader.ReadSByte(); //read int8 from input
                                if (k < 0) //if k<0, read -k int8 and write them to output
                                {
                                    for (var i = 0; i < -k; i++)
                                    {
                                        writer.Write(reader.ReadSByte());
                                    }
                                }
                                else //read one int8 from input and write it k+3 times to output
                                {
                                    var int8 = reader.ReadSByte();
                                    for (var i = 0; i < k + bOffset; i++)
                                    {
                                        writer.Write(int8);
                                    }
                                }
                            }
                            break;
                        }

                        default: throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
    }
}