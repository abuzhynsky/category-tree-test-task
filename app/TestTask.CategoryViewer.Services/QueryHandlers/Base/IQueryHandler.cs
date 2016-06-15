namespace TestTask.CategoryViewer.Services.QueryHandlers.Base
{
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Queries.Base;

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}