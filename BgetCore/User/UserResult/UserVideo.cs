using Newtonsoft.Json;

namespace BgetCore.User.UserResult
{

    public class UserVideo
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public UploadedVideo UploadedVideo { get; set; }

        public string RawJsonIfNotSuccessful { get; set; }
    }
}
