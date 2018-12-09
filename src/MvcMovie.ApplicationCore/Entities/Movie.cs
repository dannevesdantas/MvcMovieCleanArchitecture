using System;

namespace MvcMovie.ApplicationCore.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string Rating { get; set; }
    }
}
