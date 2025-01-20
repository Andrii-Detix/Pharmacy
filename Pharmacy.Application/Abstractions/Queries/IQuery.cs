using MediatR;

namespace Pharmacy.Application.Abstractions.Queries;

public interface IQuery<TResult> : IRequest<TResult>
{
    
}