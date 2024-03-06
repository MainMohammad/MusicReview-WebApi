using MusicReview.Models;
using MusicReview.Data;

namespace MusicReview.Data
{
    public class Seed
    {
        public static void SeedDataContext(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var _ctx = serviceScope.ServiceProvider.GetService<DbCtx>();
                _ctx.Database.EnsureCreated();

                if (!_ctx.Labels.Any())
                {
                    _ctx.Labels.AddRange(new List<Label>()
                    {
                        new Label()
                        {
                            Name = "CactusJack"
                        },

                        new Label()
                        {
                            Name = "OVO"
                        },

                        new Label()
                        {
                            Name = "Idk"
                        }
                    });
                    _ctx.SaveChanges();
                }

                if (!_ctx.Artists.Any())
                {
                    _ctx.Artists.AddRange(new List<Artist>()
                    {
                        new Artist()
                        {
                            Name = "Drake",
                            LabelId = 2
                        },

                        new Artist()
                        {
                            Name = "Travis Scott",
                            LabelId = 1
                        },

                        new Artist()
                        {
                            Name = "The Weekend",
                            LabelId = 3
                        }
                    });
                    _ctx.SaveChanges();
                }

                if (!_ctx.Genres.Any())
                {
                    _ctx.Genres.AddRange(new List<Genre>()
                    {
                        new Genre()
                        {
                            Name = "Trap"
                        },

                        new Genre()
                        {
                            Name = "Rap"
                        },

                        new Genre()
                        {
                            Name = "Pop"
                        }
                    });
                    _ctx.SaveChanges();
                }

                if (!_ctx.Musics.Any())
                {
                    _ctx.Musics.AddRange(new List<Music>()
                    {
                        new Music()
                        {
                            Name = "Sicko Mode",
                            ReleaseDate = new DateTime(2018, 08, 21)
                        },

                        new Music()
                        {
                            Name = "Back2Back",
                            ReleaseDate = new DateTime(2015, 07, 29)
                        },

                        new Music()
                        {
                            Name = "Blinding lights",
                            ReleaseDate = new DateTime(2019, 11, 29)
                        }
                    });
                    _ctx.SaveChanges();
                }

                if (!_ctx.Reviewers.Any())
                {
                    _ctx.Reviewers.AddRange(new List<Reviewer>()
                    {
                        new Reviewer()
                        {
                            FirstName = "Mohammad",
                            LastName = "Bagheri"
                        },

                        new Reviewer()
                        {
                            FirstName = "AmirReza",
                            LastName = "Heydari"
                        },

                        new Reviewer()
                        {
                            FirstName = "MohammadReza",
                            LastName = "MohammadAliBeyk"
                        }
                    });
                    _ctx.SaveChanges();
                }

                if (!_ctx.Reviews.Any())
                {
                    _ctx.Reviews.AddRange(new List<Review>()
                    {
                        new Review()
                        {
                            Title = "Best song ever",
                            Text = "This def gonna rule the next 10 years in the music market.",
                            Rating = 5,
                            MusicId = 1,
                            ReviewerId = 1
                        },

                        new Review()
                        {
                            Title = "Trash",
                            Text = "Money, Bitches, Gang repeat is all the content of this song!",
                            Rating = 1,
                            MusicId = 1,
                            ReviewerId = 2
                        },

                        new Review()
                        {
                            Title = "Sicko Mode",
                            Text = "Its really a mid track Idk why people have gone crazy about it.",
                            Rating = 3,
                            MusicId = 1,
                            ReviewerId = 3
                        },

                        new Review()
                        {
                            Title = "Back 2 Back",
                            Text = "The most underrated freestyle I've ever heard.",
                            Rating = 5,
                            MusicId = 2,
                            ReviewerId = 2
                        },

                        new Review()
                        {
                            Title = "Drizzy",
                            Text = "This the real Drizzy not the 2023 version",
                            Rating = 4,
                            MusicId = 2,
                            ReviewerId = 3
                        },

                        new Review()
                        {
                            Title = "Fire",
                            Text = "He used to drop bars...",
                            Rating = 5,
                            MusicId = 2,
                            ReviewerId = 1
                        },

                        new Review()
                        {
                            Title = "Masterpiece",
                            Text = "Weekend knows how to do it",
                            Rating = 4,
                            MusicId = 3,
                            ReviewerId = 2
                        },

                        new Review()
                        {
                            Title = "Nostalgia",
                            Text = "This reminded me of Micheal in the late 80's great.",
                            Rating = 4,
                            MusicId = 3,
                            ReviewerId = 1
                        },

                        new Review()
                        {
                            Title = "Blinded Minds",
                            Text = "This shit is overrated as fuck!",
                            Rating = 2,
                            MusicId = 3,
                            ReviewerId = 3
                        }
                    });
                    _ctx.SaveChanges();
                }

                if (!_ctx.MusicArtists.Any())
                {
                    _ctx.MusicArtists.AddRange(new List<MusicArtist>()
                    {
                        new MusicArtist()
                        {
                            ArtistId = 1,
                            MusicId = 1
                        },

                        new MusicArtist()
                        {
                            ArtistId = 2,
                            MusicId = 1
                        },

                        new MusicArtist()
                        {
                            ArtistId = 1,
                            MusicId = 2
                        },

                        new MusicArtist()
                        {
                            ArtistId = 3,
                            MusicId = 3
                        }
                    });
                    _ctx.SaveChanges();
                }

                if (!_ctx.MusicGenres.Any())
                {
                    _ctx.MusicGenres.AddRange(new List<MusicGenre>()
                    {
                        new MusicGenre()
                        {
                            GenreId = 1,
                            MusicId = 1
                        },

                        new MusicGenre()
                        {
                            GenreId = 2,
                            MusicId = 2
                        },

                        new MusicGenre()
                        {
                            GenreId = 3,
                            MusicId = 3
                        }
                    });
                    _ctx.SaveChanges();
                }
            }
        }
    }
}