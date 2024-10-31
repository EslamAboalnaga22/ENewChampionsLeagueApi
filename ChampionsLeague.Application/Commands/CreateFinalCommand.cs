namespace ChampionsLeague.Application.Commands
{
    public record CreateFinalCommand : IRequest<string>;

    public class CreateFinalHandler(IKnockoutStagesRepository knockoutPlayoffRepository)
        : IRequestHandler<CreateFinalCommand, string>
    {
        public async Task<string> Handle(CreateFinalCommand request, CancellationToken cancellationToken)
        {
            return await knockoutPlayoffRepository.CreateFinalAsync();
        }
    }
}
