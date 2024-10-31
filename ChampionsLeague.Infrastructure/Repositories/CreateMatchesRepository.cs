namespace ChampionsLeague.Infrastructure.Repositories
{
    public class CreateMatchesRepository(AppDbContext dbContext) : ICreateMatchesRepository
    {
        public List<Team> RandomTeamsInRank(List<Team> teams)
        {
            Random rnd = new();
            teams = teams.Select(i => new { value = i, rank = rnd.Next(teams.Count()) })
                        .OrderBy(n => n.rank)
                        .Select(n => n.value)
                        .ToList();

            return teams;
        }
        public void CreateMatchesInSameRank(List<Team> TeamRank)
        {
            for (int i = 0; i < 3; i++)
            {
                Game game = new()
                {
                    TeamOne = TeamRank[i].Id,
                    TeamTwo = TeamRank[i + 3].Id,
                    Round = "Group"
                };

                dbContext.Games.Add(game);
                dbContext.SaveChanges();
            }

            for (int i = 0; i < 3; i++)
            {
                Game game = new()
                {
                    TeamOne = TeamRank[i].Id,
                    TeamTwo = TeamRank[i + 6].Id,
                    Round = "Group"
                };

                dbContext.Games.Add(game);
                dbContext.SaveChanges();
            }

            for (int i = 0; i < 3; i++)
            {
                Game game = new()
                {
                    TeamOne = TeamRank[i + 6].Id,
                    TeamTwo = TeamRank[i + 3].Id,
                    Round = "Group"
                };

                dbContext.Games.Add(game);
                dbContext.SaveChanges();
            }
        }
        public void CreateMatchesForRankAndRank(List<Team> FirstTeamRank, List<Team> SecondTeamRank)
        {
            for (int i = 0; i < FirstTeamRank.Count; i++)
            {
                Game game1 = new()
                {
                    TeamOne = FirstTeamRank[i].Id,
                    TeamTwo = SecondTeamRank[i].Id,
                    Round = "Group"
                };

                dbContext.Games.Add(game1);
                dbContext.SaveChanges();
            }
            for (int i = 0; i < FirstTeamRank.Count - 1; i++)
            {
                Game game2 = new()
                {
                    TeamOne = FirstTeamRank[i].Id,
                    TeamTwo = SecondTeamRank[i + 1].Id,
                    Round = "Group"
                };

                dbContext.Games.Add(game2);
                dbContext.SaveChanges();
            }

            Game game3 = new()
            {
                TeamOne = FirstTeamRank[8].Id,
                TeamTwo = SecondTeamRank[0].Id,
                Round = "Group"
            };

            dbContext.Games.Add(game3);
            dbContext.SaveChanges();
        }
        public void CreateTable(List<Team> teams)
        {
            foreach (var team in teams)
            {
                Table table = new()
                {
                    TeamName = team.Id
                };
                dbContext.Tables.Add(table);
                dbContext.SaveChanges();
            }
        }
        public async Task<Game> CreateResultAsync(Game entity)
        {
            var game = await dbContext.Games.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (game == null || game.IsPlayed)
            {
                return null;
            }

            game.ResultTeamOne = entity.ResultTeamOne;
            game.ResultTeamTwo = entity.ResultTeamTwo;
            game.IsPlayed = true;

            if (game.Round == "Group")
            {
                var teamOne = await dbContext.Tables.FirstOrDefaultAsync(x => x.TeamName == game.TeamOne);

                var teamTwo = await dbContext.Tables.FirstOrDefaultAsync(x => x.TeamName == game.TeamTwo);

                if (teamOne == null || teamTwo == null)
                    return null;

                // Points
                teamOne.Played += 1;
                teamOne.GF += (int)entity.ResultTeamOne;
                teamOne.GA += (int)entity.ResultTeamTwo;
                teamOne.GD = teamOne.GF - teamOne.GA;

                teamTwo.Played += 1;
                teamTwo.GF += (int)entity.ResultTeamTwo;
                teamTwo.GA += (int)entity.ResultTeamOne;
                teamTwo.GD = teamTwo.GF - teamTwo.GA;

                if (entity.ResultTeamOne > entity.ResultTeamTwo)
                {
                    teamOne.Points += 3;
                    teamOne.Won += 1;

                    teamTwo.Lost += 1;
                }
                else if (entity.ResultTeamOne == entity.ResultTeamTwo)
                {
                    teamOne.Points += 1;
                    teamOne.Drawn += 1;

                    teamTwo.Points += 1;
                    teamTwo.Drawn += 1;
                }
                else if (entity.ResultTeamOne < entity.ResultTeamTwo)
                {
                    teamOne.Lost += 1;

                    teamTwo.Points += 3;
                    teamTwo.Won += 1;
                }
            }

            await dbContext.SaveChangesAsync();

            return game;
        }
        public async Task<Game> UpdateResultAsync(Game entity)
        {
            var game = await dbContext.Games.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (game == null || !game.IsPlayed)
            {
                return null;
            }

            if (game.Round == "Group")
            {


                var teamOne = await dbContext.Tables.FirstOrDefaultAsync(x => x.TeamName == game.TeamOne);

                var teamTwo = await dbContext.Tables.FirstOrDefaultAsync(x => x.TeamName == game.TeamTwo);

                if (teamOne == null || teamTwo == null)
                    return null;

                // here update table --> from old results
                teamOne.Played -= 1;
                teamOne.GF -= (int)game.ResultTeamOne;
                teamOne.GA -= (int)game.ResultTeamTwo;
                teamOne.GD = teamOne.GF - teamOne.GA;

                teamTwo.Played -= 1;
                teamTwo.GF -= (int)game.ResultTeamTwo;
                teamTwo.GA -= (int)game.ResultTeamOne;
                teamTwo.GD = teamTwo.GF - teamTwo.GA;

                if (game.ResultTeamOne > game.ResultTeamTwo)
                {
                    teamOne.Points -= 3;
                    teamOne.Won -= 1;

                    teamTwo.Lost -= 1;
                }
                else if (game.ResultTeamOne == game.ResultTeamTwo)
                {
                    teamOne.Points -= 1;
                    teamOne.Drawn -= 1;

                    teamTwo.Points -= 1;
                    teamTwo.Drawn -= 1;
                }
                else if (game.ResultTeamOne < game.ResultTeamTwo)
                {
                    teamOne.Lost -= 1;

                    teamTwo.Points -= 3;
                    teamTwo.Won -= 1;
                }
            }
            game.IsPlayed = false;

            //// here update table --> after new result
            return await CreateResultAsync(entity);
        }
        public async Task<string> RandomResultAsync()
        {
            var games = await dbContext.Games.ToListAsync();

            foreach (var game in games)
            {
                if (game.IsPlayed)
                {
                    continue;
                }

                Random rnd = new();
                var resultOne = rnd.Next(5);
                var resultTwo = rnd.Next(5);

                game.ResultTeamOne = resultOne;
                game.ResultTeamTwo = resultTwo;

                if (resultOne == resultTwo && game.Round != "Group")
                {
                    game.ResultTeamOne = resultOne + 1;
                    game.ResultTeamTwo = resultTwo;
                }

                await CreateResultAsync(game);
            }

            return "All Matches Results Are Completed.";
        }
        // Knockout Phase
        public void CreateMatchesKnockoutPlayoff(List<Table> teams)
        {
            for (int i = 0; i < 8; i++)
            {
                Game game = new()
                {
                    TeamOne = teams[8 + i].TeamName,
                    TeamTwo = teams[23 - i].TeamName,
                    Round = "Knockout-Playoff"
                };

                dbContext.Games.Add(game);
                dbContext.SaveChanges();
            }
        }
        // Round of 16
        public void CreateMatchesRoundOf16(List<Table> first8, List<Team> qualifiedKnockoutPlayoff)
        {
            var randomQualifiedTeams = RandomTeamsInRank(qualifiedKnockoutPlayoff);

            for (int i = 0; i < first8.Count(); i++)
            {
                Game game = new()
                {
                    TeamOne = first8[i].TeamName,
                    TeamTwo = randomQualifiedTeams[i].Id,
                    Round = "RoundOf16"
                };

                dbContext.Games.Add(game);
                dbContext.SaveChanges();
            }
        }
        // Quarterfinals
        public void CreateMatchesQuarterfinals(List<Team> qualifiedteams)
        {
            var randomQualifiedTeams = RandomTeamsInRank(qualifiedteams);

            for (int i = 0; i < 4; i++)
            {
                Game game = new()
                {
                    TeamOne = randomQualifiedTeams[i].Id,
                    TeamTwo = randomQualifiedTeams[i + 4].Id,
                    Round = "Quarterfinals"
                };

                dbContext.Games.Add(game);
                dbContext.SaveChanges();
            }
        }
        // Semifinals
        public void CreateMatchesSemifinals(List<Team> qualifiedteams)
        {
            var randomQualifiedTeams = RandomTeamsInRank(qualifiedteams);

            for (int i = 0; i < 2; i++)
            {
                Game game = new()
                {
                    TeamOne = randomQualifiedTeams[i].Id,
                    TeamTwo = randomQualifiedTeams[i + 2].Id,
                    Round = "Semifinals"
                };

                dbContext.Games.Add(game);
                dbContext.SaveChanges();
            }
        }
        // Finalss
        public void CreateMatchesFinal(List<Team> qualifiedteams)
        {
            var randomQualifiedTeams = RandomTeamsInRank(qualifiedteams);

            Game game = new()
            {
                TeamOne = randomQualifiedTeams[0].Id,
                TeamTwo = randomQualifiedTeams[1].Id,
                Round = "Final"
            };

            dbContext.Games.Add(game);
            dbContext.SaveChanges();
        }
    }
}
