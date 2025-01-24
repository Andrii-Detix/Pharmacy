using MediatR;
using Pharmacy.Application.Commands.UserCommands.Create;
using Pharmacy.Application.Commands.UserCommands.UpdateName;
using Pharmacy.Application.Queries.UserQueries.GetByEmail;
using Pharmacy.Application.Queries.UserQueries.GetById;

namespace Pharmacy.Web.Extensions.EndPoints;

public static class UserEndPointsExtension
{
    public static void MapUserEndPoints(this IEndpointRouteBuilder endpoints)
    {
        var usersGroup = endpoints.MapGroup("users");
        
        usersGroup.CreateUser();
        usersGroup.GetUserById();
        usersGroup.GetUserByEmail();
        usersGroup.UpdateUserName();
    }

    private static void CreateUser(this RouteGroupBuilder endpoints)
    {
        endpoints.MapPost("/", async (ISender sender, CreateUserCommand user) =>
        {
            try
            {
                Guid id = await sender.Send(user);

                return Results.Ok(id);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }

    private static void GetUserById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id:guid}", async (ISender sender, Guid id) =>
        {
            try
            {
                var query = new GetUserByIdQuery(id);

                var user = await sender.Send(query);

                return Results.Ok(user);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }

    private static void GetUserByEmail(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async (ISender sender, string email) =>
        {
            try
            {
                var query = new GetUserByEmailQuery(email);

                var user = await sender.Send(query);

                return Results.Ok(user);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }

    private static void UpdateUserName(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/{id:guid}", async (ISender sender, Guid id, string name) =>
        {
            try
            {
                var command = new UpdateUserNameCommand(id, name);

                await sender.Send(command);

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }
}