using Microsoft.EntityFrameworkCore;
using SalasDeReuniaoCRUD.Infra.CrossCutting;
using SalasDeReuniaoCRUD.Infra.Data.Contexts;
using SalasDeReuniaoCRUD.WebApi.Endpoints.Internal;
using SalasDeReuniaoCRUD.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

services.AddCors(opt =>
{
    opt.AddPolicy(name: MyAllowSpecificOrigins,
                 policy =>
                 {
                     policy.AllowAnyOrigin();
                     policy.AllowAnyMethod();
                     policy.AllowAnyHeader();
                 });
});

services.AddServices();

builder.Services.AddDbContext<SalasDeReuniaoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    npgsqlOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
    }));

services.AddControllers();
services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    try
    {
        var context = service.GetRequiredService<SalasDeReuniaoContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = service.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações do banco de dados.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "StockMode API V1");
        options.DocumentTitle = "StockMode API Documentation";
        options.DefaultModelExpandDepth(-1);
    });
}

app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseEndpoints<Program>();

app.MapControllers();

app.Run();
