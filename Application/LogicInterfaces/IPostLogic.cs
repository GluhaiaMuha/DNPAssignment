using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreatePostAsync(PostCreationDTO dto);
    Task<Post?> GetPostByIdAsync(int id);
    Task<IEnumerable<Post>> GetAllPosts();
}