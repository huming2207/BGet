using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BgetCore.User
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

    public class OfficialVerify
    {

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }
    }

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

    public class Data
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

    public class UserInfo
    {

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        public bool IsSuccessful { get; set; }
        public string rawJsonIfNotSuccessful { get; set; }
    }

}
