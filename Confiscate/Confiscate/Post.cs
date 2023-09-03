using Confiscate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Confiscate
{
    public class Post
    {
        public static async Task<object> PostForAccessToken()
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", APIData.ClientID),
                    new KeyValuePair<string, string>("client_secret", APIData.ClientSecret)
                });

                HttpResponseMessage response = await client.PostAsync(APIData.SpotifyTokenURL, content);

                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            } 
        }
    }
}



