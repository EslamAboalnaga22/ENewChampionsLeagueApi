namespace ChampionsLeague.Infrastructure.Repositories
{
    public class LeagueRepository(AppDbContext dbContext, ICreateMatchesRepository matchesRepository) : ILeagueRepository
    {
        public async Task<string> CreateLeagueAsync()
        {
            if (dbContext.Games.Count() == 0 && dbContext.Teams.Count() == 36)
            {
                // Teams according to Rank
                var TeamsA = await dbContext.Teams.Where(x => x.Rank == "A").ToListAsync();
                var TeamsB = await dbContext.Teams.Where(x => x.Rank == "B").ToListAsync();
                var TeamsC = await dbContext.Teams.Where(x => x.Rank == "C").ToListAsync();
                var TeamsD = await dbContext.Teams.Where(x => x.Rank == "D").ToListAsync();

                using var transiaction = dbContext.Database.BeginTransaction();

                // Random Teams in Rank
                var RankA = matchesRepository.RandomTeamsInRank(TeamsA);
                var RankB = matchesRepository.RandomTeamsInRank(TeamsB);
                var RankC = matchesRepository.RandomTeamsInRank(TeamsC);
                var RankD = matchesRepository.RandomTeamsInRank(TeamsD);


                // Create Game (Teams in same Rank)
                matchesRepository.CreateMatchesInSameRank(RankA);
                matchesRepository.CreateMatchesInSameRank(RankB);
                matchesRepository.CreateMatchesInSameRank(RankC);
                matchesRepository.CreateMatchesInSameRank(RankD);

                // Create Games (For Rank A)
                matchesRepository.CreateMatchesForRankAndRank(RankA, RankB);
                matchesRepository.CreateMatchesForRankAndRank(RankA, RankC);
                matchesRepository.CreateMatchesForRankAndRank(RankA, RankD);

                // Create Games (For Rank B)
                matchesRepository.CreateMatchesForRankAndRank(RankB, RankC);
                matchesRepository.CreateMatchesForRankAndRank(RankB, RankD);

                // Create Games (For Rank C)
                matchesRepository.CreateMatchesForRankAndRank(RankC, RankD);

                // Create Table
                matchesRepository.CreateTable(dbContext.Teams.ToList());

                transiaction.Commit();

                return "League Is Created Successfully.";
            }

            // dbContext.Games.ExecuteUpdateAsync(x => x.SetProperty(x => x.IsPlayed, false));

            return "League Is Not Created or Teams Not Completed.";
        }
        public async Task<IEnumerable<Game>> GetAllLeagueGamesAsync()
        {
            return await dbContext.Games
                    .Include(x => x.TOne)
                    .Include(x => x.TTwo)
                    .Where(x => x.Round == "Group")
                    .ToListAsync();
        }
        public async Task<Game> GetLeagueGameByIdAsync(int gameId)
        {
            return await dbContext.Games
                    .Include(x => x.TOne)
                    .Include(x => x.TTwo)
                    .FirstOrDefaultAsync(t => t.Id == gameId);
        }
        public async Task<IEnumerable<Game>> GetLeagueGamesByTeamNameAsync(string teamName)
        {
            var game = await dbContext.Teams.FirstOrDefaultAsync(x => x.TeamName == teamName);

            if (game is null)
                throw new Exception("Something Wrong When Returing Match - Maybe Team Name Is Wrong or Table Not Created");

            return await dbContext.Games
                    .Include(x => x.TOne)
                    .Include(x => x.TTwo)
                    .Where(x => x.TeamOne == game.Id || x.TeamTwo == game.Id)
                    .ToListAsync();
        }    
    }
}
