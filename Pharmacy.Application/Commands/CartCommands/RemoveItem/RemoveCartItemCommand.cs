using Pharmacy.Application.Abstractions.Commands;
using Shared.Results;

namespace Pharmacy.Application.Commands.CartCommands.RemoveItem;

public record RemoveCartItemCommand(Guid CartId, Guid ItemId) : ICommand<Result>;