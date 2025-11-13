using Microsoft.AspNetCore.Components.WebView.Maui;

namespace MauiApp25
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Wire up navigation buttons
            var loginButton = this.FindByName<Button>("LoginButton");
            var signupButton = this.FindByName<Button>("SignupButton");

            if (loginButton != null)
                loginButton.Clicked += async (s, e) => await Navigation.PushAsync(App.Services.GetService<Views.LoginPage>()!);

            if (signupButton != null)
                signupButton.Clicked += async (s, e) => {
                    var page = App.Services.GetService<Views.LoginPage>()!;
                    await Navigation.PushAsync(page);
                };
        }
    }
}
