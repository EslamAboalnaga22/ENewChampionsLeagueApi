namespace ChampionsLeague.Core.Dtos.Response
{
    public class TeamDetailsResponse
    {
        public int Id { get; set; } 
        public string TeamName { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
