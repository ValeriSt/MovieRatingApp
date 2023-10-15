using MovieRatingApp.ViewModel;
namespace MovieRatingApp.View;

public partial class UserLoginPage : ContentPage
{
	public UserLoginPage(UserLoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}