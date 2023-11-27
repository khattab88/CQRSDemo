using AutoMapper;
using FormulaOne.API.Dtos.Requests;
using FormulaOne.API.Dtos.Responses;
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

            var driver = _mapper.Map<Driver>(createDto);

            await _unitOfWork.Drivers.Add(driver);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDriver), new { driverId = driver.Id }, driver);
        }

        [HttpPut]
        public async Task<IActionResult> UpateDriver([FromBody] UpdateDriverRequest updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var driver = _mapper.Map<Driver>(updateDto);

            await _unitOfWork.Drivers.Update(driver);
            await _unitOfWork.CompleteAsync();

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
