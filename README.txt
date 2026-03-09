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