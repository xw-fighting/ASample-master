using ASample.Main.Domain.Models.AggregateRoots;
using System.Data.Entity.ModelConfiguration;

namespace ASample.Main.Storage.Map
{
    public class UserLoginMap : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginMap()
        {
            ToTable("UserLogin");
            HasKey(e => e.Id);
            Property(e => e.Name).IsRequired().HasMaxLength(20);
            Property(e => e.Password).IsRequired().HasMaxLength(30);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.DeleteTime).IsOptional();
            Property(e => e.IsDeleted).IsRequired();
            Property(e => e.ModifyTime).IsOptional();
        }
    }
}
