using LMSAppMVC.Implementation.Services;
using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.LMSDbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBookService, BookService>();

// Add Database
builder.Services.AddDbContext<LMSContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("LMSContext"),
        new MySqlServerVersion(new Version(9, 0, 0))
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=CreateBook}/{id?}")
    .WithStaticAssets();


app.Run();
