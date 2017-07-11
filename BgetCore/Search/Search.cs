using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using HtmlAgilityPack;
using BgetCore.Video;
using BgetCore.User;
using BgetCore.User.UserResult;

namespace BgetCore.Search
{
    public class Search
    {
        public async Task<List<VideoInfo>> GetAllVideoInfoByKeyword(string keyword, bool deepSearch = false)
        {
            // Set initial page from page 1
            var pageCount = 1;
            var videoInfoList = new List<VideoInfo>();
            var searchResult = await SearchByKeyword(keyword, SearchType.Video, pageCount);

            // "Iterate" the pages
            while (searchResult.IsSuccessful)
            {
                // Parse HTML content
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(searchResult.RawHtml);

                // Get Video URLs and titles
                var videoTitleAndUrlNodes = htmlDoc.DocumentNode.SelectNodes("//a[@class=\"title\"]");

                // Deep search will (wayyyyyyy much) slower the fetching speed, so it's not recommend unless tag is required.
                if (deepSearch)
                {
                    var videoInfoCrawler = new VideoInfoCrawler();

                    // The URL does not have its initial "https:", instead it starts with "//www.bilibili.com".
                    // As a result, append a "https:" before it before further crawling.
                    foreach (var singleNode in videoTitleAndUrlNodes)
                    {
                        var url = string.Format("https:{0}", singleNode.Attributes["href"].Value);
                        videoInfoList.Add(await videoInfoCrawler.GetVideoInfo(url));
                    }
                }
                else
                {
                    // Get video descriptions
                    var videoDescriptionNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class=\"des hide\"]");

                    // Get video uploaders
                    var videoUploaderNodes = htmlDoc.DocumentNode.SelectNodes("//a[@class=\"up-name\"]");

                    // Iterate all video info from result (without tags)
                    videoInfoList.AddRange(videoDescriptionNodes.Select((t, videoCountIndex) => new VideoInfo
                    {
                        // Extract author ID from URL
                        Author = Regex.Match(videoUploaderNodes[videoCountIndex].Attributes["href"].Value,
                            @"\/(\d+)\?").Groups[1].Value,

                        // Extract description, new lines and tabs are removed
                        Description = Regex.Match(t.InnerText, "[^\t|\n|\r]+").Value,

                        // Extract video URL
                        VideoPage = string.Format("https:{0}",
                            videoTitleAndUrlNodes[videoCountIndex].Attributes["href"].Value),

                        // Extract video ID
                        ContentId = Regex.Match(videoTitleAndUrlNodes[videoCountIndex].Attributes["href"].Value,
                            @"\/av(\d+)\?").Groups[1].Value,

                        // Extract title
                        Title = videoTitleAndUrlNodes[videoCountIndex].Attributes["title"].Value
                    }));
                }

                // Update page count and move to next page...
                searchResult = await SearchByKeyword(keyword, SearchType.Video, ++pageCount);
            }

            return videoInfoList;
        }

        public async Task<List<UserInfo>> GetAllUserIdFromKeyword(string keyword)
        {
            // Set initial page from page 1
            var pageCount = 1;
            var userInfoList = new List<UserInfo>();
            var searchResult = await SearchByKeyword(keyword, SearchType.UpUser, pageCount);

            // "Iterate" the pages
            while (searchResult.IsSuccessful)
            {
                // Parse HTML content
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(searchResult.RawHtml);

                // Get URLs
                var userIdNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class=\"attention-btn js-attention-user\"]");
                var userInfoGrabber = new UserInfoGrabber();

                // Iterate the nodes
                foreach (var singleNode in userIdNodes)
                {
                    var userId = singleNode.Attributes["data-id"].Value;
                    userInfoList.Add(await userInfoGrabber.GetUserInfoById(userId));
                }

                // Update page count and move to next page...
                searchResult = await SearchByKeyword(keyword, SearchType.Video, ++pageCount);
            }

