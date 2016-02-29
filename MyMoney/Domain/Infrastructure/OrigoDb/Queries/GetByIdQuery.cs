using System;
using System.Linq;
using MyMoney.Domain.Abstractions.Entities;
using MyMoney.Domain.Infrastructure.OrigoDb.Contexts;
using OrigoDB.Core;

namespace MyMoney.Domain.Infrastructure.OrigoDb.Queries
{
    [Serializable]
    public class GetByIdQuery<TEntity> : Query<EntitiesModel<TEntity>, TEntity>
        where TEntity: DomainObject
    {
        private readonly Guid _oid;

        public GetByIdQuery(Guid oid)
        {
            _oid = oid;
        }

        public override TEntity Execute(EntitiesModel<TEntity> model)
        {
            return model.Entities.SingleOrDefault(e => e.Oid.Equals(_oid));
        }
    }
}