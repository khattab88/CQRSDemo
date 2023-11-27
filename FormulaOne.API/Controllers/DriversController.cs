using AutoMapper;
using FormulaOne.API.Dtos.Requests;
using FormulaOne.API.Dtos.Responses;
using FormulaOne.API.Features.Drivers.Commands;
using FormulaOne.API.Features.Drivers.Queries;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.API.Controllers
{
    public class DriversController : BaseController
    {
        private readonly IMediator _mediator;

        public DriversController(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IMediator mediator
            ) 
            : base(unitOfWork, mapper)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var query = new GetAllDriversQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> GetDriver(Guid driverId)
        {
            var query = new GetDriverQuery(driverId);

            var result = await _mediator.Send(query);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new CreateDriverCommand(createDto);

            var driver = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetDriver), new { driverId = driver.DriverId }, driver);
        }

        [HttpPut]
        public async Task<IActionResult> UpateDriver([FromBody] UpdateDriverRequest updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new UpdateDriverCommand(updateDto);

            var result = await _mediator.Send(command);

            if(!result)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> DeleteDriver(Guid driverId)
        {
            var driver = await _unitOfWork.Drivers.GetById(driverId);

            if (driver is null)
                return NotFound();

            await _unitOfWork.Drivers.Delete(driverId);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
