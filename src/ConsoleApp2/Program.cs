using System.Net.Http;
using ConsoleApp2;
using LanguageExt;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static LanguageExt.Prelude;


var builder = Host.CreateApplicationBuilder();
builder.Services.AddHasSlack();
var app = builder.Build();

var q = from _1 in liftEff<Runtime, string?>(async rt => await Console.In.ReadLineAsync().ToValue())
        from _2 in liftEff(async () => await Console.Out.WriteLineAsync(_1).ToUnit())
        select _1;

var services = new ServiceCollection();


await using var scope = app.Services.CreateAsyncScope();
var r = q.Run(new Runtime(scope.ServiceProvider), EnvIO.New());



file readonly record struct Runtime
(
    IServiceProvider ServiceProvider
) : IHasSlack<Runtime>
{
    IServiceProvider IHas.ServiceProvider => ServiceProvider;
}
