using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace CertAuth.WebApiClient;

public class WebApiClientOptions
{
    public string ApiBaseAddress { get; set; } = string.Empty;
    public string CertificatePath { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string SecureEndpoint { get; set; } = string.Empty;
}

public class WebApiClientOptionsSetup : IConfigureOptions<WebApiClientOptions>
{
    private const string SectionName = "WebApiClientOptions";
    private readonly IConfiguration _configuration;

    public WebApiClientOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(WebApiClientOptions options)
    {
        _configuration
            .GetSection(SectionName)
            .Bind(options);
    }
}
