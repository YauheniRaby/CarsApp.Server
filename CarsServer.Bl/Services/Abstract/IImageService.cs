using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CarsServer.Bl.Services.Abstract
{
    public interface IImageService
    {
        string SaveFile(HttpRequest httpRequest);

        void DeleteFiles(IEnumerable<string> namesImage);

        bool ExistFile(string filePath);
    }
}
