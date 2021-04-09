using MovieMagic.Common.Enums;
using System.Collections.Generic;

namespace MovieMagic.DTO
{
    public class MovieCollectionModel
    {
        public List<MovieModel> MoviesModel { get; set; }

    }

    public class MovieModel : Movie
    {
        public Source Source { get; set; }
    }
}
