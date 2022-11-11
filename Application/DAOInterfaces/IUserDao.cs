using Shared.DTOs;
using Shared.Models;

namespace Application.DAOInterfaces;

public interface IUserDao
{
    public Task<UserCreationDTO> CreateUser(User user);
    public Task<User?> GetByUsername(string username);
}