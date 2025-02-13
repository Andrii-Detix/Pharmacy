using Pharmacy.Application.Abstractions.Commands;
using Shared.Results;

namespace Pharmacy.Application.Commands.UserCommands.UpdateName;

public record UpdateUserNameCommand(Guid Id, string Name) : ICommand<Result>;