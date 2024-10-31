namespace ChampionsLeague.Core.Dtos.Request
{
    public class AddTeamRequest
    {
        public string TeamName { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
