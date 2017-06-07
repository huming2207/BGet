using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BgetCore.Search
{
    public class SearchResult
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("numResults")]
        public int NumResults { get; set; }

        [JsonProperty("numPages")]
        public int NumPages { get; set; }

        [JsonProperty("curPage")]
        public int CurPage { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("trackid")]
        public string TrackId { get; set; }

        [JsonProperty("html")]
        public string RawHtml { get; set; }
        
        public bool IsSuccessful { get; set; }
        
        public string RawTextIfNotSuccessful { get; set; }
    }
}
