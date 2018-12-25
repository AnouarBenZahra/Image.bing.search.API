using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Image.bing.search.API
{
    public class Search
    {
        const string key = "enter your key here";
        const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/images/search";

        static SearchResult BingImageSearch(string searchInput)
        {
            var uri = uriBase + "?q=" + Uri.EscapeDataString(searchInput);
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.Headers["Ocp-Apim-Subscription-Key"] = key;
            HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponseAsync().Result;
            string json = new StreamReader(httpWebResponse.GetResponseStream()).ReadToEnd();

            var searchResult = new SearchResult()
            {
                jsonResult = json,
                relevantHeaders = new Dictionary<String, String>()
            };
            
            foreach (String header in webRequest.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.relevantHeaders[header] = webRequest.Headers[header];
            }
            return searchResult;
        }

        struct SearchResult
        {
            public String jsonResult;
            public Dictionary<String, String> relevantHeaders;
        }

    }

}
