using ASample.Identity.Domain.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ASample.Identity.Storage.Map
{
    public class UserRoleShipMap : EntityTypeConfiguration<UserRoleShip>
    {
        public UserRoleShipMap()
        {
            ToTable("UserRoleShip");
            HasKey(e => e.Id);
            Property(e => e.RoleId).IsRequired();
            Property(e => e.UserId).IsRequired();
        }
    }
}
