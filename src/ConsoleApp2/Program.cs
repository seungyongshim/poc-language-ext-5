using Effects;
using LanguageExt;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using static LanguageExt.Prelude;
using static Effects.Logger<IRuntime>;
using LanguageExt.Common;

var builder = Host.CreateApplicationBuilder();
builder.Logging.AddConsole();

var app = builder.Build();

await app.StartAsync().ConfigureAwait(false);

var q = from _1 in InfoEff("Hello?")
        from _2 in liftEff(() => throw new Exception("what?!"))
        from _3 in FailEff<Unit>(Error.New("how?!"))
        select unit;

await using var scope = app.Services.CreateAsyncScope();

_ = q.IfLogErrorEff().Run(new Runtime(scope.ServiceProvider), EnvIO.New());

await app.StopAsync().ConfigureAwait(false);

file interface IRuntime : IHas<IRuntime, ILogger> 
{
    
}

file record Runtime
(
    IServiceProvider ServiceProvider
) : IRuntime
{
    ILogger IHas<ILogger>.It => ServiceProvider.GetRequiredService<ILogger<Runtime>>();
    IServiceProvider IHas.ServiceProvider => ServiceProvider;
}
