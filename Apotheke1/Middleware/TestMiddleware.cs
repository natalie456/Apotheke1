using System.Text.Json;
using Microsoft.AspNetCore.Http;


namespace Apotheke1.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;
        public TestMiddleware(RequestDelegate next) => _next = next;


        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLowerInvariant();
            if (path == "/middleware/test")
            {
                if (context.Request.Method == HttpMethods.Get)
                {
                    context.Response.ContentType = "application/json";
                    var obj = new { success = true, method = "GET", message = "Middleware GET response" };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(obj));
                    return;
                }


                if (context.Request.Method == HttpMethods.Post)
                {
                    // приклад читання body
                    using var sr = new StreamReader(context.Request.Body);
                    var body = await sr.ReadToEndAsync();
                    context.Response.ContentType = "application/json";
                    var obj = new { success = true, method = "POST", received = body };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(obj));
                    return;
                }
            }


            // інакше — пропустити далі
            await _next(context);
        }
    }


    public static class TestMiddlewareExtensions
    {
        public static IApplicationBuilder UseTestMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TestMiddleware>();
        }
    }
}