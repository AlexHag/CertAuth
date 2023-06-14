using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace CertAuth.WebApiClient;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCertificateRestClient(this IServiceCollection services)
    {
        services.ConfigureOptions<WebApiClientOptionsSetup>();
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<WebApiClientOptions>>().Value;

        var certificate = new X509Certificate2(options.CertificatePath, options.Password);
        var restClientOptions = new RestClientOptions(options.ApiBaseAddress)
        {
            ClientCertificates = new X509CertificateCollection { certificate },
            RemoteCertificateValidationCallback = ValidateCertificate
        };
        var restClient = new RestClient(restClientOptions);
        
        services.AddSingleton(restClient);
        return services;
    }

    public static bool ValidateCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors errors)
    {
        Console.WriteLine($"Effective date: {certificate?.GetEffectiveDateString()}");
        Console.WriteLine($"Exp date: {certificate?.GetExpirationDateString()}");
        Console.WriteLine($"Issuer: {certificate?.Issuer}");
        Console.WriteLine($"Subject: {certificate?.Subject}");
        return true;
    }
}