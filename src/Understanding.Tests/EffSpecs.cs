using System.Net.Http;
using Effects;
using LanguageExt;
using LanguageExt.Traits;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Understanding.Tests;

public class EffSpecs
{
    [Fact]
    public void Catch1()
    {
        var q = from _1 in liftEff(() => throw new Exception("here?"))
                | @catch(ex => true, ex => unit)
                select unit;

        var r = q.Run().ThrowIfFail();

        Assert.Equal(r, unit);
    }

    [Fact]
    public void Catch2()
    {
        var q = from _1 in liftEff(() => throw new Exception("here?"))
                | @catchM(ex => ex.Is<Exception>(), ex => liftEff(() => unit))
                select unit;

        var r = q.Run().ThrowIfFail();

        Assert.Equal(r, unit);
    }

    [Fact]
    public void ReleaseWithException()
    {
        var q = from c in DisposableClassM.client
                from _1 in liftEff(() => throw new Exception("here?"))
                from _ in DisposableClassM.release(c)
                select c;

        var r = q.Run();
    }

    [Fact]
    public void ReleaseWithExceptionAndRuntime()
    {
        var q = from _1 in DisposableClass<Runtime1>.Create()
                from _2 in DisposableClass<Runtime1>.Create()
                select unit;

        var r = q.Run(new Runtime1
        (
            new DisposableClass()
        ), EnvIO.New()).ThrowIfFail();
    }
}

public static class DisposableClassM
{
    public static Eff<DisposableClass > client =>
        Resource.use<Eff, DisposableClass>(() => new DisposableClass()).As();

    public static Eff<Unit> release(DisposableClass  client) => Resource.release<Eff, DisposableClass >(client).As();
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

public static class DisposableClass<RT> where RT: IHas<RT, DisposableClass>
{
    public static Eff<RT, DisposableClass> Create() =>
        from _1 in IHas<RT, DisposableClass>.Eff
        from _2 in Resource.use<Eff, DisposableClass>(() => _1).As()
        select _2;

    public static Eff<Unit> release(DisposableClass client) => Resource.release<Eff, DisposableClass>(client).As();
}

public readonly record struct Runtime1
(
    DisposableClass HttpClient
) : IHas<Runtime1, DisposableClass>
{
    DisposableClass IHas<Runtime1, DisposableClass>.It => HttpClient;
}


