using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UploadClean.Application.Service
{
    public interface IGetAllBestSell
    {
       
    }

    public class GetAllBestSell: IGetAllBestSell
    {
        public GetAllBestSell() { }
    
    }

    public class GetAllBestCellDto
    {
        public int Id;
        public string Name;

    }
    
         
}
