using WorkspaceService.Repository;
using WorkspaceService.Services;

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
app.UseHttpsRedirection();

app.MapControllers();
app.Run();
