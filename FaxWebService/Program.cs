using FaxWebService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: FrontendSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:8081");
                      });
});

Worker.Initiliaze();

var app = builder.Build();

app.MapControllers();
app.UseRouting();
app.Run();