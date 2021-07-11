
## Open
    -rental-api -> localhost:5001 (this will open swagger) or /api/health to ckeck if it works

# Solution Structure

## Core Layer
This layer contain domain objects and business logic. This layer is pure C# - no network connections, databases, etc. allowed

### Base.Project.Core.Application
This project contains our business logic

#### Folders

- **UseCases**
    - The use cases implementation. Use cases contain application specific business rules.

### Base.Project.Core.Domain
This project should have very few or no external dependencies. The primary purpose of this project is to define application wide elements (Enums, Constants, Models, etc). We should create interfaces in this layer and then implement those interfaces in the other layers (e.g. **Infrastructure**). 

#### Folders

- **Dtos**
    - Data transfer objects
- **Enums**
    - Enums
- **Events**
    - Custom events
- **Exceptions**
    - Custom exceptions
- **Interfaces**
    - This folder will have all the interfaces that will be implemented in all other layers. Here we can create sub-folders (e.g. **Interfaces\Repositories**)
- **Entities**
    - Domain Entities. These entities are the business objects of the application

    
## Infrastructure
Frameworks usage, requirements to interact with external sources or technology specific stuff are implemented on this layer.
e.g. ORM, Database Access, Queues, Messaging, etc

### Base.Project.Infrastructure
This project contains implementations of interfaces defined in **Base.Project.Core.Domain** and also services that interact with external sources (Databases, Cache, Queues, etc)

In case we need infrastructure functionality that belong to its own project the naming convention should be 'Prefix.Infrastructure.xxxxxx' where xxxxxx is the actual Point of Concern.
e.g Prefix.Infrastructure.MailSender

#### Folders

- **IoC**
	- In this folder we can store all DI configurations that will be called from Startup.cs

```c#
public static class InfrastructureConfiguration
{
    public static void Configure(IServiceCollection services)
    {
       services.AddTransient<MyAwesomeInfraClass>();
    }
}
```

- **Repositories**
	- Data access implementation

- **Services**
	- Project specific infrastructure services (e.g, MailSender)


## Web

### Base.Project.WebApi

#### Folders

- **Controllers**
- **Filters**

```C#
public sealed class BusinessExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        DomainException domainException = context.Exception as DomainException;
        if (domainException != null)
        {
            var problemDetails = new ProblemDetails
            {
                    Status = 400,
                    Title = "Bad Request",
                    Detail = domainException.Message
            };

            context.Result = new BadRequestObjectResult(problemDetails);
            context.Exception = null;
        }
    }
}
```

- **Middlewares**
- **ViewModels**

To see how to maintain this file see:

- [Make a Readme](https://www.makeareadme.com/)
- [Markdown Editor](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor)

  
