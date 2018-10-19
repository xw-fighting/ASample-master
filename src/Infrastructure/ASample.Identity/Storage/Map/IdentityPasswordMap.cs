using ASample.Identity.Domain.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ASample.Identity.Storage.Map
{
    public class IdentityPasswordMap : EntityTypeConfiguration<IdentityPassword>
    {
        public IdentityPasswordMap()
        {
            ToTable("IdentityPassword");
            HasKey(e => e.Id);
            Property(e => e.UserId).IsRequired();
            Property(e => e.PasswordHash).IsRequired().HasMaxLength(1000);
        }
    }
}
