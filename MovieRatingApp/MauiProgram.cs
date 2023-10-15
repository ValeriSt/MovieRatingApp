using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MovieRatingApp.Services;
using MovieRatingApp.View;
using MovieRatingApp.ViewModel;

namespace MovieRatingApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIcons");
                });

            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);


            builder.Services.AddSingleton<MovieService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<UserRatingService>();

            builder.Services.AddSingleton<MovieViewModel>();
            builder.Services.AddTransient<DetailsViewModel>();
            builder.Services.AddSingleton<UserLoginViewModel>();
            builder.Services.AddSingleton<UserRegisterViewModel>();
            builder.Services.AddSingleton<UserRatingViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<DetailsPage>();
            builder.Services.AddSingleton<UserLoginPage>();
            builder.Services.AddSingleton<UserRegisterPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}