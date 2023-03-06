# RugbyManager
A backend implementation of rugby manager application

## Assumptions

## DB Design diagram

## How to run the application

To run the application without a database, open the appsettings.json and ensure the ```UseInMemoryDatabase``` entry is set to true

Then from the .\src\RugbyManager.WebApi folder, run the following command 
```dotnet run```

To test the application, run the below command from the .\test folder
```dotnet test```

## Reason for specific designs and interfaces

I used the 'clean architecture' project layout to keep the code clean and enforce SOLID coding principles

On top of the architecture, I chose to write the project in style of a CQRS application. I chose this style to prevent coupling of the logic exposed 

The API was written using the new 'minimal' API style which does not use traditional controllers. This was done because I wanted to see that when this implementation is expanded passed the point of a normal demo/tutorial, how it affects the layout of the project

## List any encountered obstacles and how you solved them

I had a bit of trouble with the validations as the normal point of attaching them was not there as I was not using traditional controllers

I managed to get around this issue by using the new ```IEndPointFilter``` functionality for minimal APIs where I was able retrieve the relevant validator and validate the inputs for the endpoints successfully 

## List resources used and relevant references

Nuget Libraries Used

* MediatR
* FluentAssertions
* FluentValidations
* EFCore
* AutoMapper
* Moq
* Swashbuckle (Swagger)
* OpenAPI

References

* [CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture)
* [Minimal API validation with ASP.NET 7.0 Endpoint Filters](https://benfoster.io/blog/minimal-api-validation-endpoint-filters/)
* [ASP.NET Core rate limiting middleware in .NET 7](https://blog.maartenballiauw.be/post/2022/09/26/aspnet-core-rate-limiting-middleware.html)
* [Filters in Minimal API apps](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/min-api-filters?view=aspnetcore-7.0)
* [Goodbye controllers, hello Minimal APIs - Nick Chapsas - NDC London 2022](https://www.youtube.com/watch?v=hPpvlKLeYYA&ab_channel=NDCConferences)
* [Minimal APIs just got all I wanted in .NET 7](https://www.youtube.com/watch?v=Kt9TiXrwIp4&ab_channel=NickChapsas)

## How long you took to complete the assignment

I broke the project down into chunks as below is the rough breakdown of the time spent in each area:

| Activity | Time Spent |
|:---------|:-----------|
| Initial setup, planning and design | 2hrs |
| Create functionality for each entity type | 4hrs |
| Validation framework for endpoints | 2hrs |
| Rate limiting for API | 0.5hrs |
| Update and Delete endpoints and related functionality for all entities | 3hrs |
| Read endpoints and related functionality for all entities | 2hrs |
| Transfer functionality | 1.5hrs |


## If you had more time, what would you do differently? Also, what would you have added additionally