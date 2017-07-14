using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace BgetWpf.Controller
{
    public class AriaExtract
    {
        public AriaExtract()
        {
            
        }

        public async Task ExtractAriaBinary(string fileName, byte[] resourceContent)
        {
            // This method came from Shadowsocks-Windows project.
            // See here: https://github.com/shadowsocks/shadowsocks-windows/blob/9e529361c4a04781c8b0574b32625a696f9e75c5/shadowsocks-csharp/Controller/FileManager.cs#L25
            // Because the uncompressed size of the file is unknown,
            // we are using an arbitrary buffer size.
            var gzipBuffer = new byte[4096];

            using (var fs = File.Create(fileName))
            using (var input = new GZipStream(new MemoryStream(resourceContent),
                CompressionMode.Decompress, false))
            {
                int bufferBlockIndex;
                while ((bufferBlockIndex = input.Read(gzipBuffer, 0, gzipBuffer.Length)) > 0)
                {
                   await fs.WriteAsync(gzipBuffer, 0, bufferBlockIndex);
                }
            }
        }
    }
}