namespace ChampionsLeague.Core.Interfaces
{
    public interface IKnockoutStagesRepository
    {
        // Knockout Phase
        Task<string> CreateKnockoutPlayAsync();
        Task<IEnumerable<Game>> GetAllKnockoutPlayoffGamesAsync();
        // Round of 16
        Task<string> CreateRoundOf16Async();
        Task<IEnumerable<Game>> GetAllRoundOf16GamesAsync();
        // Quarterfinals
        Task<string> CreateQuarterfinalsAsync();
        Task<IEnumerable<Game>> GetAllQuarterfinalsGamesAsync();
        // Semifinals
        Task<string> CreateSemifinalsAsync();
        Task<IEnumerable<Game>> GetAllSemifinalsGamesAsync();
        // Final
        Task<string> CreateFinalAsync();
        Task<IEnumerable<Game>> GetAllFinalGamesAsync();
    }
}
