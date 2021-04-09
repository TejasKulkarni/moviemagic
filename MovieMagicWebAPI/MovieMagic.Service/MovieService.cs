using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MovieMagic.DTO;
using Newtonsoft.Json;
using Serilog;
using System.Linq;
using MovieMagic.Common.Enums;
using MovieMagic.Common.Constants;

namespace MovieMagic.Service
{
    /// <summary>
    /// Movie service to process all service requests
    /// </summary>
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<MovieCollectionModel> GetAllMovies()
        {
            var firstSourceTask = RetryLogic<MovieCollection>(Constants.FilmWorldAllMoviesUrl);
            var secondSourceTask = RetryLogic<MovieCollection>(Constants.CinemaWorldAllMoviesUrl);

            await Task.WhenAll(firstSourceTask, secondSourceTask);

            var movieCollectionModel = new MovieCollectionModel
            {
                MoviesModel = new List<MovieModel>()
            };

            if (firstSourceTask?.Result != null && secondSourceTask?.Result != null)
            {
                foreach (var movie in firstSourceTask?.Result.Movies)
                    AddMovieToCollection(movieCollectionModel, movie, Source.FilmWorld);

                foreach (var movie in secondSourceTask.Result.Movies)
                {
                    if (movieCollectionModel.MoviesModel.Count(x => x.Title == movie.Title) < 1)
                        AddMovieToCollection(movieCollectionModel, movie, Source.CinemaWorld);
                }

                return movieCollectionModel;
            }            

            throw new Exception("Unable to fetch data.");
        }

        private static void AddMovieToCollection(MovieCollectionModel movieCollectionModel, Movie movie, Source source)
        {
            movieCollectionModel.MoviesModel.Add(new MovieModel
            {
                ID = movie.ID,
                Poster = movie.Poster,
                Source = source,
                Title = movie.Title,
                Type = movie.Type,
                Year = movie.Year
            });
        }

        public async Task<MovieCollection> GetAllMoviesBySource(Source source)
        {
            var sourceUrl = string.Empty;

            if (source == Source.CinemaWorld)
                sourceUrl = $"api/cinemaworld/movies";
            else if (source == Source.FilmWorld)
                sourceUrl = $"api/filmworld/movies";

            var allMovieResult = await RetryLogic<MovieCollection>(sourceUrl);

            if (allMovieResult != null)
                return allMovieResult;

            throw new Exception("Unable to fetch data.");
        }

        public async Task<MovieDetailResult> GetMovieById(string id, Source source)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new Exception("ID not passed");

            var firstSourceUrl = string.Empty;
            var secondSourceUrl = string.Empty;
            var secondSource = source;

            UpdateSourceUrl(id, source, ref firstSourceUrl, ref secondSourceUrl, ref secondSource);

            var firstSourceTask = RetryLogic<MovieDetails>(firstSourceUrl);
            var secondSourceTask = GetAllMoviesBySource(secondSource);

            await Task.WhenAll(firstSourceTask, secondSourceTask);

            if (firstSourceTask.Result != null && secondSourceTask.Result != null)
                return await GetMovieData(firstSourceTask, secondSourceTask, secondSourceUrl);

            throw new Exception("Error retrieving data");
        }

        private static void UpdateSourceUrl(string id, Source source, ref string firstSourceUrl, ref string secondSourceUrl, ref Source secondSource)
        {
            if (source == Source.CinemaWorld)
            {
                firstSourceUrl = Constants.CinemaWorldUrlById + id;
                secondSourceUrl = Constants.FilmWorldUrlById;
                secondSource = Source.FilmWorld;
            }
            else if (source == Source.FilmWorld)
            {
                firstSourceUrl = Constants.FilmWorldUrlById + id;
                secondSourceUrl = Constants.CinemaWorldUrlById;
                secondSource = Source.CinemaWorld;
            }
        }

        /// <summary>
        /// Retry logic would try to call method 3 times
        /// If it returns error state after 3 attempts
        /// it will throw error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<T> RetryLogic<T>(string url)
        {
            var exceptionList = new List<Exception>();
            var attemptCount = 0;

            do
            {
                try
                {
                    return await GetMovieData<T>(url);
                }
                catch (Exception ex)
                {
                    exceptionList.Add(ex);
                    ++attemptCount;

                    await Task.Delay(TimeSpan.FromSeconds(1));
                    Log.Error(ex.Message);
                }

            } while (attemptCount < 3);

            throw new AggregateException(exceptionList);
        }

        /// <summary>
        /// Method to get movie data based on URL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<T> GetMovieData<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(result))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    throw new Exception("No records exist.");

                if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                    throw new Exception("Service Unavailable");
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        private async Task<MovieDetailResult> GetMovieData(Task<MovieDetails> firstSourceTask, Task<MovieCollection> secondSourceTask, string secondSourceUrl)
        {
            var secondSourceMovie = secondSourceTask.Result.Movies.Where(x => x.Title == firstSourceTask.Result.Title);
            var movieDetailResult = new MovieDetailResult();

            if (secondSourceMovie != null && secondSourceMovie.Any())
            {
                var secondSourceByIdUrl = secondSourceUrl + secondSourceMovie.FirstOrDefault().ID;
                var secondSourceResult = await RetryLogic<MovieDetails>(secondSourceByIdUrl);

                if (secondSourceResult != null)
                {
                    movieDetailResult.CinemaWorldPrice = secondSourceResult.Price;
                    movieDetailResult.FilmWorldPrice = firstSourceTask.Result.Price;
                    movieDetailResult.MovieDetails = firstSourceTask.Result;
                }
            }
            else
            {
                movieDetailResult.CinemaWorldPrice = string.Empty;
                movieDetailResult.FilmWorldPrice = firstSourceTask.Result.Price;
                movieDetailResult.MovieDetails = firstSourceTask.Result;
            }

            return movieDetailResult;
        }
    }
}
