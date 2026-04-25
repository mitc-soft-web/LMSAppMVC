using FluentValidation;
using FluentValidation.AspNetCore;
using LMSAppMVC.Configuration;
using LMSAppMVC.Contracts.MailingService;
using LMSAppMVC.Contracts.Messaging;
using LMSAppMVC.Identity;
using LMSAppMVC.Implementation.MailingService;
using LMSAppMVC.Implementation.Repositories;
using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Interfaces.TemplateEngine;
using LMSAppMVC.LMSDbContext;
using LMSAppMVC.Messaging;
using LMSAppMVC.Models.DTOs.Auth.Validation;
using LMSAppMVC.Models.Entities;
using LMSAppMVC.TemplateEngine;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Scan(scan => scan
    .FromApplicationDependencies(a => a.FullName!.StartsWith("LMSAppMVC"))
    .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
    .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

 builder.Services.AddScoped<IMailSender, MailSender>()
.AddScoped<IRazorEngine, RazorEngine>();

builder.Services.AddHttpContextAccessor();



//builder.Services.AddScoped<IBookService, BookService>();


builder.Services.AddValidatorsFromAssemblyContaining<RegisterMemberRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Add Email Configurations
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));



// Add Database
builder.Services.AddDbContext<LMSContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LmsConnection")
    ));


builder.Services.AddScoped<IUserStore<User>, UserStore>();
builder.Services.AddScoped<IRoleStore<Role>, RoleStore>();
builder.Services.AddIdentity<User, Role>()
    .AddDefaultTokenProviders();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(config =>
  {
      config.LoginPath = "/Auth/login";
      config.Cookie.Name = "LMS";
      config.LogoutPath = "/Auth/logout";
      config.ExpireTimeSpan = TimeSpan.FromMinutes(15);
      config.SlidingExpiration = true;
  });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var db = services.GetRequiredService<LMSContext>();

//        var connection = db.Database.GetDbConnection();
//        Console.WriteLine($"[DB MIGRATION] Attempting to migrate database on host: {connection.DataSource}");

//        db.Database.Migrate();
//        Console.WriteLine("[DB MIGRATION] Success: Database is up to date.");
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex.Message + "An error occurred while migrating the database. Ensure the ConnectionString is correct.");
//        throw;
//    }
//}



app.Run();
