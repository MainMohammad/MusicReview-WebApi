using Microsoft.EntityFrameworkCore;
using MusicReview.Models;
using System.Net;

namespace MusicReview.Data
{
    public class DbCtx : DbContext
    {
        public DbCtx(DbContextOptions<DbCtx> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MusicArtist>()
                .HasKey(ma => new { ma.MusicId, ma.ArtistId});

            modelBuilder.Entity<MusicArtist>()
                .HasOne(m => m.Music)
                .WithMany(ma => ma.MusicArtists)
                .HasForeignKey(m => m.MusicId);
            modelBuilder.Entity<MusicArtist>()
                .HasOne(a => a.Artist)
                .WithMany(ma => ma.MusicArtists)
                .HasForeignKey(a => a.ArtistId);

            modelBuilder.Entity<MusicGenre>()
                .HasKey(mg => new { mg.MusicId, mg.GenreId });

            modelBuilder.Entity<MusicGenre>()
                .HasOne(m => m.Music)
                .WithMany(mg => mg.MusicGenres)
                .HasForeignKey(m => m.MusicId);
            modelBuilder.Entity<MusicGenre>()
                .HasOne(g => g.Genre)
                .WithMany(mg => mg.MusicGenres)
                .HasForeignKey(g => g.GenreId);
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<MusicArtist> MusicArtists { get; set; }
        public DbSet<MusicGenre> MusicGenres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
    }
}
