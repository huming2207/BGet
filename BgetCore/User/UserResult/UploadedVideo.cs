using System.Collections.Generic;
using Newtonsoft.Json;

namespace BgetCore.User.UserResult
{
    public class UploadedVideo
    {
        [JsonProperty("vlist")]
        public List<VideoList> VideoList { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }
    }
}
