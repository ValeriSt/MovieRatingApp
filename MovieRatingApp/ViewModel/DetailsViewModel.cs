using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieRatingApp.Common;
using MovieRatingApp.Model;
using MovieRatingApp.Services;
using MovieRatingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApp.ViewModel
{
    [QueryProperty("Movie", "Movie")]
    [QueryProperty("UserRating", "UserRating")]
    [QueryProperty("AvgRating", "AvgRating")]

    public partial class DetailsViewModel : BaseViewModel
    {
        public DetailsViewModel()
        {
            trackMovie = new TrackMovie();
            trackMovie.MovieChanged += MovieChanged;
        }

        [ObservableProperty]
        UserRating userRating;

        [ObservableProperty]
        double avgRating;

        [ObservableProperty]
        Movie movie;

        private TrackMovie trackMovie;

        private bool isRated = false;

        public bool IsRated
        {
            get => isRated;
            set => SetProperty(ref isRated, value);
        }

        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.Navigation.PopAsync(true); //GoToAsync($"//MainPage");
        }

        [RelayCommand]
        public void ShowPopup()
        {

            UserRatingService userRatingService = new UserRatingService();
            UserRatingViewModel viewModel = new UserRatingViewModel(userRatingService)
            {
                Movie = Movie,
                TrackMovie = trackMovie
            };
            var popup = new RateMoviePopup(viewModel);
            
            Shell.Current.CurrentPage.ShowPopup(popup);
        }

        public async void MovieChanged(object sender, EventArgs e)
        {
            UserRatingService userRatingService = new UserRatingService();
            var userRatingList = await userRatingService.GetUserRating();
            UserRating = userRatingList.FirstOrDefault(x => x.MovieId == Movie.Id && x.UserId == Utility.CurrentUser.Id);

            var movieRatings = userRatingList.FindAll(x => x.MovieId == Movie.Id);
            AvgRating = movieRatings.ConvertAll<double>(x => x.Rating).Aggregate((sum, val) => sum + val) / movieRatings.Count;
            IsRated = true;
        }
    }
}
