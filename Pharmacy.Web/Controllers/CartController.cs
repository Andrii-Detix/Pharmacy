using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Commands.CartCommands.AddCartItem;
using Pharmacy.Application.Commands.CartCommands.ChangeCartItemQuantity;
using Pharmacy.Application.Commands.CartCommands.Clear;
using Pharmacy.Application.Queries.CartQueries.GetById;
using Pharmacy.Domain.Entities;
using Pharmacy.Web.Dto.CartItems;

namespace Pharmacy.Web.Controllers;

[ApiController]
[Route("carts")]
public class CartController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Cart>> GetCartById(Guid id)
    {
        try
        {
            var query = new GetCartByIdQuery(id);

            var cart = await sender.Send(query);

            return Ok(cart);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("{id:guid}/items")]
    public async Task<ActionResult<Cart>> AddItemToCart(Guid id, CreateCartItemDto cartItemDto)
    {
        try
        {
            var command = new AddCartItemCommand(id, cartItemDto.ProductId, cartItemDto.Quantity);

            var cart = await sender.Send(command);

            return Ok(cart);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:guid}/items")]
    public async Task<ActionResult<Cart>> ChangeCartItemQuantity(Guid id, CreateCartItemDto cartItemDto)
    {
        try
        {
            var command = new ChangeCartItemQuantityCommand(id, cartItemDto.ProductId, cartItemDto.Quantity);

            var cart = await sender.Send(command);

            return Ok(cart);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:guid}/items")]
    public async Task<ActionResult> ClearCart(Guid id)
    {
        try
        {
            var command = new ClearCartCommand(id);
                
            await sender.Send(command);
                
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}