using StraightUpFax;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();

/*
Web server hosten mit endpunkt:
- endpoint to override all faxes
- endpoint to get all faxes

or
 (less traffic)
- endpoint to add a new fax
- endpoint to stop a fax
- endpoint to get all faxes
*/