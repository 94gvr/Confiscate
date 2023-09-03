using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Confiscate;
using System.Windows.Forms;

namespace Confiscate
{
    internal class Parse
    {
        public static Dictionary<string, (string Name, string ImageUrl, string Id)> ParseSearchArtistsInfo (string jsonResponse)
        {
            JObject responseObj = JObject.Parse(jsonResponse);
            JArray artists = (JArray)responseObj["artists"]["items"];

            Dictionary<string, (string Name, string ImageUrl, string Id)> artistsDictionary = new Dictionary<string, (string, string, string)>();

            foreach (var artist in artists)
            {
                string name = (string)artist["name"];
                string id = (string)artist["id"];

                JArray images = (JArray)artist["images"];
                string imageUrl = images.Count > 0 ? (string)images[2]["url"] : "No image available";

                artistsDictionary.Add(id, (name, imageUrl, id));
            }

            return artistsDictionary;
        }
        public static (string Name, string ImageUrl) ParseArtistInfo(string jsonResponse)
        {
            JObject responseObj = JObject.Parse(jsonResponse);

            string name = (string)responseObj["name"];

            JArray images = (JArray)responseObj["images"];
            string imageUrl = images.Count > 0 ? (string)images[0]["url"] : "No image available";

            return (name, imageUrl);
        }
        public static List<TrackInfo> ParseArtistTopTracks(string jsonResponse)
        {
            JObject responseObj = JObject.Parse(jsonResponse);
            JArray tracks = (JArray)responseObj["tracks"];

            List<TrackInfo> trackInfoList = new List<TrackInfo>();

            foreach (var track in tracks)
            {
                string imageUrl = (string)track["album"]["images"][0]["url"]; 
                string name = (string)track["name"];
                TimeSpan duration = TimeSpan.FromMilliseconds((int)track["duration_ms"]);
                string id = (string)track["id"];

                trackInfoList.Add(new TrackInfo
                {
                    ImageUrl = imageUrl,
                    Name = name,
                    Duration = duration,
                    Id = id
                });
            }

            return trackInfoList;
        }

        public static List<AlbumInfo> ParseArtistAlbums(string jsonResponse)
        {
            JObject responseObj = JObject.Parse(jsonResponse);
            JArray albums = (JArray)responseObj["items"];

            List<AlbumInfo> albumInfoList = new List<AlbumInfo>();

            foreach (var album in albums)
            {
                string imageUrl = (string)album["images"][0]["url"];
                string name = (string)album["name"];
                string id = (string)album["id"];

                albumInfoList.Add(new AlbumInfo
                {
                    ImageUrl = imageUrl,
                    Name = name,
                    Id = id
                });
            }

            return albumInfoList;
        }
        public static List<SingleInfo> ParseArtistSingles(string jsonResponse)
        {
            JObject responseObj = JObject.Parse(jsonResponse);
            JArray singles = (JArray)responseObj["items"];

            List<SingleInfo> SingleInfoList = new List<SingleInfo>();

            foreach (var single in singles)
            {
                string imageUrl = (string)single["images"][0]["url"];
                string name = (string)single["name"];
                string id = (string)single["id"];

                SingleInfoList.Add(new SingleInfo
                {
                    ImageUrl = imageUrl,
                    Name = name,
                    Id = id
                });
            }

            return SingleInfoList;
        }
    }
}