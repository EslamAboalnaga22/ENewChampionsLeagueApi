namespace ChampionsLeague.Application.Queries
{
    public record GetAllSemifinalsGamesQuery : IRequest<IEnumerable<GameDetailsResponse>>;

    public class GetAllSemifinalsGamesHandler(IKnockoutStagesRepository knockoutPlayoffRepository, IMapper mapper)
        : IRequestHandler<GetAllSemifinalsGamesQuery, IEnumerable<GameDetailsResponse>>
    {
        public async Task<IEnumerable<GameDetailsResponse>> Handle(GetAllSemifinalsGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await knockoutPlayoffRepository.GetAllSemifinalsGamesAsync();

            if (games is null || !games.Any())
                throw new Exception("Something Wrong When Returing Matches - Maybe Matches Not Created");

            var result = mapper.Map<IEnumerable<GameDetailsResponse>>(games);

            return result;
        }
    }
}
