using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Helper
{
    public static class FileUploader
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
			try
			{
                string folderPath = Directory.GetCurrentDirectory() + @"\wwwroot\Files\" + folderName + "\\";
                string Name = Guid.NewGuid() + Path.GetFileName(file.FileName);
                string FinalPath = Path.Combine(folderPath, Name);
                using (var stream = new FileStream(FinalPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Name;
                
            }
			catch (Exception ex)
			{

				return ex.Message;
			}
        }
        public static string RemoveFile(string folderName, string fileName)
        {
            try
            {
                string filePath =Path.Combine( Directory.GetCurrentDirectory() , "wwwroot\\Files" , folderName  , fileName);
                if (File.Exists(filePath))
                    File.Delete(filePath);
                
                
                return "File Deleted";

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

    }
}
