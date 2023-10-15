using MovieRatingApp.ViewModel;

namespace MovieRatingApp.View;

public partial class UserRegisterPage : ContentPage
{
	public UserRegisterPage(UserRegisterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}