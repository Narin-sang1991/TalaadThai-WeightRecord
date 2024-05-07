using Cet.Hw.Core.Data;
using Measurement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Data
{
    public class MeasuringHwEntityTypeConfiguration : HwEntityTypeConfiguration<Measuring>
    {
        public MeasuringHwEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BusinessEntityId).HasColumnName("BusinessEntityId");
            this.Property(t => t.StatusValue).HasColumnName("Status");
            this.Property(t => t.TypeValue).HasColumnName("Type");
            this.Property(t => t.No).HasColumnName("No");
            this.Property(t => t.ReferenceNo).HasColumnName("ReferenceNo");
            this.Property(t => t.Date).HasColumnName("Date");

            this.HasMany<MeasuringMoveItem>(t => t.MeasuringMoveItems).WithRequired(t => t.Measuring).HasForeignKey(t => t.MeasuringId);

            this.HasKey(t => t.Id);
            this.ToTable("Measuring", "Mm");

        }
    }
}
