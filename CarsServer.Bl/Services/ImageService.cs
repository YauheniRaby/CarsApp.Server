using CarsServer.Bl.Configuration;
using CarsServer.Bl.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarsServer.Bl.Services
{
    public class ImageService : IImageService
    {
        private readonly IOptionsMonitor<AppConfiguration> _optionMonitor;

        public ImageService(IOptionsMonitor<AppConfiguration> optionMonitor)
        {
            _optionMonitor = optionMonitor;
        }

        public string SaveFile(HttpRequest httpRequest)
        {
            if (httpRequest.Form.Files.Count > 0)
            {
                var file = httpRequest.Form.Files[0];

                var stream = file.OpenReadStream();
                if (stream.Length > 0)
                {
                    var path = _optionMonitor.CurrentValue.StorageImagePath;
                    Directory.CreateDirectory(path);

                    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                    
                    using (FileStream fileStream = File.Create($"{path}{fileName}", (int)stream.Length))
                    {
                        byte[] data = new byte[stream.Length];

                        stream.Read(data, 0, data.Length);
                        fileStream.Write(data, 0, data.Length);
                    }
                    if (File.Exists($"{path}{fileName}"))
                    {
                        return fileName;
                    };
                }
            }
            return string.Empty;  
        }

        public void DeleteFiles(IEnumerable<string> namesImage)
        {
            var path = _optionMonitor.CurrentValue.StorageImagePath;
            namesImage.ToList().ForEach(name => File.Delete($"{path}{name}"));
        }

        public bool ExistFile(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
