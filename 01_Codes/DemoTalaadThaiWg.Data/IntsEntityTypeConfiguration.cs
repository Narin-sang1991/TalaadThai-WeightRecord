using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.Threading.Tasks;
using Cet.Hw.Core.Domain;

namespace DemoTalaadThaiWg.Data
{
    public class IntsEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : HwEntity
    {
        public IntsEntityTypeConfiguration()
        {
            this.Property(t => t.MustData.CreatedByAppCode)
                .HasColumnName("CreatedByAppCode");
            this.Property(t => t.MustData.CreatedByUserCode)
                .HasColumnName("CreatedByUserCode");
            this.Property(t => t.MustData.CreatedDate)
                .HasColumnName("CreatedDate");
            this.Property(t => t.MustData.UpdatedByAppCode)
                .HasColumnName("UpdatedByAppCode");
            this.Property(t => t.MustData.UpdatedByUserCode)
                .HasColumnName("UpdatedByUserCode");
            this.Property(t => t.MustData.UpdatedDate)
                .HasColumnName("UpdatedDate");

            this.Property(t => t.NoteData).HasColumnName("NoteData");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion").IsRowVersion();

        }
    }
}
