using Cet.EntityFramework.Adaptor;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTalaadThaiWg.Data
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

            modelBuilder.Configurations.Add(new DocumentRunningNoEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new MeasuringIntsEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new MeasuringMoveItemIntsEntityTypeConfiguration());
            
        }
    }
}
