using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.ADOInterface;
using UploadClean.Application.Service;
using static UploadsClean.Common.Core.Application.Services.Users.Queries.GetRoles.GetUserInfoService;

namespace UploadsClean.Persistence.DataBaceContext
{
    public class DataBaseContext : IDataBaseContext
    {
        public List<GetAllProductDto> GetAllProduct()
        {


            List<GetAllProductDto> Dto = new List<GetAllProductDto>();

            return Dto;
        }
        public UserInfoList_Dto MNG_UserGetByMobileNumber(string UserName)
        {
            return new UserInfoList_Dto();  
        }

    }
}