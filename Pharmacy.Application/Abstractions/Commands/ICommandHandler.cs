﻿using MediatR;

namespace Pharmacy.Application.Abstractions.Commands;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand> 
    where TCommand : ICommand
{
    
}

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    
}