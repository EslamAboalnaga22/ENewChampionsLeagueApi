namespace ChampionsLeague.Core.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int TeamName { get; set; }
        public virtual Team? Team { get; set; } 

        [DefaultValue(null)]
        public int Points { get; set; }
        [DefaultValue(null)]
        public int Played { get; set; }
        [DefaultValue(null)]
        public int Won { get; set; }
        [DefaultValue(null)]
        public int Drawn { get; set; }
        [DefaultValue(null)]
        public int Lost { get; set; }
        [DefaultValue(null)]
        public int GF { get; set; }
        [DefaultValue(null)]
        public int GA { get; set; }

        [DefaultValue(null)]
        public int GD { get; set; }
    }
}
