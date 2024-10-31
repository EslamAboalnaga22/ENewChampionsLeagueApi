namespace ChampionsLeague.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequestTimeout("twoSecond")]
    public class LeagueController(IMediator mediator) : ControllerBase
    {
        [HttpPost("CreateLeague")]
        public async Task<IActionResult> CreateLeague()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateLeagueCommand();

            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("GetAllMatchesLeague")]
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> GetAllMatchsLeague()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetAllLeagueGamesQuery();

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet("GetMatchLeagueById")]
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> GetMatchLeagueById(int teamId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetLeagueGameByIdQuery(teamId);

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        [HttpGet("GetMatchesLeagueByTeamName")]
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> GetMatchesLeagueByTeamName(string teamName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetGamesByTeamNameQuery(teamName);

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
