using CarsServer.DA.Helpers;
using CarsServer.DA.Model;
using CarsServer.DA.Repositories.Abstract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarsServer.DA.Repositories
{
    public class CarRepositoryFromJson : ICarRepository
    {
        private readonly ILogger<CarRepositoryFromJson> _logger;

        public CarRepositoryFromJson(ILogger<CarRepositoryFromJson> logger)
        {
            _logger = logger;
        }
        public async Task<List<Car>> GetAllAsync(string pathStorage)
        {
            var result = new List<Car>();
            bool isError = false;
            if (Directory.Exists(pathStorage))
            {
                var files = Directory.GetFiles(pathStorage, "*.json");
                var cars = files.Select(async f =>
                {
                    try
                    {
                        var content = await File.ReadAllTextAsync(f);
                        var car = JsonSerializer.Deserialize<Car>(content);
                        return car;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(null, ex, ex.Message);
                        isError = true;
                        return null;
                    }
                });
                result = (await Task.WhenAll(cars)).ToList();

                if (isError && result.Count > 0)
                {
                    result.RemoveAll(c => c == null);
                }
            }
            return result;
        }

        public async Task<IEnumerable<string>> GetNamesImageAsync(IEnumerable<Guid> idList, string pathStorage)
        {
            var cars = await GetAllAsync(pathStorage);
            var result = cars.Where(car => idList.Contains(car.Id)).Select(car => car.FileName);
            return result;
        }

        public void Delete(IEnumerable<Guid> idList, string pathStorage)
        {
            idList.ToList().ForEach(id =>
            {
                var fullPath = PathHelper.CombineForJson(pathStorage, id.ToString());
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                else
                {
                    _logger.LogWarning(null, $"File {id}.json not exist.");
                }
            });
        }

        public bool Exists(Guid carId, string pathStorage)
        {
            var fullPath = PathHelper.CombineForJson(pathStorage, carId.ToString());
            return File.Exists(fullPath);
        }
        
        public Task CreateOrUpdateAsync(Car car, string pathStorage)
        {
            var fullPath = PathHelper.CombineForJson(pathStorage, car.Id.ToString());
            return File.WriteAllTextAsync(fullPath, JsonSerializer.Serialize(car));
        }
    }
}
