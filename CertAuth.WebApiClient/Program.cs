using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CertAuth.WebApiClient;

IServiceCollection services = new ServiceCollection();

services.AddSingleton<IConfiguration>(
    new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false)
        .Build());
    
services.AddOptions();
services.AddCertificateRestClient();

services.AddTransient<Client>();

var serviceProvider = services.BuildServiceProvider();
var client = serviceProvider.GetRequiredService<Client>();
await client.Execute();
