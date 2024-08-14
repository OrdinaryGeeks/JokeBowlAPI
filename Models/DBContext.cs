using Microsoft.EntityFrameworkCore;
using JokeAIAPI.Models;
namespace JokeAIAPI.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class DBContext : IdentityDbContext<User>
    {
        public DBContext(DbContextOptions options):base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
        }
        public DbSet<JokeAIAPI.Models.Category> Category { get; set; } = default!;
        public DbSet<JokeAIAPI.Models.Joke> Joke { get; set; } = default!;
        public DbSet<JokeAIAPI.Models.JokeDownVote> JokeDownVote { get; set; } = default!;
        public DbSet<JokeAIAPI.Models.JokeUpVote> JokeUpVote { get; set; } = default!;
        public DbSet<JokeAIAPI.Models.PunchLine> PunchLine { get; set; } = default!;
        public DbSet<JokeAIAPI.Models.Setup> Setup { get; set; } = default!;
        public DbSet<JokeAIAPI.Models.Subject> Subject { get; set; } = default!;
        public DbSet<JokeAIAPI.Models.JokeType> JokeType { get; set; } = default!;


    }
}
