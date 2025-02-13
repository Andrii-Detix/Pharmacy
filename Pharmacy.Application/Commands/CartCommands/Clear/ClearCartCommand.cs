using Pharmacy.Application.Abstractions.Commands;
using Shared.Results;

namespace Pharmacy.Application.Commands.CartCommands.Clear;

public record ClearCartCommand(Guid Id) : ICommand<Result>;