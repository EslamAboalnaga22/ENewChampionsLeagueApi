namespace ChampionsLeague.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController(IMediator mediator) : ControllerBase
    {
        
        [HttpGet("GetAllTeams")]
        [EnableRateLimiting("concurrencuPolicy")]
        [RequestTimeout("twoSecond")]
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> GetAllTeams()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetAllTeamsQuery();

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        [HttpGet("GetTeamById")]
        [EnableRateLimiting("concurrencuPolicy")]
        [RequestTimeout("twoSecond")]
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> GetAlGetTeamByIdlTeams(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetTeamByIdQuery(id);

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        [HttpPost("AddTeam")]
        public async Task<IActionResult> AddTeam(AddTeamRequest entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AddTeamCommand(entity);

            var result = await mediator.Send(command);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        [HttpPut("UpdateTeam")]
        public async Task<IActionResult> UpdateTeam(int teamId, UpdateTeamRequest entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateTeamCommand(teamId, entity);

            var result = await mediator.Send(command);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        [HttpDelete("DeleteTeam")]
        public async Task<IActionResult> DeleteTeam(int teamId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new DeleteTeamCommand(teamId);

            var result = await mediator.Send(command);

            if (!result)
                return BadRequest(result);
                
            return Ok(result);
        }
    }
}
