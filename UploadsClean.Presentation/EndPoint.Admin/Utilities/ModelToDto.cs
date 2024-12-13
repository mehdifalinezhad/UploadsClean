using EndPoint.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.Service.CAtegory.Command;
using UploadsClean.Common;
using UploadsClean.Domain.Entities.Users;

namespace EndPoint.Admin.Utilities
{
	public static class ModelToDto
	{
         public static ApplicationUser UserModelToDto(UserModel user)
		{
			ApplicationUser userdto = new ApplicationUser(){
				FarsiFirstName=user.FarsiFirstName,
				Password=user.Password,
				FarsiLastName = user.FarsiLastName,
				PhoneNumber=user.PhoneNumber,
				Role=user.Role,
				UserName=user.UserName,	
				
			};
			return userdto;
		}


		public static ApplicationUser UserSignInModelToDto(SignInmodelv user)
		{
			ApplicationUser userdto = new ApplicationUser()
			{
				
				Password = user.Username,
				UserName = user.Username,

			};
			return userdto;
		}


		public static AddCategoryDto CategoryModelToDto(CategoryModel model)
		{
			AddCategoryDto userdto = new AddCategoryDto()
			{
				Title= model.Title,	
				FilePath=UploadImages.SaveImage(model.File,"Category"),
				FilePathLow=UploadImages.SaveImageLow(model.File,"CategoryLow")
				
			};
			return userdto;
		}


	}
}
