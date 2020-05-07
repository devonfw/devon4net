# The Open Application Standard Platform for .NET and .NET Core

- devon4net is oriented to implement backend solutions. Microservice architecture is well supported.
- Global configuration automated. devon4Net can be instantiated on any .net core application template with no effort like AWS templates for serverless applications
- Full and configurable support for HTTP2
- Number of minium libraries needed
- Modular clean Architecture layer
- Red button functionality (aka killswitch) to stop attending API request with custom error
- Global exception API error management via middleware with custom HTTP errors
-Support to only accept request from clients with a specific client certificate on Kestrel server.
- All components use IOptions pattern to be set up properly
- Swagger generation compatible con open api v3
- External configuration file for each environment
- .NET Core 3.1 working solution (Latest v3.1.3)
- Packages and solution templates published on nuget
- Full components customization by config file
- Docker ready (My Thai Star sample fully working on docker)
- Port specification by configuration
- Dependency injection by Microsoft .net Core
- Automapper support
- Entity framework ORM (Unit of work, async methods, Model context generation guide)
- Easy LiteDB support
- .NET Standard 2.1 library ready
- Multiplatform support: Windows, Linux, Mac ready
- Samples: My Thai Star backend, Google API integration, Azure login, AOP with Castle
- Documentation site
- Embedded SPA page support
- Easy RabbitMq support (command and handlers)

Included features:

- Logging:
              - Text File
              - Sqlite database support
              - Serilog Seq Server support
              - Graylog integration ready through TCP/UDP/HTTP protocols
              - API Call params interception (simple and compose objects)
              - API error exception management

- Swagger:
              - Swagger autogenerating client from comments and annotations on controller classes
              - Full swagger client customization (Version, Title, Description, Terms, License, Json end point definition)

- JWT:
              - Issuer, audience, token expiration customization by external file configuration
              - Token generation via certificate
              - MVC inherited classes to access JWT user properties
              - API method security access based on JWT Claims

- CORS:
              - Simple CORS definition ready
              - Multiple CORS domain origin definition with specific headers and verbs

- Headers:
              - Automatic header injection with middleware.
              - Supported header definitions: AccessControlExposeHeader, StrictTransportSecurityHeader, XFrameOptionsHeader, XssProtectionHeader, XContentTypeOptionsHeader, ContentSecurityPolicyHeader, PermittedCrossDomainPoliciesHeader, ReferrerPolicyHeader

- Reporting server:
              - Partial implementation of reporting server based on My-FyiReporting (now runs on linux container)

- Testing:
              - Integration test template with sqlite support
              - Unit test template
              - Moq, xunit frameworks integrated

- Circuit breaker:
              - Integrated with HttpClient factory
              - Client Certificate customization
              - Number of retries customizables

- LiteDb:
            - Support for LiteDB
            - Provided basic repository for CRUD operations

- RabbitMq:
            - Use of EasyQNet library to perform CQRS main functions
            - Send commands
            - Handled received commands to subscribed messages
            - Automatic messaging backup when sent and handled (Internal database via LiteDB and database backup via Entity Framework)
