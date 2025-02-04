using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Application.ApplicationErrors;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.ValueObjects;
using Shared.Results;

namespace Pharmacy.Application.Queries.UserQueries.GetByEmail;

public class GetUserByEmailHandler(IPharmacyDbContext context) : IQueryHandler<GetUserByEmailQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(query.Email);

        if (emailResult.IsFailure)
        {
            return emailResult.Error;
        }
        
        Email email = emailResult.Value!;
        
        var user = await context.Users.AsNoTracking()
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        return user;
    }
}