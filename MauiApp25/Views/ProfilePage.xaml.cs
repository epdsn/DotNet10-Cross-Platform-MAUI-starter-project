using MauiApp25.Services;

namespace MauiApp25.Views
{
    public partial class ProfilePage : ContentPage
    {
        private readonly IAuthService _authService;

        public ProfilePage(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;

            SignOutButton.Clicked += async (s, e) =>
            {
                await _authService.SignOutAsync();
                // Navigate back to main page
                await Navigation.PopToRootAsync();
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UsernameLabel.Text = _authService.CurrentUsername ?? "User";
            StatusLabel.Text = _authService.IsSignedIn ? "Signed in" : "Signed out";
        }
    }
}
