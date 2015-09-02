using System;
using MyMoney.Domain.Abstractions.Entities;
using MyMoney.Domain.Infrastructure.OrigoDb.Contexts;
using OrigoDB.Core;

namespace MyMoney.Domain.Infrastructure.OrigoDb.Commands
{
    [Serializable]
    public class InsertCommand<TEntity> : Command<EntitiesModel<TEntity>>
        where TEntity: IDomainObject
    {
        private TEntity Entity { get; set; }
        public InsertCommand(TEntity entity)
        {
            Entity = entity;
        }
        public override void Execute(EntitiesModel<TEntity> model)
        {
            model.Entities.Add(Entity);
        }
    }
}