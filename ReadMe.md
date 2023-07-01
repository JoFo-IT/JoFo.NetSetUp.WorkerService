![](./.Media/02_SocialMedia/repository-open-graph.png)

# JoFo.NetSetUp.WorkerService

## Project description

The StartUp Nuget package WorkerService as the boot configuration for the .NET application, managing the WorkerService startup and
shutdown. It ensures logging is provided, configures hosting for the application, catches potential exceptions, and logs
information during WorkerService shutdown.

## Features include
- 
- __Logging Initialization:__
    - Uses `StaticLogger.EnsureInitialized()` to ensure the logging service, provided by Serilog, has been initialized.

- __Configuration Setup:__
    - An `IConfigurationBuilder` is used to set up configurations. These configurations can be customized via `config()` method. Additional configurations are added with `_config.AddConfigurations()`.

- __Host Builder Setup:__
    - The host builder is created using `Host.CreateDefaultBuilder()` which configures the app configuration on the created host builder. Your custom configurations (built with `_config.Build()`) can be passed to it.

- __Serilog Addition:__
    - Add Serilog to the host builder for providing logging services with `_builder.AddSeriLog(_configuration)`.

- __App Building and Running:__
    - The application is built with `_builder.Build()`. The built host can then be modified using the `app()` action. The app is run with `_app.RunAsync()`.

- __Server Status Messages:__
  - Informational messages are displayed on the console to notify when the server is up and ready for service with `Console.WriteLine("Server Up")`.

- __Exception Handling:__
    - It provides robust exception handling. When any exception occurs, it checks if it's `StopTheHostException`. If it's not, the exception message is logged as fatal error with `Log.Fatal(ex, "Unhandled exception")`.

- __Server Shutdown:__
    - Information about server shutting down is logged with `Log.Information("Server Shutting down...")` and the logging service is terminated with `Log.CloseAndFlush()`.

This `StartUp` class provides a clean and organized control flow to start and run your server with useful logging and configuration features.

## Example

```csharp
using Demo;
using WorkerServiceSetup;

StartUp.Run(
    (config) => { },
    (builder,config) =>
    {
        builder.ConfigureServices(services => services.AddHostedService<Worker>());
    },
    (app) => { });
```

## Contributing

Contributions to this project are welcome. If you have any suggestions, bug fixes, or additional features to contribute,
please submit a pull request.