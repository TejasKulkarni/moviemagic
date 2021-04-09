using MovieMagic.Common.Enums;
using MovieMagic.DTO;
using System.Threading.Tasks;

namespace MovieMagic.Service
{
    public interface IMovieService
    {
        Task<MovieCollectionModel> GetAllMovies();

        Task<MovieCollection> GetAllMoviesBySource(Source source);

        Task<MovieDetailResult> GetMovieById(string id, Source source);
    }
}
