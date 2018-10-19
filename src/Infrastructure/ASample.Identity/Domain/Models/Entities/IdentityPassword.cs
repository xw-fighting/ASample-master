using ASmaple.Domain.Models;
using System;

namespace ASample.Identity.Domain.Models.Entities
{
    public class IdentityPassword : Entity
    {
        /// <summary>
        /// 登录用户编号
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 登录密码【加密后的】
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
