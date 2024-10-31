namespace ChampionsLeague.Infrastructure.Repositories
{
    public class TableRepository(AppDbContext dbContext) : ITableRepository
    {
        public async Task<IEnumerable<Table>> GetTableAsync()
        {
            return await dbContext.Tables
                    .Include(x => x.Team)
                    .OrderByDescending(x => x.Points)
                    .ThenByDescending(x => x.GD)
                    .ToListAsync();
        }

        public async Task<Table> GetTableForOneTeamAsync(string teamName)
        {
            var game = await dbContext.Teams.FirstOrDefaultAsync(x => x.TeamName == teamName);

            if (game is null)
                throw new Exception("Something Wrong When Returing Match - Maybe Team Name Is Wrong or Table Not Created");

            return await dbContext.Tables
                    .Include(x => x.Team)
                    .FirstOrDefaultAsync(x => x.TeamName == game.Id);
        }
    }
}
