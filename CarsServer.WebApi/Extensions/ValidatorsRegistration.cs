using BusinessLayer.Vlidators;
using CarsServer.Bl.DTOs;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CarsServer.WebApi.Extensions
{
    public static class ValidatorsRegistration
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<CarDto>, CarDtoValidator>();
            services.AddSingleton<IValidator<CarCreateDto>, CarCreateDtoValidator>();
        }
    }
}
