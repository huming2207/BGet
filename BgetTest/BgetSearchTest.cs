using System.Threading.Tasks;
using BgetCore.Search;
using Xunit;
using static System.Decimal;

namespace BgetTest
{
    public class BgetSearchTest
    {

        /// <summary>
        /// Try get Dami's phone review (大米评测) videos
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RunDamiVideoSearch()
        {
            var bSearch = new Search();
            
            var damiResult =
                await bSearch.GetAllVideoInfoByKeyword("大米评测");

            Assert.NotNull(damiResult);
            Assert.NotEqual(damiResult.Count, Zero);
        }
    }
}