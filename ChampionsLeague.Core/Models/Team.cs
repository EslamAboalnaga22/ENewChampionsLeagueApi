namespace ChampionsLeague.Core.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public virtual Table? Table { get; set; }
        public virtual ICollection<Game> MatchesOne { get; set; } = new List<Game>();
        public virtual ICollection<Game> MatchesTwo { get; set; } = new List<Game>();
    }
}
