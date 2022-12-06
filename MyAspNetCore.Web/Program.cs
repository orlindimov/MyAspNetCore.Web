using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyAspNetCore.Web.Filters;
using MyAspNetCore.Web.Helpers;
using MyAspNetCore.Web.Models;
using MyAspNetCore.Web.Validators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductValidator>());


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
//resim eklemek icin

builder.Services.AddScoped<IHelper, Helper>();//helper dan nesne ornegi uretir

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<NotFoundFilter>();

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
//kimlik yetkilendirme kimlik dogrulama islemi yapar.


// blog/abc => blog controller > article action method calissin
// blog/ddd => blog controller > article action method calissin
//app.MapControllerRoute(
//    name: "pages",
//    pattern: "blog/{*article}",
//    defaults:new {controller="blog",action="Article"});

//app.MapControllerRoute(
//    name: "article",
//    pattern: "{controller=Blog}/{action=Article}/{name}/{id}");


//app.MapControllerRoute(
//    name: "productpages",//random bi isim veriyoruz
//    pattern: "{controller}/{action}/{page}/{pagesize}");
//app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