            return userInfoList;
        }

        private async Task<SearchResult> SearchByKeyword(string keyword, SearchType searchType, int searchPage = 1)
        {
            /* 
             * Search API format: 
                     http://search.bilibili.com/ajax_api/
                         [video|upuser|bangumi|pgc|live|special|topic|drawyoo]?keyword=keywords
                         &page=1&order=totalrank&_=time 
                    
                    Parameter: - keyword: keyword (encoded string)
                               - page: page number (unsigned int)
                               - order: keep "totalrank" since it works... (string)
                               - _: unix timestamp in ms (unsigned int64?)
             
             */

            // Declare HTTP Client handler to ignore any 301 responses...
            // Workaround for: https://github.com/huming2207/BGet/commit/c0dd7aba9dea00539bb26e289431f9e93b4fcfa2
            var httpClientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };

            // Declare HTTP client, so far not necessary to set UA and/or Referrer
            // C# System.Net.HttpClient need to set a slash at the end of the BaseAddress URL to prevent some issues
            //      Detail: https://stackoverflow.com/questions/23438416/why-is-httpclient-baseaddress-not-working
            var httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri("http://search.bilibili.com/ajax_api/"),
            };

            // Force using Internet Explorer 10's user agent to get the flash version instead of pure HTML5 version
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (MSIE 10.0; Windows NT 6.1; Trident/5.0)");


            // Get the raw JSON from API
            string responseResultJson;

            switch (searchType)
            {
                case SearchType.Bangumi:
                {
                    httpClient.DefaultRequestHeaders.Referrer =
                        new Uri(string.Format(
                            "http://search.bilibili.com/ajax_api/bangumi?keyword={0}&page={1}&order=totalrank&_={2}",
                            keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));

                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("bangumi?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Drawyoo:
                {
                    httpClient.DefaultRequestHeaders.Referrer =
                        new Uri(string.Format(
                            "http://search.bilibili.com/ajax_api/drawyoo?keyword={0}&page={1}&order=totalrank&_={2}",
                            keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));

                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("drawyoo?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Live:
                {
                    httpClient.DefaultRequestHeaders.Referrer =
                        new Uri(string.Format(
                            "http://search.bilibili.com/ajax_api/live?keyword={0}&page={1}&order=totalrank&_={2}",
                            keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));

                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("live?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Pgc:
                {
                    httpClient.DefaultRequestHeaders.Referrer =
                        new Uri(string.Format(
                            "http://search.bilibili.com/ajax_api/pgc?keyword={0}&page={1}&order=totalrank&_={2}",
                            keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));

                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("pgc?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Special:
                {
                    httpClient.DefaultRequestHeaders.Referrer =
                        new Uri(string.Format(
                            "http://search.bilibili.com/ajax_apispecial?keyword={0}&page={1}&order=totalrank&_={2}",
                            keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));

                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("special?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Topic:
                {
                    httpClient.DefaultRequestHeaders.Referrer =
                        new Uri(string.Format(
                            "http://search.bilibili.com/ajax_api/topic?keyword={0}&page={1}&order=totalrank&_={2}",
                            keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));

                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("topic?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.UpUser:
                {
                    httpClient.DefaultRequestHeaders.Referrer =
                        new Uri(string.Format(
                            "http://search.bilibili.com/ajax_api/upuser?keyword={0}&page={1}&order=totalrank&_={2}",
                            keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));

                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("upuser?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Video:
                {
                    httpClient.DefaultRequestHeaders.Referrer =
                        new Uri(string.Format(
                            "http://search.bilibili.com/ajax_api/video?keyword={0}&page={1}&order=totalrank&_={2}",
                            keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));

                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("video?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));

                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(searchType), searchType, null);
                }
            }

            // If the search is unsuccessful, return the object with correct information
            if (responseResultJson.Contains("没有相关数据") || responseResultJson.Contains("text"))
            {
                var invalidSearchResult = new SearchResult()
                {
                    IsSuccessful = false,
                    RawTextIfNotSuccessful = responseResultJson
                };

                httpClient.Dispose();
                return invalidSearchResult;
            }
            else
            {
                // Deserialize the object into SearchResult and return.
                var jsonObject = JsonConvert.DeserializeObject<SearchResult>(responseResultJson,
                    new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                jsonObject.IsSuccessful = true;
                jsonObject.RawTextIfNotSuccessful = string.Empty;

                httpClient.Dispose();
                return jsonObject;
            }
        }
    }
}