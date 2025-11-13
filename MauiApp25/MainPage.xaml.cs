using Microsoft.AspNetCore.Components.WebView.Maui;

namespace MauiApp25
{
    public partial class MainPage : ContentPage
    {
        private readonly Services.IAuthService _authService;

        public MainPage()
        {
            InitializeComponent();

            _authService = App.Services.GetService<Services.IAuthService>()!;

            // Wire up navigation buttons
            var loginButton = this.FindByName<Button>("LoginButton");
            var signupButton = this.FindByName<Button>("SignupButton");
            var logoutButton = this.FindByName<Button>("LogoutButton");

            if (loginButton != null)
                loginButton.Clicked += async (s, e) => await Navigation.PushAsync(App.Services.GetService<Views.LoginPage>()!);

            if (signupButton != null)
                signupButton.Clicked += async (s, e) => {
                    var page = App.Services.GetService<Views.LoginPage>()!;
                    await Navigation.PushAsync(page);
                };

            if (logoutButton != null)
            {
                logoutButton.Clicked += async (s, e) =>
                {
                    await _authService.SignOutAsync();
                };
            }

            // Subscribe to auth state changes to update button visibility
            _authService.AuthStateChanged += OnAuthStateChanged;
            UpdateButtons();
        }

        private void OnAuthStateChanged()
        {
            MainThread.BeginInvokeOnMainThread(UpdateButtons);
        }

        private void UpdateButtons()
        {
            var loginButton = this.FindByName<Button>("LoginButton");
            var signupButton = this.FindByName<Button>("SignupButton");
            var logoutButton = this.FindByName<Button>("LogoutButton");

            var isSignedIn = _authService.IsSignedIn;
            if (loginButton != null) loginButton.IsVisible = !isSignedIn;
            if (signupButton != null) signupButton.IsVisible = !isSignedIn;
            if (logoutButton != null) logoutButton.IsVisible = isSignedIn;
        }
    }
}
