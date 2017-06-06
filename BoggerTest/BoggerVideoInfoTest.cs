using System;
using Xunit;
using BoggerCore;

namespace BoggerTest
{
    public class BoggerVideoInfoTest
    {
        private VideoInfoCrawler videoInfoCrawler = null;

        public BoggerVideoInfoTest()
        {
            videoInfoCrawler = new VideoInfoCrawler();
        }

        [Fact]
        public void ValidVideoInfoTest()
        {
            // Video URL: http://www.bilibili.com/video/av349183/
            // "Onepublic - Feel Again MV", Content ID 540559
            var videoInfoForAv349183 = videoInfoCrawler.GetVideoInfo("av349183").Result;
            Assert.NotNull(videoInfoForAv349183);
            Assert.True(videoInfoForAv349183.ContentId.Equals("540559"));
            Console.Read();
        }
    }
}