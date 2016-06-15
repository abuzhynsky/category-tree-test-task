namespace TestTask.CategoryViewer.Repository.Specifications
{
    using Base;

    using Domain.Model;

    public class ByIdSpecification : Specification<Category>
    {
        public ByIdSpecification(int id) : base(_ => _.CategoryId == id)
        {
        }
    }
}