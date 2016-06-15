namespace TestTask.CategoryViewer.Services.Queries
{
    using System.Collections.Generic;

    using ApiModels;
    using Base;

    public class ByLevelQuery : IQuery<IEnumerable<CategoryModel>>
    {
        public int Level { get; set; }
    }
}