namespace ChampionsLeague.Application.Commands
{
    public record CreateSemifinalsCommand : IRequest<string>;

    public class CreateSemifinalsHandler(IKnockoutStagesRepository knockoutPlayoffRepository)
        : IRequestHandler<CreateSemifinalsCommand, string>
    {
        public async Task<string> Handle(CreateSemifinalsCommand request, CancellationToken cancellationToken)
        {
            return await knockoutPlayoffRepository.CreateSemifinalsAsync();
        }
    }
}
