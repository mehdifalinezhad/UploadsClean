using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using UploadClean.Application.Service;
using UploadsClean.Domain.Entities.Users;


namespace EndPoint.Admin.Security.DynamicRole
{
    public class DynamicRoleHandler : AuthorizationHandler<DynamicRoleRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICommonUtilities _utilities;
        private readonly IMemoryCache _memoryCache;
        private readonly IDataProtector _protectorToken;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
       // private readonly AppDbContext _dbContext;

        public DynamicRoleHandler(IHttpContextAccessor contextAccessor, ICommonUtilities utilities, IDataProtectionProvider dataProtectionProvider, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager/*, AppDbContext appDbContext*/)
        {
            _contextAccessor = contextAccessor;
            _utilities = utilities;
        
            _protectorToken = dataProtectionProvider.CreateProtector("RvgGuid");
            _signInManager = signInManager;
            _userManager = userManager;
            //_dbContext = appDbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicRoleRequirement requirement)
        {
            var httpContext = _contextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return;

            var dbRoleValidationGuid = _memoryCache.GetOrCreate("RoleValidationGuid", p =>
            {
                p.AbsoluteExpiration = DateTimeOffset.MaxValue;
                return _utilities.DataBaseRoleValidationGuid();
            });

            //var allAreasName = _memoryCache.GetOrCreate("allAreasName", p =>
            //{
            //    p.AbsoluteExpiration = DateTimeOffset.MaxValue;
            //    return _utilities.GetAllAreasNames();
            //});

            SplitUserRequestedUrl(httpContext,
                out var areaAndActionAndControllerName);

            UnprotectRvgCookieData(httpContext, out var unprotectedRvgCookie);

            if (!IsRvgCookieDataValid(unprotectedRvgCookie, userId, dbRoleValidationGuid))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) return;

                AddOrUpdateRvgCookie(httpContext, dbRoleValidationGuid, userId);

                await _signInManager.RefreshSignInAsync(user);

                // اگر بخواهیم درصورتی که کاربر کوکی از قبل نداشت هم وارد کنترلر درخواست شده شود این قسمت را از کامنت خارج می کنیم
                // اگر نخواهیم به دیتابیس سر بزنیم ، در صفحه AccessDenied میتوانیم یک دکمه قرار دهیم و از کاربر بخواهیم در صورتی که فکر میکند مشکلی بوجود آمده مجدد کلیک کند و به صفحه ReturnUrl او را منتقل کنیم
                //var userRolesId = _dbContext.UserRoles.AsNoTracking()
                //    .Where(r => r.UserId == userId)
                //    .Select(r => r.RoleId)
                //    .ToList();
                //if (!userRolesId.Any()) return;
                //var userHasClaims = _dbContext.RoleClaims.AsNoTracking().Any(rc =>
                //    userRolesId.Contains(rc.RoleId) && rc.ClaimType == areaAndActionAndControllerName);
                //if (userHasClaims) context.Succeed(requirement);

                // را به متد لاگین اضافه کنیمAddOrUpdateRvgCookie  همچنین میتوانیم متد 
            }
            else if (httpContext.User.HasClaim(areaAndActionAndControllerName, true.ToString()))
                context.Succeed(requirement);

            return;
        }

        #region Methods

        //private void SplitUserRequestedUrl(string url, IList<string> areaNames,
        //    out string areaAndControllerAndActionName)
        //{
        //    var requestedUrl = url.Split('/')
        //        .Where(t => !string.IsNullOrEmpty(t)).ToList();
        //    var urlCount = requestedUrl.Count;
        //    if (urlCount != 0 &&
        //        areaNames.Any(t => t.Equals(requestedUrl[0], StringComparison.CurrentCultureIgnoreCase)))
        //    {
        //        var areaName = requestedUrl[0];
        //        var controllerName = (urlCount == 1) ? "HomeController" : requestedUrl[1] + "Controller";
        //        var actionName = (urlCount > 2) ? requestedUrl[2] : "Index";
        //        areaAndControllerAndActionName = $"{areaName}|{controllerName}|{actionName}".ToUpper();
        //    }
        //    else
        //    {
        //        var areaName = "NoArea";
        //        var controllerName = (urlCount == 0) ? "HomeController" : requestedUrl[0] + "Controller";
        //        var actionName = (urlCount > 1) ? requestedUrl[1] : "Index";
        //        areaAndControllerAndActionName = $"{areaName}|{controllerName}|{actionName}".ToUpper();
        //    }

        //}
        private void SplitUserRequestedUrl(HttpContext httpContext, out string areaAndControllerAndActionName)
        {
            var areaName = httpContext.Request.RouteValues["area"]?.ToString() ?? "NoArea";
            var controllerName = httpContext.Request.RouteValues["controller"].ToString() + "Controller";
            var actionName = httpContext.Request.RouteValues["action"].ToString();
            areaAndControllerAndActionName = $"{areaName}|{controllerName}|{actionName}".ToUpper();
        }
        private void UnprotectRvgCookieData(HttpContext httpContext, out string unprotectedRvgCookie)
        {
            var protectedRvgCookie = httpContext.Request.Cookies
                .FirstOrDefault(t => t.Key == "RVG").Value;
            unprotectedRvgCookie = null;
            if (!string.IsNullOrEmpty(protectedRvgCookie))
            {
                try
                {
                    unprotectedRvgCookie = _protectorToken.Unprotect(protectedRvgCookie);
                }
                catch (CryptographicException)
                {
                }
            }
        }

        private bool IsRvgCookieDataValid(string rvgCookieData, string validUserId, string validRvg)
            => !string.IsNullOrEmpty(rvgCookieData) &&
               SplitUserIdFromRvgCookie(rvgCookieData) == validUserId &&
               SplitRvgFromRvgCookie(rvgCookieData) == validRvg;

        private string SplitUserIdFromRvgCookie(string rvgCookieData)
            => rvgCookieData.Split("|||")[1];

        private string SplitRvgFromRvgCookie(string rvgCookieData)
            => rvgCookieData.Split("|||")[0];

        private string CombineRvgWithUserId(string rvg, string userId)
            => rvg + "|||" + userId;

        private void AddOrUpdateRvgCookie(HttpContext httpContext, string validRvg, string validUserId)
        {
            var rvgWithUserId = CombineRvgWithUserId(validRvg, validUserId);
            var protectedRvgWithUserId = _protectorToken.Protect(rvgWithUserId);
            httpContext.Response.Cookies.Append("RVG", protectedRvgWithUserId,
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(90),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                });
        }


        #endregion
    }
}
