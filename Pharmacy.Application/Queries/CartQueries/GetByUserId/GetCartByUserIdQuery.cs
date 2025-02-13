using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.CartQueries.GetByUserId;

public record GetCartByUserIdQuery(Guid UserId) : IQuery<Result<Cart>>;