using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadsClean.Common
{
	public static class UploadImages
	{
		public static string SaveImage(IFormFile file, string DirectoryName)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory() + @"\wwwroot" + @$"\{DirectoryName}\");
			if (!Directory.Exists(path))
			{ 
				Directory.CreateDirectory(path);
			}
			string SaveName = Guid.NewGuid().ToString() + file.FileName;
			var SavedUrl = $"/{DirectoryName}/{SaveName}";
            IFormFile Image = file;
            var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", DirectoryName)).Root + $@"\{SaveName}";

			using (FileStream fs = File.Create(filepath))
			{
                Image.CopyTo(fs);
				fs.Flush();
			}

			return SavedUrl;
		}
        public static string SaveImageLow(IFormFile file, string DirectoryName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory() + @"\wwwroot" + @$"\{DirectoryName}\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string SaveName = Guid.NewGuid().ToString() + file.FileName;
            var SavedUrl = $"/{DirectoryName}/{SaveName}";

            var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", DirectoryName)).Root + $@"\{SaveName}";
			//
            using (FileStream fs = File.Create(filepath,100))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return SavedUrl;
        }
    }
}
