namespace TestTask.Category.Viewer.Init
{
    using AutoMapper;

    using CategoryViewer.Domain.Model;
    using CategoryViewer.Services.ApiModels;

    public static class MapperRegistrar
    {
        public static void Register()
        {
            Mapper.Initialize(
                mc =>
                    {
                        mc.CreateMap<Category, CategoryModel>()
                            .ForMember(destination => destination.Id, source => source.MapFrom(_ => _.CategoryId))
                            .ForMember(destination => destination.ParentId, source => source.MapFrom(_ => _.ParentCategoryId))
                            .ForMember(destination => destination.Name, source => source.MapFrom(_ => _.Name))
                            .ForMember(destination => destination.Keywords, source => source.MapFrom(_ => _.Keywords));
                    });
        }
    }
}