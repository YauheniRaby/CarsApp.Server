using CarsServer.DA.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsServer.DA.Repositories.Abstract
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllAsync(string pathStorage);

        bool Exists(Guid carId, string pathStorage);

        void Delete(IEnumerable<Guid> idList, string pathStorage);

        Task CreateOrUpdateAsync(Car car, string pathStorage);

        Task<IEnumerable<string>> GetNamesImageAsync(IEnumerable<Guid> idList, string pathStorage);
    }
}
