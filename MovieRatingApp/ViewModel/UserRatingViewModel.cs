using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieRatingApp.Common;
using MovieRatingApp.Model;
using MovieRatingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieRatingApp.ViewModel
{
    public partial class UserRatingViewModel : BaseViewModel
    {

        UserRatingService userRatingService;
        private double rating;
        private string errorMessage;
        private TrackMovie trackmovie;
        private Movie movie;
        private bool isRated = false;
        public ICommand RateCommand { get; }


        public UserRatingViewModel(UserRatingService userRatingService)
        {
            RateCommand = new RelayCommand(RateMovie);
            this.userRatingService = userRatingService;
        }

        public TrackMovie TrackMovie
        {
            get { return trackmovie; }
            set { trackmovie = value; }
        }

        public Movie Movie
        {
            get { return movie; }
            set { movie = value; }
        }

        public double Rating
        {
            get => rating;
            set => SetProperty(ref rating, value);
        }
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }
        public bool IsRated
        {
            get => isRated;
            set => SetProperty(ref isRated, value);
        }

        public async void RateMovie()
        {
            if (Utility.CurrentUser == null) 
            {
                ErrorMessage = "You need to login to use this function.";
                return;
            }

            if (movie == null)
            {
                ErrorMessage = "Please select a movie to rate.";
                return;
            }

            userRatingService.RateMovie(Rating, movie.Id);
            TrackMovie.ChangeMovie();
        }
    }
}
