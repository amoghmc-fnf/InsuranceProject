using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InsuranceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly string _uploadPath;

        public UploadController()
        {
            // Set the path where files should be uploaded
            _uploadPath = @"C:\Users\6147952\source\repos\WebApplication1\MyClientAppApi\ClaimDocument\";
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                // Ensure the directory exists
                if (!Directory.Exists(_uploadPath))
                    Directory.CreateDirectory(_uploadPath);

                // Create the file path
                var filePath = Path.Combine(_uploadPath, file.FileName);


                // Save the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Ok(new Response { FilePath = filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    public class Response
    {
        public string FilePath { get; set; }
    }

}

