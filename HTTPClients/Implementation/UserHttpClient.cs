using System.Text;
using System.Text.Json;
using HTTPClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HTTPClients.Implementation;

public class UserHttpClient : IUserService
{
    private readonly HttpClient client;

    public UserHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<String> LoginAsync(UserLoginDTO dto)
    {
        String userAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("/users/login", content);
        String responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        return responseContent;
    }

    public async Task RegisterAsync(User user)
    {
        String userAsJson = JsonSerializer.Serialize(user);
        StringContent content = new StringContent(userAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync("/users/register", content);
        String responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(responseContent);
            throw new Exception(responseContent);
        }
    }
}
