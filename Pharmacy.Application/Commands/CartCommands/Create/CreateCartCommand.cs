using Pharmacy.Application.Abstractions.Commands;
using Shared.Results;

namespace Pharmacy.Application.Commands.CartCommands.Create;

public record CreateCartCommand(Guid UserId) : ICommand<Result<Guid>>;