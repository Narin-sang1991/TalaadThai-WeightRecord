using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class ConfigCategoryHwEntityTypeConfiguration : HwEntityTypeConfiguration<ConfigurationCategory>
    {
        public ConfigCategoryHwEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsAccessWrite).HasColumnName("IsAccessWrite");

            this.HasKey(t => t.Id);
            this.ToTable("ConfigurationCategory", "Core");
        }

    }
}
