using ItuTweets.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ItuTweets.Data.Context
{
    public class ItuTweetsContext : DbContext
    {
        public ItuTweetsContext(DbContextOptions<ItuTweetsContext> options) : base(options) { }

        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<TwitterUser> TwitterUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tweet>(e =>
            {
                e.ToTable("tb_tweets");
                e.HasKey(pk => pk.Uuid);
                e.Property(t => t.Lang).HasMaxLength(10);
            });

            modelBuilder.Entity<TwitterUser>(e =>
            {
                e.ToTable("tb_twitter_users");
                e.HasKey(pk => pk.Uuid);
                e.Property(tu => tu.Name).HasMaxLength(250);
                e.Property(tu => tu.Username).HasMaxLength(250);
            });
        }
    }
}
