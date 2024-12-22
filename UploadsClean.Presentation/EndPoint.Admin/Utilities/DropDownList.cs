using UploadClean.Application.Service.CAtegory.Queries;
using UploadsClean.Common;
using UploadsClean.Persistence.DataBaceContext;

namespace EndPoint.Admin.Utilities
{
	public static class DropDownList
	{
		public static List<ListItemDto> Execute(Request request,object List)//out items
		{
			switch (request)
			{
				case Request.category:
					{
						var items = new List<ListItemDto>();
						foreach (CategoryDtoForList item in (List<CategoryDtoForList>)List)
						{
							
							items.Add(new ListItemDto()
							{
								Id = item.Id,
								Title = item.Title,

							});
						}
						items.Insert(0, new ListItemDto() { Id = 0, Title = "لطفا دسته بندی مربوط به این کالا را انتخاب کنید"});
						return items;
						
					}
				default:
					return new List<ListItemDto>();
					
				

			}
			
		}



	}

	public class ListItemDto
	{ 
	public int Id { get; set; }
	public string Title { get; set; }
	
	
	}



}
