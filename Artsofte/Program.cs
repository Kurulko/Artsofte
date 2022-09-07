using Artsofte.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


IServiceCollection services = builder.Services;

services.AddControllersWithViews().AddRazorRuntimeCompilation();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ArtsofteContext>(opts =>
{
    opts.UseSqlServer(connection);
    opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapDefaultControllerRoute();
app.Run();
