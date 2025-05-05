using Application.Features.Tournaments;
using Application.Features.Tournaments.Create;
using Application.Features.Tournaments.Delete;
using Application.Features.Tournaments.GenerateGroups;
using Application.Features.Tournaments.GetById;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Runtime.CompilerServices;
using Application.Features.Tournaments.GetGroupsClassification;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly CreateTournamentHandler _createTournamentHandler;
        private readonly DeleteTournamentHandler _deleteTournamentHandler;
        private readonly GenerateGroupsHandler _generateGroupsHandler;
        private readonly GetTournamentByIdHandler _getTournamentByIdHandler;
        private readonly GetGroupsClassificationHandler _getGroupsClassificationHandler;
        private readonly IMapper _mapper;

        public TournamentController(CreateTournamentHandler createTournamentHandler, DeleteTournamentHandler deleteTournamentHandler, GenerateGroupsHandler generateGroupsHandler, GetTournamentByIdHandler getTournamentByIdHandler, IMapper mapper, GetGroupsClassificationHandler getGroupsClassificationHandler)
        {
            _createTournamentHandler = createTournamentHandler;
            _deleteTournamentHandler = deleteTournamentHandler;
            _generateGroupsHandler = generateGroupsHandler;
            _getTournamentByIdHandler = getTournamentByIdHandler;
            _getGroupsClassificationHandler = getGroupsClassificationHandler;
            _mapper = mapper;
        }

        [HttpGet("GetGroupsClassification/{id:guid}")]
        public async Task<IActionResult> GetGroupsClassification([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var tournament = await _getGroupsClassificationHandler.ExecuteAsync(new GetGroupsClassificationQuery { TournamentId = id }, cancellationToken);

                if (tournament is null)
                    return NotFound();

                return Ok(tournament);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GenerateGroups")]
        [Authorize(Policy = "RequireUserRole")]
        public async Task<IActionResult> GenerateGroups([FromBody] GenerateGroupsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var tournament = await _generateGroupsHandler.ExecuteAsync(command, cancellationToken);

                if (tournament == false)
                    return NotFound();
                
                return Ok("The groups were generated succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get/{id:guid}")]
        public async Task<IActionResult> GetTournamentById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var tournament = await _getTournamentByIdHandler.ExecuteAsync(new GetTournamentByIdQuery { TournamentId = id}, cancellationToken);

                if (tournament is null)
                    return NotFound();

                return Ok(_mapper.Map<TournamentResponse>(tournament));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        [Authorize(Policy = "RequireUserRole")]
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                if (!Guid.TryParse(userIdClaim, out var userIdParsed))
                {
                    return BadRequest("Invalid user ID");
                }

                var tournamentCreated = await _createTournamentHandler.ExecuteAsync(userIdParsed, command, cancellationToken);
                var response = _mapper.Map<TournamentResponse>(tournamentCreated);

                return CreatedAtAction(nameof(GetTournamentById), new { id = tournamentCreated.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id:guid}")]
        [Authorize(Policy = "RequireUserRole")]
        public async Task<IActionResult> DeleteTournament([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var tournamentDeleted = await _deleteTournamentHandler.ExecuteAsync(new DeleteTournamentCommand { TournamentId = id }, cancellationToken);

                if (tournamentDeleted == false)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
