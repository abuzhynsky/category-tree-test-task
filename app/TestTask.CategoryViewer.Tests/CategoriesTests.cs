namespace TestTask.CategoryViewer.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Category.Viewer.Init;

    using Common.Exceptions;

    using Domain.Mapping.Mapping;

    using Repository.Linq2Db;
    using Repository.Linq2Db.Database;

    using Services.ApiModels;
    using Services.Queries;
    using Services.QueryHandlers;
    using Services.QueryHandlers.Decorator;

    using Xunit;

    public class CategoriesTests : IDisposable
    {
        private const string RequiresDeployedDatabase = "Requires deployed database";

        private readonly CategoriesDatabase _categoriesDatabase;

        private readonly Repository _repository;

        public CategoriesTests()
        {
            Linq2DbMap.Map();
            MapperRegistrar.Register();

            _categoriesDatabase = new CategoriesDatabase("Categories");
            _repository = new Repository(_categoriesDatabase);
        }

        [Fact(Skip = RequiresDeployedDatabase)]
        public async Task TestGetAll()
        {
            var query = new AllQuery();

            var queryHandler = new AllQueryHandler(_repository);

            var result = await queryHandler.HandleAsync(query).ConfigureAwait(false);

            Assert.Equal(8, result.Count());
            Assert.IsAssignableFrom<IEnumerable<CategoryModel>>(result);
        }

        [Fact(Skip = RequiresDeployedDatabase)]
        public async Task TestGetById()
        {
            var query = new ByIdQuery { Id = 101 };

            var queryHandler = new ByIdValidateQueryHandlerDecorator<ByIdQuery>(new ByIdQueryHandler(_repository));

            var result = await queryHandler.HandleAsync(query).ConfigureAwait(false);

            Assert.IsType<CategoryModel>(result);
            Assert.Equal(101, result.Id);
            Assert.NotNull(result.ParentId);
            Assert.Equal(100, result.ParentId.Value);
            Assert.Equal("Accounting", result.Name);
            Assert.Equal("Taxes", result.Keywords);
        }

        [Fact(Skip = RequiresDeployedDatabase)]
        public async Task TestGetNotExistingById()
        {
            var query = new ByIdQuery { Id = 110 };

            var queryHandler = new ByIdValidateQueryHandlerDecorator<ByIdQuery>(new ByIdQueryHandler(_repository));

            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await queryHandler.HandleAsync(query).ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Theory(Skip = RequiresDeployedDatabase)]
        [InlineData(100, null, "Business", "Money")]
        [InlineData(101, 100, "Accounting", "Taxes")]
        [InlineData(103, 101, "Corporate Tax", "Taxes")]
        public async Task TestGetByIdWithKeyWords(int id, int? parentId, string name, string keywords)
        {
            var query = new ByIdKeywordsQuery { Id = id };

            var queryHandler = new ByIdKeywordsQueryHandler(_repository);

            var result = await queryHandler.HandleAsync(query).ConfigureAwait(false);

            Assert.IsType<CategoryModel>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(parentId, result.ParentId);
            Assert.Equal(name, result.Name);
            Assert.Equal(keywords, result.Keywords);
        }

        [Theory(Skip = RequiresDeployedDatabase)]
        [InlineData(1, new[] { 100, 200 })]
        [InlineData(2, new[] { 101, 102, 201 })]
        [InlineData(3, new[] { 103, 109, 202 })]
        [InlineData(4, new int[0])]
        public async Task TestGetByLevel(int level, int[] ids)
        {
            var query = new ByLevelQuery { Level = level };

            var queryHandler = new ByLevelQueryHandler(_repository);

            var result = await queryHandler.HandleAsync(query).ConfigureAwait(false);

            Assert.IsAssignableFrom<IEnumerable<CategoryModel>>(result);

            var resultIds = result.Select(_ => _.Id).OrderBy(_ => _).ToArray();

            Assert.Equal(ids.Length, resultIds.Length);
            Assert.Equal(ids, resultIds);
        }

        public void Dispose()
        {
            _categoriesDatabase.Dispose();
        }
    }
}