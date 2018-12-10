using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.ApplicationCore.Entities;
using System.Collections.Generic;

namespace MvcMovie.Web.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> movies;
        public SelectList genres;
        public string movieGenre { get; set; }
    }
}
