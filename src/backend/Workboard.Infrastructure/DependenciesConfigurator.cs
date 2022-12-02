using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QueryX;
using Serilog;
using Workboard.Application.Behaviors;
using Workboard.Application.Commands.Boards;
using Workboard.Domain.Model;
using Workboard.Infrastructure.DataAccess.EF;
using Workboard.Infrastructure.DataAccess.EF.Boards;

namespace Workboard.Infrastructure
{
    public static class DependenciesConfigurator
    {
        public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<WorkboardContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("Workboard"),
                    o => o.MigrationsHistoryTable("__EFMigrationsHistory", WorkboardContext.DbSchema)));

            builder.Services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining(typeof(ValidationBehavior<,>));

            builder.Services.AddMediatR(typeof(WorkboardContext), typeof(RegisterBoard));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddQueryX();

            builder.Logging.ClearProviders();
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddHostedService<InitData>();

            builder.Services.AddScoped<IBoardRepository, BoardRepository>();

            return builder;
        }
    }
}
