namespace ChampionsLeague.Application.Commands
{
    public record StartOverCommand : IRequest<string>;
    public class StartOverHandler(IStartOverReposittory startOverReposittory)
        : IRequestHandler<StartOverCommand, string>
    {
        public async Task<string> Handle(StartOverCommand request, CancellationToken cancellationToken)
        {
            return await startOverReposittory.StartOverChampionsLeagueAsync();
        }
    }
}