using System;
using System.Collections.Generic;
using System.IO;

class Decompresser
{
    private static int Main(string[] args)
    {
        Console.WriteLine("Croc 1 WAD archives decompresser");

        if (args.Length < 2)
        {
            Console.WriteLine("Please add .wad and .idx files locations as arguments!");
            return 1;
        }
        
        try
        {
            var wadLocation = args[0];
            var idxLocation = args[1];
            var reader = new BinaryReader(File.Open(wadLocation, FileMode.Open));
            var idxLines = File.ReadAllLines(idxLocation); //read lines from .idx file
            var idxList = FillIdxList(idxLines); //then parse it 

            Decompress(reader, idxList);
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }
        
        Console.WriteLine("Decompressing complete :)");
        return 0;
    }

    //parser turns lines from .idx file into IdxLine class objects
    private static IEnumerable<IdxLine> FillIdxList(IEnumerable<string> lines)
    {
        var idxList = new List<IdxLine>();
        foreach (var line in lines)
        {
            idxList.Add(new IdxLine(line));
        }
        return idxList;
    }

    private static void Decompress(BinaryReader reader, IEnumerable<IdxLine> idx)
    {
        foreach (var file in idx)
        {
            var maxPosition = file.Length + file.Offset;
            using (var writer = new BinaryWriter(File.Open(file.FileName, FileMode.Create)))
            {
                Console.WriteLine("Unpacking: {0}", file.FileName);
                if (file.ConversionType == 'u') //already uncompressed
                {
                    while (reader.BaseStream.Position < maxPosition)
                    {
                        writer.Write(reader.ReadSByte());
                    }
                }
                else if (file.ConversionType == 'w')
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
                            for (var i = 0; i < k + 2; i++)
                            {
                                writer.Write(int16);
                            }
                        }
                    }
                }
                else if (file.ConversionType == 'b')
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
                            for (var i = 0; i < k + 3; i++)
                            {
                                writer.Write(int8);
                            }
                        }
                    }
                }
            }
        }
    }
}