namespace ChampionsLeague.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("concurrencuPolicy")]
    [RequestTimeout(3000)]
    [ResponseCache(Duration = 15)]
    public class TableController(IMediator mediator) : ControllerBase
    {
        [HttpGet("GetTable")]
        public async Task<IActionResult> GetTable()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetTableQuery();

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        [HttpGet("GetTableForOneTeam")]
        public async Task<IActionResult> GetTableForOneTeam(string teamName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetTableForOneTeamQuery(teamName);

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
