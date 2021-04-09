using MovieMagic.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieMagic.DTO
{
    public class MovieDetailResult
    {
        public string CinemaWorldPrice { get; set; }

        public string FilmWorldPrice { get; set; }

        public Source Source { get; set; }

        public MovieDetails MovieDetails { get; set; }
    }
}
