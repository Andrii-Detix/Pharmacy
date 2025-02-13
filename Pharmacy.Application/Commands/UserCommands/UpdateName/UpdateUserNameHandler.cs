using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Application.ApplicationErrors;
using Shared.Results;

namespace Pharmacy.Application.Commands.UserCommands.UpdateName;

public class UpdateUserNameHandler(IPharmacyDbContext context) : ICommandHandler<UpdateUserNameCommand, Result>
{
    public async Task<Result> Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync([request.Id], cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        Result result = user.UpdateName(request.Name);

        if (result.IsFailure)
        {
            return result;
        }
        
        await context.SaveChangesAsync(cancellationToken);

        return Result.CreateSuccess();
    }
}