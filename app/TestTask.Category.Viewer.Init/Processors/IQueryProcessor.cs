namespace TestTask.Category.Viewer.Init.Processors
{
    using System.Threading.Tasks;

    using CategoryViewer.Services.Queries.Base;

    public interface IQueryProcessor
    {
        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query);
    }
}