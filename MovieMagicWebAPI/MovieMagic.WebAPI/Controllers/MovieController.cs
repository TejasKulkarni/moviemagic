using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieMagic.Service;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieMagic.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        // GET: api/<MovieController>
        [HttpGet]
        [Route("AllMovies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllMovies();
            return Ok(result);
        }

        // GET api/<MovieController>/5
        [HttpGet("movie/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _service.GetMovieById(id);
            return Ok(result);
        }
    }
}
