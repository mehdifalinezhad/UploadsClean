using EndPoint.Admin.Models;
using EndPoint.Admin.Utilities;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using UploadClean.Application.Service.CAtegory.Queries;
using UploadClean.Application.Service.Product.Command;
using UploadsClean.Common;

namespace EndPoint.Admin.Controllers
{
	public class ProductController : Controller

	{

		private readonly IAddProductService _addProduct;
        private readonly IToastNotification _notiShow;
        private readonly IGetAllCateqoryService _getAllCateqory;


		public ProductController(IAddProductService addProduct, IToastNotification notiShow, IGetAllCateqoryService getAllCateqory)
		{
			_addProduct = addProduct;
			_notiShow = notiShow;
			_getAllCateqory = getAllCateqory;
		}

		public IActionResult AddProduct()
		{
			ResultDto<List<CategoryDtoForList>> resultCat = _getAllCateqory.Execute();


			//this view bag fill with data
			//ViewBag.CAtegory = DropDownList.Execute(UploadsClean.Common.Request.category,resultCat.Data);
			ProductModel model = new ProductModel()
			{
				listCat = DropDownList.Execute(UploadsClean.Common.Request.category, resultCat.Data)
			};
			return View(model);
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