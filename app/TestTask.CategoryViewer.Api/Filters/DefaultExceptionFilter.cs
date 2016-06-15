namespace TestTask.CategoryViewer.Api.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    using Common.Exceptions;

    public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is EntityNotFoundException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, context.Exception.Message);
                return;
            }

            if (context.Exception is ValidationException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.Exception.Message);
                return;
            }

            context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, context.Exception.Message);
        }
    }
}