using Aztu_Events.Core.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Aztu_Events.Core.Helper.FileHelper
{
    public static class FileHelper
    {

        public static async Task<bool> AutoRemove(List<string> DataUrls)
        {
            string uploadsPath = Path.Combine(WWWRootGetPaths.GetwwwrootPath + "//uploads//");

            try
            {

                string[] UploadsFolderPhoto = Directory.GetFiles(uploadsPath);

                foreach (string Upload in DataUrls)
                {
                    if (!UploadsFolderPhoto.Contains(Path.Combine(WWWRootGetPaths.GetwwwrootPath + Upload.Replace("/", "//"))))
                    {
                        File.Delete(UploadsFolderPhoto.FirstOrDefault(x => x == Path.Combine(WWWRootGetPaths.GetwwwrootPath + Upload.Replace("/", "//"))).ToString());
                    }
                }

                //Parallel.ForEach(DataUrls, i =>
                //{
                //    if (!UploadsFolderPhoto.Contains(Path.Combine(WWWRootGetPaths.GetwwwrootPath+i.Replace("/","//"))))
                //    {
                //        File.Delete(UploadsFolderPhoto.FirstOrDefault(x=>x== Path.Combine(WWWRootGetPaths.GetwwwrootPath + i.Replace("/", "//"))));
                //    }  
                //});

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static async Task<string> SaveFileAsync(this IFormFile file, string WebRootPath)
        {
            //string filePath = Path.Combine(WWWRootGetPaths.GetwwwrootPath, "uploads");
            //if (!Directory.Exists(filePath))
            //{
            //    Directory.CreateDirectory(filePath);
            //}
            //var path = "/uploads/" + Guid.NewGuid().ToString() + file.FileName;
            //using FileStream fileStream = new(WWWRootGetPaths.GetwwwrootPath + path, FileMode.Create);
            //await file.CopyToAsync(fileStream);
            //return path;
            string uploadsPath = Path.Combine(WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            string fileNameWithoutSpaces = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "");
            string fileExtension = Path.GetExtension(file.FileName);
            string uniqueFileName = $"{Guid.NewGuid().ToString()}_{fileNameWithoutSpaces}{fileExtension}";
            string filePath = Path.Combine(uploadsPath, uniqueFileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/uploads/{uniqueFileName}";
        }
        public static async Task<string> SavePdfAsync(this IFormFile file,string WebRootPath)
        {
            string filePath = Path.Combine(WWWRootGetPaths.GetwwwrootPath, "uploads", "PDFs");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var path = "/uploads/PDFs/" + Guid.NewGuid().ToString() + file.FileName.Replace(" ","_");
            using FileStream fileStream = new(WWWRootGetPaths.GetwwwrootPath + path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return path;
        }
        public static bool RemoveFileRange(this List<string> PhotoPaths)
        {
            foreach (var path in PhotoPaths)
            {
                string filePath = Path.Combine(WWWRootGetPaths.GetwwwrootPath + path);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);

                }
                else
                {
                    continue;
                }

            }
            return true;
        }
        public static bool RemoveFile(this string PhotoPath)
        {

            string filePath = Path.Combine(WWWRootGetPaths.GetwwwrootPath + PhotoPath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);

            }
            else
            {
                return false;
            }



            return true;
        }

    }
}
