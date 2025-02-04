using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Commands.CartCommands.Create;
using Pharmacy.Application.Commands.UserCommands.Create;
using Pharmacy.Application.Commands.UserCommands.UpdateName;
using Pharmacy.Application.Queries.CartQueries.GetByUserId;
using Pharmacy.Application.Queries.UserQueries.GetByEmail;
using Pharmacy.Application.Queries.UserQueries.GetById;
using Pharmacy.Domain.Entities;
using Pharmacy.Web.Extensions.ErrorExtensions;
using Shared.Results;

namespace Pharmacy.Web.Controllers;

[ApiController]
[Route("user")]
public class UserController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUser(CreateUserCommand userCommand)
    {
        Result<Guid> result = await sender.Send(userCommand);

        if (result.IsFailure)
        {
            return result.Error.ToProblemDetails();
        }

        Guid userId = result.Value;

        var cartCommand = new CreateCartCommand(userId);
        await sender.Send(cartCommand);

        return Ok(userId);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUserById(Guid id)
    {
        var query = new GetUserByIdQuery(id);

        Result<User> result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : result.Error.ToProblemDetails();
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetUserByEmail(string email)
    {
        var query = new GetUserByEmailQuery(email);

        Result<User> result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : result.Error.ToProblemDetails();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateUserName(Guid id, string name)
    {
        var command = new UpdateUserNameCommand(id, name);

        Result result = await sender.Send(command);

        return result.IsSuccess ? Ok() : result.Error.ToProblemDetails();
    }

    [HttpGet("{id:guid}/cart")]
    public async Task<ActionResult<Cart>> GetUserCart(Guid id)
    {
        try
        {
            var query = new GetCartByUserIdQuery(id);

            var cart = await sender.Send(query);

            return Ok(cart);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}