using Newtonsoft.Json;

namespace BgetCore.User.UserResult
{
    public class UserInfo
    {

        [JsonProperty("data")]
        public UserData UserData { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        public string RawJsonIfNotSuccessful { get; set; }
    }

}
