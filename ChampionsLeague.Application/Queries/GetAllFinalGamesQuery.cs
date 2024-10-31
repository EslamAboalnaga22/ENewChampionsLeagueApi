namespace ChampionsLeague.Application.Queries
{
    public record GetAllFinalGamesQuery : IRequest<IEnumerable<GameDetailsResponse>>;

    public class GetAllFinalGamesHandler(IKnockoutStagesRepository knockoutPlayoffRepository, IMapper mapper)
        : IRequestHandler<GetAllFinalGamesQuery, IEnumerable<GameDetailsResponse>>
    {
        public async Task<IEnumerable<GameDetailsResponse>> Handle(GetAllFinalGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await knockoutPlayoffRepository.GetAllFinalGamesAsync();

            if (games is null || !games.Any())
                throw new Exception("Something Wrong When Returing Matches - Maybe Matches Not Created");

            var result = mapper.Map<IEnumerable<GameDetailsResponse>>(games);

            return result;
        }
    }
}
