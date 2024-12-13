using EndPoint.Admin.Models;
using EndPoint.Admin.Utilities;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using UploadClean.Application.Service.CAtegory.Command;
using UploadClean.Application.Service.CAtegory.Queries;
using UploadsClean.Common;


namespace EndPoint.Admin.Controllers
{
	public class CategoryController : Controller
	{
		private readonly IAddCategoryServise _addCategory;
		private readonly IToastNotification _notiShow;
		private readonly IGetAllCateqoryService _getAllCateqory;
        public CategoryController(IAddCategoryServise addCategory, IToastNotification notiShow, IGetAllCateqoryService getAllCateqory)
        {
            _addCategory = addCategory;
            _notiShow = notiShow;
            _getAllCateqory = getAllCateqory;
        }

        public IActionResult AddCategory()
		{
			return View();
		}

		[HttpPost]
		public  IActionResult AddCategory(CategoryModel model)
		{

			AddCategoryDto CatToAdd = ModelToDto.CategoryModelToDto(model);
		   
			ResultDto result = _addCategory.Execute(CatToAdd);
			if (!result.IsSuccess)
			{
				_notiShow.AddErrorToastMessage("عملیات درج دسته بندی کالا با خطا مواجه شد");	
				return RedirectToAction(nameof(LisCategory));
			}

			_notiShow.AddSuccessToastMessage(AppMessage.SUCCESS);
			return RedirectToAction(nameof(LisCategory));
		}



		public IActionResult LisCategory()
		{
			ResultDto<List<CategoryDtoForList>> result = _getAllCateqory.Execute();
			if (!result.IsSuccess)
			{
                _notiShow.AddErrorToastMessage("ناتوان در نشان دادن لیست دسته بندی محصولات");
                return View();
            }

			List<CategoryModel> model =DtoToModel.ListCategoryModelToDto(result.Data);

            return View(model);
		}


	}
}
