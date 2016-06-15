namespace TestTask.CategoryViewer.Api
{
    using System.Web.Http;

    using Category.Viewer.Init;

    using Domain.Mapping.Mapping;

    using Filters;

    using JetBrains.Annotations;

    using Owin;

    using Swashbuckle.Application;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            Linq2DbMap.Map();

            MapperRegistrar.Register();

            var httpConfiguration = GlobalConfiguration.Configuration;
            IoCRegistrar.Register(httpConfiguration);

            httpConfiguration.Filters.Add(new DefaultExceptionFilterAttribute());

            GlobalConfiguration.Configure(WebApiConfig.Register);
            httpConfiguration.EnsureInitialized();

            httpConfiguration
                .EnableSwagger(c => c.SingleApiVersion("v1", "Category Viewer"))
                .EnableSwaggerUi();

            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}