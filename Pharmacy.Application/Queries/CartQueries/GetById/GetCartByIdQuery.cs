using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.CartQueries.GetById;

public record GetCartByIdQuery(Guid Id) : IQuery<Cart>;