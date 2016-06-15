namespace TestTask.CategoryViewer.Services.QueryHandlers.Decorator.Base
{
    using System;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using TestTask.CategoryViewer.Services.Queries.Base;
    using TestTask.CategoryViewer.Services.QueryHandlers.Base;

    public abstract class ValidateQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratee;

        protected ValidateQueryHandlerDecorator([NotNull] IQueryHandler<TQuery, TResult> decoratee)
        {
            if (decoratee == null)
            {
                throw new ArgumentNullException(nameof(decoratee));
            }

            this._decoratee = decoratee;
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            this.Validate(query);

            return await this._decoratee.HandleAsync(query).ConfigureAwait(false);
        }

        protected abstract void Validate(TQuery query);
    }
}