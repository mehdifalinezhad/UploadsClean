namespace EndPoint.Admin.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
       public string Title {get;set;}
       public string ImageUrl {get; set;}
       public string ImageUrlLow {get; set;}
       public IFormFile File {get;set;}
       public int Count {get;set;}
       public string prise {get;set;}
      
       
    
    }
}
