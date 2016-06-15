namespace TestTask.Category.Viewer.Init.Processors
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using CategoryViewer.Services.Queries.Base;
    using CategoryViewer.Services.QueryHandlers.Base;

    using JetBrains.Annotations;

    using SimpleInjector;

    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class QueryProcessor : IQueryProcessor
    {
        private readonly Container _container;

        public QueryProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public async Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = _container.GetInstance(handlerType);

            return await handler.HandleAsync((dynamic)query);
        }
    }
}