using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertAuth.WebApiServer.Controllers;

[ApiController]
[Route("api")]
[RequireHttps]
public class SecureController : ControllerBase
{
    private readonly ILogger<SecureController> _logger;
    
    public SecureController(ILogger<SecureController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    [Route("Secure")]
    public async Task<IActionResult> GetSecure()
    {
        var clientCert = await HttpContext.Connection.GetClientCertificateAsync();
        if (clientCert is not null)
        {
            _logger.LogInformation("Secure request with certificate {subject}", clientCert.Subject);
        } else
        {
            _logger.LogWarning("No certificate");
        }
        return Ok("Secure");
    }

    [HttpGet]
    [Route("Insecure")]
    public async Task<IActionResult> GetInsecure()
    {
        var clientCert = await HttpContext.Connection.GetClientCertificateAsync();
        if (clientCert is not null)
        {
            _logger.LogInformation("Client certificate was sent but wasn't required {subject}", clientCert.Subject);
        } else
        {
            _logger.LogInformation("Request without client certificate is allowed");
        }
        return Ok("Insecure");
    }
}