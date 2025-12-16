# SAML Metadata Validation Client Samples

This console application provides samples for validating SAML metadata using the Provisioning Service API.

## Features

The application demonstrates how to call two metadata validation endpoints:

### 1. BrugervendteSystem (Service Provider) Metadata Validation
- **Endpoint**: `POST /Validation/BrugervendteSystemSamlMetadata`

### 2. Identity Provider Metadata Validation
- **Endpoint**: `POST /Validation/IdentityProviderSamlMetadata`

## Prerequisites

- .NET 10.0 SDK
- Access to the Provisioning Service API
- Client certificate for authentication (if required by the API)

## Configuration

All configuration is now handled via `appsettings.json`:

```json
{
  "BaseUrl": "https://your-api-base-url",
  "BrugervendteSystem": {
    "XmlBase64": "<base64-encoded-xml-for-brugervendte-system>",
    "MetadataUrl": "https://example.com/brugervendte-system-metadata.xml"
  },
  "IdentityProvider": {
    "XmlBase64": "<base64-encoded-xml-for-identity-provider>",
    "MetadataUrl": "https://example.com/identity-provider-metadata.xml"
  },
  "ClientCertificate": {
    "Path": "<path-to-pfx-or-cert-file>",
    "Password": "<certificate-password>"
  }
}
```

- Replace `BaseUrl` with your Provisioning Service API base URL.
- Encode your SAML metadata XML to Base64 and paste into `XmlBase64` or provide metadata URLs - Empty value will be ignored.
- Set your client certificate path and password for authentication (if required by the Provisioning service).

### Encoding XML to Base64
You can use C# or any tool to encode your XML:

```csharp
string xml = File.ReadAllText("your-metadata.xml");
string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
```

## Usage

1. Build and run the application:
   ```bash
   dotnet run
   ```

2. The application will automatically:
   - Validate BrugervendteSystem with XML
   - Validate BrugervendteSystem with MetadataUrl
   - Validate IdentityProvider with XML
   - Validate IdentityProvider with MetadataUrl

3. View the validation results in the console:
   - **Success**: Displays the Entity ID
   - **Failure**: Displays error code, message, and detailed error list

## Sample Responses

### Successful Validation (200 OK)
```json
{
  "Id": "https://saml.n2adgangsstyring.eksterntest-stoettesystemerne.dk/runtime"
}
```

### Validation Error (400 Bad Request)
```json
{
  "Code": 400,
  "Message": "https://saml.n2adgangsstyring.eksterntest-stoettesystemerne.dk/runtime",
  "ErrorDetails": [
    "Signature missing: SAML metadata must include a valid XML digital signature.",
    "IDPSSODescriptor must have a SingleLogoutService endpoint using the HTTP-Redirect binding."
  ],
  "Version": "2.0"
}
```

### Service Unavailable (503)
```json
{
  "Code": 503,
  "Message": "Metadata validation failed.",
  "ErrorDetails": [
    "The metadata specified at https://example.com/metadata is either not available or not accessible. Error details: The remote server returned an error: (401) Unauthorized."
  ],
  "Version": "2.0"
}
```

## Request Format

Both endpoints accept the following request body:

```json
{
  "SamlMetadata": {
    "Xml": "base64-encoded-xml-string",
    "MetadataUrl": "https://example.com/metadata"
  }
}
```

**Note**: Provide either `Xml` (Base64-encoded) or `MetadataUrl`, not both.

## Authentication

The API requires client certificate authentication. Ensure you have the appropriate certificate configured in your `appsettings.json`.

## Error Handling

The application handles the following scenarios:
- Network errors (HttpRequestException)
- HTTP error responses (400, 503, etc.)
- Invalid input
- JSON deserialization errors
