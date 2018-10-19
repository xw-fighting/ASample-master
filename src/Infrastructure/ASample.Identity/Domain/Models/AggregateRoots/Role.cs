using ASmaple.Domain.Models;

namespace ASample.Identity.Domain.Models.AggregateRoots
{
    /// <summary>
    /// 管理员角色
    /// </summary>
    public class Role : AggregateRoot
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        
    }
}
