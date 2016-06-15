namespace TestTask.CategoryViewer.Common.Exceptions
{
    using System;

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("Entity wasn\'t found in the system.")
        {
        }
    }
}
