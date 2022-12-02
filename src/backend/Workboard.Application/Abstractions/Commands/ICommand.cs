using MediatR;
using Workboard.Domain.Abstractions;

namespace Workboard.Application.Abstractions.Commands;

public interface ICommand : IRequest<Response>
{
}

public interface ICommand<TValue> : IRequest<Response<TValue>>

{
}
