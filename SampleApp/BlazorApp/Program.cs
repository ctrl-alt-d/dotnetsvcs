using System.Reflection;
using MyApp.Db;
using Dotnetsvcs.DependencyInjection;
using MyApp.Svcs;
using MyApp.Svcs.Abstractions;
using MyApp.Projections;
using MyApp.Projections.Abstractions;
using Microsoft.EntityFrameworkCore;
using MyApp.Facade.BlazorServer.Abstractions;
using MyApp.Facade.BlazorServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContextFactory<TestDbContext>(
    o => o.UseSqlite("Data Source=Application.db;Cache=Shared"));

builder.Services.AddDotnetsvc<TestDbContext>(
    assembySvcImplementations: Assembly.GetAssembly(typeof(SvcsImplementation))!,
    assemblySvcAbstractions: Assembly.GetAssembly(typeof(SvcsAbstractions))!,
    assembyProjectionsImplementations: Assembly.GetAssembly(typeof(ProjectionsImplementation))!,
    assemblyProjectionsAbstractions: Assembly.GetAssembly(typeof(ProjectionsAbstraction))!,
    assembyFacadeImplementations: Assembly.GetAssembly(typeof(FacadeImplementation))!,
    assemblyFacadeAbstractions: Assembly.GetAssembly(typeof(FacadeAbstractions))!
);

var app = builder.Build();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//
app
    .Services
    .GetRequiredService<IDbContextFactory<TestDbContext>>()
    .CreateDbContext()
    .Database
    .EnsureCreated();


//

app.Run();
