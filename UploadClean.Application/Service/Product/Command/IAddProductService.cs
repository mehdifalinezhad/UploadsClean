using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.ADOInterface;
using UploadsClean.Common;

namespace UploadClean.Application.Service.Product.Command
{
    public interface IAddProductService
    {
        public ResultDto Execute(AddProductDto dto);
    }
    public class AddPtoduct : IAddProductService
    { 
    
     private readonly IDataBaseContext _context;

        public AddPtoduct(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute (AddProductDto dto)
        {               
            int TheAnswer = _context.Sp_AddProduct(dto);
            if (TheAnswer == 1)
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
    public class AddProductDto
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlLow { get; set; }
        public int Count { get; set; }
        public string prise { get; set; }
    }




}
