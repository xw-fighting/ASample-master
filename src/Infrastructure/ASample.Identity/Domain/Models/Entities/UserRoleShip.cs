using ASmaple.Domain.Models;
using System;


namespace ASample.Identity.Domain.Models.Entities
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    public class UserRoleShip : Entity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
