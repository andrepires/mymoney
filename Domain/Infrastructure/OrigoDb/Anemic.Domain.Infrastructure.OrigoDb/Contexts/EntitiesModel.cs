using System;
using System.Collections.Generic;
using MyMoney.Domain.Abstractions.Entities;
using OrigoDB.Core;

namespace MyMoney.Domain.Infrastructure.OrigoDb.Contexts
{
    [Serializable]
    public class EntitiesModel<TEntity> : Model
        where TEntity : IDomainObject
    {
        public IList<TEntity> Entities { get; set; }

        public EntitiesModel()
        {
            Entities = new List<TEntity>();
        }
    }
}