using ASample.Identity.Domain.Models.Entities;
using ASmaple.Domain.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace ASample.Identity.Domain.Models.AggregateRoots
{
    /// <summary>
    /// 会员登录
    /// </summary>
    public class IdentityUser : AggregateRoot, IUser<Guid>
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码（加密过后的）
        /// </summary>
        public string PasswordHash { get; set; }

        public IEnumerable<IdentityUserLoginInfo> LoginInfos { get; set; }
    }
}
