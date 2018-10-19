using ASmaple.Domain.Models;
using System;

namespace ASample.Main.Domain.Models.AggregateRoots
{
    public class UserLogin : AggregateRoot
    {

        /// <summary>
        /// 登陆名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

    }
}
