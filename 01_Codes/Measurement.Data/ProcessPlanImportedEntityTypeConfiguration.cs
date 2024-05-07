using Cet.Hw.Core.Data;
using Measurement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Data
{
    public class ProcessPlanImportedEntityTypeConfiguration : HwEntityTypeConfiguration<ProcessPlanImported>
    {
        public ProcessPlanImportedEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImportedDate).HasColumnName("ImportedDate");
            this.Property(t => t.ImportedNo).HasColumnName("ImportedNo");


            this.HasMany<ProcessPlan>(t => t.ProcessPlanItems).WithRequired(t => t.ProcessPlanImported).HasForeignKey(t => t.ProcessPlanImportedId);

            this.HasKey(t => t.Id);
            this.ToTable("ProcessPlanImported", "Mm");

        }
    }
}
