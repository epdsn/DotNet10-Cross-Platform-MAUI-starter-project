using MauiApp25.Services;

namespace MauiApp25.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly IAuthService _authService;

        public LoginPage(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            SignInButton.Clicked += SignInButton_Clicked;
        }

        private async void SignInButton_Clicked(object? sender, EventArgs e)
        {
            MessageLabel.Text = string.Empty;

            var username = UsernameEntry.Text ?? string.Empty;
            var password = PasswordEntry.Text ?? string.Empty;

            var success = await _authService.SignInAsync(username, password);
            if (success)
            {
                // Navigate to ProfilePage
                var profile = App.Services.GetService<ProfilePage>() ?? new ProfilePage(_authService);
                await Navigation.PushAsync(profile);
            }
            else
            {
                MessageLabel.Text = "Invalid credentials";
            }
        }
    }
}
