namespace ChampionsLeague.Application.Commands
{
    public record UpdateTeamCommand(int TeamId , UpdateTeamRequest TeamRequest) : IRequest<TeamDetailsResponse>;

    public class UpdateTeamHandler(ITeamRepository teamRepository, IMapper mapper)
        : IRequestHandler<UpdateTeamCommand, TeamDetailsResponse>
    {
        public async Task<TeamDetailsResponse> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var result = mapper.Map<Team>(request.TeamRequest);

            if (result is null || request.TeamRequest is null || request.TeamId == 0)
                    throw new Exception("Something Wrong In Updated Data");

            var team = await teamRepository.UpdateTeamAsync(request.TeamId, result);

            var returnResult = mapper.Map<TeamDetailsResponse>(team);

            return returnResult;
        }
    }
}
