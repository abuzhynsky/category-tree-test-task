namespace TestTask.CategoryViewer.Repository.Specifications.Base
{
    using System;
    using System.Linq.Expressions;

    public abstract class Specification<T>
    {
        protected Specification(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Expression<Func<T, bool>> Predicate { get; }
    }
}