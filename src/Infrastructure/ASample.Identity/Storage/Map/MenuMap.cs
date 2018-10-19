using ASample.Identity.Domain.Models.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Identity.Storage.Map
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            ToTable("Menu");
            HasKey(c => c.Id);
            Property(c => c.Title).IsRequired().HasMaxLength(20);
            Property(c => c.Description).IsRequired().HasMaxLength(500);
        }
    }
}
