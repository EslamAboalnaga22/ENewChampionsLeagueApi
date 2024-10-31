namespace ChampionsLeague.Core.Interfaces
{
    public interface ILeagueRepository
    {
        Task<string> CreateLeagueAsync();
        Task<IEnumerable<Game>> GetAllLeagueGamesAsync();
        Task<Game> GetLeagueGameByIdAsync(int gameId);
        Task<IEnumerable<Game>> GetLeagueGamesByTeamNameAsync(string teamName);
    }
}
