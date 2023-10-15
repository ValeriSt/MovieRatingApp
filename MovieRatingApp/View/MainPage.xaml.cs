using MovieRatingApp.ViewModel;

namespace MovieRatingApp.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MovieViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }
}