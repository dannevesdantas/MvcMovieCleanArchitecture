using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvcMovie.ApplicationCore.Entities;

namespace MvcMovie.Infrastructure.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>(ConfigureMovie);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");

            builder.Property(m => m.Title)
                .HasMaxLength(60);

            builder.Property(m => m.Genre)
                .IsRequired(true)
                .HasMaxLength(30);

            builder.Property(m => m.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Property(m => m.Rating)
                .HasMaxLength(5);
        }
    }
}
