using System.Threading.Tasks;
using BgetCore.Search;
using Xunit;

namespace BgetTest
{
    public class BgetSearchesTest
    {
        private Search _search;
        
        public BgetSearchesTest()
        {
            _search = new Search();
        }

        [Fact]
        public async Task BgetUserSearchWithExactKeywordTest()
        {
            var userSearchResult = await _search.GetAllUserIdFromKeyword("大米评测");
            
            Assert.NotNull(userSearchResult);
            Assert.True(userSearchResult.Count > 0);
        }

        [Fact]
        public async Task BgetUserSearchWithUnsureKeywordTest()
        {
            var userSearchResult = await _search.GetAllUserIdFromKeyword("局座");

            Assert.NotNull(userSearchResult);
            Assert.True(userSearchResult.Count > 0);
        }

        [Fact]
        public async Task BgetVideoSearchTest()
        {
            var videoSearchResult = await _search.GetAllVideoInfoByKeyword("局座");
            
            Assert.NotNull(videoSearchResult);
            Assert.True(videoSearchResult.Count > 0);
        }
        
    }
}