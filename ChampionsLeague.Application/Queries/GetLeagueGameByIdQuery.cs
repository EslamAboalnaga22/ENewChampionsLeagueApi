namespace ChampionsLeague.Application.Queries
{
    public record GetLeagueGameByIdQuery(int GameId) : IRequest<GameDetailsResponse>;

    public class GetGameByIdHandler(ILeagueRepository gameRepository, IMapper mapper)
        : IRequestHandler<GetLeagueGameByIdQuery, GameDetailsResponse>
    {
        public async Task<GameDetailsResponse> Handle(GetLeagueGameByIdQuery request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetLeagueGameByIdAsync(request.GameId);

            if (game is null)
                throw new Exception("Something Wrong When Returing Match - Maybe Match Id Is Wrong or Matches Not Created");

            var result = mapper.Map<GameDetailsResponse>(game);

            return result;
        }
    }
}
