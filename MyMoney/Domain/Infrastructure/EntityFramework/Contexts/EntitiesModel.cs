using System.Data.Entity;
using MyMoney.Domain.BasicImplementations.Entities;
using MyMoney.Domain.Infrastructure.EntityFramework.Mappings;
using MyMoney.Domain.Infrastructure.EntityFramework.Migrations;

namespace MyMoney.Domain.Infrastructure.EntityFramework.Contexts
{
    public class EntitiesModel : DbContext
    {
        public EntitiesModel() : base("MyMoneyDb")
        {
            //Add your configurations here
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EntitiesModel, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Add your mappings here
            modelBuilder.Configurations.Add(new AccountMappings());
        }

        public DbSet<Account> Accounts { get; set; }
    }
}