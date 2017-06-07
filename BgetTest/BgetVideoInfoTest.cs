using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using BgetCore;


namespace BgetTest
{
    public class BgetVideoInfoTest
    {
        private VideoInfoCrawler videoInfoCrawler = null;

        public BgetVideoInfoTest()
        {
            videoInfoCrawler = new VideoInfoCrawler();
        }

        [Fact]
        public async Task WhatMakesYouBeautifulMvTest()
        {
            // Video URL: http://www.bilibili.com/video/av3355175/
            // Video is "What makes you beautiful", Content ID 5309628
            Console.WriteLine("[TEST] Running test for av3355175...");
            var videoInfoForAv3355175 = await videoInfoCrawler.GetVideoInfo("av3355175");

            // Validate the beautiful result and see if it correct content ID
            Assert.NotNull(videoInfoForAv3355175);
            Assert.True(videoInfoForAv3355175.ContentId.Equals("5309628"));
        }

        [Fact]
        public async Task FeelAgainMvTest()
        {
            // Video URL: http://www.bilibili.com/video/av349183/
            // Video is "OnePublic - Feel Again", Content ID 540559
            Console.WriteLine("[TEST] Running test for av349183...");
            var videoInfoForAv349183 = await videoInfoCrawler.GetVideoInfo("av349183");

            // Now it should "feels again", at least not null lol.
            Assert.NotNull(videoInfoForAv349183);
            Assert.True(videoInfoForAv349183.ContentId.Equals("540559"));
        }

        [Fact]
        public async Task JustTheWayYouAreMvTest()
        {
            // Video URL: http://www.bilibili.com/video/av3898141/
            // Video is "Just the way you are", Content ID 6270102
            Console.WriteLine("[TEST] Running test for av3898141...");
            var videoInfoForAv3898141 = await videoInfoCrawler.GetVideoInfo("av3898141");
            Assert.NotNull(videoInfoForAv3898141);
            Assert.True(videoInfoForAv3898141.ContentId.Equals("6270102"));
        }


        [Fact]
        public void InvalidVideoInfoTest()
        {
            Console.WriteLine("[TEST] Running test for a video which does not exist...");
            var exception = Record.Exception(() => videoInfoCrawler.GetVideoInfo("foobar").Result);
            Assert.IsType(typeof(HttpRequestException), exception.InnerException);
        }
    }
}