using System;
using System.Threading.Tasks;

namespace ModuERP.Core.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(string username, string password); // <-- required
        void Logout();
        event Action OnAuthStateChanged;
        bool IsAuthenticated { get; }
        string? CurrentUser { get; }
    }
}
