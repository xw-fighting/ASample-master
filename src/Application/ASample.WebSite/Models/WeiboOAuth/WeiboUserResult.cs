using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ASample.WebSite.Models.WeiboOAuth
{
    public class WeiboUserResult
    {
        [JsonProperty("id")]
         public string Id { get; set; }
        [JsonProperty("screen_name")]
        public string screen_name { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("province")]
        public string province { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("location")]
        public string location { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        [JsonProperty("profile_image_url")]
        public string profile_image_url { get; set; }

        [JsonProperty("domain")]
        public string domain { get; set; }

        [JsonProperty("gender")]
        public string gender { get; set; }

        [JsonProperty("followers_count")]
        public string followers_count { get; set; }

        [JsonProperty("friends_count")]
        public string friends_count { get; set; }

        [JsonProperty("screen_name")]
        public string statuses_count { get; set; }

        [JsonProperty("favourites_count")]
        public string favourites_count { get; set; }

        [JsonProperty("created_at")]
        public string created_at { get; set; }

        [JsonProperty("following")]
        public string following { get; set; }

        [JsonProperty("allow_all_act_msg")]
        public string allow_all_act_msg { get; set; }

        [JsonProperty("geo_enabled")]
        public string geo_enabled { get; set; }

        [JsonProperty("verified")]
        public string verified { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("allow_all_comment")]
        public string allow_all_comment { get; set; }

        [JsonProperty("avatar_large")]
        public string avatar_large { get; set; }

        [JsonProperty("verified_reason")]
        public string verified_reason { get; set; }

        [JsonProperty("follow_me")]
        public string follow_me { get; set; }

        [JsonProperty("online_status")]
        public string online_status { get; set; }

        [JsonProperty("bi_followers_count")]
        public string bi_followers_count { get; set; }
    }
   public class Childs
{
    public string created_at { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public string favorited { get; set; }
        public string truncated { get; set; }
        public string in_reply_to_status_id { get; set; }
        public string in_reply_to_user_id { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public string geo { get; set; }
        public string mid { get; set; }
        public string annotations { get; set; }
        public string reposts_count { get; set; }
        public string comments_count { get; set; }
    }


}