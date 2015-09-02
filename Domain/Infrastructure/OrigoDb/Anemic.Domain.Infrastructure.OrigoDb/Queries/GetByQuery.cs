using System;
using System.Linq;
using MyMoney.Domain.Abstractions.Entities;
using MyMoney.Domain.Infrastructure.OrigoDb.Contexts;
using OrigoDB.Core;

namespace MyMoney.Domain.Infrastructure.OrigoDb.Queries
{
    [Serializable]
    public class GetByQuery<TEntity> : Query<EntitiesModel<TEntity>, IQueryable<TEntity>>
        where TEntity : DomainObject
    {
        private Func<TEntity, bool> Expression { get; set; }

        public GetByQuery(Func<TEntity, bool> expression)
        {
            Expression = expression;
        }

        public override IQueryable<TEntity> Execute(EntitiesModel<TEntity> model)
        {
            return model.Entities.Where(Expression).AsQueryable();
        }
    }
}