using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.GetById;

public class GetUserByIdHandler(IPharmacyDbContext _context) : IQueryHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.Id);

        if (user is null)
        {
            throw new Exception($"User with id {request.Id} not found");
        }
        
        return user;
    }
}