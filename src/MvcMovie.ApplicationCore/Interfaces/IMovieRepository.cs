using MvcMovie.ApplicationCore.Entities;
using System.Collections.Generic;

namespace MvcMovie.ApplicationCore.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        ICollection<Movie> SearchByTitle(string title);
        ICollection<Movie> SearchByGenre(string genre);
        ICollection<string> GetAllGenres();
    }
}
