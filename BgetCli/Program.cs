using System;
using System.Diagnostics;
using BgetCore.Video;

namespace BgetCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("[INFO] Enter your video ID, e.g. av349183: ");
            string inputVideo = Console.ReadLine();
            Console.WriteLine("[INFO] Please wait...");

            var cidCrawler = new VideoInfoCrawler();
            var videoInfo = cidCrawler.GetVideoInfo(inputVideo).Result;
            string cid = videoInfo.ContentId;
            Console.WriteLine("[DEBUG] CID is " + cid);
            Console.WriteLine("[INFO] Title: " + videoInfo.Title);
            Console.WriteLine("[INFO] Author: " + videoInfo.Author);
            Console.WriteLine("[INFO] Description: " + videoInfo.Description + "\n\n\n");

            var videoUrlGrabber = new VideoUrlCrawler();
            var videoUrl = videoUrlGrabber.GetUrlBySingleContentId(cid, inputVideo).Result;
            Console.WriteLine("[INFO] Got {0} video sections.", videoUrl.Durl.Count);

            

            foreach(var url in videoUrl.Durl)
            {
                Console.WriteLine("[INFO] Flash video URL: {0}", url.Url);
                
                if(url.BackupUrl != null && url.BackupUrl.Url.Count != 0)
                {
                    foreach(var mp4Url in url.BackupUrl.Url)
                    {
                        Console.WriteLine("[INFO] MP4 backup video URL: {0}", mp4Url);
                    }
                }
            }

            

            Console.Read();
        }
    }
}