using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.Hw.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Cet.Hw.Core.Data
{
    public class UserEntityTypeConfiguration : HwEntityTypeConfiguration<User>
    {
        public UserEntityTypeConfiguration() : base()
        {
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Login).HasColumnName("Login");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.IsGroup).HasColumnName("IsGroup");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Realm).HasColumnName("Realm");
            this.Property(t => t.UserUID).HasColumnName("UserUID");
            this.Property(t => t.Path).HasColumnName("Path");

            this.HasMany<User>(t => t.ChildMembers).WithMany().Map(m =>
            {
                m.MapLeftKey("GroupId");
                m.MapRightKey("UserId");
                m.ToTable("Member", "Core");
            });
            //this.HasMany<User>(t => t.AllMembers).WithMany().Map(m =>
            //{
            //    m.MapLeftKey("ParentUserCode");
            //    m.MapRightKey("ChildUserCode");
            //    m.ToTable("PrincipalMember", "Core");
            //});


            this.HasKey(t => t.Id);
            this.ToTable("User", "Core");
        }
    }
}