using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace quadrolingoAPI.Models
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Exercise> Exercises { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserWord> UserWords { get; set; } = null!;
        public DbSet<Word> Words { get; set; } = null!;
        public DbSet<WordExercise> WordExercises { get; set; } = null!;

    }
}
