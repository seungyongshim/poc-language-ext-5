using Effects;
using LanguageExt;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using static LanguageExt.Prelude;
using LanguageExt.Common;

var builder = Host.CreateApplicationBuilder();
builder.Logging.AddConsole();

var app = builder.Build();

await app.StartAsync().ConfigureAwait(false);

var q = from logger in ServiceProviderEffect.GetLoggerEff("main")
        let q1 = from _1 in unitEff
                 from _2 in logger.InfoEff("here")
                 from _3 in logger.InfoEff("we")
                 from _4 in logger.InfoEff("are")
                 from _5 in liftEff(() => throw new Exception("what?!"))
                 from _6 in FailEff<Unit>(Error.New("how?!"))
                 select unit
        from _1 in logger.IfErrorEff(q1)
        select unit;

await using var scope = app.Services.CreateAsyncScope();

_ = q.Run(scope.ServiceProvider, EnvIO.New());

await app.StopAsync().ConfigureAwait(false);
