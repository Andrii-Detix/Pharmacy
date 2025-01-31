using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Commands.CartCommands.Create;
using Pharmacy.Application.Commands.UserCommands.Create;
using Pharmacy.Application.Commands.UserCommands.UpdateName;
using Pharmacy.Application.Queries.CartQueries.GetByUserId;
using Pharmacy.Application.Queries.UserQueries.GetByEmail;
using Pharmacy.Application.Queries.UserQueries.GetById;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Web.Controllers;

[ApiController]
[Route("user")]
public class UserController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUser(CreateUserCommand userCommand)
    {
        try
        {
            Guid id = await sender.Send(userCommand);

            var cartCommand = new CreateCartCommand(id);
            await sender.Send(cartCommand);
                
            return Ok(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUserById(Guid id)
    {
        try
        {
            var query = new GetUserByIdQuery(id);

            var user = await sender.Send(query);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetUserByEmail(string email)
    {
        try
        {
            var query = new GetUserByEmailQuery(email);

            var user = await sender.Send(query);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateUserName(Guid id, string name)
    {
        try
        {
            var command = new UpdateUserNameCommand(id, name);

            await sender.Send(command);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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