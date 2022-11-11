using System.Security.Claims;
using Shared.Models;

namespace BlazorWASM.Auth;

public interface IAuthService
{
    public Task LoginAsync(String username, String password);
    public Task LogoutAsync();
    public Task RegisterAsync(User user);
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}