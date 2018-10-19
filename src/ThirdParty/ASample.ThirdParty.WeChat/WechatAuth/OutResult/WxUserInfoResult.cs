using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.WeChat.WechatAuth.OutResult
{
    public class WxUserInfoResult
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息
        /// </summary>
        [JsonProperty(PropertyName = "subscribe")]
        public int Subscribe { get; set; }

        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }

        /// <summary>
        /// 用户的昵称
        /// </summary>
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        [JsonProperty(PropertyName = "sex")]
        public int Sex { get; set; }


        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [JsonProperty(PropertyName = "headimgurl")]
        public string Headimgurl { get; set; }

        /// <summary>
        /// 用户关注时间，为时间戳
        /// </summary>
        [JsonProperty(PropertyName = "subscribe_time")]
        public string SubscribeTime { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        [JsonProperty(PropertyName = "unionid")]
        public string Unionid { get; set; }

        /// <summary>
        /// 公众号运营者对粉丝的备注
        /// </summary>
        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }
    }
}
