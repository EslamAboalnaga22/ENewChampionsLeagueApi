namespace ChampionsLeague.Application.Queries
{
    public record GetTableQuery : IRequest<IEnumerable<TableDetailsResponse>>;

    public class GetTableHandler(ITableRepository tableRepository, IMapper mapper)
        : IRequestHandler<GetTableQuery, IEnumerable<TableDetailsResponse>>
    {
        public async Task<IEnumerable<TableDetailsResponse>> Handle(GetTableQuery request, CancellationToken cancellationToken)
        {
            var table = await tableRepository.GetTableAsync();

            if (table is null || !table.Any())
                throw new Exception("Something Wrong When Returing Table - Maybe Table Not Created");

            var result = mapper.Map<IEnumerable<TableDetailsResponse>>(table);

            return result;
        }
    }
}
