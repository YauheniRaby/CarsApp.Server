using AutoMapper;
using CarsServer.Bl.DTOs;
using CarsServer.DA.Model;
using System;

namespace CarsServer.WebApi.AutoMap
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.Path, opt => opt.Ignore());
            CreateMap<CarDto, Car>();

            CreateMap<CarCreateDto, Car>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => Guid.NewGuid()));
        }
    }
}