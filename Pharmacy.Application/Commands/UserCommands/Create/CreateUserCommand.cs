using Pharmacy.Application.Abstractions.Commands;

namespace Pharmacy.Application.Commands.UserCommands.Create;

public record CreateUserCommand(string Name, string Email, string Password) : ICommand<Guid>;