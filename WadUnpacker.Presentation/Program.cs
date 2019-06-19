using System;
using System.IO;
using WadUnpacker.Core;

namespace WadUnpacker.Presentation
{
    public class Program
    {
        private static int Main(string[] args)
        {
            Console.WriteLine("Croc 1 WAD Unpacker");

            try
            {
                Compression.Decompress(Compression.LoadWad(args[1]), Compression.LoadIdx(args[0]));
            }
            catch (Exception e)
            {
                if (e is IOException || e is IndexOutOfRangeException) Console.WriteLine("Wrong IDX and/or WAD files!");
                else if (e is ArgumentOutOfRangeException)
                    Console.WriteLine(
                        "Wrong or no IDX file loaded - please check if IDX file has the same name as WAD file");
                else Console.WriteLine("Unhandled exception: {0}", e);
            }

            return 0;
        }
    }
}