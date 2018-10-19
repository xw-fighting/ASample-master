using ASmaple.Domain.Models;
using System;

namespace ASample.Main.Domain.Models.AggregateRoots
{
    public class UserInfo : AggregateRoot
    {
        /// <summary>
        /// 登录用户名编号
        /// </summary>
        public Guid LoginId { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Brithday { get; set; }
    }
}
