using System;
using System.IO;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Legislative.Repository;
using Legislative.Schema;
using Legislative.Services;
using Legislative.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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
        // 
        services.AddSingleton<ILegalEventRepository, LegalEventRepository>();
        services.AddSingleton<ILmonAnalysisRepository, LmonAnalysisRepository>();

        var settings = TryRetrieveSettings<PostgresSettings>("appsettings.json");
        services.AddSingleton(settings ??= new PostgresSettings());

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

        var configuration = app.ApplicationServices.GetService<IConfiguration>();

        app.UseWebSockets();
        app.UseGraphQLWebSockets<LegislationSchema>();

        app.UseGraphQL<ISchema>();
        app.UseGraphQLPlayground();

    }

    public static T TryRetrieveSettings<T>(string filename) where T : class
    {
        var physicalFileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(physicalFileProvider, filename, true, true)
            .Build();

        var name = typeof(T).Name;
        var appSettingsSection = configuration.GetSection(name);
        if (appSettingsSection == null)
            throw new Exception("No appsettings section has been found");

        var settings = appSettingsSection.Get<T>();

        return settings;
    }
}