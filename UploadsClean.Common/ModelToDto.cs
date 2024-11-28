using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadsClean.Domain.Entities.Users;

namespace UploadsClean.Common
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



	}
}
