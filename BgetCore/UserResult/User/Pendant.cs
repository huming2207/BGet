using Newtonsoft.Json;

namespace BgetCore.User.UserResult
{
    public class Pendant
    {

        [JsonProperty("expire")]
        public int Expire { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pid")]
        public int Pid { get; set; }
    }

}
