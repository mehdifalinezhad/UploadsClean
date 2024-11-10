

namespace EndPoint.Admin.Utilities
{
    public static class CurrentUser
    {
        
        public static ActiveUser Get()
        {
            HttpContextAccessor httpContextAccessor = new();
            return SessionExtension.GetObject<ActiveUser>(httpContextAccessor.HttpContext.Session, "ActiveUser");
          
                
        }
        public static void Set(ActiveUser activeUser)
        {
            HttpContextAccessor httpContextAccessor = new();
            SessionExtension.SetObject(httpContextAccessor.HttpContext.Session, "ActiveUser", activeUser);


        }
    }
}
