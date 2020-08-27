
# Croc: Legend of the Gobbos WAD archive unpacker
Croc: Legend of the Gobbos is a platform game developed by Argonaut Games and published by Fox Interactive in 1997. It uses .WAD archives for data compression for almost every asset type in the game, for example models, soundtrack, textures.
## How does it work?
Unpacker uses .idx files with corresponding file names to .wad file names. For example, maps.wad needs maps.idx for successful decompression, as it contains essential information about files in archive. Each line in the .idx file represents file data in the corresponding .wad file. The algorithms being used are sligthly modified versions of [Run-Length Encoding](https://en.wikipedia.org/wiki/Run-length_encoding).
## Usage:
Both library and execution projects use .NET Core 3.1 runtime. They are splitted due to the fact that I may upgrade project with more features in future.
Compile with:
```
dotnet build
```
Run compiled program located in
*WadUnpacker.Presentation\bin\Debug\netcoreapp3.1* with arguments as follows:
```
dotnet WadUnpacker.Presentation.dll wad_file_location idx_file_location output_destination
```
