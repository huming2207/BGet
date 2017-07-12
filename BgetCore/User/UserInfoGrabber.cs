using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using BgetCore.User.UserResult;
using Newtonsoft.Json;

namespace BgetCore.User
{
    public class UserInfoGrabber
    {
        public async Task<UserInfo> GetUserInfoById(string userId)
        {
            // Declare HTTP client
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://space.bilibili.com/"),
                DefaultRequestHeaders = {Referrer = new Uri(string.Format("http://space.bilibili.com/{0}?from=search", userId))}
            };

            // Force using Internet Explorer 10's user agent to get the flash version instead of pure HTML5 version
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (MSIE 10.0; Windows NT 6.1; Trident/5.0)");

            // Referrer must be correctly set, otherwise they will reply HTTP 400.

            // POST content has two args, one is User ID (mid) 
            //      and the other is cross-site reference (csrf).
            // Cross-site reference seems hasn't been implemented yet, so just leave it blank instead.
            var postContent = new StringContent(string.Format("mid={0}", userId), Encoding.UTF8, "application/x-www-form-urlencoded");

            // Fire the hole!
            var postResponse = await httpClient.PostAsync("ajax/member/GetInfo", postContent);

            // Detect if successful or not
            // It's not efficient to parse JSON here, so just use string.Contains() instead.
            // ...actually I'm just too lazy to do so...
            if (!postResponse.IsSuccessStatusCode || 
                postResponse.Content.ReadAsStringAsync().Result.Contains("\"status\": false"))
            {
                httpClient.Dispose();
                return new UserInfo()
                {
                    Status = false,
                    RawJsonIfNotSuccessful = await postResponse.Content.ReadAsStringAsync()
                };
            }
            else
            {
                // If successful, parse JSON and return object
                var rawJson = await postResponse.Content.ReadAsStringAsync();
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(rawJson);
                httpClient.Dispose();
                return userInfo;
            }
        }
    }
}
