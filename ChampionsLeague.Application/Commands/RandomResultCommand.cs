namespace ChampionsLeague.Application.Commands
{
    public record RandomResultCommand : IRequest<string>;

    public class RandomResultHandler(ICreateMatchesRepository matchesRepository)
        : IRequestHandler<RandomResultCommand, string>
    {
        public async Task<string> Handle(RandomResultCommand request, CancellationToken cancellationToken)
        {
            return await matchesRepository.RandomResultAsync();
        }
    }
}
