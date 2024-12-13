using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.ADOInterface;
using UploadsClean.Common;

namespace UploadClean.Application.Service
{
    public interface IGetAllProduct
    {

        public ResultDto<List<GetAllProductDto>> getAllProductDto();
        // public ResultDto<List<GetAllProductDto> Ex();
    }

    public class GetAllProduct : IGetAllProduct
    {
        private IDataBaseContext _context;

        public GetAllProduct(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<GetAllProductDto>> getAllProductDto()
        {

            List<GetAllProductDto> dtos= _context.GetAllProduct();
            if(dtos==null)
            {
                return new ResultDto<List<GetAllProductDto>>()
                {
                    IsSuccess = false,
                
                }; 


            }

            return new ResultDto<List<GetAllProductDto>>()
            {
               IsSuccess=true,
                Data = dtos 
              
            };    
		}










    }

    public class GetAllProductDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Prise { set; get; }
        public int CategoryProductId { set; get; }
    }

}


