using Pharmacy.Application.Abstractions.Commands;
using Shared.Results;

namespace Pharmacy.Application.Commands.ProductCommands.Create;

public record CreateProductCommand(string Name, decimal Price, int Quantity) : ICommand<Result<Guid>>;