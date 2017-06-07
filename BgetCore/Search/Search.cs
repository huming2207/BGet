﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;
using HtmlAgilityPack;
using BgetCore.Video;

namespace BgetCore.Search
{
    public class Search
    {
        public async Task<List<VideoInfo>> GetAllVideoInfoByKeyword(string keyword)
        {
            // Set initial page from page 1
            int pageCount = 1;
            var videoInfoList = new List<VideoInfo>();
            var searchResult = await SearchByKeyword(keyword, SearchType.Video, pageCount);

            // "Iterate" the pages
            while (searchResult.IsSuccessful)
            {
                // Parse HTML content
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(searchResult.RawHtml);

                // Get URLs
                var videoTitleAndUrlNodes = htmlDoc.DocumentNode.SelectNodes("//a[@class=\"title\"]");
                var videoInfoCrawler = new VideoInfoCrawler();

                // The URL does not have its initial "https:", instead it starts with "//www.bilibili.com".
                // As a result, append a "https:" before it before further crawling.
                foreach (var singleNode in videoTitleAndUrlNodes)
                {
                    var url = string.Format("https:{0}", singleNode.Attributes["href"].Value);
                    videoInfoList.Add(await videoInfoCrawler.GetVideoInfo(url));
                }
                
                // Update page count and move to next page...
                searchResult = await SearchByKeyword(keyword, SearchType.Video, ++pageCount);
            }

            return videoInfoList;
        }

        public async Task<List<int>> GetAllUserIdFromKeyword(string keyword)
        {

        }

        public async Task<SearchResult> SearchByKeyword(string keyword, SearchType searchType, int searchPage = 1)
        {
            /* 
             * Search API format: 
                     http://search.bilibili.com/ajax_api/[video|upuser|bangumi|pgc|live|special|topic|drawyoo]?keyword=keywordsB&page=1&order=totalrank&_=time 
                    
                    Parameter: - keyword: keyword (encoded string)
                               - page: page number (unsigned int)
                               - order: keep "totalrank" since it works... (string)
                               - _: unix timestamp in ms (unsigned int64?)
             
             */

            // Declare HTTP client, so far not necessary to set UA and/or Referrer
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://search.bilibili.com/ajax_api")
            };
            
            // Get the raw JSON from API
            string responseResultJson = string.Empty;
            
            switch (searchType)
            {
                case SearchType.Bangumi:
                {
                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("/bangumi?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Drawyoo:
                {
                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("/drawyoo?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Live:
                {
                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("/live?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Pgc:
                {
                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("/pgc?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Special:
                {
                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("/special?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Topic:
                {
                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("/topic?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.UpUser:
                {
                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("/upuser?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
                            searchPage.ToString(), DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                    break;
                }
                case SearchType.Video:
                {
                    responseResultJson = await httpClient.GetStringAsync(
                        string.Format("/video?keyword={0}&page={1}&order=totalrank&_={2}", keyword,
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