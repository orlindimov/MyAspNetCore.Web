using MiddlewareExample.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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



#region MyRegion
//next ile middleware i degerlendirip bi sonrakine aktaricaz
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Before 1.Middleware\n");

//    await next();

//    await context.Response.WriteAsync("after 1.Middleware\n");


//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Before 2.Middleware\n");

//    await next();

//    await context.Response.WriteAsync("after 2.Middleware\n");


//});

////sonlandirici middleware burdan itibaren geriye donucek
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Terminal 3.Middleware\n");
//});

#endregion map ve ran kullanmi

//app.Map("/ornek", app =>
//{
//    app.Run(async context =>
//    {
//        await context.Response.WriteAsync("ornek url i icin miidleware");
//    });
//});



app.UseMiddleware<WhiteIpAddressControlMiddleware>();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
