# KOMBIT
KOMBIT Støttesystemer

# Project Test Environment

The sample applications are externally available in the Project Test Environment.

## Test Services

### Context Handler
[https://adgangsstyringeksempler.test-stoettesystemerne.dk/CHTestSigningService](https://adgangsstyringeksempler.test-stoettesystemerne.dk/CHTestSigningService)

### Security Token Service

#### STS Test Stub

The STS test stub simulates processing requests and sending responses for a WS-Trust call which a user system (Anvendersystem) can send. The STS Test Stub will perform syntax and security validation of the received token request, and return a proper response.

The main URL of the STS test stub is:

[https://adgangsstyringeksempler.test-stoettesystemerne.dk/STS](https://adgangsstyringeksempler.test-stoettesystemerne.dk/STS)

WSDL for the STS test stub can be downloaded here:

[https://adgangsstyringeksempler.test-stoettesystemerne.dk/STS/kombit/sts/mex?wsdl](https://adgangsstyringeksempler.test-stoettesystemerne.dk/STS/kombit/sts/mex?wsdl)

#### STS Test Signing Service

The STS test signing service is available here:

[https://adgangsstyringeksempler.test-stoettesystemerne.dk/STSTestSigningService](https://adgangsstyringeksempler.test-stoettesystemerne.dk/STSTestSigningService)

The STS test signing service can be called with a previously issued SAML assertion as input, and will reply with an updated version of the SAML assertion in which the following elements are updated:

- Id
- Timestamp
- Signature

## Sample Applications

### .Net Sample Applications

[https://adgangsstyringeksempler.test-stoettesystemerne.dk/Consumer](https://adgangsstyringeksempler.test-stoettesystemerne.dk/Consumer)

### Java STS Sample Applications

[https://adgangsstyringeksempler.test-stoettesystemerne.dk/Consumer/ServiceConsumer](https://adgangsstyringeksempler.test-stoettesystemerne.dk/Consumer/ServiceConsumer)


