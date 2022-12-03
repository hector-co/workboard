using QueryX;
using Workboard.Application.Abstractions.Queries;

namespace Workboard.Application.Queries.Developers;

public class ListDeveloperDto : Query<DeveloperDto>, IQuery<IEnumerable<DeveloperDto>>
{
}
