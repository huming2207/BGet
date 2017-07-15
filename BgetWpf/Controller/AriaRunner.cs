using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace BgetWpf.Controller
{
    public class AriaRunner
    {
        /// <summary>
        /// Automatically extract aria2 download engine to temporary path, then run it with pre-described command.
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            // The newest way to get CPU architecture, even Mono is supported (but sadly, it doesn't support WPF)
            // See here for more details: https://stackoverflow.com/questions/767613/identifying-the-cpu-architecture-type-using-c-sharp

            typeof(object).Module.GetPEKind(out PortableExecutableKinds portableExecutableKinds, out ImageFileMachine imageFileMachine);
            Directory.CreateDirectory("bget_temp");

            switch (imageFileMachine)
            {
                case ImageFileMachine.I386:
                {
                    await ExtractAriaBinary("bget_temp/aria2.exe", AriaBinary.Aria32);
                    break;
                }

                case ImageFileMachine.AMD64:
                case ImageFileMachine.IA64:
                {
                    await ExtractAriaBinary("bget_temp/aria2.exe", AriaBinary.Aria64);
                    break;
                }

                case ImageFileMachine.ARM:
                {
                    // If, I mean if lol, Microsoft does release some desktop devices with ARM CPUs later, 
                    // let this program stop here and make x86 great again! :)
                    MessageBox.Show("This program does not support ARM CPUs, please consider using alternative tools (e.g. You-Get).", "ERROR", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
            }
        }

        /// <summary>
        /// Stop and clean up temporary directory. 
        /// Let's make this program nicer, not another Qihoo 360 or Baidu lol.
        /// </summary>
        /// <returns></returns>
        public async Task Stop()
        {
            // Force the process exit first...
            try
            {
                foreach (var selectedProcess in Process.GetProcessesByName("aria2"))
                {
                    selectedProcess.Kill();
                    selectedProcess.WaitForExit();
                }

                // Then remove the whole directory...
                Directory.Delete("bget_temp");
            }
            catch (Exception error)
            {
                //TODO: handle error
            }

        }

        private async Task ExtractAriaBinary(string fileName, byte[] resourceContent)
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