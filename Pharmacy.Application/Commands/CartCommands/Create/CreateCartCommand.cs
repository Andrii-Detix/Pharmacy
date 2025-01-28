using Pharmacy.Application.Abstractions.Commands;

namespace Pharmacy.Application.Commands.CartCommands.Create;

public record CreateCartCommand(Guid UserId) : ICommand<Guid>;