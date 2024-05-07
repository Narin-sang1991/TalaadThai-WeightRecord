using Cet.Hw.Core.Data;
using Measurement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Data
{
    public class DocumentRunningNoEntityTypeConfiguration : HwEntityTypeConfiguration<DocumentRunningNo>
    {
        public DocumentRunningNoEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OuId).HasColumnName("OuId");
            this.Property(t => t.Prefix).HasColumnName("Prefix");
            this.Property(t => t.RunningNo).HasColumnName("RunningNo");
            this.Property(t => t.DocumentType).HasColumnName("DocumentType");

            this.HasKey(t => new { t.Id });
            this.ToTable("DocumentRunningNo", "Mm");
        }
    }
}
