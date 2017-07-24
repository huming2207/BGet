using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BgetCore.Video;

namespace BgetWpf.Model
{
    public class BgetModel
    {
        /// <summary>
        /// Generate video link
        /// 
        /// Note:
        /// 1. UserAgent (MSIE10) and Referrer must be set to the downloader, otherwise server will return 500
        /// 2. Exception may be raised if something goes wrong (e.g. no video found)
        /// </summary>
        /// <param name="contentUrl"></param>
        /// <returns></returns>
        private async Task<List<string>> GenerateVideoLink(string contentUrl)
        {
            // BGetCore library related stuff
            var videoInfoCrawler = new VideoInfoCrawler();
            var videoInfo = await videoInfoCrawler.GetVideoInfo(contentUrl);

            var videoUrlGrabber = new VideoUrlCrawler();
            var videoUrl = await videoUrlGrabber.GetUrlBySingleContentId(videoInfo);

            var urlList = new List<string>();

            // Just a practice of LINQ...
            if (Properties.Settings.Default.PreferFlv)
            {
                videoUrl.Durl.ForEach(url => urlList.Add(url.Url));
            }
            else
            {
                urlList.AddRange(
                    videoUrl.Durl.Where(url => url.BackupUrl != null && url.BackupUrl.Url.Count != 0)
                        .SelectMany(url => url.BackupUrl.Url));
            }

            return urlList;
        }
    }
}
