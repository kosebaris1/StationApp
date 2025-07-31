using Application.Features.Mediator.Commands.AdminCommands;
using Application.Features.Mediator.Queries.AdminQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllUserByIsApproved")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await _mediator.Send(new GetAllUserByIsApprovedQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut("ApproveUser/{id}")]
        public async Task<IActionResult> ApproveUser(int id)
        {
            try
            {
                await _mediator.Send(new ApproveUserCommand { UserId= id});
                return Ok("Kullanıcı onaylandı.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut("RejectUser/{id}")]
        public async Task<IActionResult> RejectUser(int id)
        {
            try
            {
                await _mediator.Send(new RejectUserCommand { UserId = id });
                return Ok("Kullanıcı reddedildi.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
