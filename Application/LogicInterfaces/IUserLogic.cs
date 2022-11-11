using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<UserCreationDTO> CreateUser(User user);
    public Task<UserCreationDTO> LogIn(String username, String password);
}