using System;
using System.Collections.Generic;
using System.Text;

namespace MovieMagic.DTO
{
    public class MovieCollection
    {
        public List<Movie> Movies { get; set; }
    }

    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
