namespace MauiApp25.Services
{
    public class AuthService : IAuthService
    {
        private readonly Dictionary<string, string> _users = new();
        private string? _currentUsername;

        public bool IsSignedIn => _currentUsername != null;

        public string? CurrentUsername => _currentUsername;

        public event Action? AuthStateChanged;

        public Task<bool> SignInAsync(string username, string password)
        {
            var ok = _users.TryGetValue(username, out var stored) && stored == password;
            _currentUsername = ok ? username : null;
            NotifyStateChanged();
            return Task.FromResult(ok);
        }

        public Task<bool> SignUpAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || _users.ContainsKey(username))
                return Task.FromResult(false);

            _users[username] = password;
            _currentUsername = username;
            NotifyStateChanged();
            return Task.FromResult(true);
        }

        public Task SignOutAsync()
        {
            _currentUsername = null;
            NotifyStateChanged();
            return Task.CompletedTask;
        }

        public void SeedTestUser(string username = "testuser", string password = "password")
        {
            _users[username] = password;
        }

        private void NotifyStateChanged() => AuthStateChanged?.Invoke();
    }
}
