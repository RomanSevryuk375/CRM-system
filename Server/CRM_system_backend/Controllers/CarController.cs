using AutoMapper;
using CRM_system_backend.Contracts.Car;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Car;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IMapper _mapper;

    public CarController(
        ICarService carService,
        IMapper mapper)
    {
        _carService = carService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<CarItem>>> GetPagedCars([FromQuery]CarFilter filter, CancellationToken ct)
    {
        var dto = await _carService.GetPagedCars(filter, ct);
        var cout = await _carService.GetCountCars(filter, ct);

        var response = _mapper.Map<List<CarResponse>>(dto);

        Response.Headers.Append("x-total-count", cout.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarItem>> GetCarById(long id, CancellationToken ct)
    {
        var car = await _carService.GetCarById(id, ct);

        return Ok(car);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateCar([FromBody] CarRequest request, CancellationToken ct)
    {
        Console.WriteLine($"CONTROLLER DEBUG: Recieved JSON mapped to: OwnerId={request.OwnerId}, StatusId={request.StatusId}");
        var (car, errors) = Car.Create(
            0,
            request.OwnerId,
            (CarStatusEnum)request.StatusId,
            request.Brand,
            request.Model,
            request.YearOfManufacture,
            request.VinNumber,
            request.StateNumber,
            request.Mileage);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _carService.CreateCar(car!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateCar(long id, [FromBody]CarUpdateRequest request, CancellationToken ct)
    {
        var model = new CarUpdateModel(
            request.StatusId,
            request.Brand,
            request.Model,
            request.YearOfManufacture,
            request.Mileage);

        var Id = await _carService.UpdateCar(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteCar(long id, CancellationToken ct)
    {
        var Id = await _carService.DeleteCar(id, ct);

        return Ok(Id);
    }
}
