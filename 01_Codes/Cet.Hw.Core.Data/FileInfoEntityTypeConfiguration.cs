using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class FileInfoEntityTypeConfiguration : HwEntityTypeConfiguration<CoreFileInfo>
    {
        public FileInfoEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.ExtensionId).HasColumnName("ExtensionId");
            this.Property(t => t.RelationId).HasColumnName("RelationId");
            this.Property(t => t.RelationTypeValue).HasColumnName("RelationType");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasRequired<CoreFileExtension>(t => t.CoreFileExtension).WithMany().HasForeignKey(t => t.ExtensionId);

            this.HasKey(t => t.Id);
            this.ToTable("FileInfo", "Core");
        }

    }
}
