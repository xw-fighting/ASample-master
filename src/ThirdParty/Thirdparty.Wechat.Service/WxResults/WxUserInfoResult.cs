using System;
using System.Collections.Generic;
using System.Reflection;
using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    /// <summary>
    ///  WxUserInfo 包装类
    /// </summary>
    public class WxUserInfoResult
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        [JsonProperty("subscribe")]
        public int Subscribe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("openid")]
        public string Openid { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        [JsonProperty("sex")]
        public int Sex { get; set; }

        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        ///  城市
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        ///  省份
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }

        /// <summary>
        ///  国家
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [JsonProperty("headimgurl")]
        public string Headimgurl { get; set; }

        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        [JsonProperty("subscribe_time")]
        public int SubscribeTime { get; set; }

        /// <summary>
        /// 	只有在用户将公众号实名认证到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        [JsonProperty("unionid")]
        public int UnionId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 用户所在的分组ID
        /// </summary>
        [JsonProperty("groupid")]
        public int Groupid { get; set; }

        /// <summary>
        /// 用户被打上的标签ID列表
        /// </summary>
        [JsonProperty("tagid_list")]
        public List<string> TagidList { get; set; }

        public WxUserInfoResult() { }

        //public WxUserInfoResult(WxUserInfo userInfo)
        //{
        //    Subscribe = userInfo.Subscribe;
        //    Groupid = userInfo.Groupid;
        //    Sex = userInfo.Sex;
        //    SubscribeTime = userInfo.SubscribeTime;
        //    UnionId = userInfo.UnionId;
        //    City = userInfo.City;
        //    Country = userInfo.Country;
        //    Headimgurl = userInfo.Headimgurl;
        //    Language = userInfo.Language;
        //    Nickname = userInfo.Nickname;
        //    Openid = userInfo.Openid;
        //    Province = userInfo.Province;
        //    Remark = userInfo.Remark;
        //    TagidList = userInfo.TagidList;
        //}

        public WxUserInfo GetWxUserInfo()
        {
            return this.AutoMapTo<WxUserInfo>(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}