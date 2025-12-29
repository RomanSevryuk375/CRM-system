using CRM_system_backend.Contracts.Absence;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Absence;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;

        public AbsenceController(IAbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AbsenceItem>>> GetPagedAbsence(
            [FromQuery] AbsenceFilter filter)
        {
            var dto = await _absenceService.GetPagedAbsence(filter);

            var response = dto.Select(a => new AbsenceResponse(
                a.Id,
                a.WorkerName,
                a.workerId,
                a.typeId,
                a.StartDate,
                a.EndDate));

            var count = await _absenceService.GetCountAbsence(filter);
            Response.Headers.Append("x-total-count", count.ToString());

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAbsence([FromBody] AbsenceRequest request)
        {
            var (absence, errors) = Absence.Create(
                0,
                request.WorkerId,
                request.TypeId,
                request.StartDate,
                request.EndDate);

            if (errors.Any()) 
                return BadRequest(errors);

            var Id = await _absenceService.CreateAbsence(absence!);

            return Ok(Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateAbsence(int id, [FromBody] AbsenceUpdateRequest request)
        {
            var model = new AbsenceUpdateModel
                (
                    request.typeId,
                    request.startDate,
                    request.endDate
                );

            var Id = await _absenceService.UpdateAbsence(id, model);

            return Ok(Id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAbsnce(int id)
        {
            var Id = await _absenceService.DeleteAbsence(id);

            return Ok(Id);
        }
    }
}
