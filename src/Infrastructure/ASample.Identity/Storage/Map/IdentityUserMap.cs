using ASample.Identity.Domain.Models.AggregateRoots;
using System.Data.Entity.ModelConfiguration;

namespace ASample.Identity.Storage.Map
{
    public class IdentityUserMap : EntityTypeConfiguration<IdentityUser>
    {
        public IdentityUserMap()
        {
            ToTable("IdentityUser");
            HasKey(c => c.Id);
            Property(c => c.UserName).IsRequired().HasMaxLength(20);
            Property(c => c.PasswordHash).IsRequired().HasMaxLength(1000);

            Property(c => c.CreateTime).IsRequired();
            Property(c => c.ModifyTime).IsOptional();
            Property(c => c.DeleteTime).IsOptional();
            Property(c => c.IsDeleted).IsRequired();
            
        }
    }
}
