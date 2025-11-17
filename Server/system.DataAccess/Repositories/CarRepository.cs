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
        var carEntities = await _context.Cars
            .AsNoTracking()
            .ToListAsync();
        var cars = carEntities
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

    public async Task<int> GetCount()
    {
        return await _context.Cars.CountAsync();
    }
 
    public async Task<List<Car>> GetByOwnerId(int ownerId)
    {
        var carEntity = await _context.Cars
            .Where(c  => c.OwnerId == ownerId)
            .AsNoTracking()
            .ToListAsync();

        var cars = carEntity
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

    public async Task<int> GetCountByOwnerId(int ownerId)
    {
        return await _context.Cars.CountAsync(c => c.OwnerId == ownerId);
    }

    public async Task<List<Car>> GetById(List<int> carIds)
    { 
        var carEntity = await _context.Cars
            .AsNoTracking()
            .Where(c => carIds.Contains(c.Id))
            .ToListAsync();

        var cars = carEntity
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

    public async Task<int> GetCountById(List<int> carIds)
    {
        return await _context.Cars.Where(c => carIds.Contains(c.Id)).CountAsync();
    }

    public async Task<int> Create(Car car)
    {
        var (_, error) = Car.Create(
            0,
            car.OwnerId,
            car.Brand,
            car.Model,
            car.YearOfManufacture,
            car.VinNumber,
            car.StateNumber,
            car.Mileage);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Car: {error}");

        var carEntities = new CarEntity
        {
            OwnerId = car.OwnerId,
            Brand = car.Brand,
            Model = car.Model,
            YearOfManufacture = car.YearOfManufacture,
            VinNumber = car.VinNumber,
            StateNumber = car.StateNumber,
            Mileage = car.Mileage
        };

        await _context.Cars.AddAsync(carEntities);
        await _context.SaveChangesAsync();

        return carEntities.Id;
    }

    public async Task<int> Update(int id, string? brand, string? model, int? yearOfManufacture, string? vinNumber, string? stateNumber, int? mileage)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id) 
            ?? throw new Exception("Car not found");

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
        var carEntities = await _context.Cars
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
