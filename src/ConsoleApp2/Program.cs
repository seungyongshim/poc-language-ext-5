using Effects;
using LanguageExt;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using static LanguageExt.Prelude;
using LanguageExt.Common;

var builder = Host.CreateApplicationBuilder();
builder.Logging.AddConsole();
builder.Services.AddHttpClient();

var app = builder.Build();

await app.StartAsync().ConfigureAwait(false);

var q = from logger in ServiceProviderEffect.GetLoggerEff("main")
        from http in ServiceProviderEffect.GetHttpClientEff("main")
        let f1 = from _1 in logger.InfoEff("here")
                 from _2 in logger.InfoEff("we")
                 from _3 in logger.InfoEff("are")
                 from _4 in liftEff(() => throw new Exception("what?!"))
                 from _5 in FailEff<Unit>(Error.New("how?!"))
                 select unit
        from _1 in logger.IfErrorEff(f1)
        from res in http.GetEff("")
        select unit;

await using var scope = app.Services.CreateAsyncScope();
_ = q.Run(scope.ServiceProvider, EnvIO.New()).ThrowIfFail();

await app.StopAsync().ConfigureAwait(false);
