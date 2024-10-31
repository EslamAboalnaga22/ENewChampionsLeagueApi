namespace ChampionsLeague.Application.Commands
{
    public record AddResultGameCommand(ResultGameRequest ResultGameRequest) : IRequest<GameDetailsResponse>;

    public class AddResultGameHandler(ICreateMatchesRepository createMatchesRepository, IMapper mapper)
        : IRequestHandler<AddResultGameCommand, GameDetailsResponse>
    {
        public async Task<GameDetailsResponse> Handle(AddResultGameCommand request, CancellationToken cancellationToken)
        {
            var result = mapper.Map<Game>(request.ResultGameRequest);

            if (result is null || request.ResultGameRequest is null)
                throw new Exception("Something Wrong When Added Data");

            var game = await createMatchesRepository.CreateResultAsync(result);

            var returnResult = mapper.Map<GameDetailsResponse>(game);

            if (game is null)
                throw new Exception("Something Wrong - No Game With This Id or Match Is Played Aleardy");

            return returnResult;
        }
    }
}
