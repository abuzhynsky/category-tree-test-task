namespace TestTask.CategoryViewer.Services.QueryHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ApiModels;

    using AutoMapper;

    using Base;

    using Queries;

    using Repository;

    public class ByLevelQueryHandler : IQueryHandler<ByLevelQuery, IEnumerable<CategoryModel>>
    {
        private readonly IRepository _repository;

        public ByLevelQueryHandler(IRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            _repository = repository;
        }

        public async Task<IEnumerable<CategoryModel>> HandleAsync(ByLevelQuery query)
        {
            var categories = await _repository.GetCategoriesByLevel(query.Level).ConfigureAwait(false);

            return Mapper.Map<IEnumerable<CategoryModel>>(categories);
        }
    }
}