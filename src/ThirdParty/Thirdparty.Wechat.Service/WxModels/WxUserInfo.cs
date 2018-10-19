﻿using System.Collections.Generic;

namespace Thirdparty.Wechat.Service.WxModels
{
    public class WxUserInfo
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public int Subscribe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Openid { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        ///  城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///  省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        ///  国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Headimgurl { get; set; }

        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public int SubscribeTime { get; set; }

        /// <summary>
        /// 	只有在用户将公众号实名认证到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public int UnionId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 用户所在的分组ID
        /// </summary>
        public int Groupid { get; set; }

        /// <summary>
        /// 用户被打上的标签ID列表
        /// </summary>
        public List<string> TagidList { get; set; }
    }
}