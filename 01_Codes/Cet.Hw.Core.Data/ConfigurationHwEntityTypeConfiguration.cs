using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class ConfigurationHwEntityTypeConfiguration : HwEntityTypeConfiguration<Configuration>
    {
        public ConfigurationHwEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ConfigCategoryId).HasColumnName("ConfigCategoryId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.AllowClientAccess).HasColumnName("AllowClientAccess");


            this.Property(t => t.ConfigurationValue).HasColumnName("ConfigurationValue");
            //this.Property(t => t.XmlValue).HasColumnName("XmlValue");


            this.HasRequired<ConfigurationCategory>(t => t.Category).WithMany().HasForeignKey(t => t.ConfigCategoryId);

            this.HasKey(t => t.Id);
            this.ToTable("Configuration", "Core");
        }

    }
}
