namespace ChampionsLeague.Application.Queries
{
    public record GetTableForOneTeamQuery(string TeamName) : IRequest<TableDetailsResponse>;

    public class GetTableForOneTeamHandler(ITableRepository tableRepository, IMapper mapper)
        : IRequestHandler<GetTableForOneTeamQuery, TableDetailsResponse>
    {
        public async Task<TableDetailsResponse> Handle(GetTableForOneTeamQuery request, CancellationToken cancellationToken)
        {
            var table = await tableRepository.GetTableForOneTeamAsync(request.TeamName);

            if (table is null)
                throw new Exception("Something Wrong When Returing Table - Maybe Team Name Is Wrong or Table Not Created");

            var result = mapper.Map<TableDetailsResponse>(table);

            return result;
        }
    }
}
