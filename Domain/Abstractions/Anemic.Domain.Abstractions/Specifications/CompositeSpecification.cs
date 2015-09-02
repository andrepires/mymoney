using MyMoney.Domain.Abstractions.Specifications.LogicOperators;

namespace MyMoney.Domain.Abstractions.Specifications
{
    public abstract class CompositeSpecification<TEntity> : ISpecification<TEntity>
    {
        public abstract string Name { get; }

        public abstract bool IsSatisfiedBy(TEntity entity);

        public ISpecification<TEntity> And(ISpecification<TEntity> other)
        {
            return new And<TEntity>(this, other);
        }

        public ISpecification<TEntity> Or(ISpecification<TEntity> other)
        {
            return new Or<TEntity>(this, other);
        }

        public ISpecification<TEntity> Not()
        {
            return new Not<TEntity>(this);
        }

    }

}