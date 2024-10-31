namespace ChampionsLeague.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnockoutStagesController(IMediator mediator) : ControllerBase
    {
        // Knockout Playoff

        [HttpPost("CreateKnockoutPlayoff")]
        public async Task<IActionResult> CreateKnockoutPlayoff()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateKnockoutPlayoffCommand();

            var result = await mediator.Send(command);

            return Ok(result);
        }
        [HttpGet("GetAllMatchesKnockoutPlayoff")]
        public async Task<IActionResult> GetAllMatchesKnockoutPlayoff()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetAllKnockoutPlayoffGamesQuery();

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        // Round Of 16

        [HttpPost("CreateRoundOf16")]
        public async Task<IActionResult> CreateRoundOf16()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateRoundOf16Command();

            var result = await mediator.Send(command);

            return Ok(result);
        }
        [HttpGet("GetAllMatchesRoundOf16")]
        public async Task<IActionResult> GetAllMatchesRoundOf16()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetAllRoundOf16GamesQuery();

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        // Quarterfinals
        [HttpPost("CreateQuarterfinals")]
        public async Task<IActionResult> CreateQuarterfinals()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateQuarterfinalsCommand();

            var result = await mediator.Send(command);

            return Ok(result);
        }
        [HttpGet("GetAllMatchesQarterfinals")]
        public async Task<IActionResult> GetAllMatchesQarterfinals()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetAllQuarterfinalsGamesQuery();

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        // Quarterfinals
        [HttpPost("CreateSemifinals")]
        public async Task<IActionResult> CreateSemifinals()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateSemifinalsCommand();

            var result = await mediator.Send(command);

            return Ok(result);
        }
        [HttpGet("GetAllMatchesSemifinals")]
        public async Task<IActionResult> GetAllMatchesSemifinals()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetAllSemifinalsGamesQuery();

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        // Quarterfinals
        [HttpPost("CreateFinal")]
        public async Task<IActionResult> CreateFinal()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateFinalCommand();

            var result = await mediator.Send(command);

            return Ok(result);
        }
        [HttpGet("GetAllMatchesFinal")]
        public async Task<IActionResult> GetAllMatchesFinal()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetAllFinalGamesQuery();

            var result = await mediator.Send(query);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
