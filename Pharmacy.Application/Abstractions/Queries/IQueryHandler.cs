using MediatR;

namespace Pharmacy.Application.Abstractions.Queries;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> 
    where TQuery : IQuery<TResult>
{
    
}