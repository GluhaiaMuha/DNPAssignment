using Shared.DTOs;
using Shared.Models;

namespace HTTPClients.ClientInterfaces;

public interface IPostsService
{
    Task<Post> CreateAsync(PostCreationDTO dto);
    Task<Post> GetPostByIdAsync(int id);
    Task<IEnumerable<Post>> GetAllPostsAsync();
    
    Task<IEnumerable<Post>> GetAsync();
    
    
    

}