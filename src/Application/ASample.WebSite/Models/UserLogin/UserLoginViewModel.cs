using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ASample.WebSite.Models
{
    public class UserLoginPagedViewModel
    {
        [JsonProperty("rows")]
        public List<UserLoginViewModel> Rows { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class UserLoginViewModel
    {
        /// <summary>
        /// 登陆名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public string DeleteTimeText => DeleteTime == null?"":DeleteTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTimeText => CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
}