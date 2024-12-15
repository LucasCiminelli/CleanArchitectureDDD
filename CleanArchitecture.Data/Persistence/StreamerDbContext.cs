using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContext : DbContext
    {
        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options)
        {
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=localhost; 
        //        Initial Catalog=Streamer;user id=sa;password=VaxiDrez2025$")
        //    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
        //    .EnableSensitiveDataLogging();
        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                { 
                    case EntityState.Added:
                        entry.Entity.CreatedDate =  DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Streamer)
                .HasForeignKey(m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Director>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Director)
                .HasForeignKey(m => m.DirectorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Video>()
                .HasMany(m => m.Actores)
                .WithMany(m => m.Videos)
                .UsingEntity<VideoActor>
                (
                    x => x
                    .HasOne(p => p.Actor)
                    .WithMany(p => p.VideoActors)
                    .HasForeignKey(p => p.ActorId),

                    x => x
                    .HasOne(v => v.Video)
                    .WithMany(v => v.VideoActors)
                    .HasForeignKey(v => v.VideoId),

                    x =>
                    {
                        x.HasKey(t => new { t.ActorId, t.VideoId });
                    }
                );

            modelBuilder.Entity<VideoActor>()
                .Ignore(va => va.Id);

            //modelBuilder.Entity<Video>()
            //    .HasMany(p => p.Actores)
            //    .WithMany(t => t.Videos)
            //    .UsingEntity<VideoActor>(
            //        pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
            //    );

        
        }


        public DbSet<Streamer>? Streamers { get; set; }

        public DbSet<Video>? Videos { get; set; }

        public DbSet<Actor>? Actores { get; set; }

        public DbSet<Director>? Directores { get; set; }

    }
}
