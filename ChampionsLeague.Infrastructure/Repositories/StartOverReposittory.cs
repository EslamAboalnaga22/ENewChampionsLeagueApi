namespace ChampionsLeague.Infrastructure.Repositories
{
    public class StartOverReposittory(AppDbContext dbContext) : IStartOverReposittory
    {
        public async Task<string> StartOverChampionsLeagueAsync()
        {
            using var transaction = dbContext.Database.BeginTransaction();

            await dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Tables");
            await dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Games");
            await dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE AuditLogs");

            // 1: Drop the foreign key constraints
            await dbContext.Database.ExecuteSqlRawAsync("ALTER TABLE Tables DROP CONSTRAINT FK_Tables_Teams_TeamName");
            await dbContext.Database.ExecuteSqlRawAsync("ALTER TABLE Games DROP CONSTRAINT FK_Games_Teams_TeamOne");
            await dbContext.Database.ExecuteSqlRawAsync("ALTER TABLE Games DROP CONSTRAINT FK_Games_Teams_TeamTwo");

            // 2: Truncate the table (Parent Table)
            await dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Teams");

            // 3: Recreate the foreign key constraints
            await dbContext.Database.ExecuteSqlRawAsync("ALTER TABLE Tables ADD CONSTRAINT FK_Tables_Teams_TeamName FOREIGN KEY (TeamName) REFERENCES Teams(Id)");
            await dbContext.Database.ExecuteSqlRawAsync("ALTER TABLE Games ADD CONSTRAINT FK_Games_Teams_TeamOne FOREIGN KEY (TeamOne) REFERENCES Teams(Id)");
            await dbContext.Database.ExecuteSqlRawAsync("ALTER TABLE Games ADD CONSTRAINT FK_Games_Teams_TeamTwo  FOREIGN KEY (TeamTwo) REFERENCES Teams(Id)");

            transaction.Commit();

            return "All Information Is Removed.";
        }
    }
}
