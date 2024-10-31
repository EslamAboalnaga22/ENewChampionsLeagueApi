namespace ChampionsLeague.Application.Commands
{
    public record CreateQuarterfinalsCommand : IRequest<string>;

    public class CreateQuarterfinalsHandler(IKnockoutStagesRepository knockoutPlayoffRepository)
        : IRequestHandler<CreateQuarterfinalsCommand, string>
    {
        public async Task<string> Handle(CreateQuarterfinalsCommand request, CancellationToken cancellationToken)
        {
            return await knockoutPlayoffRepository.CreateQuarterfinalsAsync();
        }
    }
}
