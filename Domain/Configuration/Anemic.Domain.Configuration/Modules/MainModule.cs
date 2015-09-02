using System.Configuration;
using System.Data.Entity;
using MyMoney.Domain.Abstractions.Repositories;
using MyMoney.Domain.Application.Managers;
using MyMoney.Domain.BasicImplementations.Entities;
using MyMoney.Domain.Infrastructure.EntityFramework.Contexts;
using Ninject;
using Ninject.Modules;

namespace MyMoney.Domain.Configuration.Modules
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            //Create your bindings here
            Bind<DbContext>().To<EntitiesModel>();
            Bind<IGenericRepository<Account>>()
                .To<Infrastructure.EntityFramework.Repositories.GenericRepository<Account>>().Named("EntityFrameworkRepository");
            Bind<IGenericRepository<Account>>()
                .To<Infrastructure.OrigoDb.Repositories.GenericRepository<Account>>().Named("OrigoDbRepository");
            Bind<AccountManager>().ToMethod((context) =>
            {
                var repositoryType = ConfigurationManager.AppSettings["PersistenceMode"];
                var repository = context.Kernel.Get<IGenericRepository<Account>>(repositoryType);
                return new AccountManager(repository);
            });
        }
    }
}