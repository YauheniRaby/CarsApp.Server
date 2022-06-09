using CarsServer.Bl.DTOs;
using CarsServer.Bl.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsServer.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService; 
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> GetAllAsync()
        {
            var hostUrl = HttpContext.Request.Host.Value;
            var result = await _carService.GetAllAsync(hostUrl);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromBody] IEnumerable<Guid> idList)
        {
            if (!idList.Any())
            {
                return BadRequest();
            }
            await _carService.DeleteAsync(idList);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<CarDto> CreateAsync([FromBody] CarCreateDto carCreateDto)
        {
            var result = _carService.CreateAsync(carCreateDto);
            return Created(string.Empty, result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] CarDto carDto)
        {
            var isExist = _carService.ExistsAsync(carDto.Id);
            if (!isExist)
            {
                return NotFound();
            }

            await _carService.UpdateAsync(carDto);            
            return NoContent();
        }
    }
}
