using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Commands.ProductCommands.Create;
using Pharmacy.Application.Queries.ProductQueries.GetById;
using Pharmacy.Application.Queries.ProductQueries.GetByNamePart;
using Pharmacy.Domain.Entities;
using Pharmacy.Web.Extensions.ErrorExtensions;
using Shared.Results;

namespace Pharmacy.Web.Controllers;

[ApiController]
[Route("products")]
public class ProductController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> AddProduct(CreateProductCommand command)
    {
            Result<Guid> result = await sender.Send(command);

            return result.IsSuccess ? Ok(result.Value) : result.Error.ToProblemDetails();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetProductById(Guid id)
    {
            var query = new GetProductByIdQuery(id);
                
            Result<Product> result = await sender.Send(query);
                
            return result.IsSuccess ? Ok(result.Value) : result.Error.ToProblemDetails();
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProductsByNamePart(string namePart)
    {
            var query = new GetProductsByNamePartQuery(namePart);
                
            Result<List<Product>> result = await sender.Send(query);
                
            return result.IsSuccess ? Ok(result.Value) : result.Error.ToProblemDetails();
    }
}