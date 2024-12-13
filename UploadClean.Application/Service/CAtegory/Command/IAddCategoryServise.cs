using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.ADOInterface;
using UploadsClean.Common;

namespace UploadClean.Application.Service.CAtegory.Command
{
	public interface IAddCategoryServise
	{
		public ResultDto Execute(AddCategoryDto dto); 

	}
	public class AddCategoryServise : IAddCategoryServise
	{

		private readonly IDataBaseContext _context;

		public AddCategoryServise(IDataBaseContext context)
		{
			_context = context;
		}

		public ResultDto Execute(AddCategoryDto dto)
		{
			int Answer = _context.Sp_AddCategory(dto);
			if (Answer == 1)
			{

				return new ResultDto
				{
					IsSuccess = true,
					Message = AppMessage.SUCCESS

				};

			}
			else 
			{
				return new ResultDto
				{
					IsSuccess = true,
					Message = AppMessage.ERROR

				};

			}
		}

	}


	public class AddCategoryDto
     { 
		public string Title { get; set; }
		public  string FilePath { get; set; }
		public  string FilePathLow { get; set; }


     }


}
