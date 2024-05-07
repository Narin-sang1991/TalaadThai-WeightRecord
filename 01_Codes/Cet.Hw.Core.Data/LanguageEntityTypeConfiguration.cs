using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class LanguageEntityTypeConfiguration : HwEntityTypeConfiguration<Language>
    {
        public LanguageEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.HasKey(t => t.Code);

            this.ToTable("Language", "Core");
        }
    }
}
