using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Legislative.Schema;
using Legislative.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotnetGraphQL;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }

    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILegalEventService, LegalEventService>();
        services.AddSingleton<IAnalysisService, AnalysisService>();
        services.AddSingleton<ILegislationEventService, LegislationEventService>();

        services.AddGraphQL(builder => builder
            .AddApolloTracing()
            .AddHttpMiddleware<ISchema>()
            .AddWebSocketsHttpMiddleware<LegislationSchema>()
            .AddSchema<LegislationSchema>()
            .ConfigureExecutionOptions(options =>
            {
                options.EnableMetrics = Environment.IsDevelopment();
                var logger = (options.RequestServices ?? throw new Exception(nameof(options))).GetRequiredService<ILogger<Startup>>();
                options.UnhandledExceptionDelegate = ctx =>
                {
                    logger.LogError("{Error} occurred", ctx.OriginalException.Message);
                    return Task.CompletedTask;
                };
            })
            .AddSystemTextJson()
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
            .AddWebSockets()
            .AddDataLoader()
            .AddGraphTypes(typeof(LegislationSchema).Assembly));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseWebSockets();
        app.UseGraphQLWebSockets<LegislationSchema>();

        app.UseGraphQL<ISchema>();
        app.UseGraphQLPlayground();
    }
}