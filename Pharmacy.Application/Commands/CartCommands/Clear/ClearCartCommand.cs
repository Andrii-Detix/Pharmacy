using Pharmacy.Application.Abstractions.Commands;

namespace Pharmacy.Application.Commands.CartCommands.Clear;

public record ClearCartCommand(Guid Id) : ICommand;