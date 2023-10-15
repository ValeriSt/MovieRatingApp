using CommunityToolkit.Mvvm.Input;
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
    public partial class UserRegisterViewModel : BaseViewModel
    {
        UserService _userService;

        private string username;
        private string password;
        private string errorMessage;

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

        public ICommand RegisterCommand { get; }

        public UserRegisterViewModel(UserService userService)
        {
            RegisterCommand = new RelayCommand(PerformRegistration);
            _userService = userService;
        }

        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.Navigation.PopAsync(true); //GoToAsync($"//MainPage");
        }

        private async void PerformRegistration()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Please enter both username and password.";
                return;
            }

            // Check if the username is already taken
            List<User> userList = await _userService.GetUsers();

            if (userList.Exists(u => u.UserName == Username))
            {
                ErrorMessage = "Username already in use. Please choose a different one.";
                return;
            }

            // Add the new user
            _userService.AddUser(Username, Password);

            // Optionally, you can navigate to the login page after registration is successful.
            // Here, we assume there's a LoginPage in your Shell structure with the route "//LoginPage".
            await Shell.Current.GoToAsync("//UserLoginPage");
        }
    }
}
