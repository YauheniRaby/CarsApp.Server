using CarsServer.DA.Repositories;
using CarsServer.DA.Repositories.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace CarsServer.WebApi.Extensions
{
    public static class RepositoriesRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            //services.AddSingleton<ICarRepository, CarRepositoryFromDB>();
            services.AddSingleton<ICarRepository, CarRepositoryFromJson>();
        }
    }
}
