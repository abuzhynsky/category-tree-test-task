namespace TestTask.CategoryViewer.Services.Queries
{
    using ApiModels;

    using Base;

    public class ByIdQuery : IQuery<CategoryModel>
    {
        public int Id { get; set; }
    }
}