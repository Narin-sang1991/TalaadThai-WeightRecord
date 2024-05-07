using Cet.EntityFramework.Adaptor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Data
{
    public class DbContextInitializer : IDbContextInitializer
    {
        public void Initialize(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UnitHwEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new MeasuringHwEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new MeasuringMoveItemHwEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new DocumentRunningNoEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ProcessPlanImportedEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ProcessPlanEntityTypeConfiguration());
        }
    }
}
