namespace TestTask.CategoryViewer.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Common.Exceptions;

    using Moq;

    using Services.ApiModels;
    using Services.Queries;
    using Services.QueryHandlers.Base;
    using Services.QueryHandlers.Decorator;

    using Xunit;

    public class ValidatorDecoratorsTests
    {
        [Fact]
        public async Task TestGetByIdValidation()
        {
            var mock = new Mock<IQueryHandler<ByIdQuery, CategoryModel>>();

            var query = new ByIdQuery { Id = 0 };

            var queryHandler = new ByIdValidateQueryHandlerDecorator<ByIdQuery>(mock.Object);

            await Assert.ThrowsAsync<ValidationException>(async () => await queryHandler.HandleAsync(query).ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestGetByLevelValidation()
        {
            var mock = new Mock<IQueryHandler<ByLevelQuery, IEnumerable<CategoryModel>>>();

            var query = new ByLevelQuery { Level = 0 };

            var queryHandler = new ByLevelValidateQueryHandlerDecorator(mock.Object);

            await Assert.ThrowsAsync<ValidationException>(async () => await queryHandler.HandleAsync(query).ConfigureAwait(false)).ConfigureAwait(false);
        }
    }
}