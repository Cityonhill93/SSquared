using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using SSquared.Lib.Data;
using SSquared.Lib.Repositories.Extensions;
using SSquared.Lib.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SSquaredDbContext>();
builder.Services.AddApiVersioning(e =>
{
    e.DefaultApiVersion = new ApiVersion(1);
    e.AssumeDefaultVersionWhenUnspecified = true;
    e.ReportApiVersions = true;
});
builder.Services.AddRepositories();
builder.Services.AddSSquaredServices();

var app = builder.Build();

//Apply migrations
using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
using var dbContext = scope.ServiceProvider.GetRequiredService<SSquaredDbContext>();
dbContext.Database.Migrate();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
