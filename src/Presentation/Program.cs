using Serilog;
using Presentation.Middleware;
using Application.Boopstrap;
using Infrastructure.Boopstrap;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

// Servicios
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.ConfigureLogging();

var app = builder.Build();

// Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Autenticación v1");
        c.RoutePrefix = "docs";
    });
}

app.UseSerilogRequestLogging();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();