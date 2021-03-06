﻿using MvcMovie.ApplicationCore.Entities;
using MvcMovie.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MvcMovie.Infrastructure.Data
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MvcMovieContext dbContext) : base(dbContext)
        {
        }

        public ICollection<string> GetAllGenres()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _dbContext.Movies
                                            orderby m.Genre
                                            select m.Genre;

            return genreQuery.Distinct().ToList();

        }

        public ICollection<Movie> GetByTitle(string title)
        {
            return _dbContext.Movies.Where(m => m.Title.Contains(title)).ToList();
        }

        public ICollection<Movie> GetByGenre(string genre)
        {
            return _dbContext.Movies.Where(m => m.Genre == genre).ToList();
        }
    }
}
