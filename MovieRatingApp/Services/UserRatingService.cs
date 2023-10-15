using MovieRatingApp.Common;
using MovieRatingApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApp.Services
{
    public class UserRatingService
    {
        public async Task<List<UserRating>> GetUserRating()
        {
            string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(appDataDirectory, "userRatingsList.json");

            if (!File.Exists(filePath))
            {
                return new List<UserRating>();
            }

            string contents = File.ReadAllText(filePath);

            List<UserRating> userRatingsList = JsonConvert.DeserializeObject<List<UserRating>>(contents);

            if (userRatingsList == null)
            {
                return new List<UserRating>();
            }

            return userRatingsList;
        }

        public async void RateMovie(double stars, int movieId)
        {
            List<UserRating> userRatingsList;
            userRatingsList = await GetUserRating();

            if (userRatingsList != null && userRatingsList.Count > 0)
            {
                if (userRatingsList.Count(x => x.UserId == Utility.CurrentUser.Id && x.MovieId == movieId) > 0)
                {
                    return;
                }

            }

            userRatingsList.Add(new UserRating()
            {
                Id = userRatingsList.Count > 0 ? userRatingsList.Max(u => u.Id) + 1 : 1,//userRatingsList.Last() == null ? 1 : userRatingsList.Last().Id + 1,
                UserId = Utility.CurrentUser.Id,
                MovieId = movieId,
                Rating = stars
            });

            string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string filePath = Path.Combine(appDataDirectory, "userRatingsList.json");

            File.WriteAllText(filePath, JsonConvert.SerializeObject(userRatingsList));
        }
    }
}
