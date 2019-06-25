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

            if (args.Length != 2)
            {
                Console.WriteLine("Please specify WAD then IDX file locations as arguments");
            }

            try
            {
                Compression.Decompress(Compression.LoadWad(args[0]), Compression.LoadIdx(args[1]));
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case IOException _:
                    case IndexOutOfRangeException _:
                        Console.WriteLine("Wrong IDX and/or WAD files!");
                        break;
                    case ArgumentOutOfRangeException _:
                        Console.WriteLine(
                            "Wrong or no IDX file loaded - please check if IDX file has the same name as WAD file");
                        break;
                    default:
                        Console.WriteLine("Unhandled exception: {0}", e);
                        break;
                }
            }

            return 0;
        }
    }
}