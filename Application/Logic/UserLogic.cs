using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private IUserDao dao;

    public UserLogic(IUserDao dao)
    {
        this.dao = dao;
    }

    public async Task<UserCreationDTO> CreateUser(User user)
    {
        User? existing = await dao.GetByUsername(user.userName);

        if (existing != null)
        {
            throw new Exception($" Username: {user.userName} already exists");
        }
        
        ValidateData(user);
         
        return await dao.CreateUser(user);
    }

    public async Task<UserCreationDTO> LogIn(String username, String password)
    {
        User? user = await dao.GetByUsername(username);
        if (user == null)
        {
            throw new Exception($"The user {username} does not exist");
        }

        if (!user.password.Equals(password))
        {
            throw new Exception("Incorrect password");
        }

        UserCreationDTO userToSendDto = new UserCreationDTO(user.userName);

        return userToSendDto;
    }
    
    private static void ValidateData(User user)
    {
        String username = user.userName;

        if (username.Length > 20 || username.Length < 5)
        {
            throw new Exception("Username must have more than 5 characters and less than 21");
        }
    }
}