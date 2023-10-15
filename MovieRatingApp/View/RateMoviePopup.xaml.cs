using CommunityToolkit.Maui.Views;
using MovieRatingApp.Model;
using MovieRatingApp.ViewModel;

namespace MovieRatingApp.View;

public partial class RateMoviePopup : Popup
{
    public RateMoviePopup(UserRatingViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
    private void OnSaveClicked(object sender, EventArgs e)
    {
        Close(); // Close the pop-up
    }
    private void OnCloseClicked(object sender, EventArgs e)
    {
        Close(); // Close the pop-up
    }

}