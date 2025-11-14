namespace MauiApp25
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Store the service provider for app-wide access
            Services = serviceProvider;

            // Use MainPage as the startup page. Resolve via DI so constructor dependencies are injected.
            var mainPage = (MainPage)serviceProvider.GetService(typeof(MainPage)) ?? new MainPage(serviceProvider.GetService<Services.NetworkScanner>()!);
            MainPage = new NavigationPage(mainPage) { Title = "MauiApp25" };
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(MainPage) { Title = "MauiApp25" };
        }
    }
}
