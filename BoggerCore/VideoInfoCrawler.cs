using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BoggerCore
{
    public class VideoInfoCrawler
    {
        public async Task<VideoInfo> GetVideoInfo(string avId)
        {
            var htmlDoc = new HtmlDocument();
            string rawHtml = await _GetRawHtmlPage(avId);
            htmlDoc.LoadHtml(rawHtml);

            return new VideoInfo()
            {
                ContentId = _GetVideoContentId(rawHtml),
                Description = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name=\"description\"]").Attributes["content"].Value,
                Tags = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name=\"keywords\"]").Attributes["content"].Value.Split(','),
                Author = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name=\"author\"]").Attributes["content"].Value,
                Title = htmlDoc.DocumentNode.SelectSingleNode("//div[@class=\"v-title\"]/h1").InnerText
            };
        }

        private string _GetVideoContentId(string rawHtml)
        {
            string htmlLineBuffer = string.Empty;
            
            // Find out the line with these contents:
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

        private async Task<string> _GetRawHtmlPage(string avId)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://www.bilibili.com/video/"),
            };

            // Force using Internet Explorer 10's user agent to get the flash version instead of HTML5 version
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (MSIE 10.0; Windows NT 6.1; Trident/5.0)");

            // Get the HTML string
            string rawHtml = await httpClient.GetStringAsync(avId);
            httpClient.Dispose();
            return rawHtml;
        }
    }
}