using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadClean.Application.ADOInterface;
using UploadClean.Application.Service;
using UploadClean.Application.Service.CAtegory.Command;
using UploadClean.Application.Service.CAtegory.Queries;
using UploadClean.Application.Service.Product.Command;
using UploadsClean.Common.Core.Application.Services.Users.Queries.GetRoles;

namespace UploadsClean.Persistence.DataBaceContext
{
    public class DataBaseContext : IDataBaseContext
    {
        private readonly IConfiguration _configuration;

      //  public object Convertor { get; private set; }

        public DataBaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<GetAllProductDto> GetAllProduct()
        {
            List<GetAllProductDto> Dto = new List<GetAllProductDto>();

            return Dto;
        }
        public UserInfoList_Dto MNG_UserGetByMobileNumber(string UserName)
        {
            return new UserInfoList_Dto();
        }
        public int Sp_AddCategory(AddCategoryDto dto)
        {
            var connectionSql = _configuration.GetSection("connectionStrings").GetSection("defaultConnection").Value;

            SqlConnection connection = new SqlConnection(connectionSql);
            SqlCommand command = new SqlCommand("SP_AddCategoyStore", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Title", dto.Title);
            command.Parameters.AddWithValue("@FilePath", dto.FilePath);
            command.Parameters.AddWithValue("@FilePathLow", dto.FilePathLow);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                return 1;
            }
            catch
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }


        public List<CategoryDtoForList> SpGetCategoryList()
        {
            string conwctionsql = _configuration.GetSection("connectionStrings").GetSection("defaultConnection").Value;
            SqlConnection connection = new SqlConnection(conwctionsql);
            SqlCommand command = new SqlCommand("SpGetCategoryListStore", connection);
 //OutPut Parametrs ===> command.Parameters.Add
 //(new SqlParameter("@qqq",SqlDbType.Int)).Direction= ParameterDirection.Output; 
            command.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            try
            {
                connection.Open();

                dt.Load(command.ExecuteReader());
                //int a = Convertor.ToInt(command.Parameters["@qqq"].Value));
                List<CategoryDtoForList> dtos = new List<CategoryDtoForList>();


                foreach (DataRow item in dt.Rows)
                {

                    dtos.Add(new CategoryDtoForList()
                    {
                        Id = item["id"].ToInt(),
                        Title = item["Title"].ToString(),
                        FilePath = item["FilePath"].ToString(),
                        FilePathLow = item["FilePathLow"].ToString()
                    });
                }

                    return dtos;
            }
            catch
            {
                return new List<CategoryDtoForList>();

            }
            finally
            {
                connection.Close();
            }
        }


      
       public int Sp_delCAtegory(int Id) 
        {
            string connection = _configuration.GetSection("connectionStrings").GetSection("defaultConnection").Value;
            SqlConnection sqlConnection= new SqlConnection(connection);
            SqlCommand command = new SqlCommand("SpDelCategory_Del", sqlConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@Id",Id);
            command.Parameters.Add(new SqlParameter("@Id", Id));
            try
            {

                sqlConnection.Open();
                command.ExecuteNonQuery();
                return 1;
            }
            catch
            {
                return -1;
            }
            finally
            { 
                sqlConnection.Close();  
            }

        }

        public int Sp_AddProduct(AddProductDto dto)
        {
            string connection = _configuration.GetSection("connectionStrings").GetSection("defaultConnection").Value;
            SqlConnection sc = new SqlConnection(connection);
            SqlCommand command = new SqlCommand("SP_AddProduct", sc);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Title", dto.Title));
            command.Parameters.Add(new SqlParameter("@imageUrl", dto.ImageUrl));
            command.Parameters.Add(new SqlParameter("@imageUrlLow", dto.ImageUrlLow));
            command.Parameters.Add(new SqlParameter("@Count", dto.Count));
            command.Parameters.Add(new SqlParameter("@Prise", dto.prise));
            try
            {
                sc.Open();
                command.ExecuteNonQuery();
                return 1;
            }
            catch 
            {
                return -1;
            }
        }

    }
}    