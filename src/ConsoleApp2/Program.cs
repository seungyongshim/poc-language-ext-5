using LanguageExt;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static LanguageExt.Prelude;
using LanguageExt.Common;

var builder = Host.CreateApplicationBuilder();
builder.Logging.AddConsole();
builder.Services.AddHttpClient();

var builder = Host.CreateApplicationBuilder();
builder.Services.AddHasSlack();
var app = builder.Build();



await using var scope = app.Services.CreateAsyncScope();

