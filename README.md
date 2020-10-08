# auth-service
An Auth Service using Azure Function with Clean Architecture.

We build this system as a part of our microservices system. In this project, we handle the auth service domain:
- Sign Up (by email, phone)
- Sign In (OAuth 2.0 with JWT)
- Change Password

## Clean Architecture
Referring to uncle Bob’s Clean Architecture, we're using these layer to build our Auth Service.

### Domain Layer
This layer contains all entities, business logic, interfaces and exceptions. This layer should not depent on any technology. All other layer point on this layer, and use these entities to communicate with each other.

### Application Layer
This layer will handle all of the usecases of the system. This layer will be called by the delivery/input layer, processing the usecases, call repository layer to store the data if needed, and provide data to serve into delivery.

### Repository Layer
We devide this layer to:
- Persistence Project to CRUD data to database.
- External Services Project to CRUD data to another microservices.
- Event Publisher Project to publish event data, so that another microservices that need can use it.

### Delivery and Input Layer
In this layer, we create REST API Project to accept input from user, send the input to application layer for processing, and return the delivery data to user.
We also can add Event Handler Project to handle other microservices event, and send the data to application layer for processing. 

## Technologies
- Azure Function v3
- .NET Core 3.1
- MongoDB to store data
- Event Hub to publish events
- XUnit, FluentAssertions

## Project Structure
### src
- AuthSevice.Domain: This project is the Domain Layer
- AuthSevice.API: This project is a part of Delivery/Input Layer
- AuthSevice.Persistence: This project is a part of Repository Layer to save data to database
- AuthSevice.EventPublisher: This project is a part of Repository Layer to send new user event
- AuthSevice.AccessTokenHandler: This project is a library project to handle access token validation. For example, user send access token to change password, and we use this project to validate the access token. Also, you can publish this project as a NuGet package, and any other microservices project can use this NuGet package to handle access token validation.

### tests
- AuthService.Domain.UnitTests: Used to test the AuthSevice.Domain project
- AuthService.AccessTokenHandler.UnitTests: Used to test the AuthSevice.AccessTokenHandler project
- AuthService.Persistence.IntegrationTests: Used to integration test the AuthSevice.Persistence project.
- AuthService.API.IntegrationTests: Used to integration test AuthSevice.API project. We start the AuthSevice.API, send the http request, check the http response, and also check the database.

### generator
- AuthService.GeneratorUtil: Used to generate RSA private and public keys. This keys is used to signed the JWT.

