namespace ChampionsLeague.Core.Dtos.Response
{
    public class GameDetailsResponse
    {
        public int Id { get; set; }
        public string TeamOne { get; set; } = string.Empty;
        public string TeamTwo { get; set; } = string.Empty;
        public int? ResultTeamOne { get; set; } 
        public int? ResultTeamTwo { get; set; } 
        public string Round { get; set; } = string.Empty;
        public string? Stadium { get; set; } = string.Empty;
        public DateTime? Time { get; set; }
        public bool IsPlayed { get; set; }
    }
}
