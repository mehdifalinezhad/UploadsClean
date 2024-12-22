using EndPoint.Admin.Authorization.ClaimBasedAuthorization;
using EndPoint.Admin.Security.Default;
using EndPoint.Admin.Security.DynamicRole;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using UploadClean.Application.ADOInterface;
using UploadClean.Application.Service.CAtegory.Command;
using UploadClean.Application.Service.CAtegory.Queries;
using UploadClean.Application.Service.Product.Command;
using UploadsClean.Domain.Entities.Users;
using UploadsClean.Persistence.DataBaceContext;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = new PathString("/Auth/Login");
        options.Cookie.SameSite = SameSiteMode.Unspecified;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20.0);
        options.AccessDeniedPath = new PathString("/Auth/Login");
        options.LogoutPath = new PathString("/Auth/Login");
        options.SlidingExpiration = true;

        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    });
    builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
    builder.Services.AddScoped<IAddCategoryServise, AddCategoryServise>();
    builder.Services.AddScoped<IGetAllCateqoryService, GetAllCateqoryService>();
    builder.Services.AddScoped<IDelCategoryService, DelCategoryService>();
    builder.Services.AddScoped<IAddProductService, AddPtoduct>();

    builder.Services.AddMvc().AddNToastNotifyNoty(new NotyOptions
    {
        Timeout = 2000,
        ProgressBar = true,
        Modal = true,
        Layout = "topCenter",
        Theme = "metroui"
    });

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSingleton<IAuthorizationHandler, ClaimHandler>();
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
  {
      options.IdleTimeout = TimeSpan.FromMinutes(20);
      options.Cookie.HttpOnly = true;
      options.Cookie.IsEssential = true;
  });
    builder.Services.AddClaimBasedAuthorization();
    builder.Services.AddAuthorizationBuilder()
     .AddPolicy("DynamicRole", policy =>
         policy.Requirements.Add(new DynamicRoleRequirement()));

    builder.Services.AddDbContext<AppDbContext>();

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                 .AddDefaultTokenProviders()
                 .AddEntityFrameworkStores<AppDbContext>();

    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = false;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    //use middleware
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.Run();







