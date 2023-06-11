using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace API_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFile(string fileName)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images/Books", fileName);
            string contentType = GetImageContentType(imagePath);

            return new PhysicalFileResult(imagePath, contentType); ;
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile formFile)
        {
            var file = formFile; // Lấy tệp tin ảnh từ request

            if (file != null && file.Length > 0)
            {
                // Đường dẫn đến thư mục lưu trữ ảnh trong dự án
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Images/Books");

                // Tạo đường dẫn đến tệp tin ảnh mới
                string filePath = Path.Combine(uploadPath, file.FileName);

                // Lưu tệp tin ảnh vào thư mục "Images"
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return Ok(file.FileName);
            }

            return BadRequest("No image file uploaded.");
        }

        private string GetImageContentType(string imagePath)
        {
            // Lấy đuôi file từ đường dẫn ảnh
            string fileExtension = Path.GetExtension(imagePath);

            // Kiểm tra đuôi file và trả về ContentType tương ứng
            switch (fileExtension)
            {
                case ".jpg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                default:
                    return "application/octet-stream"; // ContentType mặc định nếu không xác định được định dạng
            }
        }
    }
}
