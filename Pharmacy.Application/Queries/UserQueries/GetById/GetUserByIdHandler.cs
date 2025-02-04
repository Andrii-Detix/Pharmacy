using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Application.ApplicationErrors;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.UserQueries.GetById;

public class GetUserByIdHandler(IPharmacyDbContext context) : IQueryHandler<GetUserByIdQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.AsNoTracking()
            .Where(u => u.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        return user;
    }
}