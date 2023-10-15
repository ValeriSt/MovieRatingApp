using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieRatingApp.Common;
using MovieRatingApp.Model;
using MovieRatingApp.Services;
using MovieRatingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieRatingApp.ViewModel
{
    public partial class UserLoginViewModel : BaseViewModel
    {

        private string username;
        private string password;
        private string errorMessage;

        private UserService userService;

        private List<User> Users;

        IConnectivity connectivity;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }

        public ICommand LoginCommand { get; }

        public UserLoginViewModel(IConnectivity connectivity)
        {
            LoginCommand = new RelayCommand(PerformLogin);
            Users = new List<User>();
            userService = new UserService();
            this.connectivity = connectivity;
        }

        private async void PerformLogin()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Interner issue", "Check your internet and try again!", "OK");
                return;
            }
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Please enter both username and password.";
                return;
            }

            Users = await userService.GetUsers();
            // Check if there is a user with the provided username and password
            var user = Users.Find(u => u.UserName == Username && u.Password == Password);

            if (user != null)
            {
                // Set the current user using the Utility class
                Username = Password = string.Empty;
                Utility.CurrentUser = user;
                // Successful login, navigate to the MainPage
                await NavigateToMainPage();              
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }

        private async Task NavigateToMainPage()
        {
            await Shell.Current.GoToAsync($"//MainPage");
        }

        [RelayCommand]
        async Task NavigateToRegisterPage()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Interner issue", "Check your internet and try again!", "OK");
                return;
            }
            await Shell.Current.GoToAsync($"{nameof(UserRegisterPage)}", true);
        }
    }
}
