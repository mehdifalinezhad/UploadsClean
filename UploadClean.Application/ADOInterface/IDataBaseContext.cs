using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.Service;
using UploadClean.Application.Service.CAtegory.Command;
using UploadClean.Application.Service.CAtegory.Queries;
using UploadsClean.Common;
using static UploadsClean.Common.Core.Application.Services.Users.Queries.GetRoles.GetUserInfoService;

namespace UploadClean.Application.ADOInterface
{
    public interface IDataBaseContext
    {
        public List<GetAllProductDto> GetAllProduct();
        public UserInfoList_Dto MNG_UserGetByMobileNumber(string UserName);
        int Sp_AddCategory(AddCategoryDto dto);
        List<CategoryDtoForList> SpGetCategoryList();


    }
}