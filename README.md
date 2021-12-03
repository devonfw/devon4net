# The DEV-ON time Platform for the .NET stack

devon4net is a real Clean Architecture designed templates to build cloud native solutions (AWS Serverless/Lambda), microservices, WebAPI applications and protobuf applications. 

It is build to simplify and standarize the development life-cycle by means of the Microsoft's .Net stack. 

All functionalities are built to work as individual and configurable components (well-defined json settings). So, that means you can use them separately or even use all of them in a single application.

Some key-features are:
- Real modular clean Architecture layer
- Global configuration automated. devon4Net can be instantiated on any .net core application template with no effort like AWS templates for serverless applications
- Number of minium libraries. Every component can be used isolated via Nuget in any .NET application without the Weab API Template
- Full and configurable support for HTTP2
- Red button functionality (aka killswitch) to stop attending API request with custom error
- Global management via middleware with error management via custom HTTP errors, killswitch functionality, model state validation
- Support to only accept request from clients with a specific client certificate on Kestrel server.
- Cloud native. AWS templates integration. You can integrate devon4net in your serverless application in a easy way.
- All components use IOptions pattern to be set up properly
- Swagger generation compatible with the latest Open API version. 
- External configuration file for each environment
- .NET 6 working solution (Latest v6.0.0)
- Packages and solution templates published on nuget
- Full components customization by config file
- Docker ready (My Thai Star sample fully working on docker)
- Port specification by configuration
- Dependency injection by Microsoft .net Core
- Automapper support
- Entity framework ORM (Unit of work, async methods, Model context generation guide)
- Easy LiteDB support
- Multiplatform support provided by the framework: Windows, Linux and Mac ready
- Samples: My Thai Star backend, Google API integration, Azure login, AOP with Castle
- Documentation site
- Embedded SPA page support
- Easy RabbitMq support (command and event handlers)
- MediatR library for in memory messaging + event handling
- Microfocus SMAX support (component is not maintained, but you can use it as sample to work with)
- Easy class FluentValidation. Create your own rules to determine if an instance of a class is valid or not
- CyberArk integration (component is not maintained, but you can use it as sample to work with)
- Ansible Tower integration. devon4net integrates with Ansible Tower to setup your cloud servers in a easy way (component is not maintained, but you can use it as sample to work with)
- gRPC + Protobuf support
- Kafka: Message producer and consumer handlers. Create/Delete topics as well
- Extra secrets/settings file support
- Added ValidateAntiForgeryToken support via XSRF-TOKEN in cookie and header

Included features:
- Cloud Native AWS :
              - Serverless API project and Lambda Project with multifunction support able to manage secrets as configuration settings, S3, CDK...
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
              - Easy configuration with just one configuration node in your settings file

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
            - Use of EasyQNet library to perform CQRS main functions between different microservices
            - Send commands / Subscribe queues with one C# sentence
            - Events management: Handled received commands to subscribed messages
            - Automatic messaging backup when sent and handled (Internal database via LiteDB and database backup via Entity Framework)

- MediatR:
            - Use of MediatR library to perform CQRS main functions in memory
            - Send commands / Subscribe queues with one C# sentence
            - Events management: Handled received commands to subscribed messages
            - Automatic messaging backup when sent and handled (Internal database via LiteDB and database backup via Entity Framework)
- SmaxHcm:
            - Component to manage Microfocus SMAX for cloud infrastructure services management

- CyberArk:
            - Manage safe credentials with CyberArk

- AnsibleTower:
            - Ansible automates the cloud infrastructure. devon4net integrates with Ansible Tower via API consumption endpoints

- gRPC+Protobuf:
            - Added Client + Server basic templates sample gRPC with Google's Protobuf protocol using devon4net

- Kafka:
            - Added Apache Kafka support for deliver/consume messages and create/delete topics as well             
