using ASmaple.Domain.Models;
using System;

namespace ASample.Identity.Domain.Models.Entities
{
    /// <summary>
    /// 角色权限关联表
    /// </summary>
    public class RoleMenuShip : Entity
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单权限编号
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
