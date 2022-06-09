using AutoMapper;

namespace CarsServer.WebApi.AutoMap
{
    public class MapperConfig
    {
        public static MapperConfiguration GetConfiguration()
        {
            var configExpression = new MapperConfigurationExpression();

            configExpression.AddProfile<CarProfile>();

            var config = new MapperConfiguration(configExpression);
            config.AssertConfigurationIsValid();

            return config;
        }
    }
}