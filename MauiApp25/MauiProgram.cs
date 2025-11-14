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

            // Register services
            builder.Services.AddSingleton<Services.NetworkScanner>();
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

            return builder.Build();
        }
    }
}
