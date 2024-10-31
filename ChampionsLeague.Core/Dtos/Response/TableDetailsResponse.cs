namespace ChampionsLeague.Core.Dtos.Response
{
    public class TableDetailsResponse
    {
        public int Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public int Points { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int GF { get; set; }
        public int GA { get; set; }
        public int GD { get; set; }
    }
}
