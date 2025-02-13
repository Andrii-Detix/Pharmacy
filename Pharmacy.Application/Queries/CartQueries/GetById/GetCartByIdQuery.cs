using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.CartQueries.GetById;

public record GetCartByIdQuery(Guid Id) : IQuery<Result<Cart>>;