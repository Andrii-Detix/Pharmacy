using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.UserQueries.GetById;

public record GetUserByIdQuery(Guid Id) : IQuery<Result<User>>;