using Cet.Hw.Core.Data;
using Measurement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Data
{
    public class MeasuringMoveItemHwEntityTypeConfiguration : HwEntityTypeConfiguration<MeasuringMoveItem>
    {
        public MeasuringMoveItemHwEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MeasuringId).HasColumnName("MeasuringId");
            this.Property(t => t.GatewayItemTypeValue).HasColumnName("GatewayItemType");
            this.Property(t => t.SeqNo).HasColumnName("SeqNo");
            this.Property(t => t.ProductBarcode).HasColumnName("ProductBarcode");
            this.Property(t => t.CoilNetWeight).HasColumnName("CoilNetWeight"); 
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.UnitPerRatio).HasColumnName("UnitPerRatio");
            this.Property(t => t.NetWeight).HasColumnName("NetWeight");
            this.Property(t => t.TareWeight).HasColumnName("TareWeight");
            this.Property(t => t.WeightUnitId).HasColumnName("WeightUnitId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.HasRequired(t => t.WeightUnit).WithMany().HasForeignKey(t => t.WeightUnitId);

            this.HasKey(t => t.Id);
            this.ToTable("MeasuringMoveItem", "Mm");

        }
    }
}
