using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.ProductQueries.GetByNamePart;

public record GetProductsByNamePartQuery(string NamePart) : IQuery<Result<List<Product>>>;