using Pharmacy.Application.Extensions;
using Pharmacy.Persistence;
using Pharmacy.Web.Extensions;
using Pharmacy.Web.Extensions.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapUserEndPoints();

app.Run();