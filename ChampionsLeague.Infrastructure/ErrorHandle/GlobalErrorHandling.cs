namespace ChampionsLeague.Infrastructure.ErrorHandle
{
    public class GlobalErrorHandling() : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //logger.LogError(exception, exception.Message);

            var details = new ProblemDetails()
            {
                Detail = $"Api Error {exception.Message}",
                Instance = "API",
                Status = (int) HttpStatusCode.InternalServerError,
                Title = "Api Error: ",
                Type = "Server Error"
            };

            var respose = JsonSerializer.Serialize(details);

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(respose, cancellationToken);

            return true;
        }
    }
}
