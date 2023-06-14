using Microsoft.Extensions.Options;
using RestSharp;

namespace CertAuth.WebApiClient;

public class Client
{
    private readonly RestClient _client;
    private readonly IOptions<WebApiClientOptions> _options;
    public Client(
        RestClient client,
        IOptions<WebApiClientOptions> options)
    {
        _client = client;
        _options = options;
    }

    public async Task Execute()
    {
        var restRequest = new RestRequest(_options.Value.SecureEndpoint, Method.Get);
        var response = await _client.ExecuteAsync(restRequest);
        
        if (!response.IsSuccessful) 
        {
            Console.WriteLine($"HTTPS request failed; Status Code: {response.StatusCode}, Content: {response.Content};");
            throw new Exception();
        }
        
        Console.WriteLine(response.Content);
    }
}
