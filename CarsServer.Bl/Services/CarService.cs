using AutoMapper;
using CarsServer.Bl.Configuration;
using CarsServer.Bl.DTOs;
using CarsServer.Bl.Services.Abstract;
using CarsServer.DA.Model;
using CarsServer.DA.Repositories.Abstract;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsServer.Bl.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<AppConfiguration> _optionsMonitor;
        private readonly IImageService _imageService;

        public CarService(ICarRepository carRepository, IMapper mapper, IOptionsMonitor<AppConfiguration> optionsMonitor, IImageService imageService)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _optionsMonitor = optionsMonitor;
            _imageService = imageService;
            
        }

        public async Task<List<CarDto>> GetAllAsync(string hostUrl)
        {
            var cars = await _carRepository.GetAllAsync(_optionsMonitor.CurrentValue.StorageJsonPath);
            var result = _mapper.Map<List<CarDto>>(cars);
            BulkSetPathAdress(result, hostUrl);
            return result;
        }

        public bool ExistsAsync(Guid carId)
        {
            return _carRepository.Exists(carId, _optionsMonitor.CurrentValue.StorageJsonPath);
        }

        public async Task DeleteAsync(IEnumerable<Guid> idList)
        {
            var namesImage = await _carRepository.GetNamesImageAsync(idList, _optionsMonitor.CurrentValue.StorageJsonPath);
            _imageService.DeleteFiles(namesImage);
            _carRepository.Delete(idList, _optionsMonitor.CurrentValue.StorageJsonPath);            
        }

        public async Task<CarDto> CreateAsync(CarCreateDto carCreateDto)
        {
            var car = _mapper.Map<Car>(carCreateDto);
            await _carRepository.CreateOrUpdateAsync(car, _optionsMonitor.CurrentValue.StorageJsonPath);
            return _mapper.Map<CarDto>(car);
        }

        public Task UpdateAsync(CarDto carDto)
        {
            return _carRepository.CreateOrUpdateAsync(_mapper.Map<Car>(carDto), _optionsMonitor.CurrentValue.StorageJsonPath);
        }

        private void BulkSetPathAdress(List<CarDto> carDtos, string hostUrl)
        {
            carDtos.ForEach(carDto => carDto.Path = string.Format(_optionsMonitor.CurrentValue.StorageImagesUri, hostUrl, carDto.FileName));
        }

    }
}
