using Effects;
using LanguageExt.Traits;
using Microsoft.Extensions.Logging;

namespace Understanding.Tests;

public class EffSpecs
{
    [Fact]
    public void ReleaseWithExceptionAndRuntime()
    {
        var q = from _1 in IHas<Runtime, DisposableClass>.Eff
                from _2 in Resource.use<Eff, DisposableClass>(() => _1)
                from _3 in Resource.use<Eff, DisposableClass>(() => _1)
                select unit;

        var r = q.Run(new Runtime
        (
            new DisposableClass(),
            LoggerFactory.Create(a => { }).CreateLogger("")
        ), EnvIO.New()).ThrowIfFail();
    }
}

public class DisposableClass : IDisposable
{
    bool disposed = false;

    public void Dispose() => disposed = disposed switch
    {
        false => true,
        true => throw new ObjectDisposedException(nameof(DisposableClass)),
    };
}

public readonly record struct Runtime
(
    DisposableClass HttpClient,
    ILogger Logger
) : IHas<Runtime, ILogger>,
    IHas<Runtime, DisposableClass>
{
    ILogger IHas<ILogger>.It => Logger;
    DisposableClass IHas<DisposableClass>.It => HttpClient;
}


