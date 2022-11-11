namespace Shared.DTOs;

public class UserLoginDTO
{
    public String userName { get; }
    public String password { get; }

    public UserLoginDTO(String userName, String password)
    {
        this.userName = userName;
        this.password = password;
    }
}