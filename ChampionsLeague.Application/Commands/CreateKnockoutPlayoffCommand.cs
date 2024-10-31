namespace ChampionsLeague.Application.Commands
{
    public record CreateKnockoutPlayoffCommand : IRequest<string>;

    public class CreateKnockoutPlayoffHandler(IKnockoutStagesRepository knockoutPlayoffRepository)
        : IRequestHandler<CreateKnockoutPlayoffCommand, string>
    {
        public async Task<string> Handle(CreateKnockoutPlayoffCommand request, CancellationToken cancellationToken)
        {
            return await knockoutPlayoffRepository.CreateKnockoutPlayAsync();
        }
    }
}
