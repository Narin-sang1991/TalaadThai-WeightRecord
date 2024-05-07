using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cet.Hw.Core.Domain;

namespace Cet.Hw.Core.Data
{
    public class MenuTranslateEntityTypeConfiguration : HwEntityTypeConfiguration<MenuTranslate>
    {
        public MenuTranslateEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
            this.Property(t => t.Name).HasColumnName("Name");

            //this.HasRequired<Language>(t => t.Lang).WithMany().HasForeignKey(t => t.LanguageCode);

            this.HasKey(t => new { t.Code, t.LanguageCode });

            this.ToTable("MenuTranslate", "Core");
        }
    }
}
