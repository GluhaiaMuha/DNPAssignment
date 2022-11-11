using Shared.DTOs;
using Shared.Models;

namespace HTTPClients.ClientInterfaces;

public interface IUserService
{
    public Task<String> LoginAsync(UserLoginDTO dto);
    public Task RegisterAsync(User user);
}