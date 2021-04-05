using MovieMagic.DTO;
using System.Threading.Tasks;

namespace MovieMagic.Service
{
    public interface IMovieService
    {
        Task<MovieCollection> GetAllMovies();

        Task<MovieDetails> GetMovieById(string id);
    }
}
