namespace ChampionsLeague.Core.Dtos.Request
{
    public record UpdateResultGameCommand(ResultGameRequest ResultGameRequest) : IRequest<GameDetailsResponse>;

    public class UpdateResultGameHandler(ICreateMatchesRepository createMatchesRepository, IMapper mapper)
        : IRequestHandler<UpdateResultGameCommand, GameDetailsResponse>
    {
        public async Task<GameDetailsResponse> Handle(UpdateResultGameCommand request, CancellationToken cancellationToken)
        {
            var result = mapper.Map<Game>(request.ResultGameRequest);

            if (result is null || request.ResultGameRequest is null)
                throw new Exception("Something Wrong In Updated Data");

            var game = await createMatchesRepository.UpdateResultAsync(result);

            var returnResult = mapper.Map<GameDetailsResponse>(game);

            if (game is null)
                throw new Exception("Something Wrong - No Game With This Id");

            return returnResult;
        }
    }
}
