using BlazorServerApp.Areas.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Db;
using System.Reflection;
using Dotnetsvcs.DependencyInjection;
using MyApp.Svcs;
using MyApp.Svcs.Abstractions;
using MyApp.Projections;
using MyApp.Projections.Abstractions;
using MyApp.Facade.BlazorServer;
using MyApp.Facade.BlazorServer.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContextFactory<TestDbContext>(
    o => o.UseSqlite("Data Source=../Tmp/Application.db;Cache=Shared"));

builder.Services.AddDotnetsvc<TestDbContext>(
    assembySvcImplementations: Assembly.GetAssembly(typeof(SvcsImplementation))!,
    assemblySvcAbstractions: Assembly.GetAssembly(typeof(SvcsAbstractions))!,
    assembyProjectionsImplementations: Assembly.GetAssembly(typeof(ProjectionsImplementation))!,
    assemblyProjectionsAbstractions: Assembly.GetAssembly(typeof(ProjectionsAbstraction))!,
    assembyFacadeImplementations: Assembly.GetAssembly(typeof(FacadeImplementation))!,
    assemblyFacadeAbstractions: Assembly.GetAssembly(typeof(FacadeAbstractions))!
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<TestDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app
    .Services
    .GetRequiredService<IDbContextFactory<TestDbContext>>()
    .CreateDbContext()
    .Database
    .EnsureCreated();


app.Run();
