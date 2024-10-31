namespace ChampionsLeague.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartOverController(IMediator mediator) : ControllerBase
    {
        [HttpDelete]    
        public async Task<IActionResult> StartOverAgain()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new StartOverCommand();

            var result = await mediator.Send(command);

            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
