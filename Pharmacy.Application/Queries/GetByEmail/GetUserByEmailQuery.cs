using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.GetByEmail;

public record GetUserByEmailQuery(string Email) : IQuery<User>;