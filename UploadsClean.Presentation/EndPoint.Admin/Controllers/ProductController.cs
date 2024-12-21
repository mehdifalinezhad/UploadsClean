using EndPoint.Admin.Models;
using EndPoint.Admin.Utilities;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using UploadClean.Application.Service.Product.Command;
using UploadsClean.Common;

namespace EndPoint.Admin.Controllers
{
	public class ProductController : Controller

	{

		private readonly IAddProductService _addProduct;
        private readonly IToastNotification _notiShow;


        public ProductController(IAddProductService addProduct, IToastNotification notiShow)
        {
            _addProduct = addProduct;
            _notiShow = notiShow;
        }

        public IActionResult AddProduct()
		{

			return View();
		}
		
		
		[HttpPost]
		public IActionResult AddProduct(ProductModel model)
		{
            AddProductDto dtoToAdd = ModelToDto.AddproductToModel(model);

            ResultDto resultDto = _addProduct.Execute(dtoToAdd);
            if (!resultDto.IsSuccess)    
            {
                _notiShow.AddAlertToastMessage("ناتوان در درج محصول");
                return View();

            }
            _notiShow.AddSuccessToastMessage(AppMessage.SUCCESS);
               return View();
         //   return RedirectToAction("ListProduct");
		}
        public IActionResult ListProduct()
        {

		


            return View();
        }


    }



}