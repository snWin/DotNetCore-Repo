using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Dependency Injection - Design pattern - to reduce the coupling between components
//DI is lifetime of services.
//Instead of instantiating objects within a class those objects are passed in as parameters to the class like passing it to the Constructor or the method instead this 
//asp.net core provides a build-in DI container that can be used to manage the dependencies of an application
//The DI container is responsible for creating and managing the lifetime of objects and their dependencies.
//It allows you to register services and their implementations, and then it will automatically resolve those dependencies when needed.
//DbInject
//Instead of instantiating objects within a class,
//those objects are passed in as parameters to the class like passing it to the Constructor of the method instead.

//to inject DB context class, let's use the Builder object
//Builder.Services.AddDbContext<DB context class that we want to inject the type of DB context >
//it needs a DBContextOptions handled by the OptionBuilder
//Because of the below code, the application will manage all the instances of this DB context class whenever we call it inside controllers or repositories
builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
