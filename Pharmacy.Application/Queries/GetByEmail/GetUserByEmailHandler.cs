using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.ValueObjects;

namespace Pharmacy.Application.Queries.GetByEmail;

public class GetUserByEmailHandler(IPharmacyDbContext _context) : IQueryHandler<GetUserByEmailQuery, User>
{
    public async Task<User> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var email = Email.Create(query.Email);
        
        var user = await _context.Users.AsNoTracking()
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new Exception($"User with email {query.Email} does not exist");
        }
        
        return user;
    }
}