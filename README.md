# Croc: Legend of the Gobbos WAD archives decompresser
.wad decompresser uses .idx files with same file names as .wad archives. For example, maps.wad needs maps.idx for successful decompression, as it contains essential information about files in maps.wad archive. Each line in the .idx file represents file data in the .wad with the same name as the .idx, also each bit of information is separated by a comma `,`. Note that decompresser does not work with Croc 2 WAD archives as they use different algorithms.

The algorithms being used are sligthly modified versions of Run-Length Encoding.

