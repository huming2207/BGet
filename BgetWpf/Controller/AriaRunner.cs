using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AriaNet.Aria;

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
                    MessageBox.Show(
                        "This program does not support ARM CPUs, please consider using alternative tools (e.g. You-Get).",
                        "ERROR",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(1);
                    break;
                }
            }

            // Run aria2c main process in the backend...
            var ariaArgument = "--enable-rpc=true" +
                               $" --optimize-concurrent-downloads={Properties.Settings.Default.AutoDecideConcurrentTask.ToString().ToLower()}" +
                               $" --split={Properties.Settings.Default.SplitPerTask}" +
                               $" --max-connection-per-server={Properties.Settings.Default.MaxConnPerServer}" +
                               $" --disk-cache={Properties.Settings.Default.DiskCache}M" +
                               $" --max-overall-download-limit={Properties.Settings.Default.GlobalDownloadLimit}K" +
                               $" --max-overall-upload-limit={Properties.Settings.Default.GlobalUploadLimit}K" +
                              
                               // BT stuff
                               $" --enable-dht={Properties.Settings.Default.EnableDht.ToString().ToLower()}" +
                               $" --enable-dht6={Properties.Settings.Default.EnableDht.ToString().ToLower()}" +
                               $" --enable-peer-exchange={Properties.Settings.Default.EnablePex.ToString().ToLower()}" +
                               $" --bt-enable-lpd={Properties.Settings.Default.EnableLpd.ToString().ToLower()}" +
                               $" --bt-require-crypto={Properties.Settings.Default.ForceEncrypt.ToString().ToLower()}" +
                               (Properties.Settings.Default.EnableEncrypt
                                   ? " --bt-min-crypto-level=arc4"
                                   : " --bt-min-crypto-level=plain") +
                               $" --bt-hash-check-seed={Properties.Settings.Default.CheckBeforeSeed.ToString().ToLower()}" +
                               $" --peer-id-prefix={Properties.Settings.Default.PeerIdPerfix}" +
                               $" --user-agent={Properties.Settings.Default.TorrentUserAgent}" +
                               $" --listen-port={Properties.Settings.Default.TorrentPort}" +
                               $" --seed-ratio={Properties.Settings.Default.SeedRatio}" +
                               $" --bt-max-peers={Properties.Settings.Default.MaxPeers}";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + @"\bget_temp\aria2.exe")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    Arguments = ariaArgument
                }
            };

            // Fire the hole!
            // If failed, then exit
            if (!process.Start())
            {
                MessageBox.Show(
                    "Failed to start Aria2 downloader.","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Stop and clean up temporary directory. 
        /// Let's make this program nicer, not another Qihoo 360 or Baidu lol.
        /// </summary>
        /// <returns></returns>
        public async Task Stop(bool forceQuit = false)
        {
            DownloadManager downloadManager;

            downloadManager = Properties.Settings.Default.UseBundledAria ?
                new DownloadManager() :
                new DownloadManager(Properties.Settings.Default.ExternalRpc);

            // Force the process exit first...
            try
            {
                if (forceQuit)
                {
                    foreach (var selectedProcess in Process.GetProcessesByName("aria2"))
                    {
                        selectedProcess.Kill();
                        selectedProcess.WaitForExit();
                    }
                }
                else
                {
                    // If graceful shutdown failed, retry force stop mode
                    if (!await downloadManager.Shutdown())
                    {
                        await Stop(true);
                    }
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