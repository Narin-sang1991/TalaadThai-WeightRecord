using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.Hw.Core.Domain;


namespace Cet.Hw.Core.Data
{
    public class OrganizationUnitEntityTypeConfiguration : HwEntityTypeConfiguration<OrganizationUnit>
    {
        public OrganizationUnitEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.TaxID).HasColumnName("TaxID");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.TelNo).HasColumnName("TelNo");
            this.Property(t => t.FaxNo).HasColumnName("FaxNo");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Path).HasColumnName("Path");

            this.HasOptional<OrganizationUnit>(t => t.Parent).WithMany().HasForeignKey(t => t.ParentId);

            this.HasKey(t => t.Id);
            this.ToTable("OrganizationUnit", "Core");
        }
    }
}
