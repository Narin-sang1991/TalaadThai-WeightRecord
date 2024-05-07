using Cet.Hw.Core.Data;
using Measurement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Data
{
    public class ProcessPlanEntityTypeConfiguration : HwEntityTypeConfiguration<ProcessPlan>
    {
        public ProcessPlanEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProcessPlanImportedId).HasColumnName("ProcessPlanImportedId");
            this.Property(t => t.SeqNo).HasColumnName("SeqNo");
            this.Property(t => t.PosNo).HasColumnName("PosNo");
            this.Property(t => t.ProcessMachineCode).HasColumnName("ProcessMachineCode");
            this.Property(t => t.ProcessPlanDate).HasColumnName("ProcessPlanDate");
            this.Property(t => t.CoilNo).HasColumnName("CoilNo");
            this.Property(t => t.CoilWeight).HasColumnName("CoilWeight");
            this.Property(t => t.RelatedId).HasColumnName("RelatedId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.HasOptional<MeasuringMoveItem>(t => t.RelatedItem).WithMany(t => t.ProcessPlans).HasForeignKey(t => t.RelatedId);

            this.HasKey(t => t.Id);
            this.ToTable("ProcessPlan", "Mm");

        }
    }
}
