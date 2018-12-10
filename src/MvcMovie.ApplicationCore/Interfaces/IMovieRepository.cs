using MvcMovie.ApplicationCore.Entities;
using System.Collections.Generic;

namespace MvcMovie.ApplicationCore.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        ICollection<Movie> GetByTitle(string title);
        ICollection<Movie> GetByGenre(string genre);
        ICollection<string> GetAllGenres();
    }
}
