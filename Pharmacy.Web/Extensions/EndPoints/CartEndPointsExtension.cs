using MediatR;
using Pharmacy.Application.Commands.CartCommands.AddCartItem;
using Pharmacy.Application.Commands.CartCommands.ChangeCartItemQuantity;
using Pharmacy.Application.Commands.CartCommands.Clear;
using Pharmacy.Application.Queries.CartQueries.GetById;
using Pharmacy.Web.Dto.CartItems;

namespace Pharmacy.Web.Extensions.EndPoints;

public static class CartEndPointsExtension
{
    public static void MapCartEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var cartGroup = endpoints.MapGroup("/carts");
        
        cartGroup.GetById();
        cartGroup.AddCartItem();
        cartGroup.ChangeCartItemQuantity();
        cartGroup.ClearCart();
    }

    private static void GetById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id:guid}", async (ISender sender, Guid id) =>
        {
            try
            {
                var query = new GetCartByIdQuery(id);

                var cart = await sender.Send(query);

                return Results.Ok(cart);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }

    private static void AddCartItem(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/{id:guid}/items", async (ISender sender, Guid id, CreateCartItemDto cartItemDto) =>
        {
            try
            {
                var command = new AddCartItemCommand(id, cartItemDto.ProductId, cartItemDto.Quantity);

                var cart = await sender.Send(command);

                return Results.Ok(cart);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }
    
    private static void ChangeCartItemQuantity(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/{id:guid}/items", async (ISender sender, Guid id, CreateCartItemDto cartItemDto) =>
        {
            try
            {
                var command = new ChangeCartItemQuantityCommand(id, cartItemDto.ProductId, cartItemDto.Quantity);

                var cart = await sender.Send(command);

                return Results.Ok(cart);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }

    private static void ClearCart(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/{id:guid}/items", async (ISender sender, Guid id) =>
        {
            try
            {
                var command = new ClearCartCommand(id);
                
                await sender.Send(command);
                
                return Results.Ok();
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }
}