using MediatR;

namespace Pharmacy.Application.Commands.UserCommands.Create;

public record UserCreatedEvent(Guid Id) : INotification;