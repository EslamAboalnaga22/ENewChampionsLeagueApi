namespace ChampionsLeague.Application.Queries
{
    public record GetAllQuarterfinalsGamesQuery : IRequest<IEnumerable<GameDetailsResponse>>;

    public class GetAllQuarterfinalsGamesHandler(IKnockoutStagesRepository knockoutPlayoffRepository, IMapper mapper)
        : IRequestHandler<GetAllQuarterfinalsGamesQuery, IEnumerable<GameDetailsResponse>>
    {
        public async Task<IEnumerable<GameDetailsResponse>> Handle(GetAllQuarterfinalsGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await knockoutPlayoffRepository.GetAllQuarterfinalsGamesAsync();

            if (games is null || !games.Any())
                throw new Exception("Something Wrong When Returing Matches - Maybe Matches Not Created");

            var result = mapper.Map<IEnumerable<GameDetailsResponse>>(games);

            return result;
        }
    }
}

