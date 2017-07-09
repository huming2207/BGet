using Newtonsoft.Json;

namespace BgetCore.User.UserResult
{
    public class Nameplate
    {

        [JsonProperty("condition")]
        public string Condition { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("image_small")]
        public string ImageSmall { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nid")]
        public int Nid { get; set; }
    }

}
