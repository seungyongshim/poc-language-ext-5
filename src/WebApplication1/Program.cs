using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/", (JsonElement body) =>
{
    Console.WriteLine(body.GetRawText());
});

app.Run();
