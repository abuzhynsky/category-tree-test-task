namespace TestTask.CategoryViewer.Services.QueryHandlers.Decorator
{
    using System.Collections.Generic;

    using ApiModels;

    using Base;

    using Common.Exceptions;

    using JetBrains.Annotations;

    using Queries;

    using QueryHandlers.Base;

    public class ByLevelValidateQueryHandlerDecorator : ValidateQueryHandlerDecorator<ByLevelQuery, IEnumerable<CategoryModel>>
    {
        public ByLevelValidateQueryHandlerDecorator([NotNull] IQueryHandler<ByLevelQuery, IEnumerable<CategoryModel>> decoratee) : base(decoratee)
        {
        }

        protected override void Validate(ByLevelQuery query)
        {
            if (query.Level <= 0)
            {
                throw new ValidationException("Level should be more than 0");
            }
        }
    }
}