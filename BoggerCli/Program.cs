using System;
using System.Diagnostics;
using BoggerCore;

namespace BoggerCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("[INFO] Enter your video ID, e.g. av2961146: ");
            string avId = Console.ReadLine();
            Console.WriteLine("[INFO] Please wait...");

            var cidCrawler = new CidCrawler();
            string cid = cidCrawler.GetVideoContentId(avId).Result;
            Debug.WriteLine("[DEBUG] CID is " + cid);

            var videoUrlGrabber = new VideoUrlGrabber();
            var videoUrl = videoUrlGrabber.GetUrlBySingleContentId(cid, avId).Result;

            Console.WriteLine("[INFO] Got {0} video sections.", videoUrl.Durl.Count);

            foreach(var url in videoUrl.Durl)
            {
                Console.WriteLine("[INFO] URL: {0}", url.Url);
            }

            Console.Read();
        }
    }
}