using System;
using System.Linq;
using MyMoney.Domain.Abstractions.Entities;
using MyMoney.Domain.Abstractions.Repositories;
using MyMoney.Domain.Abstractions.Values;
using MyMoney.Domain.BasicImplementations.Values;
using MyMoney.Domain.Infrastructure.OrigoDb.Commands;
using MyMoney.Domain.Infrastructure.OrigoDb.Contexts;
using MyMoney.Domain.Infrastructure.OrigoDb.Queries;
using OrigoDB.Core;

namespace MyMoney.Domain.Infrastructure.OrigoDb.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : DomainObject
    {
        private IEngine<EntitiesModel<TEntity>> Context { get; set; }
        private IOperationResult OperationResult { get; set; }
        public GenericRepository()
        {
            Context = Engine.For<EntitiesModel<TEntity>>();
        }

        public IOperationResult Create(TEntity entity)
        {
            OperationResult = new OperationResult();
            try
            {
                var command = new InsertCommand<TEntity>(entity);
                Context.Execute(command);
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
                var query = new GetByIdQuery<TEntity>(oid);
                var entity = Context.Execute(query);
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
                var query = new GetAllQuery<TEntity>();
                var entities = Context.Execute(query).AsQueryable();
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
                var query = new GetByQuery<TEntity>(expression);
                var entities = Context.Execute(query);
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
                var query = new GetByIdQuery<TEntity>(oid);
                var existingEntity = Context.Execute(query);

                if (existingEntity == null)
                {
                    OperationResult.Errors.Add(string.Format("The entity id {0} doesn't exist in the database.", oid));
                }
                else
                {
                    var deleteCommand = new DeleteCommand<TEntity>(entity.Oid);
                    Context.Execute(deleteCommand);
                    var insertCommand = new InsertCommand<TEntity>(entity);
                    Context.Execute(insertCommand);
                    OperationResult.Oks.Add(string.Format("Entity {0} was succesfully updated.", entity.Oid));
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
                var query = new GetByIdQuery<TEntity>(entity.Oid);
                var existingEntity = Context.Execute(query);
                if (existingEntity == null)
                {
                    OperationResult.Errors.Add(string.Format("The entity id {0} doesn't exist in the database.", entity.Oid));
                }
                else
                {
                    var deleteCommand = new DeleteCommand<TEntity>(entity.Oid);
                    Context.Execute(deleteCommand);
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
                var query = new GetByIdQuery<TEntity>(oid);
                var existingEntity = Context.Execute(query);
                if (existingEntity == null)
                {
                    OperationResult.Errors.Add(string.Format("The entity id {0} doesn't exist in the database.", oid));
                }
                else
                {
                    var deleteCommand = new DeleteCommand<TEntity>(oid);
                    Context.Execute(deleteCommand);
                    OperationResult.Oks.Add(string.Format("Entity {0} was succesfully removed from the database.", oid));
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