using Effects;
using LanguageExt;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using static LanguageExt.Prelude;

var builder = Host.CreateApplicationBuilder();
builder.Logging.AddConsole();

var app = builder.Build();

await app.StartAsync().ConfigureAwait(false);

var q = from _1 in Effects.Logger<Runtime>.Info("Hello?")
        from _2 in liftEff(() => throw new Exception("what?!"))
        select unit;

_ = q.IfLogError().Run(new Runtime(app.Services.GetRequiredService<ILogger<Program>>()), EnvIO.New());

await app.StopAsync().ConfigureAwait(false);

file readonly record struct Runtime
(
    ILogger Logger
) : IHas<Runtime, ILogger>
{
    ILogger IHas<Runtime, ILogger>.It => Logger;
}
