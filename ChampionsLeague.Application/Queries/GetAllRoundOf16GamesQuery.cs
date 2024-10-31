namespace ChampionsLeague.Application.Queries
{
    public record GetAllRoundOf16GamesQuery : IRequest<IEnumerable<GameDetailsResponse>>;

    public class GetAllRoundOf16GamesHandler(IKnockoutStagesRepository knockoutPlayoffRepository, IMapper mapper)
        : IRequestHandler<GetAllRoundOf16GamesQuery, IEnumerable<GameDetailsResponse>>
    {
        public async Task<IEnumerable<GameDetailsResponse>> Handle(GetAllRoundOf16GamesQuery request, CancellationToken cancellationToken)
        {
            var games = await knockoutPlayoffRepository.GetAllRoundOf16GamesAsync();

            if (games is null || !games.Any())
                throw new Exception("Something Wrong When Returing Matches - Maybe Matches Not Created");

            var result = mapper.Map<IEnumerable<GameDetailsResponse>>(games);

            return result;
        }
    }
}
