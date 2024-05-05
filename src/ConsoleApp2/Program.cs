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

_ = q.IfLogErrorEff().Run(new Runtime(app.Services.GetRequiredService<ILogger<Program>>()), EnvIO.New());

await app.StopAsync().ConfigureAwait(false);

file interface IRuntime : IHas<IRuntime, ILogger> 
{
    
}

file record Runtime
(
    ILogger Logger
) : IRuntime
{
    ILogger IHas<IRuntime, ILogger>.It => Logger;
}
