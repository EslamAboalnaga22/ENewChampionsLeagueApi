namespace ChampionsLeague.Application.Queries
{
    public record GetAllKnockoutPlayoffGamesQuery : IRequest<IEnumerable<GameDetailsResponse>>;

    public class GetAllKnockoutPlayoffGamesHandler(IKnockoutStagesRepository knockoutPlayoffRepository, IMapper mapper)
        : IRequestHandler<GetAllKnockoutPlayoffGamesQuery, IEnumerable<GameDetailsResponse>>
    {
        public async Task<IEnumerable<GameDetailsResponse>> Handle(GetAllKnockoutPlayoffGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await knockoutPlayoffRepository.GetAllKnockoutPlayoffGamesAsync();

            if (games is null || !games.Any())
                throw new Exception("Something Wrong When Returing Matches - Maybe Matches Not Created");

            var result = mapper.Map<IEnumerable<GameDetailsResponse>>(games);

            return result;
        }
    }
}
