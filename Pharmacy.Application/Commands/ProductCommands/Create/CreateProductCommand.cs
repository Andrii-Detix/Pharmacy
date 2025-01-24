using Pharmacy.Application.Abstractions.Commands;

namespace Pharmacy.Application.Commands.ProductCommands.Create;

public record CreateProductCommand(string Name, decimal Price, int Quantity) : ICommand<Guid>;