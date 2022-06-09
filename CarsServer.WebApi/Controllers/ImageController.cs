using CarsServer.Bl.Configuration;
using CarsServer.Bl.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace CarsServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IOptionsMonitor<AppConfiguration> _optionsMonitor;
        public ImageController(IImageService imageService, IOptionsMonitor<AppConfiguration> optionsMonitor)
        {
            _imageService = imageService;
            _optionsMonitor = optionsMonitor;
        }

        [HttpPost]
        public ActionResult<string> PostUpload()
        {
            var fileName = _imageService.SaveFile(Request);
            return Ok(fileName);
        }

        [HttpGet("{fileName}")]
        public ActionResult GetDownload(string fileName)
        {
            string file_path = $"{Environment.CurrentDirectory}\\{_optionsMonitor.CurrentValue.StorageImagePath}{fileName}";
            if(_imageService.ExistFile(file_path))
            return PhysicalFile(file_path, "image/jpeg", fileName);
            return BadRequest();
        }
    }
}
