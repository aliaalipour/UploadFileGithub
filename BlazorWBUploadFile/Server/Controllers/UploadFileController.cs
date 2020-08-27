using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWBUploadFile.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {

        [HttpPost]
        [Route("FileUploadAsync")]
        public async Task<bool> FileUploadAsync()
        {
            try
            {

                if (HttpContext.Request.Form.Files.Any())
                {
                    var file = HttpContext.Request.Form.Files.FirstOrDefault();
                    string name = file.Name;
                    string fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\CourseImage\" + name + @"\");

                    filePath = filePath.Replace("\\Server\\", "\\Client\\");

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    using (var stream = new FileStream(filePath + fileName, FileMode.OpenOrCreate))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
