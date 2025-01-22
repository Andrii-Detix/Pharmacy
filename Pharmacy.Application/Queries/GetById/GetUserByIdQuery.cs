using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.GetById;

public record GetUserByIdQuery(Guid Id) : IQuery<User>;