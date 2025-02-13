using Pharmacy.Application.Abstractions.Commands;
using Shared.Results;

namespace Pharmacy.Application.Commands.UserCommands.Create;

public record CreateUserCommand(string Name, string Email, string Password) : ICommand<Result<Guid>>;