namespace ChampionsLeague.Core.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int TeamOne { get; set; } 
        public virtual Team? TOne { get; set; }
        public int TeamTwo { get; set; } 
        public virtual Team? TTwo { get; set; }
        public int? ResultTeamOne { get; set; } 
        public int? ResultTeamTwo { get; set; } 
        public string Round { get; set; } = string.Empty;
        public string? Stadium { get; set; } = string.Empty;
        public DateTime? Time { get; set; } 
        public bool IsPlayed { get; set; }
    }
}
