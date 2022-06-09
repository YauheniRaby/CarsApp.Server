using AutoMapper;
using CarsServer.Bl.Services;
using CarsServer.Bl.Services.Abstract;
using CarsServer.WebApi.AutoMap;
using Microsoft.Extensions.DependencyInjection;

namespace CarsServer.WebApi.Extensions
{
    public static class ServicesRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICarService, CarService>();
            services.AddSingleton<IImageService, ImageService>();
            services.AddSingleton<IMapper>(service => new Mapper(MapperConfig.GetConfiguration())); 
        }
    }
}
