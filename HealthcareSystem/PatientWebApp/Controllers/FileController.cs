using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.DTOs;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public RedirectResult Post([FromForm] IFormFile file, [FromQuery] string jmbg)
        {
            if (file == null || file.Length == 0)
            {
                return RedirectPermanent("http://localhost:65117/html/upload_image.html");
            }

            string path = _webHostEnvironment.WebRootPath + "\\Uploads\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fileStream = System.IO.File.Create(path + file.FileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            return RedirectPermanent("http://localhost:65117/html/patients_home_page.html");
        }
    }
}
