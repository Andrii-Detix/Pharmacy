using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.CartQueries.GetByUserId;

public record GetCartByUserIdQuery(Guid UserId) : IQuery<Cart>;