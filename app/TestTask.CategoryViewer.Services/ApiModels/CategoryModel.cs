namespace TestTask.CategoryViewer.Services.ApiModels
{
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature, ImplicitUseTargetFlags.WithMembers)]
    public class CategoryModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public string Keywords { get; set; }
    }
}