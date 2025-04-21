using System.Net;

namespace NZWalks.API.MiddleWare
{
    public class ExeptionHandlerMiddleware
    {
        private readonly ILogger<ExeptionHandlerMiddleware> logger1;
        private readonly RequestDelegate next;

        public ExeptionHandlerMiddleware(ILogger<ExeptionHandlerMiddleware> logger1, RequestDelegate next)
        {
            this.logger1 = logger1;
            this.next = next;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                var errorId = Guid.NewGuid();
               //Log this Exception
               logger1.LogError(ex, $"{errorId}:{ex.Message}");

                //Return a custom Error Response
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went Wrong! We looking to resolve this."

                };
                await  context.Response.WriteAsJsonAsync(error);




            }
        }

    }
}
