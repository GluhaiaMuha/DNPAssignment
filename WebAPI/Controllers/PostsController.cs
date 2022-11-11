using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostLogic postLogic;

    public PostsController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePostAsync(PostCreationDTO dto)
    {
        try
        {
            Post post = await postLogic.CreatePostAsync(dto);
            return Created($"/posts/{post.postId}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Post>> GetPosts()
    {
        var posts = await postLogic.GetAllPosts();
        return Ok(posts);
    }
    

}