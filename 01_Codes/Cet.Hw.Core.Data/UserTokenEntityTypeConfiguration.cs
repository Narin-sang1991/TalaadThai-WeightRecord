using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.Hw.Core.Domain;


namespace Cet.Hw.Core.Data
{
    public class UserTokenEntityTypeConfiguration : HwEntityTypeConfiguration<UserToken>
    {
        public UserTokenEntityTypeConfiguration()
            : base()
        {
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Token).HasColumnName("Token");

            this.HasKey(t => t.UserId);
            this.ToTable("UserToken", "Core");
        }
    }
}
