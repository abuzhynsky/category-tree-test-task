namespace TestTask.CategoryViewer.Repository.Linq2Db
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Common.Exceptions;

    using Database;

    using Domain.Model;

    using LinqToDB;

    using Specifications.Base;

    public class Repository : IRepository
    {
        private readonly CategoriesDatabase _database;

        public Repository(CategoriesDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            _database = database;
        }

        public async Task<Category> GetCategoryWithKeywords(int id)
        {
            var category = await _database.GetCategoryWithKeywords(id).SingleOrDefaultAsync().ConfigureAwait(false);

            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            return category;
        }

        public async Task<IReadOnlyCollection<Category>> GetCategoriesByLevel(int level)
        {
            return await _database.GetCategoriesByLevel(level).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<Category>> GetAllAsync()
        {
            return await _database.Categories.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Category> FindOneAsync(Specification<Category> specification)
        {
            var category = await _database.Categories.AsQueryable().SingleOrDefaultAsync(specification.Predicate).ConfigureAwait(false);

            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            return category;
        }
    }
}