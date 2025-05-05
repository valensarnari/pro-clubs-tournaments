using Application.Features.Teams;
using Application.Features.Teams.Add;
using Application.Features.Teams.GetById;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly AddTeamHandler _addTeamHandler;
        private readonly GetTeamByIdHandler _getTeamByIdHandler;
        private readonly IMapper _mapper;

        public TeamController(AddTeamHandler addTeamHandler, IMapper mapper, GetTeamByIdHandler getTeamByIdHandler)
        {
            _addTeamHandler = addTeamHandler;
            _mapper = mapper;
            _getTeamByIdHandler = getTeamByIdHandler;
        }

        [HttpGet("Get/{id:guid}")]
        public async Task<IActionResult> GetTeamById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var team = await _getTeamByIdHandler.ExecuteAsync(new GetTeamByIdQuery { TeamId = id }, cancellationToken);

                if (team is null)
                    return NotFound();

                return Ok(_mapper.Map<TeamResponse>(team));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddTeam([FromBody] AddTeamCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var teamCreated = await _addTeamHandler.ExecuteAsync(command, cancellationToken);

                if (teamCreated is null)
                    return NotFound();

                var response = _mapper.Map<TeamResponse>(teamCreated);

                return CreatedAtAction(nameof(GetTeamById), new { id = teamCreated.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
