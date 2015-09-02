namespace MyMoney.Domain.Abstractions.Specifications.LogicOperators
{
    internal class Not<TEntity> : CompositeSpecification<TEntity>
    {
        public override string Name
        {
            get { return "Not Logic Between Specifications"; }
        }

        private ISpecification<TEntity> Wrapped { get; set; }

        public Not(ISpecification<TEntity> wrapped)
        {
            Wrapped = wrapped;
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return !Wrapped.IsSatisfiedBy(entity);
        }

    }
}