namespace TestTask.CategoryViewer.Domain.Model
{
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class Category
    {
        public int CategoryId { get; set; }

        public int? ParentCategoryId { get; set; }

        public string Name { get; set; }

        public string Keywords { get; set; }
    }
}