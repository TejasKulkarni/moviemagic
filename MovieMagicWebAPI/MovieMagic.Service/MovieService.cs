using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MovieMagic.DTO;
using Newtonsoft.Json;
using Serilog;

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

        public async Task<MovieCollection> GetAllMovies()
        {
            var url = $"api/filmworld/movies";

            var result = await RetryLogic<MovieCollection>(url);

            if (result != null)
                return result;

            throw new Exception("Unable to fetch data.");
        }

        public async Task<MovieDetails> GetMovieById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new Exception("ID not passed");

            var url = $"api/filmworld/movie/{id}";

            var result = await RetryLogic<MovieDetails>(url);

            if (result != null)
                return result;

            throw new Exception("No data found");
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

                    await Task.Delay(TimeSpan.FromSeconds(3));
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
                if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    throw new Exception("No records exist.");

                if(response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                    throw new Exception("Service Unavailable");
            }

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
