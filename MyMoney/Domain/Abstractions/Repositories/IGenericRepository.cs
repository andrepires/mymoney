using System;
using MyMoney.Domain.Abstractions.Entities;
using MyMoney.Domain.Abstractions.Values;

namespace MyMoney.Domain.Abstractions.Repositories
{
    public interface IGenericRepository<TEntity> 
        where TEntity : IDomainObject
    {
        //Create
        IOperationResult Create(TEntity entity);

        //Read
        IOperationResult GetByOid(Guid oid);
        IOperationResult GetAll();
        IOperationResult GetBy(Func<TEntity, bool> expression);
        
        //Update
        IOperationResult Update(Guid oid, TEntity entity);
        
        //Delete
        IOperationResult Delete(TEntity entity);
        IOperationResult Delete(Guid oid);

    }
}