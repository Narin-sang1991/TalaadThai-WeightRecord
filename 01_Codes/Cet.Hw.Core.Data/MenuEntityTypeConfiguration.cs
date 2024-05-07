using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cet.Hw.Core.Domain;

namespace Cet.Hw.Core.Data
{
    public class MenuEntityTypeConfiguration : HwEntityTypeConfiguration<Menu>
    {
        public MenuEntityTypeConfiguration() : base()
        {
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ParentCode).HasColumnName("ParentCode");
            this.Property(t => t.ResourceUID).HasColumnName("ResourceUID");
            this.Property(t => t.Ordinary).HasColumnName("Ordinary");
            this.Property(t => t.Command).HasColumnName("Command");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsHidden).HasColumnName("IsHidden");


            this.HasMany(t => t.MenuTranslates).WithRequired().HasForeignKey(t => t.Code);

            this.HasKey(t => t.Code);
            this.ToTable("Menu", "Core");
        }
    }
}
