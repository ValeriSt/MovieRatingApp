using CommunityToolkit.Maui.Views;
using MovieRatingApp.ViewModel;
using MovieRatingApp.View;
using MovieRatingApp.Services;

namespace MovieRatingApp.View;

public partial class DetailsPage : ContentPage
{
    public DetailsPage(DetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}