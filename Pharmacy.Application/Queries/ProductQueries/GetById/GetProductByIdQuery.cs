using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.ProductQueries.GetById;

public record GetProductByIdQuery(Guid Id) : IQuery<Result<Product>>;