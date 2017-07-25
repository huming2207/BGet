using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using BgetCore.User.UserResult;

namespace BgetCore.User
{
    public class UserVideoGrabber
    {
        public async Task<List<UserVideo>> GetAllVideoFromUser(string userId, string keyword = "")
        {
            // Set the initial page to 1
            var currentPage = 1;
            
            var userVideoList = new List<UserVideo>();
            var userVideo = await GetVideoFromUser(userId, 100, currentPage, keyword);
            var totalPages = userVideo.UploadedVideo.Pages;

            // "Iterate" the user video page
            while (userVideo.Status && currentPage <= totalPages)
            {
                userVideoList.Add(userVideo);

                // Set to next page
                userVideo = await GetVideoFromUser(userId, 100, ++currentPage, keyword);
            }

            return userVideoList;
        }

        public async Task<UserVideo> GetVideoFromUser(string userId, int pageSize = 30, int page = 1, string keyword = "")
        {
            // If user inputs a user URL then only ID should be reserved.
            // Normally the format is "http(s)://space.bilibili.com/123456",
            // Remove the "http://" and "https://" then do a split to get the User ID
            if (userId.Contains("http"))
            {
                userId = userId.Replace("http://", "").Replace("https://", "").Split('/')[1];
            }

            // Declare HTTP client
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://space.bilibili.com/"),
            };

            // Set referrer in case HTTP 400 when accessing User APIs
            httpClient.DefaultRequestHeaders.Referrer = new Uri(string.Format("http://space.bilibili.com/{0}", userId));

            // Force using Internet Explorer 10's user agent to get the flash version instead of pure HTML5 version
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (MSIE 10.0; Windows NT 6.1; Trident/5.0)");

            // Fire the hole!
            var responseJson = await httpClient.GetStringAsync(
                $"ajax/member/getSubmitVideos?mid={userId}&pagesize={pageSize.ToString()}" +
                $"&tid=0&page={page.ToString()}&keyword={keyword}&order=senddate" +
                $"&_={DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()}");

            // Convert to object
            var jsonObject = JsonConvert.DeserializeObject<UserVideo>(responseJson);

            // Include JSON string if not successful.
            if(jsonObject.Status)
            {
                return jsonObject;
            }
            else
            {
                jsonObject.RawJsonIfNotSuccessful = responseJson;
                return jsonObject;
            }

        }
    }
}
