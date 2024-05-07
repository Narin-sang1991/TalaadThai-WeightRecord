using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class AuditLogEntityTypeConfiguration : HwEntityTypeConfiguration<AuditLog>
    {

        public AuditLogEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AuditLogMessageCode).HasColumnName("AuditLogMessageCode");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.EventDate).HasColumnName("EventDate");

            this.HasKey(t => t.Id);

            this.HasRequired<AuditLogMessage>(t => t.AuditLogMessage).WithMany().HasForeignKey(t => t.AuditLogMessageCode);

            this.ToTable("AuditLog", "Core");

        }
    }
}



