using CRMSystem.Core.DTOs.Car;
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

    private IQueryable<CarEntity> ApplyFilter(IQueryable<CarEntity> query, CarFilter filter)
    {
        if (filter.OwnerIds != null && filter.OwnerIds.Any())
            query = query.Where(c => filter.OwnerIds.Contains(c.OwnerId));

        return query;
    }

    public async Task<List<CarItem>> Get(CarFilter filter)
    {
        var query = _context.Cars.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "owner" => filter.IsDescending
                ? query.OrderByDescending(c => c.Client == null
                    ? string.Empty
                    : c.Client.Name)
                : query.OrderBy(c => c.Client == null
                    ? string.Empty
                    : c.Client.Name),
            "status" => filter.IsDescending
                ? query.OrderByDescending(c => c.Status == null
                    ? string.Empty
                    : c.Status.Name)
                : query.OrderBy(c => c.Status == null
                    ? string.Empty
                    : c.Status.Name),
            "brand" => filter.IsDescending
                ? query.OrderByDescending(c => c.Brand)
                : query.OrderBy(c => c.Brand),
            "model" => filter.IsDescending
                ? query.OrderByDescending(c => c.Model)
                : query.OrderBy(c => c.Model),
            "yearofmanufacture" => filter.IsDescending
                ? query.OrderByDescending(c => c.YearOfManufacture)
                : query.OrderBy(c => c.YearOfManufacture),
            "vinnumber" => filter.IsDescending
                ? query.OrderByDescending(c => c.VinNumber)
                : query.OrderBy(c => c.VinNumber),
            "statenumber" => filter.IsDescending
                ? query.OrderByDescending(c => c.StateNumber)
                : query.OrderBy(c => c.StateNumber),
            "mileage" => filter.IsDescending
                ? query.OrderByDescending(c => c.Mileage)
                : query.OrderBy(c => c.Mileage),

            _ => filter.IsDescending
                ? query.OrderByDescending(c => c.Id)
                : query.OrderBy(c => c.Id),
        };

        var projection = query.Select(c => new CarItem(
            c.Id,
            c.Client == null
                ? string.Empty
                : $"{c.Client.Name} {c.Client.Surname}",
            c.Status == null
                ? string.Empty
                : c.Status.Name,
            c.StatusId,
            c.Brand,
            c.Model,
            c.YearOfManufacture,
            c.VinNumber,
            c.StateNumber,
            c.Mileage));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<CarItem?> GetById(long id)
    {
        var carItem = await _context.Cars
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CarItem(
                c.Id,
                c.Client == null 
                    ? "" 
                    : $"{c.Client.Name} {c.Client.Surname}",
                c.Status == null 
                    ? "" 
                    : c.Status.Name,
                c.StatusId,
                c.Brand,
                c.Model,
                c.YearOfManufacture,
                c.VinNumber,
                c.StateNumber,
                c.Mileage
            ))
            .FirstOrDefaultAsync();

        return carItem; 
    }

    public async Task<int> GetCount(CarFilter filter)
    {
        var query = _context.Cars.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(Car car)
    {
        var carEntities = new CarEntity
        {
            OwnerId = car.OwnerId,
            StatusId = (int)car.StatusId,
            Brand = car.Brand,
            Model = car.Model,
            YearOfManufacture = car.YearOfManufacture,
            VinNumber = car.VinNumber,
            StateNumber = car.StateNumber,
            Mileage = car.Mileage
        };

        await _context.Cars.AddAsync(carEntities);
        Console.WriteLine($"DEBUG: Saving Car with StatusId = {carEntities.StatusId}");
        await _context.SaveChangesAsync();

        return carEntities.Id;
    }

    public async Task<long> Update(long id, CarUpdateModel model)
    {
        var entity = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new Exception("Car not found");

        if (!string.IsNullOrWhiteSpace(model.Brand)) entity.Brand = model.Brand;
        if (!string.IsNullOrWhiteSpace(model.Model)) entity.Model = model.Model;
        if (model.YearOfManufacture.HasValue) entity.YearOfManufacture = model.YearOfManufacture.Value;
        if (model.Mileage.HasValue) entity.Mileage = model.Mileage.Value;
        if (model.StatusId.HasValue) entity.StatusId = (int)model.StatusId.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var carEntities = await _context.Cars
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(long id)
    {
        return await _context.Cars
            .AsNoTracking()
            .AnyAsync(c => c.Id == id);
    }
}
