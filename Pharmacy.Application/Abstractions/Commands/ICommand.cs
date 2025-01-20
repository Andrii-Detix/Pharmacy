using MediatR;

namespace Pharmacy.Application.Abstractions.Commands;

public interface ICommand : IRequest
{
    
}

public interface ICommand<TResult> : IRequest<TResult>
{
    
}