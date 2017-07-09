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
            int currentPage = 1;
            var userVideoList = new List<UserVideo>();
            var userVideo = await GetVideoFromUser(userId, 100, currentPage, keyword);

            // "Iterate" the user video page
            while(userVideo.Status)
            {
                userVideoList.Add(userVideo);

                // Set to next page
                userVideo = await GetVideoFromUser(userId, 100, ++currentPage, keyword);
            }

            return userVideoList;
        }

        public async Task<UserVideo> GetVideoFromUser(string userId, int pageSize = 30, int page = 1, string keyword = "")
        {
            // Declare HTTP client
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://space.bilibili.com/ajax"),
            };

            // Set referrer in case HTTP 400 when accessing User APIs
            httpClient.DefaultRequestHeaders.Referrer = new Uri(string.Format("http://space.bilibili.com/{0}", userId));

            // Fire the hole!
            var responseJson = await httpClient.GetStringAsync(
                string.Format("/member/getSubmitVideos?mid={0}&pagesize={1}&tid=0&page={2}&keyword={3}&order=senddate&_={4}", 
                                userId, 
                                pageSize.ToString(), 
                                page.ToString(), 
                                keyword, 
                                DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()));

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
