using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using SV_CodingCase.Configuration;
using SV_CodingCase.Middleware;

var builder = WebApplication.CreateBuilder(args);
StringReplacer.ReplaceStringInFile(Path.Combine(Directory.GetCurrentDirectory(), "Web/script.js"), "5031", "ASPNETCORE_HTTP_PORTS");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Logging.AddConsole();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    if (Environment.GetEnvironmentVariable("BASE_URL") is not null)
    {
        StringReplacer.ReplaceStringInFile(Path.Combine(Directory.GetCurrentDirectory(), "Web/script.js"), "http://localhost:5031", "BASE_URL");
        StringReplacer.ReplaceStringInFile(Path.Combine(Directory.GetCurrentDirectory(), "Web/script.js"), "http://localhost:8080", "BASE_URL");
    }
}

app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Web/")),
    EnableDefaultFiles = true
});

app.UseRouting();
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});
if (!app.Environment.IsDevelopment())
{
    app.MapGet("/", (HttpContext httpContext) =>
    {
        string? baseUrl = Environment.GetEnvironmentVariable("BASE_URL")!;
        var html = @$"<!DOCTYPE html>
<html lang=""en"">
  <head>
    <meta charset=""UTF-8"" />
    <title>Search Page</title>
    <link rel=""stylesheet"" href=""http://localhost:8080/styles.css"" />
  </head>
  <body>
    <form id=""searchForm"">
      <input type=""text"" id=""searchInput"" placeholder=""Enter search query..."" />
    </form>
    <div id=""searchResults""></div>
    <script src=""http://localhost:8080/script.js""></script>
  </body>
</html>";
        html = string.IsNullOrEmpty(baseUrl) ? html : html.Replace("http://localhost:8080", baseUrl);
        httpContext.Response.ContentType = "text/html";
        return Results.Content(html);
    });
}
app.Run();
