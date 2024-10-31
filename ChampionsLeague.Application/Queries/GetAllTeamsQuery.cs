namespace ChampionsLeague.Application.Queries
{
    public record GetAllTeamsQuery : IRequest<IEnumerable<TeamDetailsResponse>>;

    public class GetAllTeamsHandler(ITeamRepository teamRepository, IMapper mapper)
        : IRequestHandler<GetAllTeamsQuery, IEnumerable<TeamDetailsResponse>>
    {
        public async Task<IEnumerable<TeamDetailsResponse>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = await teamRepository.GetAllTeamAsync();

            if (teams is null || !teams.Any())
                throw new Exception("Something Wrong When Returing Teams - Maybe Teams Not Added");

            var result = mapper.Map<IEnumerable<TeamDetailsResponse>>(teams);

            return result;
        }
    }
}
