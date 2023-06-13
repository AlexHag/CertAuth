using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace CertAuth.WebApiClient;

public class Program
{
    public static async Task Main(string[] args)
    {
        X509Certificate2 clientCertificate = new X509Certificate2("Certificates/cert.pfx");
        var handler = new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls12,
            ServerCertificateCustomValidationCallback = ValidateCertificate
        };
        handler.ClientCertificates.Add(clientCertificate);

        var client = new HttpClient(handler);

        var result = await client.GetAsync("https://localhost:5001/api/secure");
        var response = await result.Content.ReadAsStringAsync();
        Console.WriteLine(response);
    }

    // Implement certificate validation logic here
    static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        Console.WriteLine($"Effective date: {certificate?.GetEffectiveDateString()}");
        Console.WriteLine($"Exp date: {certificate?.GetExpirationDateString()}");
        Console.WriteLine($"Issuer: {certificate?.Issuer}");
        Console.WriteLine($"Subject: {certificate?.Subject}");
        return true;
    }
}