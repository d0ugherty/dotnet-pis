using DotNetPIS.ApiClient;
using DotNetPIS.Data;
using DotNetPIS.Data.Repositories;
using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=../DotNetPIS.Data/dotnetpis.db";
    opt.UseSqlite(connectionString, x => x.MigrationsAssembly("DotNetPIS.Data"));
    opt.EnableSensitiveDataLogging();
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<SeptaApiClient>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

builder.Services.AddScoped<ISeptaApiClient, SeptaApiClient>();

builder.Services.AddScoped<StopService>();
builder.Services.AddScoped<RouteService>();
builder.Services.AddScoped<MapService>();
builder.Services.AddScoped<InfoBoardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
