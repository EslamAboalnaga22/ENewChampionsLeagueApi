namespace ChampionsLeague.Application.Commands
{
    public record CreateRoundOf16Command : IRequest<string>;

    public class CreateRoundOf16fHandler(IKnockoutStagesRepository knockoutPlayoffRepository)
        : IRequestHandler<CreateRoundOf16Command, string>
    {
        public async Task<string> Handle(CreateRoundOf16Command request, CancellationToken cancellationToken)
        {
            return await knockoutPlayoffRepository.CreateRoundOf16Async();
        }
    }
}
