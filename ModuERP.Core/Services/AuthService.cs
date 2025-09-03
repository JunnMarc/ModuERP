using Microsoft.EntityFrameworkCore;
using ModuERP.Core.Interfaces;
using ModuERP.Data.Common.Db;
using ModuERP.Data.Common.Entities;
using System.Security.Cryptography;
using System.Text;

namespace ModuERP.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly ModuERPDbContext _db;
        private readonly ISessionStorage _storage;

        public AuthService(ModuERPDbContext db, ISessionStorage storage)
        {
            _db = db;
            _storage = storage;

            CurrentUser = _storage.Get("current_user");
            IsAuthenticated = !string.IsNullOrEmpty(CurrentUser);
        }

        public bool IsAuthenticated { get; private set; }
        public string? CurrentUser { get; private set; }
        public event Action? OnAuthStateChanged;

        public async Task<bool> RegisterAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 4)
                return false;

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return false;

            if (await _db.Users.AnyAsync(u => u.Username == username))
                return false;

            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Role = "User"
            };

            try
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var hash = HashPassword(password);
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hash);

            if (user != null)
            {
                IsAuthenticated = true;
                CurrentUser = user.Username;
                _storage.Set("current_user", user.Username);
                OnAuthStateChanged?.Invoke();
                return true;
            }

            IsAuthenticated = false;
            CurrentUser = null;
            _storage.Remove("current_user");
            OnAuthStateChanged?.Invoke();
            return false;
        }

        public void Logout()
        {
            IsAuthenticated = false;
            CurrentUser = null;
            _storage.Remove("current_user");
            OnAuthStateChanged?.Invoke();
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
