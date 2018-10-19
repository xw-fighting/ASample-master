using ASample.Identity.Domain.Models.AggregateRoots;
using System.Data.Entity.ModelConfiguration;

namespace ASample.Identity.Storage.Map
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Role");
            HasKey(e => e.Id);
            Property(e => e.Title).IsRequired().HasMaxLength(100);
            Property(e => e.Description).IsOptional().HasMaxLength(200);
        }
    }
}
