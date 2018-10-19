using ASample.Identity.Domain.Models.AggregateRoots;
using System.Data.Entity.ModelConfiguration;


namespace ASample.Identity.Storage.Map
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");
            HasKey(e => e.Id);
            Property(e => e.UserName).IsRequired().HasMaxLength(100);
            Property(e => e.Password).IsRequired().HasMaxLength(100);
            Property(e => e.RealName).IsRequired().HasMaxLength(100);
            Property(e => e.Phone).IsOptional().HasMaxLength(100);
            Property(e => e.Email).IsOptional().HasMaxLength(100);
            Property(e => e.Sort).IsRequired();
            Property(e => e.CreateTime).IsRequired();
        }
    }
}
