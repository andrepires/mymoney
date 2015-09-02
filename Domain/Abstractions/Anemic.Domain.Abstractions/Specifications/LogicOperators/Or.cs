namespace MyMoney.Domain.Abstractions.Specifications.LogicOperators
{
    internal class Or<TEntity> : CompositeSpecification<TEntity>
    {
        public override string Name
        {
            get { return "Or Logic Between Specifications"; }
        }

        private ISpecification<TEntity> Left { get; set; }
        private ISpecification<TEntity> Right { get; set; }

        public Or(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            Left = left;
            Right = right;
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return Left.IsSatisfiedBy(entity) || Right.IsSatisfiedBy(entity);
        }

    }
}