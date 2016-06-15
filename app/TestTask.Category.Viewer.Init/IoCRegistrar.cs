namespace TestTask.Category.Viewer.Init
{
    using System.Web.Http;

    using CategoryViewer.Repository;
    using CategoryViewer.Repository.Linq2Db;
    using CategoryViewer.Repository.Linq2Db.Database;
    using CategoryViewer.Services.QueryHandlers.Base;
    using CategoryViewer.Services.QueryHandlers.Decorator;

    using Processors;

    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    public static class IoCRegistrar
    {
        public static void Register(HttpConfiguration configuration)
        {
            var container = new Container();

            container.Register<IQueryProcessor, QueryProcessor>();

            container.RegisterWebApiRequest(() => new CategoriesDatabase("Categories"));
            container.RegisterWebApiRequest<IRepository>(() => new Repository(container.GetInstance<CategoriesDatabase>()));

            container.Register(typeof(IQueryHandler<,>), new[] { typeof(IQueryHandler<,>).Assembly });

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(ByIdValidateQueryHandlerDecorator<>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(ByLevelValidateQueryHandlerDecorator));

            container.RegisterWebApiControllers(configuration);

            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
