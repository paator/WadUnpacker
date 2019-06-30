using System;
using System.IO;
using WadUnpacker.Core;

namespace WadUnpacker.Presentation
{
    public static class Program
    {
        private static int Main(string[] args)
        {
            Console.WriteLine("Croc 1 WAD Unpacker");

            if (args.Length < 3)
            {
                Console.WriteLine("Please specify WAD, IDX files then path of unpacked files locations as arguments.");
                Environment.Exit(-1);
            }

            try
            {
                Compression.Decompress(Compression.LoadWad(args[0]), Compression.LoadIdx(args[1]), args[2]);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case IOException _:
                    case IndexOutOfRangeException _:
                        Console.WriteLine("Wrong IDX and/or WAD files!");
                        throw;
                    case ArgumentOutOfRangeException _:
                        Console.WriteLine(
                            "Wrong or no IDX file loaded - please check if IDX file has the same name as WAD file.");
                        throw;
                    default:
                        Console.WriteLine("Unhandled exception: {0}", e);
                        throw;
                }
            }
            Console.WriteLine("{0} unpacking complete. :)", args[0]);

            return 0;
        }
    }
}