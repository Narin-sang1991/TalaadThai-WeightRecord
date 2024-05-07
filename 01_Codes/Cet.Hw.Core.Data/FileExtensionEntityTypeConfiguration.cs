using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class FileExtensionEntityTypeConfiguration : HwEntityTypeConfiguration<CoreFileExtension>
    {
        public FileExtensionEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FileType).HasColumnName("FileType");
            this.Property(t => t.MimeType).HasColumnName("MimeType");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasKey(t => t.Id);
            this.ToTable("FileExtension", "Core");
        }

    }
}