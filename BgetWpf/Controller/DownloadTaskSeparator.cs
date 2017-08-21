using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using AriaNet;
using BgetCore.User;
using BgetCore.Video;
using BgetWpf.Properties;

namespace BgetWpf.Controller
{
    public class DownloadTaskSeparator
    {
        /// <summary>
        ///     The main method for this class, import the URL and sparate automatically.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task TaskHandler(string url, IProgress<double[]> progressStatus)
        {
            // Declare aria manager
            var ariaManager = new AriaManager();

            // Case 1: Single Bilibili video
            if (Regex.IsMatch(url, @"bilibili.com/video/av(\d+)"))
            {
                // Make some fake news
                progressStatus.Report(new[] {0d, 0d});

                await ariaManager.AddUri(await GenerateGeneralVideoLink(url),
                    "Mozilla/5.0 (MSIE 10.0; Windows NT 6.1; Trident/5.0)", url);

                // Make some fake news again
                progressStatus.Report(new[] {1d, 1d});
            }

            // Case 2: A user and his/her videos
            else if (Regex.IsMatch(url, @"space.bilibili.com/(\d+)"))
            {
                await ariaManager.AddUri(await GenerateUserVideoLink(url, progressStatus),
                    "Mozilla/5.0 (MSIE 10.0; Windows NT 6.1; Trident/5.0)", url);
            }

            // Case 3: Aria2 commandline
            else if (Regex.IsMatch(url, @"^(aria2c).*http(s?)://"))
            {
                MessageBox.Show("Not yet implemented, use URL instead.");
            }

            // Case 4: Normal task
            else if (Regex.IsMatch(url, @"^http(s?)://"))
            {
                await ariaManager.AddUri(new List<string> {url});
            }

            // Case 5: Don't know wtf is this...
            else
            {
                progressStatus.Report(new[] {-999d, -999d});
            }
        }

        /// <summary>
        ///     Generate video link
        ///     Note:
        ///     1. UserAgent (MSIE10) and Referrer must be set to the downloader, otherwise server will return 500
        ///     2. Exception may be raised if something goes wrong (e.g. no video found)
        /// </summary>
        /// <param name="contentUrl"></param>
        /// <returns></returns>
        private async Task<List<string>> GenerateGeneralVideoLink(string contentUrl)
        {
            // BGetCore library related stuff
            var videoInfoCrawler = new VideoInfoCrawler();
            var videoInfo = await videoInfoCrawler.GetVideoInfo(contentUrl);

            var videoUrlGrabber = new VideoUrlCrawler();
            var videoUrl = await videoUrlGrabber.GetUrlBySingleContentId(videoInfo);

            var urlList = new List<string>();

            // Just a practice of LINQ...
            if (Settings.Default.PreferFlv)
                videoUrl.Durl.ForEach(url => urlList.Add(url.Url));
            else
                urlList.AddRange(
                    videoUrl.Durl.Where(url => url.BackupUrl != null && url.BackupUrl.Url.Count != 0)
                        .SelectMany(url => url.BackupUrl.Url));

            return urlList;
        }

        /// <summary>
        ///     Generate video links for all videos from a certain user.
        ///     This method may costs quite a long time. As a result, a IProgess is implemented to retrieve the status
        ///     Note:
        ///     1. UserAgent (MSIE10) and Referrer must be set to the downloader, otherwise server will return 500
        ///     2. Exception may be raised if something goes wrong (e.g. no video found)
        ///     3. Status reporting example is here:
        ///     https://stackoverflow.com/questions/19980112/how-to-do-progress-reporting-using-async-await/19980151#19980151
        ///     It's a two-element double array. The first is the page status and the second is the video status
        /// </summary>
        /// <param name="contentUrl"></param>
        /// <param name="progressStatus"></param>
        /// <returns></returns>
        private async Task<List<string>> GenerateUserVideoLink(string contentUrl, IProgress<double[]> progressStatus)
        {
            // BGet library related stuff
            var userVideoGrabber = new UserVideoGrabber();
            var userVideoResult = await userVideoGrabber.GetAllVideoFromUser(contentUrl);

            // Create a list to get all video links
            var urlList = new List<string>();

            for (var currentVideoPage = 0; currentVideoPage < userVideoResult.Count; currentVideoPage++)
            for (var currentVideo = 0;
                currentVideo < userVideoResult[currentVideoPage].UploadedVideo.VideoList.Count;
                currentVideo++)
            {
                var video = userVideoResult[currentVideoPage].UploadedVideo.VideoList[currentVideo];
                urlList.AddRange(await GenerateGeneralVideoLink(video.ContentId));

                // Report the status 
                progressStatus.Report(
                    new[]
                    {
                        currentVideoPage / (double) userVideoResult.Count,
                        currentVideo / (double) userVideoResult[currentVideoPage].UploadedVideo.VideoList.Count
                    });
            }

            return urlList;
        }
    }
}