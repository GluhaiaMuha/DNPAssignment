using Application.DAOInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class PostsDaoImpl : IPostDao
{
    private readonly FileContext context;

    public PostsDaoImpl(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreatePostAsync(Post post)
    {
        int postId = 1;
        
        if (context.Posts.Any())
        {
            postId = context.Posts.Max(p => p.postId);
            postId++;
        }
        post.postId = postId;
        context.Posts.Add(post);
        context.SaveChange();
        return Task.FromResult(post);
    }

    public Task<Post?> GetPostByIdAsync(int id)
    {
        Post? post = context.Posts.FirstOrDefault(p => p.postId == id);
        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAllPosts()
    {

        IEnumerable<Post> posts = context.Posts;
        
        return Task.FromResult(posts);
    }
}