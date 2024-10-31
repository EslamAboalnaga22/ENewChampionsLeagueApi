namespace ChampionsLeague.Infrastructure.Repositories
{
    public class KnockoutStagesRepository(AppDbContext dbContext, ICreateMatchesRepository matchesRepository) : IKnockoutStagesRepository
    {
        // Knockout Phase
        public async Task<string> CreateKnockoutPlayAsync()
        {
            var checkMatchesPlayedNumber = dbContext.Tables.All(x => x.Played == 8);

            var checkMatchesIsPlayed = dbContext.Games.Where(x => x.Round == "Group")
                                                      .All(x => x.IsPlayed == true);

            var checkMatchesRoundIsCreated = dbContext.Games.Any(x => x.Round == "Knockout-Playoff");

            if (!((checkMatchesPlayedNumber && checkMatchesIsPlayed) ^ checkMatchesRoundIsCreated))
                return "Matches In League Not Completed or The Championship Has Not Started.";

            var allTeamsInTable = await dbContext.Tables
                        .OrderByDescending(x => x.Points)
                        .ThenByDescending(x => x.GD)
                        .ToListAsync();

            matchesRepository.CreateMatchesKnockoutPlayoff(allTeamsInTable);

            return "Knockout-Playoff Matches Is Created.";
        }
        public async Task<IEnumerable<Game>> GetAllKnockoutPlayoffGamesAsync()
        {
            return await dbContext.Games
                    .Include(x => x.TOne)
                    .Include(x => x.TTwo)
                    .Where(x => x.Round == "Knockout-Playoff")
                    .ToListAsync();
        }

        // Round of 16
        public async Task<string> CreateRoundOf16Async()
        {
            var checkMatchesHasResultAndPlayed = dbContext.Games
                        .Where(x => x.Round == "Knockout-Playoff")
                        .All(x => x.ResultTeamOne != null && 
                                  x.ResultTeamTwo != null && 
                                  x.IsPlayed == true);

            var checkMatchesRoundIsCreated = dbContext.Games
                       .Any(x => x.Round == "RoundOf16");

            if (!(checkMatchesHasResultAndPlayed ^ checkMatchesRoundIsCreated))
                return "Matches In Knockout-Playoff Not Completed or The Championship Has Not Started.";

            var first8 = await dbContext.Tables
                        .OrderByDescending(x => x.Points)
                        .ThenByDescending(x => x.GD)
                        .Take(8)
                        .ToListAsync();

            var KnockoutPlayoffMatches = await GetAllKnockoutPlayoffGamesAsync();

            if (KnockoutPlayoffMatches.Count() == 0)
                return "Matches In Knockout-Playoff Not Completed or The Championship Has Not Started.";

            List<Team> teamsQualifiedKnockoutPlayoff = [];

            foreach (var match in KnockoutPlayoffMatches)
            {
                if (match.ResultTeamOne > match.ResultTeamTwo)
                {
                    teamsQualifiedKnockoutPlayoff.Add(new Team
                    {
                        Id = match.TeamOne,

                    });
                }
                else
                {
                    teamsQualifiedKnockoutPlayoff.Add(new Team
                    {
                        Id = match.TeamTwo,

                    });
                }
            }

            matchesRepository.CreateMatchesRoundOf16(first8, teamsQualifiedKnockoutPlayoff);

            return "Matches In Round Of 16 Are Created.";
        }
        public async Task<IEnumerable<Game>> GetAllRoundOf16GamesAsync()
        {
            return await dbContext.Games
                    .Include(x => x.TOne)
                    .Include(x => x.TTwo)
                    .Where(x => x.Round == "RoundOf16")
                    .ToListAsync();
        }

        // Quarterfinals
        public async Task<string> CreateQuarterfinalsAsync()
        {
            var checkMatchesHasResultAndPlayed = dbContext.Games
                        .Where(x => x.Round == "RoundOf16")
                        .All(x => x.ResultTeamOne != null && x.ResultTeamTwo != null && x.IsPlayed == true);

            var checkMatchesRoundIsCreated = dbContext.Games
                        .Any(x => x.Round == "Quarterfinals");

            if (!(checkMatchesHasResultAndPlayed ^ checkMatchesRoundIsCreated))
                return "Matches In Round Of 16 Not Completed or The Championship Has Not Started.";

            var RoundOf16Matches = await GetAllRoundOf16GamesAsync();

            if (RoundOf16Matches.Count() == 0)
                return "Matches In Round Of 16 Not Completed or The Championship Has Not Started.";

            List<Team> teamsQualifiedQuarterfinals = [];

            foreach (var match in RoundOf16Matches)
            {
                if (match.ResultTeamOne > match.ResultTeamTwo)
                {
                    teamsQualifiedQuarterfinals.Add(new Team
                    {
                        Id = match.TeamOne,

                    });
                }
                else
                {
                    teamsQualifiedQuarterfinals.Add(new Team
                    {
                        Id = match.TeamTwo,

                    });
                }
            }

            matchesRepository.CreateMatchesQuarterfinals(teamsQualifiedQuarterfinals);

            return "Matches In Quarterfinals Are Created.";
        }
        public async Task<IEnumerable<Game>> GetAllQuarterfinalsGamesAsync()
        {
            return await dbContext.Games
                   .Include(x => x.TOne)
                   .Include(x => x.TTwo)
                   .Where(x => x.Round == "Quarterfinals")
                   .ToListAsync();
        }

        // Semifinals
        public async Task<string> CreateSemifinalsAsync()
        {
            var checkMatchesHasResultAndPlayed = dbContext.Games
                       .Where(x => x.Round == "Quarterfinals")
                       .All(x => x.ResultTeamOne != null && x.ResultTeamTwo != null && x.IsPlayed == true);

            var checkMatchesRoundIsCreated = dbContext.Games
                        .Any(x => x.Round == "Semifinals");

            if (!(checkMatchesHasResultAndPlayed ^ checkMatchesRoundIsCreated))
                return "Matches In Quarterfinals Not Completed or The Championship Has Not Started.";

            var QuarterfinalsMatches = await GetAllQuarterfinalsGamesAsync();

            if (QuarterfinalsMatches.Count() == 0)
                return "Matches In Quarterfinals Not Completed or The Championship Has Not Started.";

            List<Team> teamsQualifiedSemifinals = [];

            foreach (var match in QuarterfinalsMatches)
            {
                if (match.ResultTeamOne > match.ResultTeamTwo)
                {
                    teamsQualifiedSemifinals.Add(new Team
                    {
                        Id = match.TeamOne,

                    });
                }
                else
                {
                    teamsQualifiedSemifinals.Add(new Team
                    {
                        Id = match.TeamTwo,

                    });
                }
            }

            matchesRepository.CreateMatchesSemifinals(teamsQualifiedSemifinals);

            return "Matches In Semifinals Are Created.";
        }
        public async Task<IEnumerable<Game>> GetAllSemifinalsGamesAsync()
        {
            return await dbContext.Games
                   .Include(x => x.TOne)
                   .Include(x => x.TTwo)
                   .Where(x => x.Round == "Semifinals")
                   .ToListAsync();
        }

        // Finals
        public async Task<string> CreateFinalAsync()
        {
            var checkMatchesHasResultAndPlayed = dbContext.Games
                       .Where(x => x.Round == "Semifinals")
                       .All(x => x.ResultTeamOne != null && x.ResultTeamTwo != null && x.IsPlayed == true);

            var checkMatchesRoundIsCreated = dbContext.Games
                        .Any(x => x.Round == "Final");

            if (!(checkMatchesHasResultAndPlayed ^ checkMatchesRoundIsCreated))
                return "Matches In Semifinals Not Completed or The Championship Has Not Started.";

            var SemifinalsMatches = await GetAllSemifinalsGamesAsync();

            if (SemifinalsMatches.Count() == 0)
                return "Matches In Semifinals Not Completed or The Championship Has Not Started.";

            List<Team> teamsQualifiedFinal = [];

            foreach (var match in SemifinalsMatches)
            {
                if (match.ResultTeamOne > match.ResultTeamTwo)
                {
                    teamsQualifiedFinal.Add(new Team
                    {
                        Id = match.TeamOne,

                    });
                }
                else
                {
                    teamsQualifiedFinal.Add(new Team
                    {
                        Id = match.TeamTwo,

                    });
                }
            }

            matchesRepository.CreateMatchesFinal(teamsQualifiedFinal);

            return "Matches In Final Are Created.";

        }
        public async Task<IEnumerable<Game>> GetAllFinalGamesAsync()
        {
            return await dbContext.Games
                  .Include(x => x.TOne)
                  .Include(x => x.TTwo)
                  .Where(x => x.Round == "Final")
                  .ToListAsync();
        }
    }
}
