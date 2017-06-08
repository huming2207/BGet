using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BgetCore.Video
{
    public class VideoInfoCrawler
    {
        public async Task<VideoInfo> GetVideoInfo(string inputVideo)
        {
            var htmlDoc = new HtmlDocument();
            var rawHtml = await _GetRawHtmlPage(inputVideo);
            htmlDoc.LoadHtml(rawHtml);

            var videoId = string.Empty;

            if(inputVideo.Contains("bilibili.com/video/av"))
            {
                // If the inputVideo is a video URL, then get the last item (Video ID) from video URL
                videoId = inputVideo.Split('/')[inputVideo.Split('/').Length - 1];
            }
            else
            {
                // If the inputVideo (seems to be) already a video ID, then pass the video ID directly
                videoId = inputVideo;
            }

            return new VideoInfo()
            {
                ContentId = _GetVideoContentId(rawHtml),
                VideoId = videoId,
                Description = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name=\"description\"]").Attributes["content"].Value,
                Tags = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name=\"keywords\"]").Attributes["content"].Value.Split(','),
                Author = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name=\"author\"]").Attributes["content"].Value,
                Title = htmlDoc.DocumentNode.SelectSingleNode("//div[@class=\"v-title\"]/h1").InnerText
            };
        }

        private string _GetVideoContentId(string rawHtml)
        {
            string htmlLineBuffer = string.Empty;
            
            // Here we can't use HtmlAgilityPack to parse HTML because some video pages seem to be different from others.
            // So a better solution is, parse the HTML string manually.
            // <script type='text/javascript'>EmbedPlayer('player', "//static.hdslb.com/play.swf", "cid=15430504&aid=9337458&pre_ad=0");</script>
            var stringReader = new StringReader(rawHtml);
            string cidStr = string.Empty;
            while ((htmlLineBuffer = stringReader.ReadLine()) != null)
            {
                if (htmlLineBuffer.Contains("cid") && htmlLineBuffer.Contains("swf"))
                {
                    cidStr = htmlLineBuffer.Split('\"')[3] // Get "cid=15430504&aid=9337458&pre_ad=0"
                        .Split('&')[0]                     // Get "cid=15430504"
                        .Split('=')[1];                    // Get "1543054"
                    
                    stringReader.Dispose();
                    
                    return cidStr;
                }
            }

            // If CID not found, return an empty string.
            stringReader.Dispose();
            return cidStr;
        }

        private async Task<string> _GetRawHtmlPage(string inputVideo)
        {
            var httpClient = new HttpClient();

            // If the video ID contains the URL. If it has, don't set the base address again.
            if (!inputVideo.Contains("bilibili.com/video/av"))
            {
                httpClient.BaseAddress = new Uri("https://www.bilibili.com/video/");
            }

            // Force using Internet Explorer 10's user agent to get the flash version instead of HTML5 version
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (MSIE 10.0; Windows NT 6.1; Trident/5.0)");

            // Get the HTML string
            string rawHtml = await httpClient.GetStringAsync(inputVideo);
            httpClient.Dispose();
            return rawHtml;
        }
    }
}