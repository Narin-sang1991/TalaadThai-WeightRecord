using Cet.Core.Utility;
using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core;
using Cet.Hw.Core.Domain;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTalaadThaiWg.Data
{
    public class IntsUnitOfWork : QueryableUnitOfWork
    {
        private IUnityContainer container;

        public IntsUnitOfWork(string configurationName, IUnityContainer container)
            : base(configurationName)
        {
            this.container = container;
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.ObjectMaterialized += this.ObjectMaterialized;
            objectContext.CommandTimeout = 500;
        }

        public override int SaveChanges()
        {
            //Todo : Should change to login profile
            //UserProfile profile = this.container.Resolve<AuthenticationServiceBase>().GetCurrentUserProfile(false);
            UserProfile profile = new UserProfile(Guid.NewGuid(), Guid.Empty, HardwareInfo.GetComputerName(), null);

            foreach (DbEntityEntry item in ChangeTracker.Entries())
            {
                if (item.Entity is HwEntity)
                {
                    var erpEntityItem = item.Entity as HwEntity;
                    if (item.State == EntityState.Added)
                    {
                        if (profile != null && profile.UserId != Guid.Empty)
                            erpEntityItem.MustData.SetMustDataNewMode(profile.Name, null, DateTimeOffset.Now);
                        else
                            erpEntityItem.MustData.SetCreatedDate(DateTimeOffset.Now);
                    }
                    else if (item.State == EntityState.Modified)
                    {
                        if (profile != null && profile.UserId != Guid.Empty)
                            erpEntityItem.MustData.SetMustDataUpdateMode(profile.Name, null, DateTimeOffset.Now);
                        else
                            erpEntityItem.MustData.SetUpdatedDate(DateTimeOffset.Now);
                    }
                }
            }
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex is System.Data.Entity.Validation.DbEntityValidationException)
                {
                    foreach (var x in ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors)
                    {
                        Console.WriteLine("++++++++++++++++++++++" + x.ValidationErrors);
                    }
                }
                throw ex;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Add(new RegisterFunctionConvention());   

            string defaultSchema = string.Empty;
            try
            {
                defaultSchema = container.Resolve<string>("DefaultSchema");
            }
            catch { }

            if (!string.IsNullOrEmpty(defaultSchema))
                modelBuilder.HasDefaultSchema(defaultSchema);

            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Conventions.Remove<TypeNameForeignKeyDiscoveryConvention>();
            modelBuilder.Conventions.Remove<NavigationPropertyNameForeignKeyDiscoveryConvention>();
            modelBuilder.ComplexType<MustData>();

            var x = container.ResolveAll<IDbContextInitializer>();
            foreach (IDbContextInitializer y in x)
                y.Initialize(modelBuilder);
        }

        void ObjectMaterialized(object sender, System.Data.Entity.Core.Objects.ObjectMaterializedEventArgs e)
        {
            // do property injection here using e.Entity    
            if (e.Entity is EntityBase)
            {
                var entity = e.Entity as EntityBase;
                container.BuildUp(entity.GetType(), entity);
            }
        }
    }
}
