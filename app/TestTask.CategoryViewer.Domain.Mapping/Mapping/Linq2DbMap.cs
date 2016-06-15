namespace TestTask.CategoryViewer.Domain.Mapping.Mapping
{
    using LinqToDB.Mapping;

    using TestTask.CategoryViewer.Domain.Model;

    public static class Linq2DbMap
    {
        public static void Map()
        {
            var mappingSchema = MappingSchema.Default;
            var builder = mappingSchema.GetFluentMappingBuilder();

            builder.Entity<Category>()
                .HasTableName("category")
                .Property(t => t.CategoryId)
                    .HasColumnName("category_id")
                    .IsIdentity()
                    .IsPrimaryKey()
                .Property(t => t.ParentCategoryId)
                    .HasColumnName("parent_category_id")
                .Property(t => t.Name)
                    .HasColumnName("name")
                .Property(t => t.Keywords)
                    .HasColumnName("keywords");
        }
    }
}