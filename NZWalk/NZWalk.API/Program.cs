using System.Reflection;
using System.Reflection.Metadata;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;

using NZWalk.API.Repositories;
using Microsoft.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi(); // default

// Learn more about configuration swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Add the Swagger generator to the services collection in Program.cs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection - Design pattern - to reduce the coupling between components
//DI is lifetime of services.
//Instead of instantiating objects within a class those objects are passed in as parameters to the class like passing it to the Constructor or the method instead this 
//asp.net core provides a build-in DI container that can be used to manage the dependencies of an application
//The DI container is responsible for creating and managing the lifetime of objects and their dependencies.
//It allows you to register services and their implementations, and then it will automatically resolve those dependencies when needed.
//DbInject
//Instead of instantiating objects within a class,
//those objects are passed in as parameters to the class like passing it to the Constructor of the method instead.

//to inject DB context class, use the Builder object
//Builder.Services.AddDbContext<DB context class that we want to inject the type of DB context >
//it needs a DBContextOptions handled by the OptionBuilder
//Because of the below code, the application will manage all the instances of this DB context class whenever we call it inside controllers or repositories
builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString")));

builder.Services.AddScoped<IRegionRepository,SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

//AutoMapper is injected into the application by registering it in here, program.cs file.
//In Program.cs, register AutoMapper to scan your assembly for mapping profiles:
// Use the params Type[] overload. Avoid using a named parameter that may not exist in the installed package.
//builder.Services.AddAutoMapper(typeof(Program));

// after installed Downgrade versino, 12.0.1, it is solved.
/*builder.Services.AddAutoMapper(typeof(Program).Assembly);*/

//Registering AutoMapper to scan your assembly means configuring the library to automatically find all classes that inherit from Profile within your project, eliminating the need to manually register each mapping configuration. 
//If the application runs, it will scan all the mapping 

// inject Identity package before Authentication injection
builder.Services.AddIdentityCore<IdentityUser>()
	.AddRoles<IdentityRole>()
	.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
	.AddEntityFrameworkStores<NZWalksAuthDbContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit= false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;
});



//Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	options.TokenValidationParameters =new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience=true,
		ValidateLifetime=true,
		ValidateIssuerSigningKey= true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
	});
// make sure you use "Jwt" object in json file


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	//Enable the middleware for serving the generated JSON document and the Swagger UI
	app.UseSwagger();
	app.UseSwaggerUI();

	//app.MapOpenApi();
}

app.UseHttpsRedirection();

// Add Authentication into Middleware Pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
