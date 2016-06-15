namespace TestTask.CategoryViewer.Repository.Linq2Db.Database
{
    using System.Reflection;

    using Domain.Model;

    using LinqToDB;
    using LinqToDB.Data;

    public class CategoriesDatabase : DataConnection
    {
        public CategoriesDatabase(string connectionString) : base(connectionString)
        {
        }

        public ITable<Category> Categories
        {
            get
            {
                return GetTable<Category>();
            }
        }

        [Sql.TableFunction(Name = "get_category_with_keywords")]
        public ITable<Category> GetCategoryWithKeywords(int id)
        {
            return GetTable<Category>(this, (MethodInfo)MethodBase.GetCurrentMethod(), id);
        }

        [Sql.TableFunction(Name = "get_categories_by_level")]
        public ITable<Category> GetCategoriesByLevel(int level)
        {
            return GetTable<Category>(this, (MethodInfo)MethodBase.GetCurrentMethod(), level);
        }
    }
}