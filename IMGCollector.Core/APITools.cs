using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IMGCollector.Core
{
    public static class APITools
    {
        public static string GetJSON(string endpoint)
        {
            string jsonResult = "";
            using (WebClient wc = new WebClient())
            {
                jsonResult = wc.DownloadString(endpoint);
            }
            return jsonResult;
        }
    }
}
