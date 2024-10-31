namespace ChampionsLeague.Core.Interfaces
{
    public interface ICreateMatchesRepository
    {
        // League Phase
        List<Team> RandomTeamsInRank(List<Team> teams);
        void CreateMatchesInSameRank(List<Team> TeamRank);
        void CreateMatchesForRankAndRank(List<Team> FirstTeamRank, List<Team> SecondTeamRank);
        void CreateTable(List<Team> teams);
        Task<Game> CreateResultAsync(Game entity);
        Task<Game> UpdateResultAsync(Game entity);
        Task<string> RandomResultAsync();

        // Knockout Phase
        void CreateMatchesKnockoutPlayoff(List<Table> teams);

        // Round of 16
        void CreateMatchesRoundOf16(List<Table> first16, List<Team> qualifiedKnockoutPlayoff);

        // Quarterfinals
        void CreateMatchesQuarterfinals(List<Team> qualifiedteams);
        // Semifinals
        void CreateMatchesSemifinals(List<Team> qualifiedteams);
        // Finals
        void CreateMatchesFinal(List<Team> qualifiedteams);
    }
}
