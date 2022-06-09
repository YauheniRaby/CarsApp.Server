using CarsServer.DA.Configuration;
using CarsServer.DA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsServer.DA.Repositories
{
    public class CarRepositoryFromDB /*: ICarRepository*/
    {
        private readonly RepositoryContext _context;

    public CarRepositoryFromDB(RepositoryContext context)
    {
        _context = context;
    }

    public Task CreateAsync(Car car)
    {
        _context.Cars.Add(car);
        return _context.SaveChangesAsync();
    }

    public Task DeleteAsync(IEnumerable<Guid> idList)
    {
        var cars = _context.Cars.Where(car => idList.Any(id => car.Id.ToString() == id.ToString()));
        _context.Cars.RemoveRange(cars);
        return _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(Guid carId)
    {
        return _context.Cars.AnyAsync(car => car.Id.ToString() == carId.ToString());
    }

    public Task<List<Car>> GetAllAsync()
    {
        return _context.Cars.ToListAsync();
    }

    public IEnumerable<string> GetNamesImage(IEnumerable<Guid> idList)
    {
        return _context.Cars.Where(car => idList.Any(id => car.Id.ToString() == id.ToString())).Select(car => car.FileName);
    }

    public Task UpdateAsync(Car car)
    {
        var carCurrent = _context.Cars.Find(car.Id);
        _context.Entry(carCurrent).CurrentValues.SetValues(car);

        return _context.SaveChangesAsync();
    }
}
}
