

using UploadClean.Application.ADOInterface;
using UploadsClean.Common;
using static UploadsClean.Common.Core.Application.Services.Users.Queries.GetRoles.GetUserInfoService;

namespace UploadsClean.Common.Core.Application.Services.Users.Queries.GetRoles
{
    public interface IGetUserInfoService
    {
        ResultDto<UserInfoList_Dto> Execute(string UserName);
    }
    public class GetUserInfoService : IGetUserInfoService
    {
        private readonly IDataBaseContext _context;
        public GetUserInfoService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<UserInfoList_Dto> Execute(string UserName)
        {
            UserInfoList_Dto UserInfo = _context.MNG_UserGetByMobileNumber(UserName);
            if (UserInfo != null)
            {
                return new ResultDto<UserInfoList_Dto>()
                {
                    Data = UserInfo,
                    IsSuccess = true,
                    Message = "عملیات موفق آمیز",
                };
            }
            else 
            
            {
                return new ResultDto<UserInfoList_Dto>()
                {

                    IsSuccess = true,
                    Message = "عملیات با خطا",
                };
            }


        }

        public class UserInfoList_Dto
        {
            public long MyKey { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Mobile { get; set; }
        }

    }

}
