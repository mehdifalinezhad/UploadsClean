using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.ADOInterface;
using UploadClean.Application.Service.CAtegory.Command;
using UploadsClean.Common;

namespace UploadClean.Application.Service.CAtegory.Queries
{
    public interface IGetAllCateqoryService
    {
        public ResultDto<List<CategoryDtoForList>> Execute();

    }

    public class GetAllCateqoryService :IGetAllCateqoryService

    {
        private readonly IDataBaseContext _context;

        public GetAllCateqoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public  ResultDto<List<CategoryDtoForList>> Execute()
        {
            List<CategoryDtoForList> answer = _context.SpGetCategoryList();
            if (answer != null)
            {
                return new ResultDto<List<CategoryDtoForList>>()
                {
                    Data = answer,
                    IsSuccess = true,
                    Message = AppMessage.SUCCESS
                };
            }
            else 
            {
                return new ResultDto<List<CategoryDtoForList>>()
                {
                    
                    IsSuccess = false,
                    Message = AppMessage.ERROR
                };
            }
        }
    }

    public class CategoryDtoForList
    {
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string FilePathLow { get; set; }
    
        

    }

}
