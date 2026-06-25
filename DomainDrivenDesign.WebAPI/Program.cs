using Carter;
using DomainDrivenDesign.Application;
using DomainDrivenDesign.Infrastructure;
using DomainDrivenDesign.WebAPI;
using DomainDrivenDesign.WebAPI.Controllers;
using Microsoft.AspNetCore.OData;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCarter();
builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();
builder.Services.AddControllers()
    .AddOData(options =>
    options
    .AddRouteComponents("odata", AppODataController.GetEdmModel())
    .Expand().Count().Select().Filter().OrderBy().SetMaxTop(null));

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseExceptionHandler();

app.MapControllers();

app.MapCarter();

//var scope = app.Services.CreateScope();
//var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
//User user = new()
//{
//    Email = "test@test.com",
//    UserName = "test",
//    FirstName = "Test",
//    LastName = "Kullanıcı",
//};
//var result = await userManager.CreateAsync(user, "1");

app.Run();