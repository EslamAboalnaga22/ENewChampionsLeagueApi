namespace ChampionsLeague.Application.Queries
{
    public record GetAllLeagueGamesQuery : IRequest<IEnumerable<GameDetailsResponse>>;

    public class GetAllGamesHandler(ILeagueRepository gameRepository, IMapper mapper)
        : IRequestHandler<GetAllLeagueGamesQuery, IEnumerable<GameDetailsResponse>>
    {
        public async Task<IEnumerable<GameDetailsResponse>> Handle(GetAllLeagueGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await gameRepository.GetAllLeagueGamesAsync();

            if (games is null || !games.Any())
                throw new Exception("Something Wrong When Returing Matches - Maybe Matches Not Created");

            var result = mapper.Map<IEnumerable<GameDetailsResponse>>(games);

            return result;
        }
    }
}
