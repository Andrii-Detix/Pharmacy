using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Commands.CartCommands.AddCartItem;

public record AddCartItemCommand(Guid CartId, Guid ProductId, int Quantity) : ICommand<Cart>;