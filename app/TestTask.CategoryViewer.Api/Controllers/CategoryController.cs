namespace TestTask.CategoryViewer.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Category.Viewer.Init.Processors;

    using JetBrains.Annotations;

    using Services.ApiModels;
    using Services.Queries;

    public class CategoryController : ApiController
    {
        private readonly IQueryProcessor _queryProcessor;

        public CategoryController([NotNull] IQueryProcessor queryProcessor)
        {
            if (queryProcessor == null)
            {
                throw new ArgumentNullException(nameof(queryProcessor));
            }

            _queryProcessor = queryProcessor;
        }

        public async Task<IEnumerable<CategoryModel>> Get()
        {
            var query = new AllQuery();

            return await _queryProcessor.ProcessAsync(query).ConfigureAwait(true);
        }

        public async Task<CategoryModel> Get(int id)
        {
            var query = new ByIdQuery { Id = id };

            return await _queryProcessor.ProcessAsync(query).ConfigureAwait(true);
        }

        [Route("api/category/{id}/keywords")]
        public async Task<CategoryModel> GetKeywords(int id)
        {
            var query = new ByIdKeywordsQuery { Id = id };

            return await _queryProcessor.ProcessAsync(query).ConfigureAwait(true);
        }

        [Route("api/category/level/{level}")]
        public async Task<IEnumerable<CategoryModel>> GetByLevel(int level)
        {
            var query = new ByLevelQuery { Level = level };

            return await _queryProcessor.ProcessAsync(query).ConfigureAwait(true);
        }
    }
}
