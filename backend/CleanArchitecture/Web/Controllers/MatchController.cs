using Application.Features.Matches.GetByTournamentAndMatchDay;
using Application.Features.Matches.UploadResults;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly GetMatchesByTournamentAndRoundHandler _getMatchesByTournamentAndRoundHandler;
        private readonly UploadResultsByRoundHandler _uploadResultsByRoundHandler;
        private readonly IMapper _mapper;

        public MatchController(GetMatchesByTournamentAndRoundHandler getMatchesByTournamentAndRoundHandler, IMapper mapper, UploadResultsByRoundHandler uploadResultsByRoundHandler)
        {
            _getMatchesByTournamentAndRoundHandler = getMatchesByTournamentAndRoundHandler;
            _mapper = mapper;
            _uploadResultsByRoundHandler = uploadResultsByRoundHandler;
        }

        [HttpPut("UploadResults")]
        [Authorize(Policy = "RequireUserRole")]
        public async Task<IActionResult> UploadResults([FromBody] List<UploadResultsByRoundCommand> command, CancellationToken cancellationToken = default)
        {
            try
            {
                if (await _uploadResultsByRoundHandler.ExecuteAsync(command, cancellationToken))
                {
                    return Ok("Results uploaded successfully");
                }

                return BadRequest("Failed to upload results");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByTournamentAndRound")]
        [Authorize(Policy = "RequireUserRole")]
        public async Task<IActionResult> GetMatchByTournamentAndRound([FromQuery] GetMatchesByTournamentAndRoundQuery command, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await _getMatchesByTournamentAndRoundHandler.ExecuteAsync(command, cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
