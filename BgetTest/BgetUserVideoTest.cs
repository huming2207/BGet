using System.Threading.Tasks;
using BgetCore.User;
using Xunit;

namespace BgetTest
{
    public class BgetUserVideoTest
    {
        private UserVideoGrabber _userVideoGrabber;

        public BgetUserVideoTest()
        {
            _userVideoGrabber = new UserVideoGrabber();
        }

        [Fact]
        public async Task BgetValidUserVideoListTest()
        {
            // 大米评测, a phone review channel.
            // URL: http://space.bilibili.com/8372353
            var resultList = await _userVideoGrabber.GetAllVideoFromUser("8372353");

            Assert.NotNull(resultList);
            Assert.True(resultList.Count > 0);

            // See if each result is correct
            foreach (var result in resultList)
            {
                Assert.True(result.Status);
            }
        }

        [Fact]
        public async Task BgetInvalidUserVideoListTest()
        {
            // ...just a fake ID
            var result = await _userVideoGrabber.GetAllVideoFromUser("99999999999999");

            Assert.NotNull(result);
            Assert.True(!result[0].Status);
        }
    }
}