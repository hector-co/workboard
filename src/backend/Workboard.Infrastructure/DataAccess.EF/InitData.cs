using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

        await context.Database.MigrateAsync(cancellationToken);

        await AddData(context, env, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task AddData(WorkboardContext context, IHostEnvironment environment, CancellationToken cancellationToken)
    {
    }
}

