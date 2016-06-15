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

    public class AllQueryHandler : IQueryHandler<AllQuery, IEnumerable<CategoryModel>>
    {
        private readonly IRepository _repository;

        public AllQueryHandler(IRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            _repository = repository;
        }

        public async Task<IEnumerable<CategoryModel>> HandleAsync(AllQuery query)
        {
            var categories = await _repository.GetAllAsync().ConfigureAwait(false);

            return Mapper.Map<IEnumerable<CategoryModel>>(categories);
        }
    }
}