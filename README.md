# KOMBIT
KOMBIT Støttesystemer

# Project Test Environment

The sample applications are externally available in the Project Test Environment.

## Test Services

### Context Handler
[https://adgangsstyring.projekt-stoettesystemerne.dk/CHTestSigningService](https://adgangsstyring.projekt-stoettesystemerne.dk/CHTestSigningService)

### Security Token Service

#### STS Test Stub

The STS test stub simulates processing requests and sending responses for a WS-Trust call which a user system (Anvendersystem) can send. The STS Test Stub will perform syntax and security validation of the received token request, and return a proper response.

The main URL of the STS test stub is:

[https://adgangsstyring.projekt-stoettesystemerne.dk/STS](https://adgangsstyring.projekt-stoettesystemerne.dk/STS)

WSDL for the STS test stub can be downloaded here:

[https://adgangsstyring.projekt-stoettesystemerne.dk/STS/kombit/sts/mex?wsdl](https://adgangsstyring.projekt-stoettesystemerne.dk/STS/kombit/sts/mex?wsdl)

#### STS Test Signing Service

The STS test signing service can be called with a previously issued SAML assertion as input, and will reply with an updated version of the SAML assertion in which the following elements are updated:

- Id
- Timestamp
- Signature

The STS test signing service is available here:

[https://adgangsstyring.projekt-stoettesystemerne.dk/STSTestSigningService](https://adgangsstyring.projekt-stoettesystemerne.dk/STSTestSigningService)

## Sample Applications

### .Net Sample Applications

[https://adgangsstyring.projekt-stoettesystemerne.dk/Consumer](https://adgangsstyring.projekt-stoettesystemerne.dk/Consumer)

### Java STS Sample Applications

[https://adgangsstyring.projekt-stoettesystemerne.dk/Consumer/ServiceConsumer](https://adgangsstyring.projekt-stoettesystemerne.dk/Consumer/ServiceConsumer)


