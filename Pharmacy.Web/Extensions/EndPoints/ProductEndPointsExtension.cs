using MediatR;
using Pharmacy.Application.Commands.ProductCommands.Create;
using Pharmacy.Application.Queries.ProductQueries.GetById;
using Pharmacy.Application.Queries.ProductQueries.GetByNamePart;

namespace Pharmacy.Web.Extensions.EndPoints;

public static class ProductEndPointsExtension
{
    public static void MapProductEndPoints(this IEndpointRouteBuilder endpoints)
    {
        var productsGroup = endpoints.MapGroup("products");
        
        productsGroup.AddProduct();
        productsGroup.GetProductById();
        productsGroup.GetProductsByNamePart();
    }

    private static void AddProduct(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/", async (ISender sender, CreateProductCommand command) =>
        {
            try
            {
                Guid id = await sender.Send(command);

                return Results.Ok(id);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }

    private static void GetProductById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id}", async (ISender sender, Guid id) =>
        {
            try
            {
                var query = new GetProductByIdQuery(id);
                
                var product = await sender.Send(query);
                
                return Results.Ok(product);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }

    private static void GetProductsByNamePart(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async (ISender sender, string namePart) =>
        {
            try
            {
                var query = new GetProductsByNamePartQuery(namePart);
                
                var products = await sender.Send(query);
                
                return Results.Ok(products);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }
}