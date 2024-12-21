using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.ADOInterface;
using UploadsClean.Common;

namespace UploadClean.Application.Service.CAtegory.Command
{
    public interface IDelCategoryService
    {
        public ResultDto Execute(int Id);

    }
    public class DelCategoryService : IDelCategoryService
    {
            private readonly IDataBaseContext _context;

            public DelCategoryService(IDataBaseContext context)
            {
                _context = context;
            }


            public ResultDto Execute(int Id)
            {
                int a = _context.Sp_delCAtegory(Id);
                if (a == 1)
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
                        IsSuccess = false,
                        Message = AppMessage.ERROR
                    };

                }


            }


    }

}
