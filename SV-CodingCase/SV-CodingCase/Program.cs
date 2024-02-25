using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using SV_CodingCase.Configuration;
using SV_CodingCase.Middleware;

var builder = WebApplication.CreateBuilder(args);
StringReplacer.ReplaceStringInFile(Path.Combine(Directory.GetCurrentDirectory(), "Web/script.js"), "5031");

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
    app.MapGet("/", () =>
 new ContentResult
 {
     ContentType = "text/html",
     Content = "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\" /><title>Search Page</title><link rel=\"stylesheet\" href=\"https://sv-coding-case.onrender.com/styles.css\" /></head><body><form id=\"searchForm\"><input type=\"text\" id=\"searchInput\" placeholder=\"Enter search query...\" /></form><div id=\"searchResults\"></div><script src=\"https://sv-coding-case.onrender.com/script.js\"></script></body></html>\r\n"
 }
);
}
app.Run();
