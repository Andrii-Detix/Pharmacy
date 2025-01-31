using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Commands.ProductCommands.Create;
using Pharmacy.Application.Queries.ProductQueries.GetById;
using Pharmacy.Application.Queries.ProductQueries.GetByNamePart;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Web.Controllers;

[ApiController]
[Route("products")]
public class ProductController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> AddProduct(CreateProductCommand command)
    {
        try
        {
            Guid id = await sender.Send(command);

            return Ok(id);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetProductById(Guid id)
    {
        try
        {
            var query = new GetProductByIdQuery(id);
                
            var product = await sender.Send(query);
                
            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProductsByNamePart(string namePart)
    {
        try
        {
            var query = new GetProductsByNamePartQuery(namePart);
                
            var products = await sender.Send(query);
                
            return Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}