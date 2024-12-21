using EndPoint.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.Service.CAtegory.Command;
using UploadClean.Application.Service.Product.Command;
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

      public static AddProductDto AddproductToModel(ProductModel model)
		{
			AddProductDto dto = new AddProductDto()
			{ 
			Title= (model.Title==null)?string.Empty:model.Title,
            Count= (model.Count == null) ?0 : model.Count,
            ImageUrl= (model.ImageUrl == null) ? string.Empty : model.ImageUrl,
            ImageUrlLow = (model.ImageUrlLow == null) ? string.Empty : model.ImageUrlLow,
             prise = (model.prise == null) ? string.Empty : model.prise

           
			};


			return dto;
        }


    }
}
