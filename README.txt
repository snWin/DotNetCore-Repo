https://www.youtube.com/watch?v=FPR7VVSg76o&list=PL_OVypi7ED9Et7fRYH8YBbybS65sBj2UE&index=41
The original of source.
I Followed this video playlist and made practice.


Tested with Browser.
Tested with Postman.

1. Domain Driven Development

2. DbContext Class
-Maintaining Connection To Db
-Track Changes -- (Entity Framework Core (EF Core) entities are tracked by the DbContext instance by default. This mechanism, known as "Change Tracking". So no need to change "Update" method. We have to use "dbContext.SaveChanges()".)
-Perform CRUD operations
-Bridge between domain models and the database
-using Entity Framework Core

Controller <---> DbContext <--> Database

DbContext Class is a bridge between Domain Model Classes (Controller) and DataBase.
DbContext Class is a primary class that is responsible for interacting with the DataBase 
and performing CRUD operation on our DataBase Tables.

3. Add Entity Framework Core
Tools--> NuGet Package Manager --> Package console

The "Add-Migration" command is an Entity Framework (EF) Core tool used to scaffold a new database migration based on changes made to your data model (DbContext and entity classes since the last migration.

PM> Add-Migration "Migration name" - To create Migration folder
PM> update-database   -- To create Database based on Migration

Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools --This is the package that has responsible to run migration.
				    --Migration will create Database Table based on Models.Domain


4. DTO (Data Transfer Object) Vs Domain Models
Client -- DTO -- API -- Domain Model Database

DTO has subset of the properties of a domain object.
DTO is designed to transfer data over network and send data back to Client. (not directly send data back to client from Domain Model.)
if Client add to add new resource, DTO receive it.


Domain Model has properties.
DTO - The subset of these properties can be manipulate inside the DTO and send to Client

* Separation of Concerns (SoC)
- Domain Model is tightly coupling with Db Schema in Entity Framework (EF) Core
- by decoupling internal domain models or database entities from external API representations.
eg: In Database Table, ID field is int. But in UI, this ID field should be presented as string. We can manipulate this inside DTO


Improved Performance - By selecting only necessary fields and send to Client. no need to send whole object
Security - to prevent sensitive internal data from being exposed in API responses
Versioning - can be versioned independently because DTO keep the contract between the client and the API.
	     API is not exposing data directly to Client.

contract = legally binding agreement between two or more parties 




5 
Asynchronous Programmings 
Repository Pattern
Automapper

** Asynchronous Programmings **
- the recommended approach for I/O-bound database operations

EF Core Async Methods:
1. ToListAsync()
2. FirstOrDefaultAsync()
3. SingleOrDefaultAsync()
4. CountAsync()
5. SaveChangesAsync()
-- There is no Async for Remove() to delete data.

Standard (synchronous) methods block the execution thread while waiting for the database operation (query, save, etc) to complete. can lead the performance issue under heavy load.

Async methods (like ToListAsync()) free up the thread to proceed other tasks while the application waits for the database to respond. This increase scalability and responsiveness, especially in web API that handle many concurrent requests.


** Repository Pattern **

Controller <--> Repository -- (DbContext) <--> Database

- Design pattern to separate the data access layer from the rest of the application 
- Provide interface without exposing DataBase implementation
- Helps create abstraction

-Decoupling
(decoupling the data access layer from the rest of the application)
-Consistency 
(you can switch the logic and the data stores behind the implementation repository. the controller has no knowledge about the data stores)
-Performance 
(every connection to the database goes through the repository we can also improve the performance of the application by using caching, batching or other optimization techniques)
-Multiple data sources 
(switching - between different data sources - from MSSQL to MongoDB- without affecting the application logic)


**Automapper**

Install AutoMapper - A conversion-based object-object mapper

using AutoMapper;

CreateMap<Source,Desitation>.ReverseMap();

Register in program.cs
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));



6. Add Swagger

https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio

(1) Install swashbuckler
	- Open "Manage Nuget Packages", Search the key word "swashbuckle" under Browse tag
	Or
	- Install the below PowerShell from Package Manager Console
		Install-Package Swashbuckle.AspNetCore -Version 10.1.5
(2) Register in Program.cs
(3) Browse https://localhost:portnumber/swagger/index.html



