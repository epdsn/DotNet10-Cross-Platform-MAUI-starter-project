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

            // Use MainPage as the startup page. Wrap in NavigationPage for navigation.
            var mainPage = serviceProvider.GetService<MainPage>() ?? new MainPage();
            MainPage = new NavigationPage(mainPage) { Title = "MauiApp25" };
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(MainPage) { Title = "MauiApp25" };
        }
    }
}
