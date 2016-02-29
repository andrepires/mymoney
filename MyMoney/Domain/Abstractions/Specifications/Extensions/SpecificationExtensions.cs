using MyMoney.Domain.Abstractions.Specifications.LogicOperators;

namespace MyMoney.Domain.Abstractions.Specifications.Extensions
{
    public static class SpecificationExtensions
    {
        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            return new And<TEntity>(left, right);
        }

        public static ISpecification<TEntity> Or<TEntity>(this ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            return new Or<TEntity>(left, right);
        }

        public static ISpecification<TEntity> Not<TEntity>(this ISpecification<TEntity> specification)
        {
            return new Not<TEntity>(specification);
        }
    }
}