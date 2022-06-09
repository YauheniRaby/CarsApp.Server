using CarsServer.Bl.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsServer.Bl.Services.Abstract
{
    public interface ICarService
    {
        Task<List<CarDto>> GetAllAsync(string hostUrl);

        bool ExistsAsync(Guid carId);

        Task DeleteAsync(IEnumerable<Guid> idList);

        Task<CarDto> CreateAsync(CarCreateDto carCreateDto);

        Task UpdateAsync(CarDto carDto);
    }
}
