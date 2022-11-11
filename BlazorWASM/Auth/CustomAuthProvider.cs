using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWASM.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{

    private readonly IAuthService service;
    
    public CustomAuthProvider(IAuthService service)
    {
        this.service = service;
        service.OnAuthStateChanged += AuthStateChanged;   
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await service.GetAuthAsync();
                
        return new AuthenticationState(principal);
    }
    
    private void AuthStateChanged(ClaimsPrincipal principal)
    {
         NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
     }
}