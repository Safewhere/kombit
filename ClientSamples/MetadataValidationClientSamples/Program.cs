using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace MetadataValidationClientSamples;

class Program
{
    private static HttpClient httpClient;
    private static IConfigurationRoot config;

    static async Task Main(string[] args)
    {
        // Load configuration
        config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string baseUrl = config["BaseUrl"] ?? "";
        var brugervendteSystemXml = config["BrugervendteSystem:XmlBase64"] ?? "";
        var brugervendteSystemUrl = config["BrugervendteSystem:MetadataUrl"] ?? "";
        var identityProviderXml = config["IdentityProvider:XmlBase64"] ?? "";
        var identityProviderUrl = config["IdentityProvider:MetadataUrl"] ?? "";
        var certPath = config["ClientCertificate:Path"] ?? "";
        var certPassword = config["ClientCertificate:Password"] ?? "";

        // Setup HttpClient with client certificate if configured
        var handler = new HttpClientHandler();
        if (!string.IsNullOrWhiteSpace(certPath))
        {
            try
            {
                var cert = X509CertificateLoader.LoadPkcs12FromFile(certPath, certPassword);
                handler.ClientCertificates.Add(cert);
                Console.WriteLine($"Loaded client certificate: {cert.Subject}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load client certificate: {ex.Message}");
            }
        }

        httpClient = new HttpClient(handler);

        Console.WriteLine("SAML Metadata Validation Client Samples");
        Console.WriteLine("========================================\n");

        // 1. BrugervendteSystem with Xml
        if (!string.IsNullOrEmpty(brugervendteSystemXml))
        {
            await ValidateBrugervendteSystemMetadata(baseUrl, brugervendteSystemXml, "");
        }

        // 2. BrugervendteSystem with MetadataUrl
        if (!string.IsNullOrEmpty(brugervendteSystemUrl))
        {
            await ValidateBrugervendteSystemMetadata(baseUrl, "", brugervendteSystemUrl);
        }

        // 3. IdentityProvider with Xml
        if (!string.IsNullOrEmpty(identityProviderXml))
        {
            await ValidateIdentityProviderMetadata(baseUrl, identityProviderXml, "");
        }

        // 4. IdentityProvider with MetadataUrl
        if (!string.IsNullOrEmpty(identityProviderUrl))
        {
            await ValidateIdentityProviderMetadata(baseUrl, "", identityProviderUrl);
        }
    }

    static async Task ValidateBrugervendteSystemMetadata(string baseUrl, string xmlBase64, string metadataUrl)
    {
        Console.WriteLine("\n=== Validate BrugervendteSystem SAML Metadata ===");
        Console.WriteLine($"Input: XmlBase64: {(!string.IsNullOrEmpty(xmlBase64))}, MetadataUrl: {(!string.IsNullOrEmpty(metadataUrl))}");

        var request = new
        {
            SamlMetadata = new
            {
                Xml = xmlBase64,
                MetadataUrl = metadataUrl
            }
        };

        try
        {
            Console.WriteLine("\nSending validation request...");
            var response = await httpClient.PostAsJsonAsync(
                $"{baseUrl}/Validation/BrugervendteSystemSamlMetadata",
                request
            );
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\n✓ Validation successful!");
                var result = JsonSerializer.Deserialize<ValidationResult>(responseContent);
                Console.WriteLine($"Entity ID: {result?.Id}");
            }
            else
            {
                Console.WriteLine($"\n✗ Validation failed (Status: {response.StatusCode})");
                var errorResult = JsonSerializer.Deserialize<ErrorResult>(responseContent);
                if (errorResult != null)
                {
                    Console.WriteLine($"Code: {errorResult.Code}");
                    Console.WriteLine($"Message: {errorResult.Message}");
                    if (errorResult.ErrorDetails != null && errorResult.ErrorDetails.Length > 0)
                    {
                        Console.WriteLine("\nError Details:");
                        foreach (var detail in errorResult.ErrorDetails)
                        {
                            Console.WriteLine($"  - {detail}");
                        }
                    }
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"\n✗ Request failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ An error occurred: {ex.Message}");
        }
    }

    static async Task ValidateIdentityProviderMetadata(string baseUrl, string xmlBase64, string metadataUrl)
    {
        Console.WriteLine("\n=== Validate Identity Provider SAML Metadata ===");
        Console.WriteLine($"Input: XmlBase64: {(!string.IsNullOrEmpty(xmlBase64))}, MetadataUrl: {(!string.IsNullOrEmpty(metadataUrl))}");

        var request = new
        {
            SamlMetadata = new
            {
                Xml = xmlBase64,
                MetadataUrl = metadataUrl
            }
        };

        try
        {
            Console.WriteLine("\nSending validation request...");
            var response = await httpClient.PostAsJsonAsync(
                $"{baseUrl}/Validation/IdentityProviderSamlMetadata",
                request
            );
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\n✓ Validation successful!");
                var result = JsonSerializer.Deserialize<ValidationResult>(responseContent);
                Console.WriteLine($"Entity ID: {result?.Id}");
            }
            else
            {
                Console.WriteLine($"\n✗ Validation failed (Status: {response.StatusCode})");
                var errorResult = JsonSerializer.Deserialize<ErrorResult>(responseContent);
                if (errorResult != null)
                {
                    Console.WriteLine($"Code: {errorResult.Code}");
                    Console.WriteLine($"Message: {errorResult.Message}");
                    if (errorResult.ErrorDetails != null && errorResult.ErrorDetails.Length > 0)
                    {
                        Console.WriteLine("\nError Details:");
                        foreach (var detail in errorResult.ErrorDetails)
                        {
                            Console.WriteLine($"  - {detail}");
                        }
                    }
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"\n✗ Request failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ An error occurred: {ex.Message}");
        }
    }
}

record ValidationResult
{
    public string? Id { get; set; }
}

record ErrorResult
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public string[]? ErrorDetails { get; set; }
    public string? Version { get; set; }
}
