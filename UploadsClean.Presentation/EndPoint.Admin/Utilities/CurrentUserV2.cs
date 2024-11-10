
using Microsoft.AspNetCore.Identity;
using UploadsClean.Common.Core.Application.Services.Users.Queries.GetRoles;
using UploadsClean.Domain.Entities.Users;


namespace EndPoint.Admin.Utilities
{
    public class CurrentUserV2:ICurrentUserV2
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGetUserInfoService _getUserInfoService;
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserV2(UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor, IGetUserInfoService getUserInfoService)
        {
            _accessor = accessor;
            _getUserInfoService = getUserInfoService;
            _userManager = userManager;
        }
        public ActiveUser Get()
        {
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(httpContextAccessor.HttpContext.Session, "ActiveUser");
            
            if(activeUser!=null && activeUser.Id!=null && activeUser.Id.Length>0)
                return activeUser;

            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                if (Fill(httpContextAccessor.HttpContext.User.Identity.Name))
                {
                    return SessionExtension.GetObject<ActiveUser>(httpContextAccessor.HttpContext.Session, "ActiveUser");
                }
                else
                    return null;
            }
            return null;
        }
        public  void Set(ActiveUser activeUser)
        {
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            SessionExtension.SetObject(httpContextAccessor.HttpContext.Session, "ActiveUser", activeUser);
        }
        public  void Clear()
        {

            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            SessionExtension.SetObject(httpContextAccessor.HttpContext.Session, "ActiveUser", new ActiveUser());


        }
        public  void Reset()
        {

            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            SessionExtension.SetObject(httpContextAccessor.HttpContext.Session, "ActiveUser", null);


        }
        public  bool Fill(string UserName)
        {
            ApplicationUser user = _userManager.FindByNameAsync(UserName).Result;
            List<string> userRoles = (_userManager.GetRolesAsync(user).Result).ToList();
            

            if (user != null)
            {
                var UserInfo = _getUserInfoService.Execute(UserName);
                
                var activeUser = new ActiveUser()
                {
                    UserName = user.UserName,
                    MyKey = UserInfo.Data.MyKey,
                    FirstName = UserInfo.Data.FirstName,
                    LastName = UserInfo.Data.LastName,
                    Mobile = UserInfo.Data.Mobile,
                    UserFullName = ((UserInfo.Data.FirstName.Length > 0) ? UserInfo.Data.FirstName : user.UserName) + " " + UserInfo.Data.LastName,
                    UserIpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    Id = user.Id
                };
                Set(activeUser);
                return true;
            }
            else
            {
                return false;
            }



        }
    }
}
