using Shared.Models;

namespace Shared.DTOs;

public class PostCreationDTO
{
    public String title { get; }
    public String body { get; }
    public String userName { get; }

    public PostCreationDTO(string title, string body, string userName)
    {
        this.title = title;
        this.body = body;
        this.userName = userName;
    }
}