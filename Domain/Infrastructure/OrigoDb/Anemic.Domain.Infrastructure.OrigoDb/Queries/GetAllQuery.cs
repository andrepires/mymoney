using System;
using System.Collections.Generic;
using System.Linq;
using MyMoney.Domain.Abstractions.Entities;
using MyMoney.Domain.Infrastructure.OrigoDb.Contexts;
using OrigoDB.Core;

namespace MyMoney.Domain.Infrastructure.OrigoDb.Queries
{
    [Serializable]
    public class GetAllQuery<TEntity> : Query<EntitiesModel<TEntity>, IList<TEntity>>
        where TEntity : DomainObject
    {
        public override IList<TEntity> Execute(EntitiesModel<TEntity> model)
        {
            return model.Entities;
        }
    }
}