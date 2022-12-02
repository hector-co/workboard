using MediatR;

namespace Workboard.Application.Abstractions.Queries;

public interface IQuery<TData> : IRequest<Result<TData>>
{
}

