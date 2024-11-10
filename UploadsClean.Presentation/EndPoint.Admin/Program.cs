using EndPoint.Admin.Authorization.ClaimBasedAuthorization;
using EndPoint.Admin.Security.Default;
using EndPoint.Admin.Security.DynamicRole;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UploadClean.Application.ADOInterface;
using UploadsClean.Domain.Entities.Users;
using UploadsClean.Persistence.DataBaceContext;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

   builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


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





