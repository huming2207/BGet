using Newtonsoft.Json;

namespace BgetCore.User.UserResult
{
    public class OfficialVerify
    {

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }
    }

}
