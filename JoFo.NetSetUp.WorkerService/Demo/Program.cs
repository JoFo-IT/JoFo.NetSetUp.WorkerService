using Demo;
using WorkerServiceSetup;

StartUp.Run(
    (config) => { },
    (builder,config) =>
    {
        builder.ConfigureServices(services => services.AddHostedService<Worker>());
    },
    (app) => { });