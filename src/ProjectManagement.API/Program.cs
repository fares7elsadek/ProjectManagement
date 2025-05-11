using ProjectManagement.API.Extensions;
using ProjectManagement.API.Middlewares;
using ProjectManagement.Application.Extensions;
using ProjectManagement.Domain.Extensions;
using ProjectManagement.Infrastructure.Extensions;
using ProjectManagement.Infrastructure.Seeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDomain(builder.Configuration);
builder.AddAPI();
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
await SeedDatabaseAsync(app);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();



async Task SeedDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IAppSeeder>();
    await seeder.SeedAsync();
}
