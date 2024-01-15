using Microsoft.AspNetCore.Http;

namespace BusinessApp.Business.Helpers
{
    public static class FileManager
    {
        public static bool CheckType(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }

        public static bool CheckLength(this IFormFile file, int length) 
        {
            return file.Length / 1024 / 1024 < length;
        }

        public static async Task<string> UploadFile(this IFormFile file, string web, string path)
        {
            var UploadPath = Path.Combine(web, path);

            if(!Directory.Exists(UploadPath)) 
            {
                Directory.CreateDirectory(UploadPath);
            }

            string FileName = Guid.NewGuid().ToString() + file.FileName;

            using(var stream = new FileStream(Path.Combine(UploadPath, FileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return FileName;
        }
    }
}
