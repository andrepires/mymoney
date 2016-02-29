using System;
using System.Data.Entity;
using System.Linq;
using MyMoney.Domain.Abstractions.Entities;
using MyMoney.Domain.Abstractions.Repositories;
using MyMoney.Domain.Abstractions.Values;
using MyMoney.Domain.BasicImplementations.Values;

namespace MyMoney.Domain.Infrastructure.EntityFramework.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : DomainObject
    {
        private DbContext Context { get; set; }
        private IOperationResult OperationResult { get; set; }
        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public IOperationResult Create(TEntity entity)
        {
            OperationResult = new OperationResult();
            try
            {
                Context.Set<TEntity>().Add(entity);
                Context.SaveChanges();
                OperationResult.Oks.Add(string.Format("Entity {0} was succesfully added to the context.", entity.Oid));
            }
            catch (Exception ex)
            {
                OperationResult.Errors.Add(string.Format("An error occurred when adding the entity to the context. Message: {0}", ex.Message));
            }

            return OperationResult;
        }

        public IOperationResult GetByOid(Guid oid)
        {
            OperationResult = new OperationResult();
            try
            {
                var entity = Context.Set<TEntity>().Find(oid);
                if(entity == null) OperationResult.Errors.Add(string.Format("Object id {0} was not found.", oid)); 
                OperationResult.Oks.Add(entity);
            }
            catch (Exception ex)
            {
                OperationResult.Errors.Add(string.Format("An error occurred when retrieving the entity with id {0} from the context. Message: {1}", oid, ex.Message));
            }
            return OperationResult;
        }

        public IOperationResult GetAll()
        {
            OperationResult = new OperationResult();
            try
            {
                var entities = Context.Set<TEntity>().ToList();
                OperationResult.Oks.Add(entities);
            }
            catch (Exception ex)
            {
                OperationResult.Errors.Add(string.Format("An error occurred when retrieving entities from the context. Message: {0}", ex.Message));
            }
            return OperationResult;
        }

        public IOperationResult GetBy(Func<TEntity, bool> expression)
        {
            OperationResult = new OperationResult();
            try
            {
                var entities = Context.Set<TEntity>().Where(expression);
                OperationResult.Oks.Add(entities);
            }
            catch (Exception ex)
            {
                OperationResult.Errors.Add(string.Format("An error occurred when retrieving entities from the context. Message: {0}", ex.Message));
            }
            return OperationResult;
        }

        public IOperationResult Update(Guid oid, TEntity entity)
        {
            OperationResult = new OperationResult();
            try
            {
                var existingEntity = Context.Set<TEntity>().Find(oid);
                if (existingEntity == null)
                {
                    OperationResult.Errors.Add(string.Format("The entity oid {0} doesn't exist in the database.", oid));
                }
                else
                {
                    Context.Entry(existingEntity).CurrentValues.SetValues(entity);
                    Context.SaveChanges();
                    OperationResult.Oks.Add(string.Format("Entity {0} was succesfully updated.", oid));
                }
            }
            catch (Exception ex)
            {
                OperationResult.Errors.Add(string.Format("An error occurred when retrieving entities from the context. Message: {0}", ex.Message));
            }
            return OperationResult;
        }

        public IOperationResult Delete(TEntity entity)
        {
            OperationResult = new OperationResult();
            try
            {
                var existingEntity = Context.Set<TEntity>().Find(entity.Oid);
                if (existingEntity == null)
                {
                    OperationResult.Errors.Add(string.Format("The entity Oid {0} doesn't exist in the database.", entity.Oid));
                }
                else
                {
                    Context.Set<TEntity>().Remove(existingEntity);
                    Context.SaveChanges();
                    OperationResult.Oks.Add(string.Format("Entity {0} was succesfully removed from the database.", entity.Oid));
                }
            }
            catch (Exception ex)
            {
                OperationResult.Errors.Add(string.Format("An error occurred when retrieving entities from the context. Message: {0}", ex.Message));
            }
            return OperationResult;
        }

        public IOperationResult Delete(Guid oid)
        {
            OperationResult = new OperationResult();
            try
            {
                var existingEntity = Context.Set<TEntity>().Find(oid);
                if (existingEntity == null)
                {
                    OperationResult.Errors.Add(string.Format("The entity oid {0} doesn't exist in the database.", oid));
                }
                else
                {
                    Context.Set<TEntity>().Remove(existingEntity);
                    Context.SaveChanges();
                    OperationResult.Oks.Add(string.Format("Entity oid {0} was succesfully removed from the database.", oid));
                }
            }
            catch (Exception ex)
            {
                OperationResult.Errors.Add(string.Format("An error occurred when retrieving entities from the context. Message: {0}", ex.Message));
            }
            return OperationResult;
        }
    }
}