namespace ChampionsLeague.Infrastructure.Repositories
{
    public class TeamRepository(AppDbContext dbContext) : ITeamRepository
    {
        public async Task<IEnumerable<Team>> GetAllTeamAsync()
        {
            return await dbContext.Teams.ToListAsync();
        }
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return await dbContext.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
        }
        public async Task<Team> AddTeamAsync(Team entity)
        {
            await dbContext.Teams.AddAsync(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<Team> UpdateTeamAsync(int teamId, Team entity)
        {
            var team = await dbContext.Teams.FirstOrDefaultAsync(t => t.Id == teamId);

            if (team is null)
                throw new Exception("Something Wrong - No Team With This Id");

            team.TeamName = entity.TeamName;
            team.Rank = entity.Rank;
            team.Country = entity.Country;

            await dbContext.SaveChangesAsync();

            return team;

        }
        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            var team = await dbContext.Teams.FirstOrDefaultAsync(t => t.Id == teamId);

            if (team is null)
                throw new Exception("Something Wrong - No Team With This Id");

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
