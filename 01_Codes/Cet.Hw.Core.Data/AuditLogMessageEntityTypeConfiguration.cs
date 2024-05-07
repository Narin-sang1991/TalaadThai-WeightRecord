using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class AuditLogMessageEntityTypeConfiguration : HwEntityTypeConfiguration<AuditLogMessage>
    {
        public AuditLogMessageEntityTypeConfiguration() : base()
        {
            this.HasKey(t => t.Code);

            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.LevelNumber).HasColumnName("LevelNumber");
            this.Property(t => t.Template).HasColumnName("Template");
            this.Property(t => t.AuditLogCategoryCode).HasColumnName("AuditLogCategoryCode");

            this.HasRequired<AuditLogCategory>(t => t.AuditLogCategory).WithMany().HasForeignKey(t => t.AuditLogCategoryCode);

            this.ToTable("AuditLogMessage", "Core");
        }
    }
}
