using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using BgetCore.Video;
using BgetCore.Video.VideoResult;

namespace BgetTest
{
    public class BgetVideoUrlTest
    {
        private VideoUrlCrawler _videoUrlCrawler = null;
        private VideoInfoCrawler _videoInfoCrawler = null;
        
        public BgetVideoUrlTest()
        {
            _videoUrlCrawler = new VideoUrlCrawler();
            _videoInfoCrawler = new VideoInfoCrawler();
        }

        [Fact]
        public async Task GetVideoUrlForFeelAgain()
        {
            var result = await _videoUrlCrawler.GetUrlBySingleContentId(
                await _videoInfoCrawler.GetVideoInfo("av349183"));
            
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Durl.Count);

            _PrintUrl(result);
        }
        
        [Fact]
        public async Task GetVideoUrlForJustTheWayYouAre()
        {
            var result = await _videoUrlCrawler.GetUrlBySingleContentId(
                await _videoInfoCrawler.GetVideoInfo("av3898141"));
            
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Durl.Count);

            _PrintUrl(result);
        }
        
        [Fact]
        public async Task GetVideoUrlForWhatMakesYouBeautiful()
        {
            var result = await _videoUrlCrawler.GetUrlBySingleContentId(
                await _videoInfoCrawler.GetVideoInfo("av3355175"));
            
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Durl.Count);

            _PrintUrl(result);
        }

        private void _PrintUrl(VideoUrl videoUrl)
        {
            foreach (var dataUrl in videoUrl.Durl)
            {
                if (dataUrl.BackupUrl != null)
                {
                    foreach (var backupUrl in dataUrl.BackupUrl.Url)
                    {
                        Console.WriteLine(string.Format("[TEST] Got backup URL {0} for video av349183.", backupUrl));
                    }
                }
                
                Console.WriteLine(string.Format("[TEST] Got main URL {0} for video av349183.", dataUrl.Url));
            }
        }
    }
}