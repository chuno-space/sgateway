```CSharp
var builder = WebApplication.CreateBuilder(args);

// Keep LettuceEncrypt for automatic certificate management
builder.Services.AddLettuceEncrypt();

// Load the manually installed certificate from a file or certificate store
var certificate = new X509Certificate2("path-to-your-certificate.pfx", "pfx-password");

// Configure Kestrel to handle multiple domains with different certificates
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(443, listenOptions =>
    {
        // Use LettuceEncrypt for automatic certificate handling (other domains)
        listenOptions.UseHttps();

        // Add manual certificate for a specific domain (e.g., mytrustedomain.com)
        listenOptions.UseHttps(httpsOptions =>
        {
            httpsOptions.ServerCertificateSelector = (context, name) =>
            {
                if (name == "mytrustedomain.com")
                {
                    return certificate; // Use your custom certificate for this domain
                }
                return null; // For other domains, LettuceEncrypt will take over
            };
        });
    });
});

var app = builder.Build();
```