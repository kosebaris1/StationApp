using Application.Features.Mediator.Commands.WeatherStationCommands;
using Application.Features.Mediator.Queries.WeatherStationQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherStationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherStationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddStationWithData")]
        public async Task<IActionResult> AddStationWithData([FromBody] CreateWeatherStationWithDataCommand command)
        {
            await _mediator.Send(command);
            return Ok("İstasyon ve hava verisi başarıyla eklendi.");
        }

        [HttpGet("GetAllWeatherStationWithLastData")]
        public async Task<IActionResult> GetAllWithLastData()
        {
            var result = await _mediator.Send(new GetAllWeatherStationsWithLastDataQuery());
            return Ok(result);
        }

    }
}
