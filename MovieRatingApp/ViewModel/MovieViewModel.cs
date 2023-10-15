using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRatingApp.Model;
using MovieRatingApp.Services;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using MovieRatingApp.View;
using CommunityToolkit.Mvvm.ComponentModel;
using MovieRatingApp.Common;

namespace MovieRatingApp.ViewModel
{
    public partial class MovieViewModel : BaseViewModel
    {
        MovieService movieService;
        UserRatingService userRatingService;
        public ObservableCollection<Movie> Movies { get; } = new();


        public MovieViewModel(MovieService movieService, UserRatingService userRatingService)
        {
            Title = "Movies";
            this.movieService = movieService;
            this.userRatingService = userRatingService;
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GoToDetailsAsync(Movie movie)
        {
            if (movie is null) return;
            var userRatingList = await userRatingService.GetUserRating();
            var userRating = userRatingList.FirstOrDefault(x => x.MovieId == movie.Id && x.UserId == Utility.CurrentUser.Id);
            var movieRatings = userRatingList.FindAll(x => x.MovieId == movie.Id);
            var avgRating = 0.0;
            if (movieRatings.Count != 0)
            {
                avgRating = movieRatings.ConvertAll<double>(x => x.Rating).Aggregate((sum, val) => sum + val) / movieRatings.Count;
            }
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, new Dictionary<string, object>
            {
                {"UserRating", userRating},
                {"Movie", movie},
                {"AvgRating", avgRating }
            });
        }

        [RelayCommand]
        async Task GetMoviesAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var movies = await movieService.GetMovies();
                if (Movies.Count != 0)
                {
                    Movies.Clear();
                }
                foreach (var movie in movies)
                {
                    Movies.Add(movie);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", "Unable to see movies.", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
        [RelayCommand]
        async Task LogoutUser()
        {
            // You should replace "MainPage" with the actual route to your MainPage
            await Shell.Current.GoToAsync($"//UserLoginPage");
        }
    }
}
