namespace TestTask.CategoryViewer.Services.QueryHandlers
{
    using System;
    using System.Threading.Tasks;

    using ApiModels;

    using AutoMapper;

    using Base;

    using Queries;

    using Repository;

    using Repository.Specifications;

    public class ByIdKeywordsQueryHandler : IQueryHandler<ByIdKeywordsQuery, CategoryModel>
    {
        private readonly IRepository _repository;

        public ByIdKeywordsQueryHandler(IRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            _repository = repository;
        }

        public async Task<CategoryModel> HandleAsync(ByIdKeywordsQuery query)
        {
            var specification = new ByIdSpecification(query.Id);

            var category = await _repository.FindOneAsync(specification).ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(category.Keywords))
            {
                return Mapper.Map<CategoryModel>(category);
            }

            var categoryWithKeywords = await _repository.GetCategoryWithKeywords(query.Id).ConfigureAwait(false);
            category.Keywords = categoryWithKeywords.Keywords;

            return Mapper.Map<CategoryModel>(category);
        }
    }
}