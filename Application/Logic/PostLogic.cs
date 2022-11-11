using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private IPostDao postDao;
    private IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreatePostAsync(PostCreationDTO dto)
    {
        User? user = await userDao.GetByUsername(dto.userName);
        if (user == null)
        {
            throw new Exception($"User: {dto.userName} not found.");
        }
        
        Post post = new Post(user, dto.title, dto.body);
        
        ValidatePost(post);
        
        Post created = await postDao.CreatePostAsync(post);
        return created;
    }

    public async Task<Post?> GetPostByIdAsync(int id)
    {
        return await postDao.GetPostByIdAsync(id);
    }
    
    private static void ValidatePost(Post post)
    {
        if (string.IsNullOrEmpty(post.title))
        {
            throw new Exception("Posts can't have empty title!!!!");
        }

        if (string.IsNullOrEmpty(post.body))
        {
            throw new Exception("Posts can't have empty body!!!!");
        }
    }
}