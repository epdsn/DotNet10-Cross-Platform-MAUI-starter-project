using Microsoft.JSInterop;
using MauiApp25.Services;

namespace MauiApp25.Components.Services
{
    public class AuthInterop : IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IAuthService _authService;
        private DotNetObjectReference<AuthInterop>? _ref;

        public AuthInterop(IJSRuntime jsRuntime, IAuthService authService)
        {
            _jsRuntime = jsRuntime;
            _authService = authService;
            _authService.AuthStateChanged += AuthStateChangedHandler;
        }

        private void AuthStateChangedHandler()
        {
            // Notify Blazor UI of state change
            _ = NotifyJsAsync();
        }

        public async Task NotifyJsAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("__authStateChanged");
            }
            catch
            {
                // ignore JS errors when not available
            }
        }

        public ValueTask DisposeAsync()
        {
            _authService.AuthStateChanged -= AuthStateChangedHandler;
            _ref?.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
