using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace RandomStuffFrontEnd.Components.Services
{
    public class UserSession
    {
        private readonly ProtectedSessionStorage _storage;

        public bool IsLoggedIn { get; private set; }
        public long UserId { get; private set; }

        public UserSession(ProtectedSessionStorage storage)
        {
            _storage = storage;
        }

        public async Task Login(long userId)
        {
            IsLoggedIn = true;
            UserId = userId;
            await _storage.SetAsync("userId", userId);
        }

        public async Task Logout()
        {
            IsLoggedIn = false;
            UserId = 0;
            await _storage.DeleteAsync("userId");
        }

        public async Task LoadSessionUserIdAsync()
        {
            var result = await _storage.GetAsync<long>("userId");
            if (result.Success)
            {
                UserId = result.Value;
                IsLoggedIn = true;
            }
        }
    }
}
