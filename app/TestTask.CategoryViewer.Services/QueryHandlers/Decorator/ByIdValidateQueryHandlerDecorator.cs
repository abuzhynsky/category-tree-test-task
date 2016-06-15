namespace TestTask.CategoryViewer.Services.QueryHandlers.Decorator
{
    using ApiModels;

    using Base;

    using Common.Exceptions;

    using JetBrains.Annotations;

    using Queries;

    using QueryHandlers.Base;

    public class ByIdValidateQueryHandlerDecorator<TQuery> : ValidateQueryHandlerDecorator<TQuery, CategoryModel> where TQuery : ByIdQuery
    {
        public ByIdValidateQueryHandlerDecorator([NotNull] IQueryHandler<TQuery, CategoryModel> decoratee) : base(decoratee)
        {
        }

        protected override void Validate(TQuery query)
        {
            if (query.Id <= 0)
            {
                throw new ValidationException("Id should be more than 0");
            }
        }
    }
}