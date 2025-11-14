using Microsoft.AspNetCore.Components.WebView.Maui;
using MauiApp25.Models;

namespace MauiApp25
{
    public partial class MainPage : ContentPage
    {
        private readonly Services.IAuthService _authService;
        private readonly Services.NetworkScanner _scanner;

        public MainPage(Services.NetworkScanner scanner)
        {
            InitializeComponent();

            _authService = App.Services.GetService<Services.IAuthService>()!;
            _scanner = scanner;

            // Wire up navigation buttons
            var loginButton = this.FindByName<Button>("LoginButton");
            var signupButton = this.FindByName<Button>("SignupButton");
            var logoutButton = this.FindByName<Button>("LogoutButton");
            var scanButton = this.FindByName<Button>("ScanButton");

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

            if (scanButton != null)
            {
                scanButton.Clicked += async (s, e) => await ScanNetwork();
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

        private async Task ScanNetwork()
        {
            int.TryParse(PortEntry.Text, out var port);
            PortEntry.IsEnabled = false;
            ScanButton.IsEnabled = false;

            try
            {
                var devices = await _scanner.ScanLocalSubnetAsync(port == 0 ? 22 : port, 200);
                DevicesList.ItemsSource = devices;
            }
            finally
            {
                PortEntry.IsEnabled = true;
                ScanButton.IsEnabled = true;
            }
        }

        private async void ConnectButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is MauiApp25.Models.DeviceInfo info)
            {
                // For demo show a simple alert and push a placeholder page
                await DisplayAlert("Connect", $"Connecting to {info.IpAddress}:{info.Port}", "OK");
                await Navigation.PushAsync(new ContentPage { Content = new Label { Text = $"Remote session to {info.IpAddress}" } });
            }
        }
    }
}
