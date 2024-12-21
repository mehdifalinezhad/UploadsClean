using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.Service;
using UploadClean.Application.Service.CAtegory.Command;
using UploadClean.Application.Service.CAtegory.Queries;
using UploadClean.Application.Service.Product.Command;
using UploadsClean.Common;
using UploadsClean.Common.Core.Application.Services.Users.Queries.GetRoles;

namespace UploadClean.Application.ADOInterface
{
    public interface IDataBaseContext
    {
        public List<GetAllProductDto> GetAllProduct();
        public UserInfoList_Dto MNG_UserGetByMobileNumber(string UserName);
        public int Sp_AddCategory(AddCategoryDto dto);
        public List<CategoryDtoForList> SpGetCategoryList();
        public int Sp_delCAtegory(int Id);
        public int Sp_AddProduct(AddProductDto dto);
    }
}