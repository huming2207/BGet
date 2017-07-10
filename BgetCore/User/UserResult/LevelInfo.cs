using Newtonsoft.Json;

namespace BgetCore.User.UserResult
{
    public class LevelInfo
    {

        [JsonProperty("current_exp")]
        public int CurrentExp { get; set; }

        [JsonProperty("current_level")]
        public int CurrentLevel { get; set; }

        [JsonProperty("current_min")]
        public int CurrentMin { get; set; }

        [JsonProperty("next_exp")]
        public string NextExp { get; set; }
    }

}
