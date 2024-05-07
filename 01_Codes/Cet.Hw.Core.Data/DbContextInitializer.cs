using Cet.EntityFramework.Adaptor;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class DbContextInitializer : IDbContextInitializer
    {
        [Dependency("DecimalPrecision")]
        public byte DecimalPrecision { get; set; }
        [Dependency("DecimalScale")]
        public byte DecimalScale { get; set; }

        public void Initialize(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(DecimalPrecision, DecimalScale));
            modelBuilder.Configurations.Add(new MenuEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new MenuTranslateEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserTokenEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new FileInfoEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new FileExtensionEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new OrganizationUnitEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ConfigCategoryHwEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ConfigurationHwEntityTypeConfiguration());
            //modelBuilder.Configurations.Add(new AuditLogEntityTypeConfiguration());
            //modelBuilder.Configurations.Add(new AuditLogCategoryEntityTypeConfiguration());
            //modelBuilder.Configurations.Add(new AuditLogMessageEntityTypeConfiguration());

        }

    }
}
