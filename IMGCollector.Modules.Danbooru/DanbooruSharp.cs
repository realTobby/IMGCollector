using IMGCollector.Core;
using IMGCollector.Modules.Danbooru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCollector.Modules.Danbooru
{
    public class DanbooruSharp
    {
        private string baseEndpoint = "https://danbooru.donmai.us/";
        private string apiKey = "pp7txHP5VerjmnxroThXFFyg";
        private string loginName = "theTobby";

        public DanbooruSharp(string apiKey, string loginName)
        {
            this.apiKey = apiKey;
            this.loginName = loginName;
        }

        public List<DanbooruPostModel> GetPopularImages(DateTime date)
        {
            string endpointToCall = baseEndpoint + "explore/posts/popular.json?";
            endpointToCall = AuthenticateRequest(endpointToCall);
            List<DanbooruPostModel> pM = DanbooruPostModel.FromJson(APITools.GetJSON(endpointToCall)).ToList();
            return pM;
        }

        private string AuthenticateRequest(string unauthorizedRequest)
        {
            if (unauthorizedRequest.Contains("?"))
            {
                return unauthorizedRequest + "&login:" + loginName + "&api_key:" + apiKey;
            }
            else
            {
                return unauthorizedRequest + "?login:" + loginName + "&api_key:" + apiKey;
            }
        }

    }
}
