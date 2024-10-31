namespace ChampionsLeague.Application.Queries
{
    public record GetGamesByTeamNameQuery(string TeamName) : IRequest<IEnumerable<GameDetailsResponse>>;

    public class GetGamesByTeamNameHandler(ILeagueRepository gameRepository, IMapper mapper)
        : IRequestHandler<GetGamesByTeamNameQuery, IEnumerable<GameDetailsResponse>>
    {
        public async Task<IEnumerable<GameDetailsResponse>> Handle(GetGamesByTeamNameQuery request, CancellationToken cancellationToken)
        {
            var games = await gameRepository.GetLeagueGamesByTeamNameAsync(request.TeamName);

            if (games is null)
                throw new Exception("Something Wrong When Returing Match - Maybe Match Team Name Is Wrong or Matches Not Created");

            var result = mapper.Map<IEnumerable<GameDetailsResponse>>(games);

            return result;
        }
    }
}
