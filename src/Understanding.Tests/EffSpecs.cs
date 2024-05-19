namespace Understanding.Tests;

public class EffSpecs
{
    [Fact]
    public void UseOnce()
    {
        var q = from __ in unitEff
                from _2 in local<IO, DisposableClass>(use(() => new DisposableClass())).As()
                select _2;

        var r = q.Run().ThrowIfFail();

        Assert.True(r.IsDisposed);
    }

    [Fact]
    public void RepeatUse()
    {
        var q = from __ in unitEff
                from _2 in repeat(Schedule.recurs(1), use(() => new DisposableClass()))
                select _2;

        var r = q.Run().ThrowIfFail();

        Assert.True(r.IsDisposed);
    }
}

public class DisposableClass : IDisposable
{
    public bool IsDisposed { get; set; } = false;

    public void Dispose() => IsDisposed = IsDisposed switch
    {
        false => true,
        true => throw new ObjectDisposedException(nameof(DisposableClass)),
    };
}




