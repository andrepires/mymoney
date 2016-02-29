using System;
using System.Linq;
using MyMoney.Domain.Abstractions.Entities;
using MyMoney.Domain.Infrastructure.OrigoDb.Contexts;
using OrigoDB.Core;

namespace MyMoney.Domain.Infrastructure.OrigoDb.Commands
{
    [Serializable]
    public class DeleteCommand<TEntity> : Command<EntitiesModel<TEntity>>
        where TEntity : IDomainObject
    {
        private Guid Oid { get; set; }

        public DeleteCommand(Guid oid)
        {
            Oid = oid;
        }

        public override void Execute(EntitiesModel<TEntity> model)
        {
            var existingEntity = model.Entities.SingleOrDefault(e => e.Oid.Equals(Oid));
            model.Entities.Remove(existingEntity);
        }
    }
}