namespace ChampionsLeague.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController(IMediator mediator) : ControllerBase
    {
        [HttpPost("AddResultMatch")]
        public async Task<IActionResult> AddResultMatchLeague(ResultGameRequest resultGameRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AddResultGameCommand(resultGameRequest);

            var result = await mediator.Send(command);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        [HttpPut("UpdateResultMatch")]
        public async Task<IActionResult> UpdateResultMatchLeague(ResultGameRequest resultGameRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateResultGameCommand(resultGameRequest);

            var result = await mediator.Send(command);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        [HttpPost("RandomResultMatches")]
        public async Task<IActionResult> RandomResultMatchesLeague()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new RandomResultCommand();

            var result = await mediator.Send(command);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
