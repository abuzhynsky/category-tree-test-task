namespace TestTask.CategoryViewer.Services.Queries.Base
{
    using System.Diagnostics.CodeAnalysis;

    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    [SuppressMessage("ReSharper", "UnusedTypeParameter", Justification = "Used by query handlers")]
    public interface IQuery<TResult>
    {
    }
}