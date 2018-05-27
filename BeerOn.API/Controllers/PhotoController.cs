using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BeerOn.API.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IHostingEnvironment _host;

        public PhotoController(IHostingEnvironment host)
        {
            _host = host;
        }

        //GET api/file/id
        [HttpGet]
        [Route("api/photo/{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            if (fileName == null || fileName == "null")
                return BadRequest();

            var stream = _host.WebRootPath + "\\uploads\\" + fileName;
            var imageFileStream = System.IO.File.OpenRead(stream);
            return File(imageFileStream, "image/jpeg");
        }
    }
}
