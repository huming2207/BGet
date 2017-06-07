using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using BgetCore.Video;

namespace BgetCore.Search
{
    public class Search
    {
        public async Task<List<VideoInfo>> GetAllVideoInfoByKeyword(string keyword)
        {

        }

        public async Task<List<int>> GetAllUserIdFromKeyword(string keyword)
        {

        }

        private async Task<SearchResult> _GetAllVideoByKeyword(string keyword)
        {
            /* 
             * Search API format: http://search.bilibili.com/ajax_api/video?keyword=keywordsB&page=10&order=totalrank&_=1496810721252 
                    
                    Parameter: - keyword: keyword (encoded string)
                               - page: page number (unsigned int)
                               - order: keep "totalrank" since it works... (string)
                               - _: unix timestamp in ms (unsigned int64?)
             
             */
        }

        private async Task<SearchResult> _GetAllUserByKeyword(string keyword)
        {
            /* 
             * Search API format: http://search.bilibili.com/ajax_api/upuser?keyword=keywordsB&page=10&order=totalrank&_=1496810721252 

                    Parameter: - keyword: keyword (encoded string)
                               - page: page number (unsigned int)
                               - order: keep "totalrank" since it works... (string)
                               - _: unix timestamp in ms (unsigned int64?)

             */
        }







    }
}
