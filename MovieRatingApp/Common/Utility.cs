using MovieRatingApp.Model;
using Newtonsoft.Json;

namespace MovieRatingApp.Common
{
    public static class Utility
    {
        public static User CurrentUser { get; set; }

        public static List<UserRating> GetUserRatings() => JsonConvert.DeserializeObject<List<UserRating>>(File.ReadAllText("userRatings.json"))
            .Where(x => x.UserId == CurrentUser.Id).ToList();

    }
}
