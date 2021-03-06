﻿using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AriaNet;

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
            Directory.CreateDirectory("bget_temp");

            // Detect architecture
            if (Environment.Is64BitOperatingSystem)
            {
                await ExtractAriaBinary("bget_temp/aria2.exe", AriaBinary.Aria64);
            }
            else
            {
                await ExtractAriaBinary("bget_temp/aria2.exe", AriaBinary.Aria32);
            }
            
            // Run aria2c main process in the backend...
            var ariaArgument = "--enable-rpc=true" +
                               (Properties.Settings.Default.SessionPath.Length < 3 
                                ? $" --save-session={Properties.Settings.Default.DownloadPath}\\bget_session" 
                                : $" --save-session={Properties.Settings.Default.SessionPath}") + 
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
            // If failed, then exit (of course lol)
            try
            {
                if (!process.Start())
                {
                    MessageBox.Show(
                        "Failed to start Aria2 downloader.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(1);
                }
                else
                {
                    var ariaManager = Properties.Settings.Default.UseBundledAria ?
                        new AriaManager() :
                        new AriaManager(Properties.Settings.Default.ExternalRpc);

                    var testAriaVersion = await ariaManager.GetVersion();

                    // If aria2 RPC service fails, exit too.
                    if (string.IsNullOrEmpty(testAriaVersion.Version))
                    {
                        MessageBox.Show(
                            "Aria2 downloader started but not working properly.\n" +
                            " Please check your firewall configuration.", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        process.Kill();
                        Environment.Exit(1);
                    }

                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Exit process went wrong!\nReason: {error.Message}", "ERROR",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                process.Kill();
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
            var ariaManager = Properties.Settings.Default.UseBundledAria ?
                new AriaManager() :
                new AriaManager(Properties.Settings.Default.ExternalRpc);

            // Force the process exit first...
            try
            {
                // Here's a workaround for "aria2.shutdown"
                var saveSessionResult = await ariaManager.SaveSession();

                if (!saveSessionResult)
                {
                    MessageBox.Show("Cannot save session before exit!", "WARNING", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                foreach (var selectedProcess in Process.GetProcessesByName("aria2"))
                {
                    selectedProcess.Kill();
                    selectedProcess.WaitForExit();
                }

                // Remove all the file from 
                foreach (var file in Directory.GetFiles("bget_temp"))
                {
                    File.Delete(file);
                }

                // Then remove the whole directory...
                Directory.Delete("bget_temp", true);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Exit process went wrong!\nReason: {error.Message}", "ERROR",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
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