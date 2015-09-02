using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoney.Domain.Configuration.Modules;
using Ninject;

namespace MyMoney.Domain.Configuration
{
    public static class Factory
    {
        private static StandardKernel StandardKernel { get; set; }

        static Factory()
        {
            StandardKernel = new StandardKernel(new MainModule());
        }

        public static TEntity Get<TEntity>()
        {
            return StandardKernel.Get<TEntity>();
        }

        public static TEntity Get<TEntity>(string mappingName)
        {
            return StandardKernel.Get<TEntity>(mappingName);
        }
    }
}
