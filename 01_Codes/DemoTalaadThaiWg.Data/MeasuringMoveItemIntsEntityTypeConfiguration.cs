using DemoTalaadThaiWg.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTalaadThaiWg.Data
{
    public class MeasuringMoveItemIntsEntityTypeConfiguration : IntsEntityTypeConfiguration<MeasuringMoveItem>
    {
        public MeasuringMoveItemIntsEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MeasuringId).HasColumnName("MeasuringId");
            this.Property(t => t.GatewayItemTypeValue).HasColumnName("GatewayItemType");
            this.Property(t => t.SeqNo).HasColumnName("SeqNo");
            this.Property(t => t.ProductBarcode).HasColumnName("ProductBarcode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.UnitPerRatio).HasColumnName("UnitPerRatio");
            this.Property(t => t.NetWeight).HasColumnName("NetWeight");
            this.Property(t => t.TareWeight).HasColumnName("TareWeight");
            this.Property(t => t.WeightUnitCode).HasColumnName("WeightUnitCode");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.HasKey(t => t.Id);
            this.ToTable("MeasuringMoveItem", "Mm");

        }
    }
}