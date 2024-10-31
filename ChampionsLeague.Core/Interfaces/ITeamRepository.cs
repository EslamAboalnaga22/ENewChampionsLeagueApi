namespace ChampionsLeague.Core.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllTeamAsync();
        Task<Team> GetTeamByIdAsync(int teamId);
        Task<Team> AddTeamAsync(Team entity);
        Task<Team> UpdateTeamAsync(int teamId, Team entity);
        Task<bool> DeleteTeamAsync(int teamId);
    }
}
