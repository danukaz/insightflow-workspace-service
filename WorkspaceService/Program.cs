using WorkspaceService.Repository;
using WorkspaceService.Services;
using WorkspaceService.Seeders;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddSingleton<IWorkspaceRepository, InMemoryWorkspaceRepository>();
builder.Services.AddScoped<WorkspaceServiceImpl>();

var app = builder.Build();


app.UseCors("AllowAll");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetRequiredService<IWorkspaceRepository>();
    WorkspaceSeeder.Seed(repo);
}

app.Run();
