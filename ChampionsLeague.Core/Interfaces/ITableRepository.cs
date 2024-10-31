namespace ChampionsLeague.Core.Interfaces
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetTableAsync();
        Task<Table> GetTableForOneTeamAsync(string teamName);
    }
}
