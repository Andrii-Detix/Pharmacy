using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Commands.CartCommands.AddCartItem;

public record AddCartItemCommand(Guid CartId, Guid ProductId, int Quantity) : ICommand<Result<Cart>>;