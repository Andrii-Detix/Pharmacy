using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.UserQueries.GetByEmail;

public record GetUserByEmailQuery(string Email) : IQuery<Result<User>>;