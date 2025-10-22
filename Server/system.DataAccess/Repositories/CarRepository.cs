using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class CarRepository : ICarRepository
{
    private readonly SystemDbContext _context;

    public CarRepository(SystemDbContext context)
    {
        _context = context;
    }
    public async Task<List<Car>> Get()
    {
        var carEntyties = await _context.Cars
            .AsNoTracking()
            .ToListAsync();
        var cars = carEntyties
            .Select(c => Car.Create(
                c.Id,
                c.OwnerId,
                c.Brand,
                c.Model,
                c.YearOfManufacture,
                c.VinNumber,
                c.StateNumber,
                c.Mileage).car)
            .ToList();

        return cars;
    }

    public async Task<int> Create(Car car)
    {
        var carEntyties = new CarEntity
        {
            OwnerId = car.OwnerId,
            Brand = car.Brand,
            Model = car.Model,
            YearOfManufacture = car.YearOfManufacture,
            VinNumber = car.VinNumber,
            StateNumber = car.StateNumber,
            Mileage = car.Mileage
        };

        await _context.Cars.AddAsync(carEntyties);
        await _context.SaveChangesAsync();

        return carEntyties.Id;
    }

    public async Task<int> Update(
        int id,
        string brand,
        string model,
        int? yearOfManufacture,
        string vinNumber,
        string stateNumber,
        int? mileage)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        if (car == null)
            throw new Exception("Car not found");
        if (!string.IsNullOrWhiteSpace(brand))
            car.Brand = brand;
        if (!string.IsNullOrWhiteSpace(model))
            car.Model = model;
        if (yearOfManufacture.HasValue)
            car.YearOfManufacture = yearOfManufacture.Value;
        if (!string.IsNullOrWhiteSpace(vinNumber))
            car.VinNumber = vinNumber;
        if (!string.IsNullOrWhiteSpace(stateNumber))
            car.StateNumber = stateNumber;
        if (mileage.HasValue)
            car.Mileage = mileage.Value;

        await _context.SaveChangesAsync();

        return car.Id;
    }

    public async Task<int> Delete(int id)
    {
        var carEntyties = await _context.Cars
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
