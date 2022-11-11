namespace Shared.DTOs;

public class UserCreationDTO
{
    public String userName { get; }

    public UserCreationDTO(String userName)
    {
        this.userName = userName;
    }
}