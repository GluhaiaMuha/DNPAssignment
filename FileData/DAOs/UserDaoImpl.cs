using Application.DAOInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class UserDaoImpl : IUserDao
{
    private FileContext context;

    public UserDaoImpl(FileContext context)
    {
        this.context = context;
    }
    
    public Task<UserCreationDTO> CreateUser(User user)
    {
        context.Users.Add(user);
        context.SaveChange();
    
        UserCreationDTO userToSend = new UserCreationDTO(user.userName);
        return Task.FromResult(userToSend);
    }
    
    public Task<User?> GetByUsername(string username)
    {
        User? user = context.Users.FirstOrDefault(u => u.userName.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        return Task.FromResult(user);
    }
}