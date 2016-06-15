namespace TestTask.CategoryViewer.Services.QueryHandlers
{
    using System.Threading.Tasks;

    using AutoMapper;

    using TestTask.CategoryViewer.Repository;
    using TestTask.CategoryViewer.Repository.Specifications;
    using TestTask.CategoryViewer.Services.ApiModels;
    using TestTask.CategoryViewer.Services.Queries;
    using TestTask.CategoryViewer.Services.QueryHandlers.Base;

    public class ByIdQueryHandler : IQueryHandler<ByIdQuery, CategoryModel>
    {
        private readonly IRepository _repository;

        public ByIdQueryHandler(IRepository repository)
        {
            this._repository = repository;
        }

        public async Task<CategoryModel> HandleAsync(ByIdQuery query)
        {
            var specification = new ByIdSpecification(query.Id);

            var category = await this._repository.FindOneAsync(specification).ConfigureAwait(false);

            return Mapper.Map<CategoryModel>(category);
        }
    }
}