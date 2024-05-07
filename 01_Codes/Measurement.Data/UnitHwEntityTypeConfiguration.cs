using Cet.Hw.Core.Data;
using Measurement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Data
{
    public class UnitHwEntityTypeConfiguration : HwEntityTypeConfiguration<Unit>
    {
        public UnitHwEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Abbreviation).HasColumnName("Abbreviation");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasKey(t => t.Id);
            this.ToTable("Unit", "Mm");

        }
    }
}
