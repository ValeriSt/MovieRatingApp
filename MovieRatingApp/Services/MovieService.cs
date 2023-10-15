using MovieRatingApp.Common;
using MovieRatingApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieRatingApp.Services
{
    public class MovieService
    {
        List<Movie> movieList = new();
        public async Task<List<Movie>> GetMovies()
        {
            if (movieList.Count > 0)
                return movieList;

            using var stream = await FileSystem.OpenAppPackageFileAsync("movie.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            movieList = JsonConvert.DeserializeObject<List<Movie>>(contents);

            return movieList;
        }
    }

}
