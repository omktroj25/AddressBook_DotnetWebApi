using Entity.Data;
using Entity.Dto;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Contract.IRepository;
using Repository;
using Service;
using Contract.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Exception;
using AddressBookApi;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[assembly: ExcludeFromCodeCoverage]

/* `var builder = WebApplication.CreateBuilder(args);` is creating a new instance of the
`WebApplication` class, which is used to configure and run an ASP.NET Core web application. It is
using the `CreateBuilder` method to create a new instance of the `WebApplicationBuilder` class,
which is used to configure the application's services, middleware, and endpoints. The `args`
parameter is an array of command-line arguments passed to the application. */
var builder = WebApplication.CreateBuilder(args);

// Add services to the container and configuration to the action filters
/* `builder.Services.AddControllers()` is adding the MVC services to the container.
`config.Filters.Add(new ModelStateActionFilter())` is adding a custom filter to the MVC pipeline
that will validate the model state of incoming requests. `ConfigureApiBehaviorOptions()` is
configuring the behavior options for the API, specifically setting `SuppressModelStateInvalidFilter`
to true, which will suppress the default behavior of returning a 400 response when the model state
is invalid. */
builder.Services.AddControllers( config => 
{
    config.Filters.Add(new ModelStateActionFilter());
}).ConfigureApiBehaviorOptions(
    options =>
    {
    options.SuppressModelStateInvalidFilter=true;
    }
);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/* `builder.Services.AddEndpointsApiExplorer()` and `builder.Services.AddSwaggerGen()` are configuring
Swagger/OpenAPI for the ASP.NET Core web application. Swagger/OpenAPI is a tool for documenting and
testing APIs. `AddEndpointsApiExplorer()` adds the API explorer middleware to the application's
services, which generates Swagger/OpenAPI metadata for the API endpoints. `AddSwaggerGen()` adds the
Swagger/OpenAPI generator to the application's services, which generates the Swagger/OpenAPI
specification document based on the metadata generated by the API explorer middleware. */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration for apidbcontext
/* `builder.Services.AddDbContext<ApiDbContext>(opt =>
opt.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"),b=>b.MigrationsAssembly("AddressBookApi")));`
is configuring the application to use Entity Framework Core to connect to a PostgreSQL database. It
is adding a scoped service of type `ApiDbContext` to the dependency injection container, which will
be used to create instances of the `ApiDbContext` class. The `UseNpgsql` method is configuring the
database provider to use PostgreSQL, and the `GetConnectionString` method is retrieving the
connection string from the application's configuration file. The `MigrationsAssembly` method is
specifying the assembly that contains the database migration classes. */
builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"),b=>b.MigrationsAssembly("AddressBookApi")));


//JWT Authentication
/* This code is configuring JWT authentication for the ASP.NET Core web application. It is adding the
JWT bearer authentication scheme to the application's services using
`JwtBearerDefaults.AuthenticationScheme`. It is then configuring the token validation parameters for
the JWT bearer authentication scheme using the `AddJwtBearer` method. The
`TokenValidationParameters` object is used to specify the parameters for validating the JWT token,
including the issuer, audience, lifetime, and signing key. The values for these parameters are
retrieved from the application's configuration file using the `builder.Configuration` object. */
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

// Configuration for logger
/* `builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());` is configuring logging
for the ASP.NET Core web application. It is adding the console logger provider to the application's
services using `AddConsole()`. This will log messages to the console output. Other logger providers
can be added as well, such as a file logger or a database logger. */
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});

/* These lines of code are registering services and dependencies in the ASP.NET Core dependency
injection container. */
builder.Services.AddScoped<MappingProfile>();
builder.Services.AddScoped<IAddressBookService,AddressBookService>();
builder.Services.AddScoped<IAddressBookRepository,AddressBookRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();


// Configure the HTTP request pipeline.
/* This code block is checking if the application is running in the development environment using the
`IsDevelopment()` method of the `IWebHostEnvironment` interface. If the application is running in
the development environment, it is configuring Swagger and Swagger UI for the application using the
`UseSwagger()` and `UseSwaggerUI()` methods. Swagger is a tool for documenting and testing APIs, and
Swagger UI is a web interface for interacting with Swagger-generated API documentation. By adding
these middleware components to the application pipeline, developers can easily view and test the API
endpoints during development. */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Exception handler
/* The above code is setting up global exception handling middleware in a C# application. It catches
any unhandled exceptions that occur during the processing of an HTTP request and returns an
appropriate error response to the client. The middleware checks the type of the exception and
returns a custom error response based on the type of the exception. If the exception is a custom
exception, it returns a response with the error code, message, and description. If the exception is
an error exception, it returns a response with the error code, message, description, and error
details. If the exception is a bad request or not found */
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            Console.WriteLine($"Something went wrong:{contextFeature.Error}");
            context.Response.ContentType = "application/json";
            BaseCustomException? baseException = contextFeature.Error as BaseCustomException;
            ErrorException? errorException = contextFeature.Error as ErrorException;
            //If the thrown exception is BaseCustomException
            if (baseException != null)
            {
                context.Response.StatusCode = baseException.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDto
                {
                    StatusCode = baseException.StatusCode,
                    Message = baseException.Messages,
                    Description = baseException.Description,
                })); 
            }
            //If the thrown exception is ErrorException to display with error
            else if (errorException != null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseErrorDto
                {
                    StatusCode = errorException.StatusCode,
                    Message = errorException.Messages,
                    Description = errorException.Description,
                    Error = errorException.Error,
                }));
            }
            //For handling the internal server globally
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDto
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error",
                    Description = "An unexpected error occurred while processing your request."
                })); 
            }
        }
    });
});



/* `app.UseAuthentication();` enables authentication middleware to be added to the request pipeline.
This middleware is responsible for authenticating the user based on the authentication scheme
specified in the request. */
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();