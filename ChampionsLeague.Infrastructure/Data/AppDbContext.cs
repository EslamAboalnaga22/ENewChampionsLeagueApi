namespace ChampionsLeague.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Table>(builder =>
            {
                builder.HasOne(t => t.Team)
                       .WithOne(t => t.Table)
                       .HasForeignKey<Table>(t => t.TeamName)
                       .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Game>(builder =>
            {
                builder.HasOne(o => o.TOne)
                       .WithMany(m => m.MatchesOne)
                       .HasForeignKey(x => x.TeamOne)
                       .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Game>(builder =>
            {
                builder.HasOne(o => o.TTwo)
                       .WithMany(m => m.MatchesTwo)
                       .HasForeignKey(x => x.TeamTwo)
                       .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var ModifiedEntities = ChangeTracker.Entries()
                                    .Where(x => x.State == EntityState.Added
                                             || x.State == EntityState.Deleted
                                             || x.State == EntityState.Modified)
                                    .ToList();

            foreach (var item in ModifiedEntities)
            {
                var auditlog = new AuditLog
                {
                    Aciton = item.State.ToString(),
                    TimeStamp = DateTime.UtcNow,
                    EntityType = item.Entity.GetType().Name,
                    Changes = GetUpdate(item)
                };

                AuditLogs.Add(auditlog);
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        private static string GetUpdate(EntityEntry entry)
        {
            var sb = new StringBuilder();

            foreach (var prop in entry.OriginalValues.Properties)
            {
                var originalValue = entry.OriginalValues[prop];
                var currentValue = entry.CurrentValues[prop];

                if(!Equals(originalValue, currentValue))
                    sb.Append($"{prop.Name}: From {originalValue} To {currentValue}");
            }

            return sb.ToString();
        }
    }
}
