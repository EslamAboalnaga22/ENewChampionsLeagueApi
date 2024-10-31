namespace ChampionsLeague.Application.Commands
{
    public record DeleteTeamCommand(int TeamId) : IRequest<bool>;

    public class DeleteTeamHandler(ITeamRepository teamRepository)
        : IRequestHandler<DeleteTeamCommand, bool>
    {
        public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            return await teamRepository.DeleteTeamAsync(request.TeamId);
        }
    }
}
