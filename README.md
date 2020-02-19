# Croc: Legend of the Gobbos WAD archives unpacker
.wad unpacker uses .idx files with same file names as .wad archives. For example, maps.wad needs maps.idx for successful decompression, as it contains essential information about files in maps.wad archive. Each line in the .idx file represents file data in the corresponding .wad file. Each bit of information in .idx is separated by a comma `,`. Note that unpacker does not work with Croc 2 WAD archives as they use different algorithms.

The algorithms being used are sligthly modified versions of Run-Length Encoding.
https://en.wikipedia.org/wiki/Run-length_encoding

Small note: The solution is splitted into 2 projects because I may use WPF in future if more features would be added.
