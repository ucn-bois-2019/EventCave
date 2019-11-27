using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCaveWeb.Utils
{
    public class Imgur
    {
        private static Imgur instance = null;
        private RestClient client;
        private const string AUTHORIZATION_HEADER = "Client-ID 95ea951e858489a";

        private Imgur()
        {
            client = new RestClient("https://api.imgur.com/3");
        }

        public static Imgur Instance
        {
            get
            {
                if (instance == null)
                {
                    return new Imgur();
                }
                return instance;
            }
        }

        public ImgurImage GetImage(string id)
        {
            if (id == null)
            {
                return new ImgurImage() { Link = "https://via.placeholder.com/512" };
                }
            var request = new RestRequest($"image/{id}", Method.GET);
            request.AddHeader("Authorization", AUTHORIZATION_HEADER);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<ImgurImageResponse>(response.Content).Image;
        }

        public List<ImgurImage> GetAlbumImages(string id)
        {
            if (id == null)
            {
                return new List<ImgurImage>();
            }
            var request = new RestRequest($"album/{id}/images", Method.GET);
            request.AddHeader("Authorization", AUTHORIZATION_HEADER);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<ImgurAlbumResponse>(response.Content).Images;
        }
    }

    public class ImgurAlbumResponse
    {
        [JsonProperty("data")]
        public List<ImgurImage> Images { get; set; }
    }

    public class ImgurImageResponse
    {
        [JsonProperty("data")]
        public ImgurImage Image { get; set; }
    }

    public class ImgurImage
    {
        [JsonProperty("link")]
        public string Link { get; set; }
    }
}