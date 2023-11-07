using FaxWebService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

Worker.Initiliaze();

var app = builder.Build();

app.MapControllers();
app.UseRouting();
app.Run();