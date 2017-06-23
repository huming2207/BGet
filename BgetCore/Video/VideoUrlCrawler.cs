using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Diagnostics;
using BgetCore.Util;

namespace BgetCore.Video
{
    public class VideoUrlCrawler
    {
        // From here, thx mate lol:
        //     https://github.com/soimort/you-get/blob/develop/src/you_get/extractors/bilibili.py#L15
        private const string MagicKey = "1c15888dc316e05a15fdd0a02ed6584f";
        
        public async Task<VideoUrl> GetUrlBySingleContentId(VideoInfo videoInfo)
        {

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://interface.bilibili.com")
            };
            
            // Set referrer (seems to be enough as you-get did the same thing lol, need to be tested later on)
            httpClient.DefaultRequestHeaders.Referrer = new Uri(videoInfo.VideoPage);
            
            // Now follows the you-get project and do some magic.
            string magicSignature =
                Md5Gen.GetMD5(string.Format("cid={0}&from=miniplay&player=1{1}", videoInfo.ContentId, MagicKey));

            string queryPath = string.Format("/playurl?cid={0}&from=miniplay&player=1&sign={1}", 
                videoInfo.ContentId, magicSignature);

            Debug.WriteLine("[DEBUG] URL got https://interface.bilibili.com" + queryPath);

            // Get XML from their API
            string rawVideoXml = await httpClient.GetStringAsync(queryPath);
            if (rawVideoXml == null) throw new ArgumentNullException(nameof(rawVideoXml));

            // Deserialize XML into VideoUrl object
            // Here is a very good example: 
            //    https://stackoverflow.com/questions/10518372/how-to-deserialize-xml-to-object
            var xmlDeserializer = new XmlSerializer(typeof(VideoUrl));
            var textReader = new StringReader(rawVideoXml);
            var videoUrl = (VideoUrl) xmlDeserializer.Deserialize(textReader);

            httpClient.Dispose();
            return videoUrl;
        }
    }
}