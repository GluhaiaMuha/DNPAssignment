using System.Net.Http.Json;
using System.Text.Json;
using HTTPClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HTTPClients.Implementation;

public class PostsHttpClient : IPostsService
{
    private readonly HttpClient client;

    public PostsHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Post> CreatePostAsync(PostCreationDTO dto)
    {

        HttpResponseMessage response = await client.PostAsJsonAsync("/posts", dto);
        string result = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        var post = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return post;
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/posts/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return post;
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/posts");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Post> posts = JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<IEnumerable<Post>> GetAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/posts");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        var post = JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return post;
    }
}