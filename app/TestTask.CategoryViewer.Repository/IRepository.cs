namespace TestTask.CategoryViewer.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Domain.Model;

    using Specifications.Base;

    public interface IRepository
    {
        Task<Category> GetCategoryWithKeywords(int id);

        Task<IReadOnlyCollection<Category>> GetCategoriesByLevel(int level);

        Task<IReadOnlyCollection<Category>> GetAllAsync();

        Task<Category> FindOneAsync(Specification<Category> specification);
    }
}