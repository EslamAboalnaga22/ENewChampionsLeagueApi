namespace ChampionsLeague.Application.Commands
{
    public record CreateLeagueCommand : IRequest<string>;

    public class GameHandler(ILeagueRepository gameRepository)
        : IRequestHandler<CreateLeagueCommand, string>
    {
        public async Task<string> Handle(CreateLeagueCommand request, CancellationToken cancellationToken)
        {
            return await gameRepository.CreateLeagueAsync();
        }
    }
}
