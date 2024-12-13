using EndPoint.Admin.Models;
using UploadClean.Application.Service.CAtegory.Queries;

namespace EndPoint.Admin.Utilities
{
    public static class DtoToModel
    {
        public static List<CategoryModel> ListCategoryModelToDto(List<CategoryDtoForList> listDto)
        {
            var models = new List<CategoryModel>();
            foreach (var item in listDto)
            {
                models.Add(new CategoryModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    ImageUrl = item.FilePath,
                    ImageUrlLow = item.FilePathLow 
                });

            }
            return models;


        }
    }
}
