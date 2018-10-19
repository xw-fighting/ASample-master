using ASmaple.Domain.Models;

namespace ASample.Identity.Domain.Models.AggregateRoots
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : AggregateRoot
    { 
        /// <summary>
        /// 菜单标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        public string Description { get; set; }
    }
}
