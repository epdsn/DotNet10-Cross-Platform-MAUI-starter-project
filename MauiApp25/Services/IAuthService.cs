namespace MauiApp25.Services
{
    public interface IAuthService
    {
        Task<bool> SignInAsync(string username, string password);
        Task<bool> SignUpAsync(string username, string password);
        Task SignOutAsync();
        bool IsSignedIn { get; }
        string? CurrentUsername { get; }

        // Event raised when auth state changes (sign in / sign out)
        event Action? AuthStateChanged;
    }
}
