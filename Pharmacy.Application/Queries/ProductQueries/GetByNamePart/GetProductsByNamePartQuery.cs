using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.ProductQueries.GetByNamePart;

public record GetProductsByNamePartQuery(string NamePart) : IQuery<List<Product>>;