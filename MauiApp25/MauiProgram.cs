using Microsoft.Extensions.Logging;

namespace MauiApp25
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            // Register pages and services for the new login system
            // Use a factory to seed a test user at registration time so the singleton always contains the test user.
            builder.Services.AddSingleton<Services.IAuthService>(sp =>
            {
                var auth = new Services.AuthService();
                auth.SeedTestUser("testuser", "password");
                return auth;
            });

            builder.Services.AddSingleton<Components.Services.AuthInterop>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<Views.LoginPage>();
            builder.Services.AddTransient<Views.ProfilePage>();

#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
			builder.Logging.AddDebug();
#endif
            builder.Logging.AddDebug();

            var app = builder.Build();

            // NOTE: Setting up a message handler between the BlazorWebView and native host can be done
            // via platform-specific handlers or by wiring up in page code-behind. We'll wire in MainPage code-behind.

            return app;
        }
    }
}
