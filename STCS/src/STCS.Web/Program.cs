using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using STCS.Infrastructure;
using STCS.Infrastructure.DbContexts;
using STCS.Infrastructure.Entities.Applications;
using STCS.Infrastructure.Services.Applications;
using STCS.Web;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var assemblyName = Assembly.GetExecutingAssembly().FullName;

builder.Host.UseSerilog((ctx, lc) => lc
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(builder.Configuration));

try
{
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new WebModule());
        containerBuilder.RegisterModule(new InfrastructureModule(connectionString, assemblyName));
    });

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddControllersWithViews();

    builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<ApplicationUserManager>()
    .AddRoleManager<ApplicationRoleManager>()
    .AddSignInManager<ApplicationSignInManager>()
    .AddDefaultTokenProviders();

    var app = builder.Build();

    Log.Information("Started");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}

catch (Exception ex)
{
    Log.Fatal("Crushed", ex);
}

finally
{
    Log.CloseAndFlush();
}

//var builder = WebApplication.CreateBuilder(args);


//builder.Host.UseSerilog((ctx, lc) => lc
//    .MinimumLevel.Debug()
//    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//    .Enrich.FromLogContext()
//    .ReadFrom.Configuration(builder.Configuration));

//try
//{
//    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//    var assemblyName = Assembly.GetExecutingAssembly().FullName;

//    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
//    {
//        containerBuilder.RegisterModule(new WebModule());
//        containerBuilder.RegisterModule(new InfrastructureModule(connectionString,
//            assemblyName));
//    });

//    builder.Services.AddDbContext<ApplicationDbContext>(options =>
//        options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
//    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//    builder.Services
//    .AddIdentity<ApplicationUser, ApplicationRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddUserManager<ApplicationUserManager>()
//    .AddRoleManager<ApplicationRoleManager>()
//    .AddSignInManager<ApplicationSignInManager>()
//    .AddDefaultTokenProviders();

//    builder.Services.AddAuthentication()
//        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//        {
//            options.LoginPath = new PathString("/Home/Index");
//            options.AccessDeniedPath = new PathString("/Account/Login");
//            options.LogoutPath = new PathString("/Account/Logout");
//            options.Cookie.Name = "STCS.Identity";
//            options.SlidingExpiration = true;
//            options.ExpireTimeSpan = TimeSpan.FromHours(1);
//        })
//        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
//        {
//            x.RequireHttpsMetadata = false;
//            x.SaveToken = true;
//            x.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
//                ValidateIssuer = true,
//                ValidateAudience = true,
//                ValidIssuer = builder.Configuration["Jwt:Issuer"],
//                ValidAudience = builder.Configuration["Jwt:Audience"],
//            };
//        });

//    builder.Services.Configure<IdentityOptions>(options =>
//    {
//        // Password settings.
//        options.Password.RequireDigit = true;
//        options.Password.RequireLowercase = false;
//        options.Password.RequireNonAlphanumeric = false;
//        options.Password.RequireUppercase = false;
//        options.Password.RequiredLength = 6;
//        options.Password.RequiredUniqueChars = 0;

//        // Lockout settings.
//        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//        options.Lockout.MaxFailedAccessAttempts = 5;
//        options.Lockout.AllowedForNewUsers = true;

//        // User settings.
//        options.User.AllowedUserNameCharacters =
//        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//        options.User.RequireUniqueEmail = true;
//    });

//    builder.Services.AddControllersWithViews();

//    var app = builder.Build();

//    // Configure the HTTP request pipeline.
//    if (app.Environment.IsDevelopment())
//    {
//        app.UseMigrationsEndPoint();
//    }
//    else
//    {
//        app.UseExceptionHandler("/Home/Error");
//        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//        app.UseHsts();
//    }

//    app.UseHttpsRedirection();
//    app.UseStaticFiles();

//    app.UseRouting();

//    app.UseAuthentication();
//    app.UseAuthorization();

//    app.MapControllerRoute(
//    name: "areas",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

//    app.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");

//    app.Run();
//}

//catch (Exception ex)
//{
//    Log.Fatal(ex, "Application start-up failed");
//}
//finally
//{
//    Log.CloseAndFlush();
//}
