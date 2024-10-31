namespace ChampionsLeague.Application.Queries
{
    public record GetTeamByIdQuery(int TeamId) : IRequest<TeamDetailsResponse>;

    public class GetTeamByIdHandler(ITeamRepository teamRepository, IMapper mapper)
        : IRequestHandler<GetTeamByIdQuery, TeamDetailsResponse>
    {
        public async Task<TeamDetailsResponse> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var team = await teamRepository.GetTeamByIdAsync(request.TeamId);

            if (team is null)
                throw new Exception("Something Wrong When Returing Team - Maybe Id Is Wrong or Team Not Exist");

            var result = mapper.Map<TeamDetailsResponse>(team);

            return result;
        }
    }
}
