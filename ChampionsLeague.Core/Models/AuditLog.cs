namespace ChampionsLeague.Core.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string User => "System-User";
        public required string EntityType {  get; set; } = string.Empty;
        public required string Aciton {  get; set; } = string.Empty;
        public required DateTime TimeStamp {  get; set; }
        public required string Changes {  get; set; } = string.Empty;
    }
}
