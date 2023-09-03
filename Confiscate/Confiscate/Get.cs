using Confiscate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Confiscate
{
    class Get
    {
        public static async Task<object> SearchArtist(string artist, string token)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "q", artist },
                { "type", "artist"},
                { "market", "UA"},
                { "limit", "5"}
            };
            string responseBody = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                string url = BuildUrlWithQueryParameters(APIData.SpotifySearchURL, queryParameters);
                HttpResponseMessage response = await client.GetAsync(url);
                responseBody = await response.Content.ReadAsStringAsync();
            }
            return responseBody;
        }
        public static async Task<object> GetArtist(string id, string token)
        {
            string artistUrl = APIData.SpotifyArtistsURL + id;
            string responseBody = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage response = await client.GetAsync(artistUrl);
                responseBody = await response.Content.ReadAsStringAsync();
            }
            return responseBody;
        }
        public static async Task<string> GetArtistTopTracks(string id, string token)
        {
            string artistUrl = APIData.SpotifyArtistsURL + id + "/top-tracks?market=UA";
            string responseBody = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage response = await client.GetAsync(artistUrl);
                responseBody = await response.Content.ReadAsStringAsync();
            }
            return responseBody;
        }
        public static async Task<string> GetArtistAlbums(string id, string amount, string token)
        {
            string artistUrl;
            if (Convert.ToInt32(amount) == 0)
            {
                artistUrl = APIData.SpotifyArtistsURL + id + $"/albums?include_groups=album&market=UA";
            }
            else
            {
                artistUrl = APIData.SpotifyArtistsURL + id + $"/albums?include_groups=album&market=UA&limit={amount}";
            }
            string responseBody = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage response = await client.GetAsync(artistUrl);
                responseBody = await response.Content.ReadAsStringAsync();
            }
            return responseBody;
        }
        public static async Task<string> GetArtistSingles(string id, string amount, string token)
        {
            string artistUrl;
            if (Convert.ToInt32(amount) == 0)
            {
                artistUrl = APIData.SpotifyArtistsURL + id + $"/albums?include_groups=single&market=UA";
            }
            else
            {
                artistUrl = APIData.SpotifyArtistsURL + id + $"/albums?include_groups=single&market=UA&limit={amount}";
            }
            string responseBody = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage response = await client.GetAsync(artistUrl);
                responseBody = await response.Content.ReadAsStringAsync();
            }
            return responseBody;
        }
        private static string BuildUrlWithQueryParameters(string baseUrl, Dictionary<string, string> parameters)
        {
            var formContent = new FormUrlEncodedContent(parameters);
            string queryString = formContent.ReadAsStringAsync().Result;
            return baseUrl + "?" + queryString;
        }

    }
}