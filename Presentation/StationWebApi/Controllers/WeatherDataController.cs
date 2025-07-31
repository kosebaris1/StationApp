using Application.Features.Mediator.Commands.WeatherDataCommands;
using Application.Features.Mediator.Queries.WeatherDataQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWeatherData()
        {
            var result = await _mediator.Send(new GetAllWeatherDataQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeatherDataById(int id)
        {
            var result = await _mediator.Send(new GetWeatherDataByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWeatherData(CreateWeatherDataCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid data.");
            }

            await _mediator.Send(command);
            return Ok("WeatherData başarıyla Eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWeatherData(UpdateWeatherDataCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid data.");
            }

            await _mediator.Send(command);
            return Ok("WeatherData başarıyla Güncellendi");
        }
        
        [HttpDelete]
        public async Task<IActionResult> RemoveWeatherData(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

           await _mediator.Send(new RemoveWeatherDataCommand(id));
            return Ok("WeatherData başarıyla Silindi");
        }
    }
}
