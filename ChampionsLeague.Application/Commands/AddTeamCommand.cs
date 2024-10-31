namespace ChampionsLeague.Application.Commands
{
    public record AddTeamCommand(AddTeamRequest TeamRequest) : IRequest<TeamDetailsResponse>;

    public class AddTeamHandler(ITeamRepository teamRepository, IMapper mapper)
        : IRequestHandler<AddTeamCommand, TeamDetailsResponse>
    {
        public async Task<TeamDetailsResponse> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {
            var result = mapper.Map<Team>(request.TeamRequest);

            if (result is null || request.TeamRequest is null)
                throw new Exception("Something Wrong When Added Data");

            var team = await teamRepository.AddTeamAsync(result);

            var returnResult = mapper.Map<TeamDetailsResponse>(team);

            if (team is null)
                throw new Exception("Something Wrong - No Team With This Id");

            return returnResult;
        }
    }
}
