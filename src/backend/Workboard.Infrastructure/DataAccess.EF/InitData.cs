using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Workboard.Application.Commands.Boards;
using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF;

public class InitData : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public InitData(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WorkboardContext>();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

        await context.Database.MigrateAsync(cancellationToken);

        await AddData(context, mediator, env, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task AddData(WorkboardContext context, IMediator mediator, IHostEnvironment environment, CancellationToken cancellationToken)
    {
        if (!await context.Set<Board>().AnyAsync(cancellationToken))
        {
            await mediator.Send(new RegisterBoard("Board1"), cancellationToken);
            await mediator.Send(new RegisterBoard("Board2"), cancellationToken);
            await mediator.Send(new RegisterBoard("Board3"), cancellationToken);
        }
    }
}

