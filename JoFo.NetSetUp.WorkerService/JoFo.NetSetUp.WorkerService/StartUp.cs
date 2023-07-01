using JoFo.NetSetUp.Helper.Configurations;
using JoFo.NetSetUp.Helper.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;


namespace WorkerServiceSetup;

public class StartUp
{
    public static void Run(Action<IConfigurationBuilder> config,
        Action<IHostBuilder, IConfigurationRoot> builder,
        Action<IHost> app)
    {
        StaticLogger.EnsureInitialized();
        Log.Information("Server Booting Up...");
        try
        {
            var _builder = Host.CreateDefaultBuilder();

            ConfigurationBuilder _config = new ConfigurationBuilder();
            config(_config);
            _config.AddConfigurations();
            _builder.ConfigureAppConfiguration((_, config) => { config = _config; });

            var _configuration = _config.Build();

            _builder.AddSeriLog(_configuration);
            builder(_builder, _configuration);

            var _app = _builder.Build();
            app(_app);
            _app.RunAsync();

            Console.WriteLine("Server Up");
            Console.ReadLine();
        }
        catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
        {
            StaticLogger.EnsureInitialized();
            Log.Fatal(ex, "Unhandled exception");
        }
        finally
        {
            StaticLogger.EnsureInitialized();
            Log.Information("Server Shutting down...");
            Log.CloseAndFlush();
        }
    }
}