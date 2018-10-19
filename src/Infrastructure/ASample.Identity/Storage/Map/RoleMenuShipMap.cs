using ASample.Identity.Domain.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ASample.Identity.Storage.Map
{
    public class RoleMenuShipMap : EntityTypeConfiguration<RoleMenuShip>
    {
        public RoleMenuShipMap()
        {
            ToTable("RoleMenuShip");
            HasKey(e => e.Id);
            Property(e => e.RoleId).IsRequired();
            Property(e => e.MenuId).IsRequired();
        }
    }
}
