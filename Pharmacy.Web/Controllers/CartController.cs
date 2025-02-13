using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Commands.CartCommands.AddCartItem;
using Pharmacy.Application.Commands.CartCommands.ChangeCartItemQuantity;
using Pharmacy.Application.Commands.CartCommands.Clear;
using Pharmacy.Application.Commands.CartCommands.RemoveItem;
using Pharmacy.Application.Queries.CartQueries.GetById;
using Pharmacy.Domain.Entities;
using Pharmacy.Web.Dto.CartItems;
using Pharmacy.Web.Extensions.ErrorExtensions;
using Shared.Results;

namespace Pharmacy.Web.Controllers;

[ApiController]
[Route("carts")]
public class CartController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Cart>> GetCartById(Guid id)
    {
        var query = new GetCartByIdQuery(id);

        Result<Cart> result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : result.Error.ToProblemDetails();
    }

    [HttpPost("{id:guid}/items")]
    public async Task<ActionResult<Cart>> AddItemToCart(Guid id, CreateCartItemDto cartItemDto)
    {
        var command = new AddCartItemCommand(id, cartItemDto.ProductId, cartItemDto.Quantity);

        Result<Cart> result = await sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : result.Error.ToProblemDetails();
    }

    [HttpPut("{id:guid}/items")]
    public async Task<ActionResult<Cart>> ChangeCartItemQuantity(Guid id, CreateCartItemDto cartItemDto)
    {
        var command = new ChangeCartItemQuantityCommand(id, cartItemDto.ProductId, cartItemDto.Quantity);

        Result<Cart> result = await sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : result.Error.ToProblemDetails();
    }

    [HttpDelete("{id:guid}/items")]
    public async Task<ActionResult> ClearCart(Guid id)
    {
        var command = new ClearCartCommand(id);

        Result result = await sender.Send(command);

        return result.IsSuccess ? Ok() : result.Error.ToProblemDetails();
    }

    [HttpDelete("{id:guid}/items/{itemId:guid}")]
    public async Task<ActionResult> RemoveCartItem(Guid id, Guid itemId)
    {
        var command = new RemoveCartItemCommand(id, itemId);
        
        Result result = await sender.Send(command);
        
        return result.IsSuccess ? Ok() : result.Error.ToProblemDetails();
    }
}