using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;

namespace InstagramSample.Utils.Instagram
{
    public class Instagram
    {
        private static HttpClient client = new HttpClient();

        public string InstagramUserId { get; set; }
        public string InstragramToken { get; set; }
        private static string instagramApiURI = "https://api.instagram.com/v1/users/{0}/media/recent?access_token={1}&count=10";

        public Instagram(string userId,string token)
        {
            this.InstagramUserId = userId;
            this.InstragramToken = token;
        }

        public InstagramFeed GetFeed()
        {
            try
            {
                var url = String.Format(instagramApiURI, this.InstagramUserId, this.InstragramToken);
                var responseMessage = client.GetAsync(url).Result;
                var body = responseMessage.Content.ReadAsStringAsync().Result;
                var jsonData = InstagramFeed.FromJson(body);
                return jsonData;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }

    //
    //    using QuickType;
    //
    //    var data = Welcome.FromJson(jsonString);

    public partial class InstagramFeed
    {
        [JsonProperty("data")]
        public List<Datum> Data { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("attribution")]
        public object Attribution { get; set; }

        [JsonProperty("caption")]
        public Caption Caption { get; set; }

        [JsonProperty("comments")]
        public Comments Comments { get; set; }

        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("likes")]
        public Comments Likes { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("location")]
        public object Location { get; set; }

        [JsonProperty("tags")]
        public List<object> Tags { get; set; }

        [JsonProperty("type")]
        public string PurpleType { get; set; }

        [JsonProperty("user")]
        public From User { get; set; }

        [JsonProperty("user_has_liked")]
        public bool UserHasLiked { get; set; }

        [JsonProperty("users_in_photo")]
        public List<object> UsersInPhoto { get; set; }
    }

    public partial class Caption
    {
        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class From
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("profile_picture")]
        public string ProfilePicture { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }

    public partial class Comments
    {
        [JsonProperty("count")]
        public long Count { get; set; }
    }

    public partial class Images
    {
        [JsonProperty("low_resolution")]
        public LowResolution LowResolution { get; set; }

        [JsonProperty("standard_resolution")]
        public LowResolution StandardResolution { get; set; }

        [JsonProperty("thumbnail")]
        public LowResolution Thumbnail { get; set; }
    }

    public partial class LowResolution
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("code")]
        public long Code { get; set; }
    }

    public partial class Pagination
    {
    }

    public partial class InstagramFeed
    {
        public static InstagramFeed FromJson(string json)
        {
            return JsonConvert.DeserializeObject<InstagramFeed>(json, Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this InstagramFeed self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}