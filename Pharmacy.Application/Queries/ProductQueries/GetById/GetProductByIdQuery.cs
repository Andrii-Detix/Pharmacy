using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.ProductQueries.GetById;

public record GetProductByIdQuery(Guid Id) : IQuery<Product>;