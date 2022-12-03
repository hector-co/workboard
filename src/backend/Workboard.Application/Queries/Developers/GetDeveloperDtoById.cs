using Workboard.Application.Abstractions.Queries;

namespace Workboard.Application.Queries.Developers;

public record GetDeveloperDtoById(int Id) : IQuery<DeveloperDto>;