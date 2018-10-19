using ASmaple.Domain.Models;
namespace ASample.Identity.Domain.Models.AggregateRoots
{
    /// <summary>
    /// 管理员用户
    /// </summary>
    public class User :AggregateRoot
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }
    }
}
