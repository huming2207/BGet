using System.Collections.Generic;
using Newtonsoft.Json;

namespace BgetCore.User.UserResult
{
    public class UserData
    {

        [JsonProperty("DisplayRank")]
        public string DisplayRank { get; set; }

        [JsonProperty("approve")]
        public bool Approve { get; set; }

        [JsonProperty("article")]
        public int Article { get; set; }

        [JsonProperty("attention")]
        public int Attention { get; set; }

        [JsonProperty("attentions")]
        public List<int> Attentions { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("coins")]
        public int Coins { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("face")]
        public string Face { get; set; }

        [JsonProperty("fans")]
        public int Fans { get; set; }

        [JsonProperty("friend")]
        public int Friend { get; set; }

        [JsonProperty("im9_sign")]
        public string Im9Sign { get; set; }

        [JsonProperty("level_info")]
        public LevelInfo LevelInfo { get; set; }

        [JsonProperty("mid")]
        public string Mid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameplate")]
        public Nameplate Nameplate { get; set; }

        [JsonProperty("official_verify")]
        public OfficialVerify OfficialVerify { get; set; }

        [JsonProperty("pendant")]
        public Pendant Pendant { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("playNum")]
        public int PlayNum { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("regtime")]
        public int Regtime { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }

        [JsonProperty("spacesta")]
        public int Spacesta { get; set; }

        [JsonProperty("theme")]
        public string Theme { get; set; }

        [JsonProperty("theme_preview")]
        public string ThemePreview { get; set; }

        [JsonProperty("toutu")]
        public string Toutu { get; set; }

        [JsonProperty("toutuId")]
        public int ToutuId { get; set; }
    }

}
